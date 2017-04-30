using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using SharpDX.DirectInput;

namespace val_plot
{
    public partial class Form1 : Form
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());

        }

        public Form1()
        {
            InitializeComponent();
            CanvasPanel.Invalidate();
            this.Size = new Size(1030, 710);
        }

        #region [Properties]

        private const int W = 800, H = 650;

        enum TX : byte
        {
            PITCH = (byte)'A', ROLL, YAW, THRUST, PID_CONFIG, MOTOR_MASK,
            LOSING_CONNECTION, FLIGHT_MODE, COMMAND, TELEMETRY_MASK
        };
        enum RX : byte
        {
            PITCH = 0, ROLL, YAW,
            PROP_PITCH, INTEGR_PITCH, DIFF_PITCH,
            PROP_ROLL, INTEGR_ROLL, DIFF_ROLL,
            PROP_YAW, INTEGR_YAW, DIFF_YAW,
            BARO_ALITUDE, SONAR_DISTANSE,
            ALTITUDE, ALTITUDE_VELOCITY,
            COMPUTING_TIME, BATTERY_VOLTAGE, SYSTEM_FLAGS,
            GPS_STATE
        }

        public int selectedPID = 0;
        public int[,] PID_array = new int[6, 6];
        static bool _continue = true;
        static bool _drawState = true;
        static SerialPort _serialPort;
        const byte NumOfTypes = 24;
        static int[] _prev = new int[NumOfTypes];
        static int[] _curr = new int[NumOfTypes];
        static int[] _scale = new int[NumOfTypes];
        static bool[] _dataForPlot = new bool[NumOfTypes];
        static int _joysticSens = 600;


        private static Pen[] _penArray = new Pen[]
            {
                new Pen(Color.Red, 1),
                new Pen(Color.Green, 1),
                new Pen(Color.Blue, 1),
                new Pen(Color.Yellow, 1),
                new Pen(Color.Black, 1),
                new Pen(Color.Gray, 1)
            };

        enum penColors : byte
        {
            none, red, green, blue, yellow, black, gray
        }


        static Pen _pRed = new Pen(Color.Red, 1);
        static Pen _pGreen = new Pen(Color.Green, 1);
        static Pen _pBlue = new Pen(Color.Blue, 1);
        static Pen _pBlack = new Pen(Color.Black, 1);
        static Pen _pGray = new Pen(Color.Gray, 1);

        static bool[] _isDrawing = new bool[40];
        static Graphics _g;
        //joystic data
        static bool _isJoysticConnect = false;
        static int _pitchPrev = 0, _rollPrev = 0;
        static int _pitchCurr = 0, _rollCurr = 0;
        static int _yawCurr = 0, _yawPrev = 0;
        static int _pitchOffset = 0, _rollOffset = 0;
        static ushort _force = 0;

        static bool[] _motorMask = { true, true, true, true };        
