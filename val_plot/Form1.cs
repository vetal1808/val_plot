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
            this.Size = new Size(1000, 650);
        }

        #region [Properties]

        static int w = 800, h = 650, div2 = 65536 / h;
        // w, h - размеры окна div2 - спец. коэф, позволяет разложить весь дипазон 2байтного инта на всю высоту окна


        static bool _continue = true;
        static bool _draw = true;
        static SerialPort _serialPort;
        static int[] prev = new int[13];
        static int[] curr = new int[13];
        static int d_t = 0;
        static Pen pRed = new Pen(Color.Red, 1);
        static Pen pGreen = new Pen(Color.Green, 1);
        static Pen pBlue = new Pen(Color.Blue, 1);
        static Pen pBlack = new Pen(Color.Black, 1);
        static Pen pGray = new Pen(Color.Gray, 1);
        static int j = 0;
        static bool[] _isDrawing = new bool[12];
        static Graphics g;
        static bool _joysticConnect = false, _is_joysticConnect = false;
        static int pitch_prev = 50, roll_prev = 50;
        static int pitch_curr = 50, roll_curr = 50;
        static int pitch0 = 0, roll0 = 0;
        static ushort force = 0, prop = 16, diff = 8, integr = 3, limP = 40, limI = 16, limD = 40;
        static uint mask=0;

        #endregion [Properties]

        #region [Handlers]

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serialPort.Close();
            _continue = false;
        }

        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            g = this.CanvasPanel.CreateGraphics();
            Thread drawerThread = new Thread(Drawer);
            drawerThread.Start();
            Thread joystickThread = new Thread(_Joystic);
            joystickThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _draw = !_draw;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemIndex = ((sender as ListBox).SelectedIndex);
            _isDrawing[itemIndex] = !_isDrawing[itemIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pitch0++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pitch0 = 0;
            roll0 = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            roll0--;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            roll0++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pitch0--;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (force < 970)
            {
                force += 30;
                string mes = Convert.ToString(force);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('f' + mes);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (limP >1)
            {
                limP--;
                string mes = Convert.ToString(limP);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('x' + mes);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (limP < 99)
            {
                limP++;
                string mes = Convert.ToString(limP);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('x' + mes);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (limD < 99)
            {
                limD++;
                string mes = Convert.ToString(limD);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('z' + mes);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            mask = 0;
            for(var k=0;k<12;k++)
            {
                if(_isDrawing[k])
                {
                    mask += Convert.ToUInt32(Math.Pow(2, k));
                }
            }
            string mes = Convert.ToString(mask);
            while (mes.Length < 5)
            {
                mes = '0' + mes;
            }
            _serialPort.WriteLine('m' + mes);

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (limI < 99)
            {
                limI++;
                string mes = Convert.ToString(limI);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('w' + mes);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (limI < 1)
            {
                limI--;
                string mes = Convert.ToString(limI);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('w' + mes);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (limD >1)
            {
                limD--;
                string mes = Convert.ToString(limD);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('z' + mes);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            force = 0;
            _serialPort.WriteLine("f00\n");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (force > 29)
            {
                force -= 30;
                string mes = Convert.ToString(force);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('f' + mes);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(prop<99)
            {
                prop++;
                string mes = Convert.ToString(prop);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('P' + mes);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (integr < 99)
            {
                integr++;
                string mes = Convert.ToString(integr);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('I' + mes);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (diff >0)
            {
                diff--;
                string mes = Convert.ToString(diff);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('D' + mes);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (prop >0)
            {
                prop--;
                string mes = Convert.ToString(prop);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('P' + mes);
            }
        }


        private void button13_Click(object sender, EventArgs e)
        {
            if (integr >0)
            {
                integr--;
                string mes = Convert.ToString(integr);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('I' + mes);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (diff < 99)
            {
                diff++;
                string mes = Convert.ToString(diff);
                while (mes.Length < 5)
                {
                    mes = '0' + mes;
                }
                _serialPort.WriteLine('D' + mes);
            }
        }



        private void Form1_Load(object sender, System.EventArgs e)
        {
            _serialPort = new SerialPort
            {
                PortName = "COM4",
                BaudRate = 9600,
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            
            _serialPort.Open();

            _continue = true;


            
        }

#endregion [Handlers]

        public static void Drawer()
        {
            string message;
            g.DrawLine(pGray, j, h / 2, w, h / 2);
            while (_continue)
            {
                try
                {
                    message = _serialPort.ReadLine();
                    if(message[0] >='A' && message[0]<='W')
                    {
                        char lol = message[0];
                        message = message.Remove(0, 1);
                        switch (lol)
                        {
                            case 'A':                            
                                curr[0] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'B':
                                curr[1] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'C':
                                curr[2] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'D':
                                curr[3] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'E':
                                curr[4] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'F':
                                curr[5] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'G':
                                curr[6] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'H':
                                curr[7] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'I':
                                curr[8] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'J':
                                curr[9] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'K':
                                curr[10] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'L':
                                curr[11] = (Convert.ToInt32(message) + 32768) / div2;
                                break;
                            case 'M':
                                d_t = (Convert.ToInt32(message));
                                break;
                            case 'W':
                                if (_draw)
                                {
                                    for(var k=0; k<12; k+=3)
                                    {
                                        if (_isDrawing[0+k])
                                            g.DrawLine(pRed, j, prev[0 + k], j + 1, curr[0 + k]);
                                        if (_isDrawing[1 + k])
                                            g.DrawLine(pGreen, j, prev[1 + k], j + 1, curr[1 + k]);
                                        if (_isDrawing[2 + k])
                                            g.DrawLine(pBlue, j, prev[2 + k], j + 1, curr[2 + k]);
                                    }
                                    j = (j + 1) % w;
                                    for (var q = 0; q < 12; q++)
                                        prev[q] = curr[q];
                                }
                                break;
                        }

                    }
                }
                catch (Exception) { }
                if (j < 1)
                {
                    try
                    {
                        g.Clear(Color.White);
                        g.DrawLine(pGray, j, h / 2, w, h / 2); // середина экрана
                    }
                    catch (Exception) { }
                }
            }
        }
        public static void _Joystic()
        {
            /*
            while (_joysticConnect == false)
            {
                Thread.Sleep(1000);

            }
            */
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;


            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                    DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                _is_joysticConnect = true;

            }
            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                _is_joysticConnect = true;
            }
            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                _is_joysticConnect = false;
            }

            if (_is_joysticConnect)
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
                bool work;
                while (_continue )
                {
                    work = false;
                    joystick.Poll();
                    var data = joystick.GetBufferedData();
                    foreach (var state in data)
                    {
                        if (state.Offset == JoystickOffset.Z)
                        {
                            pitch_curr = ((state.Value) / 33) + 500 + pitch0 * 10;
                            if(Math.Abs(pitch_curr - pitch_prev)>100)
                            {
                                string mes = Convert.ToString(pitch_curr);
                                while (mes.Length < 5)
                                {
                                    mes = '0' + mes;
                                }
                                _serialPort.WriteLine('p'+mes);
                                pitch_prev = pitch_curr;
                                
                            }
                            work = true;
                        }
                        if (state.Offset == JoystickOffset.RotationZ)
                        {
                            roll_curr = ((65536 - state.Value) / 33) + 500 + roll0*10;
                            if (Math.Abs(roll_curr - roll_prev) > 100)
                            {
                                string mes = Convert.ToString(roll_curr);
                                while (mes.Length < 5)
                                {
                                    mes = '0' + mes;
                                }
                                _serialPort.WriteLine('r' + mes);
                                roll_prev = roll_curr;
                                
                            }
                            work = true;
                        }
                        if (state.Offset == JoystickOffset.PointOfViewControllers0) // power
                        {
                            work = true;
                            var val = state.Value;
                            switch (val)
                            {
                                case 0:
                                    if(force < 970)
                                    {
                                        force += 30;
                                        string mes = Convert.ToString(force);
                                        while (mes.Length < 5)
                                        {
                                            mes = '0' + mes;
                                        }
                                        _serialPort.WriteLine('f'+mes);
                                    }
                                    break;
                                case 9000:
                                    force = 0;
                                    _serialPort.WriteLine("f00000\n");
                                    break;
                                case 18000:
                                    if (force >29)
                                    {
                                        force -= 30;
                                        string mes = Convert.ToString(force);
                                        while (mes.Length < 5)
                                        {
                                            mes = '0' + mes;
                                        }
                                        _serialPort.WriteLine('f'+mes);
                                    }
                                    break;
                                case 27000:
                                    //NOP
                                    break;
                            }

                        }
                        if(work == false)
                        {
                            Thread.Sleep(200);
                        }

                    }
                }
            }





        }
        
    }
}
