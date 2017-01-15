﻿using System;
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
            this.Size = new Size(1100, 700);
        }

        #region [Properties]

        private const int W = 800, H = 650;


        static bool _continue = true;
        static bool _drawState = true;
        static SerialPort _serialPort;
        const byte NumOfTypes = 14; 
        static int[] _prev = new int[NumOfTypes];
        static int[] _curr = new int[NumOfTypes];
        static int[] _scale = new int[NumOfTypes];
        static int _joysticSens = 600;

        static int _dT = 0;

        static Pen _pRed = new Pen(Color.Red, 1);
        static Pen _pGreen = new Pen(Color.Green, 1);
        static Pen _pBlue = new Pen(Color.Blue, 1);
        static Pen _pBlack = new Pen(Color.Black, 1);
        static Pen _pGray = new Pen(Color.Gray, 1);

        static bool[] _isDrawing = new bool[15];
        static Graphics _g;
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
        public static string conv_to_mess(int value, char letter)
        {
            string mes = Convert.ToString(value);
            while (mes.Length < 5)
                mes = ' ' + mes;
            return letter + mes;
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
            _serialPort.WriteLine("c00000\n");
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
                _serialPort.WriteLine(conv_to_mess(_force, 'J'));
                thrustValueTextbox.Text = Convert.ToString(_force/10) + '%';
            }   
        }

        private void applyMaskButton_Click(object sender, EventArgs e)
        {
            int printMask = 0;
            for(var k=0;k<15;k++)      
                if(_isDrawing[k])         
                    printMask += Convert.ToInt32(Math.Pow(2, k));

            string mes = Convert.ToString(printMask);
            while (mes.Length < 5)
            {
                mes = ' ' + mes;
            }
            _serialPort.WriteLine(conv_to_mess(printMask, 'L'));
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
                connectToComPortButton.BackColor = Color.LightGreen;
                _serialPort.WriteLine("o");
            }
        }

        private void comPortNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void loopTimeRefreshButton_Click(object sender, EventArgs e)
        {
            textBox10.Text = "d_t = " + Convert.ToString(_dT);
        }

        private void flRotorSwitcher_Click(object sender, EventArgs e)
        {
            _motorMask[3] = !_motorMask[3];
            byte tmp = 0;
            for(var i=0; i<4;i++)
            {
                if (_motorMask[i])
                {
                    tmp += Convert.ToByte(Math.Pow(2, i));
                }
            }
            _serialPort.WriteLine(conv_to_mess(tmp, 'K'));

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
                   tmp += Convert.ToByte(Math.Pow(2, i));

            _serialPort.WriteLine(conv_to_mess(tmp, 'K'));
         
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
            {
                if (_motorMask[i])
                {
                    tmp += Convert.ToByte(Math.Pow(2, i));
                }
            }
            _serialPort.WriteLine(conv_to_mess(tmp, 'K'));

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
            {
                if (_motorMask[i])
                {
                    tmp += Convert.ToByte(Math.Pow(2, i));
                }
            }
            _serialPort.WriteLine(conv_to_mess(tmp, 'K'));
            if (_motorMask[1])
                brRotorSwitcherButton.BackColor = Color.Lime;
            else
                brRotorSwitcherButton.BackColor = Color.Red;
        }

        private void thrustValueTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void prop_gain_ValueChanged(object sender, EventArgs e)
        {
            _serialPort.WriteLine(conv_to_mess(Convert.ToUInt16(prop_gain.Value), 'D'));
        }

        private void integr_gain_ValueChanged(object sender, EventArgs e)
        {
            _serialPort.WriteLine(conv_to_mess(Convert.ToUInt16(integr_gain.Value), 'E'));
        }

        private void diff_gain_ValueChanged(object sender, EventArgs e)
        {
            _serialPort.WriteLine(conv_to_mess(Convert.ToUInt16(diff_gain.Value), 'F'));
        }

        private void prop_limit_ValueChanged(object sender, EventArgs e)
        {
            _serialPort.WriteLine(conv_to_mess(Convert.ToUInt16(prop_limit.Value), 'G'));
        }

        private void inegr_limit_ValueChanged(object sender, EventArgs e)
        {
            _serialPort.WriteLine(conv_to_mess(Convert.ToUInt16(inegr_limit.Value), 'H'));
        }

        private void diff_limit_ValueChanged(object sender, EventArgs e)
        {
            _serialPort.WriteLine(conv_to_mess(Convert.ToUInt16(diff_limit.Value), 'I'));
        }



        private void scale_of_line_ValueChanged(object sender, EventArgs e)
        {
            _scale[Convert.ToByte(scaling_line.Value)] = Convert.ToInt32(scale_of_line.Value);
        }

        private void scaling_line_ValueChanged(object sender, EventArgs e)
        {
            scale_of_line.Value = _scale[Convert.ToByte(scaling_line.Value)];
        }

        private void joysticSense_ValueChanged(object sender, EventArgs e)
        {
            _joysticSens = Convert.ToUInt16(joysticSense.Value)*60;
        }

        private void offThrustButton_Click(object sender, EventArgs e)
        {
            _force = 0;
            thrustValueTextbox.Text = Convert.ToString(_force / 10) + '%';
            _serialPort.WriteLine("J00000\n");
        }

        private void downThrustButton_Click(object sender, EventArgs e)
        {
            if (_force > 29)
            {
                _force -= 30;
                _serialPort.WriteLine(conv_to_mess(_force, 'J'));
                thrustValueTextbox.Text = Convert.ToString(_force / 10) + '%';
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            for (var i = 0; i < _scale.Length; i++)
            {
                _scale[i] = 100;
            }
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
                            if (message[0] >= 'A' && message[0] <= 'Z')
                            {
                                var channal = message[0] - 'A';
                                if (channal <= NumOfTypes)
                                {
                                    message = message.Remove(0, 1);
                                    var tmp = Convert.ToInt32(message);
                                    _curr[channal] = Map(tmp, Int16.MinValue, Int16.MaxValue,
                                        (H / 2) - (H * _scale[channal] / 200), (H / 2) + (H * _scale[channal] / 200));
                                }
                                else if (channal == ('W' - 'A'))
                                {
                                    if (_drawState)
                                    {
                                        for (var k = 0; k < 12; k += 3)
                                        {
                                            if (_isDrawing[0 + k])
                                                _g.DrawLine(_pRed, carriage, _prev[0 + k], carriage + 1, _curr[0 + k]);
                                            if (_isDrawing[1 + k])
                                                _g.DrawLine(_pGreen, carriage, _prev[1 + k], carriage + 1, _curr[1 + k]);
                                            if (_isDrawing[2 + k])
                                                _g.DrawLine(_pBlue, carriage, _prev[2 + k], carriage + 1, _curr[2 + k]);
                                        }
                                        if (_isDrawing[13])
                                            _g.DrawLine(_pBlack, carriage, _prev[13], carriage + 1, _curr[13]);
                                        carriage = (carriage + 1) % W;
                                        for (var q = 0; q < 14; q++)
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
                                _serialPort.WriteLine(conv_to_mess(80, 'M')); //send every 100ms messange "M50\n"
                            }
                        }
                    }
                    joystick.Poll();
                    var data = joystick.GetBufferedData();

                    foreach (var state in data)
                    {

                        switch (state.Offset)
                        {
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
                                _serialPort.WriteLine(conv_to_mess(_force, 'J'));
                                break;
                        }
                    }
                    if (Math.Abs(_yawCurr - _yawPrev) > 30)
                    {
                        _serialPort.WriteLine(conv_to_mess(_yawCurr, 'C'));
                        _yawPrev = _yawCurr;
                    }
                    if (Math.Abs(_pitchPrev - _pitchCurr) > 30)
                    {
                        _serialPort.WriteLine(conv_to_mess(_pitchCurr - _pitchOffset, 'A'));
                        _pitchPrev = _pitchCurr;
                    }
                    if (Math.Abs(_rollPrev - _rollCurr) > 30)
                    {
                        _serialPort.WriteLine(conv_to_mess(_rollCurr + _rollOffset, 'B'));
                        _rollPrev = _rollCurr;
                    }
                    
                }
            }
        }
    }
}