//        static DateTime _nowTime = DateTime.Now;
        static long _lastCheckConnection = 0, _lastDrawerWakeup = 0;

        #endregion [Properties]

        public static void sendMessange(SerialPort _sPort, int value, char type)
        {
            if (_sPort != null && _sPort.IsOpen)
            {
                string tmp = Convert.ToString(value);
                _sPort.WriteLine(type + tmp);
            }
        }

        public static int computePidMessange(int PID_num, int coef_num, decimal value)
        {
            int tmp = PID_num;
            tmp *= 8;
            tmp += coef_num;
            tmp *= 1024;
            tmp += Convert.ToInt32(value);
            return tmp;
        }

        
        public static int Map(int val, int prevMin, int prevMax, int min, int max)
        {
            return (val - prevMin)*(max - min)/(prevMax - prevMin) + min;
        }

         #region [Handlers]


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_serialPort != null)
                if (_serialPort.IsOpen)
                    _serialPort.Close();
            _continue = false;
        }

        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            _g = this.CanvasPanel.CreateGraphics();
            Thread drawerThread = new Thread(Drawer);
            drawerThread.Start();
            Thread joystickThread = new Thread(_Joystic);
            joystickThread.Start();
        }

        private void drawingSwitcherButton_Click(object sender, EventArgs e)
        {
            _drawState = !_drawState;
            if (_drawState)
                drawingSwitcherButton.BackColor = Color.LightGreen;
            else
                drawingSwitcherButton.BackColor = Color.Red;     
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemIndex = ((sender as ListBox).SelectedIndex);
            _isDrawing[itemIndex] = !_isDrawing[itemIndex];
        }

        private void forwardOffsetButton_Click(object sender, EventArgs e)
        {
            _pitchOffset+=5;
        }

        private void resetOffsetButton_Click(object sender, EventArgs e)
        {
            _pitchOffset = 0;
            _rollOffset = 0;
        }

        private void leftOffsetButton_Click(object sender, EventArgs e)
        {
            _rollOffset-=5;
        }

        private void rightOffsetButton_Click(object sender, EventArgs e)
        {
            _rollOffset+=5;
        }

        private void backOffsetButton_Click(object sender, EventArgs e)
        {
            _pitchOffset-=5;
        }

        private void upThrustButton_Click(object sender, EventArgs e)
        {
            if (_force < 970)
            {
                _force += 30;
                sendMessange(_serialPort, _force, (char)TX.THRUST);
                thrustValueTextbox.Text = Convert.ToString(_force/10) + '%';
            }   
        }

        private void applyMaskButton_Click(object sender, EventArgs e)
        {
            ulong printMask = 0;
            byte length = (byte)_isDrawing.Length;
            if (length > 64)
                length = 64;
            for (byte i = 0; i < length; i++)
            {
                if (_isDrawing[i])
                {
                    printMask |= ((ulong)1 << i);
                }
            }

            for (byte i = 0; i < 4; i++)
            {
                var tmp = (short) ((printMask >> (i * 16)) & 0xFFFF);
                byte channel = (byte)TX.TELEMETRY_MASK;
                channel += i;
                sendMessange(_serialPort, tmp, (char)channel);
            }
        }

        private void connectToComPortButton_Click(object sender, EventArgs e)
        {
            string serialName = comPortNameBox.Text;
            _serialPort = new SerialPort
            {
                PortName = serialName,
                BaudRate = 115200,
                ReadTimeout = 500,
                WriteTimeout = 500
            };            
            _serialPort.Open();
            if (_serialPort.IsOpen)
            {
                timer1.Enabled = true;
                connectToComPortButton.BackColor = Color.LightGreen;
            }
        }

        private void comPortNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void flRotorSwitcher_Click(object sender, EventArgs e)
        {
            _motorMask[3] = !_motorMask[3];
            byte tmp = 0;
            for(var i=0; i<4;i++)
                if (_motorMask[i])
                    tmp |= (byte)(1 << i);
            sendMessange(_serialPort, tmp, (char)TX.MOTOR_MASK);
            if (_motorMask[3])
                flRotorSwitcherButton.BackColor = Color.Lime;
            else
                flRotorSwitcherButton.BackColor = Color.Red;
        }

        private void blRotorSwitcher_Click(object sender, EventArgs e)
        {
            _motorMask[2] = !_motorMask[2];
            byte tmp = 0;
            for (var i = 0; i < 4; i++)
                if (_motorMask[i])
                    tmp |= (byte)(1 << i);
            sendMessange(_serialPort, tmp, (char)TX.MOTOR_MASK);
            if (_motorMask[2])
                blRotorSwitcherButton.BackColor = Color.Lime;
            else
                blRotorSwitcherButton.BackColor = Color.Red;
        }

        private void frRotorSwitcher_Click(object sender, EventArgs e)
        {
            _motorMask[0] = !_motorMask[0];
            byte tmp = 0;
            for (var i = 0; i < 4; i++)
                if (_motorMask[i])
                    tmp |= (byte)(1 << i);
            sendMessange(_serialPort, tmp, (char)TX.MOTOR_MASK);
            if (_motorMask[0])
                frRotorSwitcherButton.BackColor = Color.Lime;
            else
                frRotorSwitcherButton.BackColor = Color.Red;
        }

        private void brRotorSwitcher_Click(object sender, EventArgs e)
        {
            _motorMask[1] = !_motorMask[1];
            byte tmp = 0;
            for (var i = 0; i < 4; i++)
                if (_motorMask[i])
                    tmp |= (byte)(1 << i);
            sendMessange(_serialPort, tmp, (char)TX.MOTOR_MASK);
            if (_motorMask[1])
                brRotorSwitcherButton.BackColor = Color.Lime;
            else
                brRotorSwitcherButton.BackColor = Color.Red;
        }

        private void thrustValueTextbox_TextChanged(object sender, EventArgs e)
        {

        }
        //--------------------
        private void prop_gain_ValueChanged(object sender, EventArgs e)
        {
            var PID_num = PID_list.SelectedIndex;
            if (prop_gain.Value == PID_array[PID_num, 0])
            {
                return;
            }
            PID_array[PID_num, 0] = Convert.ToInt32(prop_gain.Value);
            sendMessange(_serialPort, computePidMessange(PID_list.SelectedIndex,0, prop_gain.Value), (char)TX.PID_CONFIG);
        }

        private void integr_gain_ValueChanged(object sender, EventArgs e)
        {
            var PID_num = PID_list.SelectedIndex;
            if (integr_gain.Value == PID_array[PID_num, 1])
            {
                return;
            }
            PID_array[PID_num, 1] = Convert.ToInt32(integr_gain.Value);
            sendMessange(_serialPort, computePidMessange(PID_list.SelectedIndex, 1, integr_gain.Value), (char)TX.PID_CONFIG);
        }

        private void diff_gain_ValueChanged(object sender, EventArgs e)
        {
            var PID_num = PID_list.SelectedIndex;
            if (diff_gain.Value == PID_array[PID_num, 2])
            {
                return;
            }
            PID_array[PID_num, 2] = Convert.ToInt32(diff_gain.Value);
            sendMessange(_serialPort, computePidMessange(PID_list.SelectedIndex, 2, diff_gain.Value), (char)TX.PID_CONFIG);
        }

        private void prop_limit_ValueChanged(object sender, EventArgs e)
        {
            var PID_num = PID_list.SelectedIndex;
            if (prop_limit.Value == PID_array[PID_num, 3])
            {
                return;
            }
            PID_array[PID_num, 3] = Convert.ToInt32(prop_limit.Value);
            sendMessange(_serialPort, computePidMessange(PID_list.SelectedIndex, 3, prop_limit.Value), (char)TX.PID_CONFIG);
        }

        private void inegr_limit_ValueChanged(object sender, EventArgs e)
        {
            var PID_num = PID_list.SelectedIndex;
            if (inegr_limit.Value == PID_array[PID_num, 4])
            {
                return;
            }
            PID_array[PID_num, 4] = Convert.ToInt32(inegr_limit.Value);
            sendMessange(_serialPort, computePidMessange(PID_list.SelectedIndex, 4, inegr_limit.Value), (char)TX.PID_CONFIG);
        }

        private void diff_limit_ValueChanged(object sender, EventArgs e)
        {
            var PID_num = PID_list.SelectedIndex;
            if (diff_limit.Value == PID_array[PID_num, 5])
            {
                return;
            }
            PID_array[PID_num, 5] = Convert.ToInt32(diff_limit.Value);
            sendMessange(_serialPort, computePidMessange(PID_list.SelectedIndex, 5, diff_limit.Value), (char)TX.PID_CONFIG);
        }
        //-------------------
        private void scale_of_line_ValueChanged(object sender, EventArgs e)
        {
            _scale[Convert.ToByte(scaling_line.Value)] = Convert.ToInt32(scale_of_line.Value);
        }

        private void scaling_line_ValueChanged(object sender, EventArgs e)
        {
            scale_of_line.Value = _scale[Convert.ToByte(scaling_line.Value)];
        }

        private void PID_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            var element = sender as ComboBox;
            if (element != null)
            {
                selectedPID = element.SelectedIndex;
                prop_gain.Value = PID_array[selectedPID, 0];
                integr_gain.Value = PID_array[selectedPID, 1];
                diff_gain.Value = PID_array[selectedPID, 2];
                prop_limit.Value = PID_array[selectedPID, 3];
                inegr_limit.Value = PID_array[selectedPID, 4];
                diff_limit.Value = PID_array[selectedPID, 5];
            }
        }

        private void flightModeButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            var element = sender as ComboBox;
            if (element != null)
            {
                var flightMode = element.SelectedIndex;
                sendMessange(_serialPort, flightMode,(char)TX.FLIGHT_MODE);
            }
        }

        private void gpsAccuracy_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            calculationTime.Text = Convert.ToString(_curr[(byte)RX.COMPUTING_TIME]) + " ms";
            battetyVoltage.Text = Convert.ToString(_curr[(byte)RX.BATTERY_VOLTAGE]) + " mV";
        }

        private void joysticSense_ValueChanged(object sender, EventArgs e)
        {
            _joysticSens = Convert.ToUInt16(joysticSense.Value)*60;
        }

        private void offThrustButton_Click(object sender, EventArgs e)
        {
            _force = 0;
            thrustValueTextbox.Text = Convert.ToString(_force / 10) + '%';
            sendMessange(_serialPort, _force, (char)TX.THRUST);
        }

        private void downThrustButton_Click(object sender, EventArgs e)
        {
            if (_force > 29)
            {
                _force -= 30;
                sendMessange(_serialPort, _force, (char)TX.THRUST);
                thrustValueTextbox.Text = Convert.ToString(_force / 10) + '%';
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {            
            //initialization settings
            for (var i = 0; i < 6; i++)
            {
                PID_array[i, 0] = 160;
                PID_array[i, 1] = 5;
                PID_array[i, 2] = 50;
                PID_array[i, 3] = 100;
                PID_array[i, 4] = 40;
                PID_array[i, 5] = 100;
            }
            for (var i = 0; i < _scale.Length; i++)
            {
                _dataForPlot[i] = true;
                _scale[i] = 1000;
            }
            _dataForPlot[(byte)RX.BATTERY_VOLTAGE] = false;
            _dataForPlot[(byte)RX.COMPUTING_TIME] = false;
        }

        #endregion [Handlers]

        public static void Drawer()
        {
        
            var carriage = 0;
            _g.DrawLine(_pGray, carriage, H / 2, W, H / 2);
            while (_continue)
            {
                try
                {
                    if (_serialPort.IsOpen)
                    {
                        if (_serialPort.BytesToRead > 6)
                        {
                            var message = _serialPort.ReadLine();
                            if (message[0] >= 'A' && message[0] <= 'z')
                            {
                                var channal = message[0] - 'A';
                                if (channal < NumOfTypes)
                                {
                                    message = message.Remove(0, 1);
                                    var tmp = Convert.ToInt32(message);
                                    if (_dataForPlot[channal] == true)
                                    {
                                        _curr[channal] = Map(tmp, Int16.MinValue, Int16.MaxValue,
                                        (H / 2) - (H * _scale[channal] / 200), (H / 2) + (H * _scale[channal] / 200));
                                    }
                                    else
                                    {
                                        _curr[channal] = tmp;
                                    }
                                    
                                }
                                else if (channal == ('z' - 'A'))
                                {
                                    if (_drawState)
                                    {
                                        for (var k = 0; k < _curr.Length; k += 3)
                                        {
                                            if (_isDrawing[0 + k] && _dataForPlot[0+k] == true)
                                                _g.DrawLine(_pRed, carriage, _prev[0 + k], carriage + 1, _curr[0 + k]);
                                            if (_isDrawing[1 + k] && _dataForPlot[1 + k] == true)
                                                _g.DrawLine(_pGreen, carriage, _prev[1 + k], carriage + 1, _curr[1 + k]);
                                            if (_isDrawing[2 + k] && _dataForPlot[2 + k] == true)
                                                _g.DrawLine(_pBlue, carriage, _prev[2 + k], carriage + 1, _curr[2 + k]);
                                        }
                                        carriage = (carriage + 1) % W;
                                        for (var q = 0; q < _curr.Length; q++)
                                            _prev[q] = _curr[q];
                                    }
                                }
                            }
                        }
                        else
                        {
                            var nowTime = DateTime.Now;
                            long currentTick = nowTime.Ticks;
                            var wakeupTime = currentTick - _lastDrawerWakeup;
                            if (wakeupTime < 15 * 10000)
                            {
                                _lastDrawerWakeup += 15;
                                var tmp = Convert.ToInt32(15 - wakeupTime);
                                Thread.Sleep(tmp);
                            }
                            else
                            {
                                _lastDrawerWakeup = currentTick;
                            }
                        }
                    }
                    else
                        Thread.Sleep(1000);
                }
                catch (Exception) { }
                if (carriage == 1)
                {
                    try
                    {
                        _g.Clear(Color.White);
                        _g.DrawLine(_pGray, 0, (H / 2) + 1, W, (H / 2) + 1); // середина экрана
                        _g.DrawLine(_pGray, 0, (H / 2) + 1 - 160, W, (H / 2) + 1 - 160); // середина экрана
                        _g.DrawLine(_pGray, carriage, (H / 2) + 1 + 160, W, (H / 2) + 1 + 160); // середина экрана

                        carriage = 1;
                    }
                    catch (Exception) { }
                }
            }
        }
        public static void _Joystic()
        {
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;


            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                    DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                _isJoysticConnect = true;

            }
            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                _isJoysticConnect = true;
            }
            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                _isJoysticConnect = false;
            }

            if (_isJoysticConnect)
            {
                var joystick = new Joystick(directInput, joystickGuid);

                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

                // Query all suported ForceFeedback effects
                var allEffects = joystick.GetEffects();
                foreach (var effectInfo in allEffects)
                    Console.WriteLine("Effect available {0}", effectInfo.Name);

                // Set BufferSize in order to use buffered data.
                joystick.Properties.BufferSize = 128;

                // Acquire the joystick
                joystick.Acquire();

                // Poll events from joystick
                var joystickState = new JoystickState();

                while (_continue )
                {
                    Thread.Sleep(50);
                    
                    if (_serialPort != null)
                    {
                        if (_serialPort.IsOpen) {
                            var nowTime = DateTime.Now;
                            long currentTick = nowTime.Ticks;
                            if (currentTick - _lastCheckConnection > 200 * 10000) //100ms
                            {
                                _lastCheckConnection = currentTick;
                                sendMessange(_serialPort, 80, (char)TX.LOSING_CONNECTION);  //send every 100ms messange "M50\n"
                            }
                        }
                    }
                    joystick.Poll();
                    var data = joystick.GetBufferedData();

                    foreach (var state in data)
                    {

                        switch (state.Offset)
                        {
                            /*
                            case JoystickOffset.Buttons0:                                
                                break;
                            case JoystickOffset.Buttons1:
                                break;
                            case JoystickOffset.Buttons2:
                                break;
                            case JoystickOffset.Buttons3:
                                break;
                            case JoystickOffset.Buttons4:
                                break;
                            case JoystickOffset.Buttons5:
                                break;
                            case JoystickOffset.Buttons6:
                                break;
                            case JoystickOffset.Buttons7:
                                break;
                            case JoystickOffset.Buttons8:
                                break;
                            case JoystickOffset.Buttons9:
                                break;
                                */
                            case JoystickOffset.Buttons6:
                                if (state.Value != 0)
                                {
                                    _drawState = !_drawState;
                                }
                                break;
                            case JoystickOffset.X:
                                _yawCurr = Map(state.Value, 0, 65536, _joysticSens * 4, -_joysticSens * 4);
                                break;
                            case JoystickOffset.Z:
                                _pitchCurr = Map(state.Value, 0, 65536, -_joysticSens, _joysticSens);
                                break;
                            case JoystickOffset.RotationZ:
                                _rollCurr = Map(state.Value, 0, 65536, -_joysticSens, _joysticSens);
                                break;
                            case JoystickOffset.PointOfViewControllers0:
                                switch (state.Value)
                                {
                                    case 0:
                                        if (_force < 970)
                                            _force += 15;
                                        break;
                                    case 9000:
                                        _force = 0;
                                        break;
                                    case 18000:
                                        if (_force > 19)
                                            _force -= 15;
                                        break;
                                    case 27000:
                                        //NOP
                                        break;
                                }
                                sendMessange(_serialPort, _force, (char)TX.THRUST);
                                break;
                        }
                    }
                    if (Math.Abs(_yawCurr - _yawPrev) > 30)
                    {
                        sendMessange(_serialPort, _yawCurr, (char)TX.YAW);
                        _yawPrev = _yawCurr;
                    }
                    if (Math.Abs(_pitchPrev - _pitchCurr) > 30)
                    {
                        sendMessange(_serialPort, _pitchCurr - _pitchOffset, (char)TX.PITCH);
                        _pitchPrev = _pitchCurr;
                    }
                    if (Math.Abs(_rollPrev - _rollCurr) > 30)
                    {
                        sendMessange(_serialPort, _rollCurr + _rollOffset, (char)TX.ROLL);
                        _rollPrev = _rollCurr;
                    }
                    
                }
            }
        }
    }
}
