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
            this.Size = new Size(1100, 700);
        }

        #region [Properties]

        static int w = 800, h = 650, div2 = 65536 / h;
        // w, h - размеры окна div2 - спец. коэф, позволяет разложить весь дипазон 2байтного инта на всю высоту окна


        static bool _continue = true;
        static bool _draw = true;
        static SerialPort _serialPort;
        static int[] prev = new int[16];
        static int[] curr = new int[16];
        static int[] pow_prev = new int[4];
        static int[] pow_curr = new int[4];

        static int d_t = 0;
        static Pen pRed = new Pen(Color.Red, 1);
        static Pen pGreen = new Pen(Color.Green, 1);
        static Pen pBlue = new Pen(Color.Blue, 1);
        static Pen pBlack = new Pen(Color.Black, 1);
        static Pen pGray = new Pen(Color.Gray, 1);
        static int j = 0;
        static bool[] _isDrawing = new bool[16];
        static Graphics g;
        static bool _joysticConnect = false, _is_joysticConnect = false;
        static int pitch_prev = 50, roll_prev = 50;
        static int pitch_curr = 50, roll_curr = 50;
        static int yaw_curr = 50, yaw_prev = 50;
        static int pitch0 = 0, roll0 = 0;
        static ushort force = 0, prop = 16, diff = 8, integr = 4, limP = 40, limI = 40, limD = 40;
        static uint print_mask=0;
        static bool[] motor_mask = { true, true, true, true };

        static string _serialName = "COM4";

        #endregion [Properties]

        #region [Handlers]

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
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
            if (_draw)
                button1.BackColor = Color.LightGreen;
            else
                button1.BackColor = Color.Red;     
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemIndex = ((sender as ListBox).SelectedIndex);
            _isDrawing[itemIndex] = !_isDrawing[itemIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pitch0+=20;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pitch0 = 0;
            roll0 = 0;
            _serialPort.WriteLine("c00000\n");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            roll0-=20;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            roll0+=20;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pitch0-=20;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (force < 970)
            {
                force += 30;
                string mes = Convert.ToString(force);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('f' + mes);
                textBox1.Text = Convert.ToString(force/10) + '%';
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
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('x' + mes);
            }
            textBox2.Text = Convert.ToString(limP);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (limP < 300)
            {
                limP++;
                string mes = Convert.ToString(limP);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('x' + mes);
            }
            textBox2.Text = Convert.ToString(limP);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (limD < 300)
            {
                limD++;
                string mes = Convert.ToString(limD);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('z' + mes);
            }
            textBox5.Text = Convert.ToString(limD);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            print_mask = 0;
            for(var k=0;k<16;k++)
            {
                if(_isDrawing[k])
                {
                    print_mask += Convert.ToUInt32(Math.Pow(2, k));
                }
            }
            string mes = Convert.ToString(print_mask);
            while (mes.Length < 5)
            {
                mes = ' ' + mes;
            }
            _serialPort.WriteLine("l00000\n");
            _serialPort.WriteLine('m' + mes);

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (limI < 300)
            {
                limI++;
                string mes = Convert.ToString(limI);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('w' + mes);
            }
            textBox3.Text = Convert.ToString(limI);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (limI > 1)
            {
                limI--;
                string mes = Convert.ToString(limI);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('w' + mes);
            }
            textBox3.Text = Convert.ToString(limI);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            limP = Convert.ToUInt16(textBox2.Text);
            if (Convert.ToUInt16(textBox2.Text) > 1 && Convert.ToUInt16(textBox2.Text) < 100)
            {
                limP = Convert.ToUInt16(textBox2.Text);
                string mes = Convert.ToString(limP);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('x' + mes);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            limI = Convert.ToUInt16(textBox3.Text);
            if (Convert.ToUInt16(textBox3.Text) > 1 && Convert.ToUInt16(textBox3.Text) < 100)
            {
                limI = Convert.ToUInt16(textBox3.Text);
                string mes = Convert.ToString(limI);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('w' + mes);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            _serialName = textBox6.Text;
            _serialPort = new SerialPort
            {
                PortName = _serialName,
                BaudRate = 115200,
                ReadTimeout = 500,
                WriteTimeout = 500
            };            
            _serialPort.Open();
            if (_serialPort.IsOpen)
            {
                button23.BackColor = Color.LightGreen;
                _serialPort.WriteLine("o");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox8.Text) < 99 && Convert.ToInt32(textBox8.Text) > 1)
            {
                integr = Convert.ToUInt16(textBox8.Text);
                string mes = Convert.ToString(integr);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('I' + mes);
            }
            else
                textBox8.Text = Convert.ToString(integr);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox7.Text) < 99 && Convert.ToInt32(textBox7.Text) > 1)
            {
                prop = Convert.ToUInt16(textBox7.Text);
                string mes = Convert.ToString(prop);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('P' + mes);
            }
            else
                textBox7.Text = Convert.ToString(prop);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox9.Text) < 99 && Convert.ToInt32(textBox9.Text) > 1)
            {
                diff = Convert.ToUInt16(textBox9.Text);
                string mes = Convert.ToString(diff);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('D' + mes);
            }
            else
                textBox7.Text = Convert.ToString(diff);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            limD = Convert.ToUInt16(textBox5.Text);
            if (Convert.ToUInt16(textBox5.Text) > 1 && Convert.ToUInt16(textBox5.Text) < 100)
            {
                limD = Convert.ToUInt16(textBox5.Text);
                string mes = Convert.ToString(limD);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('z' + mes);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }


        private void button24_Click(object sender, EventArgs e)
        {
            textBox10.Text = "d_t = " + Convert.ToString(d_t);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            motor_mask[3] = !motor_mask[3];
            byte tmp = 0;
            for(var i=0; i<4;i++)
            {
                if (motor_mask[i])
                {
                    tmp += Convert.ToByte(Math.Pow(2, i));
                }
            }
            string mes = Convert.ToString(tmp);
            while (mes.Length < 5)
            {
                mes = ' ' + mes;
            }
            _serialPort.WriteLine('a' + mes);

            if (motor_mask[3])
                button25.BackColor = Color.LimeGreen;
            else
                button25.BackColor = Color.Red;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            motor_mask[2] = !motor_mask[2];
            byte tmp = 0;
            for (var i = 0; i < 4; i++)
            {
                if (motor_mask[i])
                {
                    tmp += Convert.ToByte(Math.Pow(2, i));
                }
            }
            string mes = Convert.ToString(tmp);
            while (mes.Length < 5)
            {
                mes = ' ' + mes;
            }
            _serialPort.WriteLine('a' + mes);

            if (motor_mask[2])
                button26.BackColor = Color.LimeGreen;
            else
                button26.BackColor = Color.Red;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            motor_mask[0] = !motor_mask[0];
            byte tmp = 0;
            for (var i = 0; i < 4; i++)
            {
                if (motor_mask[i])
                {
                    tmp += Convert.ToByte(Math.Pow(2, i));
                }
            }
            string mes = Convert.ToString(tmp);
            while (mes.Length < 5)
            {
                mes = ' ' + mes;
            }
            _serialPort.WriteLine('a' + mes);

            if (motor_mask[0])
                button27.BackColor = Color.LimeGreen;
            else
                button27.BackColor = Color.Red;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            motor_mask[1] = !motor_mask[1];
            byte tmp = 0;
            for (var i = 0; i < 4; i++)
            {
                if (motor_mask[i])
                {
                    tmp += Convert.ToByte(Math.Pow(2, i));
                }
            }
            string mes = Convert.ToString(tmp);
            while (mes.Length < 5)
            {
                mes = ' ' + mes;
            }
            _serialPort.WriteLine('a' + mes);

            if (motor_mask[1])
                button28.BackColor = Color.LimeGreen;
            else
                button28.BackColor = Color.Red;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (limD >1)
            {
                limD--;
                string mes = Convert.ToString(limD);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('z' + mes);
            }
            textBox5.Text = Convert.ToString(limD);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            force = 0;
            textBox1.Text = Convert.ToString(force / 10) + '%';
            _serialPort.WriteLine("f00000\n");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (force > 29)
            {
                force -= 30;
                string mes = Convert.ToString(force);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('f' + mes);
                textBox1.Text = Convert.ToString(force / 10) + '%';
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(prop<200)
            {
                prop++;
                string mes = Convert.ToString(prop);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('P' + mes);
            }
            textBox7.Text = Convert.ToString(prop);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (integr < 200)
            {
                integr++;
                string mes = Convert.ToString(integr);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('I' + mes);
            }
            textBox8.Text = Convert.ToString(integr);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (diff >0)
            {
                diff--;
                string mes = Convert.ToString(diff);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('D' + mes);
            }
            textBox9.Text = Convert.ToString(diff);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (prop >0)
            {
                prop--;
                string mes = Convert.ToString(prop);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('P' + mes);
            }
            textBox7.Text = Convert.ToString(prop);
        }


        private void button13_Click(object sender, EventArgs e)
        {
            if (integr >0)
            {
                integr--;
                string mes = Convert.ToString(integr);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('I' + mes);
            }
            textBox8.Text = Convert.ToString(integr);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (diff < 200)
            {
                diff++;
                string mes = Convert.ToString(diff);
                while (mes.Length < 5)
                {
                    mes = ' ' + mes;
                }
                _serialPort.WriteLine('D' + mes);
            }
            textBox9.Text = Convert.ToString(diff);
        }



        private void Form1_Load(object sender, System.EventArgs e)
        {


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
                    if (_serialPort.IsOpen)
                    {
                        message = _serialPort.ReadLine();
                        if (message[0] >= 'A' && message[0] <= 'z')
                        {
                            char lol = message[0];
                            message = message.Remove(0, 1);
                            switch (lol)
                            {
                                case 'A':
                                    curr[0] = ((Convert.ToInt32(message)*6 + 32768) / div2); //pitch 1
                                    break;
                                case 'B':
                                    curr[1] = ((Convert.ToInt32(message)*6 + 32768) / div2); //roll
                                    break;
                                case 'C':
                                    curr[2] = ((Convert.ToInt32(message)*3 + 32768) / div2); //yaw
                                    break;
                                case 'D':
                                    curr[3] = ((Convert.ToInt32(message)*200 + 32768) / div2); //proportional val on pitch
                                    break;
                                case 'E':
                                    curr[4] = ((Convert.ToInt32(message)*200 + 32768) / div2); //integated val on pitch
                                    break;
                                case 'F':
                                    curr[5] = ((Convert.ToInt32(message)*200 + 32768)/div2); //differential val on pitch
                                    break;
                                case 'G':
                                    curr[6] = ((Convert.ToInt32(message)*200 + 32768) / div2);//proportional val on roll
                                    break;
                                case 'H':
                                    curr[7] = ((Convert.ToInt32(message)*200 + 32768) / div2);//integated val on roll
                                    break;
                                case 'I':
                                    curr[8] = ((Convert.ToInt32(message)*200 + 32768) / div2);//differential val on roll
                                    break;
                                case 'J':
                                    curr[9] = ((Convert.ToInt32(message) + 32768) / div2) ; //proportional val on yaw
                                    break;
                                case 'K':
                                    curr[10] = ((Convert.ToInt32(message) + 32768) / div2) ;//integated val on yaw
                                    break;
                                case 'L':
                                    curr[11] = ((Convert.ToInt32(message) + 32768) / div2) ;//differential val on yaw
                                    break;
                                case 'M':
                                    d_t = (Convert.ToInt32(message)); // time of iteration
                                    break;
                                case 'N':
                                    curr[13] = (Convert.ToInt32(message) ) / div2; //altitude
                                    break;
                                case 'w':
                                    pow_curr[0] = (Convert.ToInt32(message) ) * 2 / div2; //motor power
                                    break;
                                case 'x':
                                    pow_curr[1] = (Convert.ToInt32(message) ) * 2 / div2;//motor power
                                    break;
                                case 'y':
                                    pow_curr[2] = (Convert.ToInt32(message) ) * 2 / div2;//motor power
                                    break;
                                case 'z':
                                    pow_curr[3] = (Convert.ToInt32(message) ) * 2 / div2;//motor power
                                    break;
                                case 'l':
                                    string mes = Convert.ToString(force);
                                    while (mes.Length < 5)
                                    {
                                        mes = ' ' + mes;
                                    }
                                    _serialPort.WriteLine('l' + mes);
                                    break;

                                case 'W':
                                    if (_draw)
                                    {
                                        for (var k = 0; k < 12; k += 3)
                                        {
                                            if (_isDrawing[0 + k])
                                                g.DrawLine(pRed, j, prev[0 + k], j + 1, curr[0 + k]);
                                            if (_isDrawing[1 + k])
                                                g.DrawLine(pGreen, j, prev[1 + k], j + 1, curr[1 + k]);
                                            if (_isDrawing[2 + k])
                                                g.DrawLine(pBlue, j, prev[2 + k], j + 1, curr[2 + k]);
                                        }
                                        if (_isDrawing[13])
                                            g.DrawLine(pBlack, j, prev[13], j + 1, curr[13]);
                                        if (_isDrawing[14])
                                        {
                                                g.DrawLine(pRed, j, pow_prev[0], j + 1, pow_curr[0]);
                                                g.DrawLine(pGreen, j, pow_prev[1], j + 1, pow_curr[1]);
                                                g.DrawLine(pBlue, j, pow_prev[2], j + 1, pow_curr[2]);
                                                g.DrawLine(pBlack, j, pow_prev[3], j + 1, pow_curr[3]);
                                        }
                                        j = (j + 1) % w;
                                        for (var q = 0; q < 14; q++)
                                            prev[q] = curr[q];
                                       pow_prev[0] = pow_curr[0];
                                       pow_prev[1] = pow_curr[1];
                                       pow_prev[2] = pow_curr[2];
                                       pow_prev[3] = pow_curr[3];
                                    }
                                    break;
                            }

                        }
                    }
                    else
                        Thread.Sleep(1000);
                    
                }
                catch (Exception) { }
                if (j < 1)
                {
                    try
                    {
                        g.Clear(Color.White);
                        g.DrawLine(pGray, j, (h / 2)+1, w, (h / 2)+1); // середина экрана
                        g.DrawLine(pGray, j, (h / 2) + 1 - 160, w, (h / 2) + 1 - 160); // середина экрана
                        g.DrawLine(pGray, j, (h / 2) + 1 + 160, w, (h / 2) + 1 + 160); // середина экрана

                        j = 1;
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

                        if (state.Offset == JoystickOffset.X)
                        {
                            yaw_curr = ((32767 - state.Value) / 4096);
                            if (Math.Abs(yaw_curr - yaw_prev) >3 )
                            {
                                string mes = Convert.ToString(yaw_curr);
                                while (mes.Length < 5)
                                {
                                    mes = ' ' + mes;
                                }
                                _serialPort.WriteLine('y' + mes);
                                yaw_prev = yaw_curr;

                            }
                            work = true;
                        }
                        if (state.Offset == JoystickOffset.Z)
                        {
                            pitch_curr = ((32767 - state.Value) / 37) + pitch0;
                            if(Math.Abs(pitch_curr - pitch_prev)>100)
                            {
                                string mes = Convert.ToString(pitch_curr);
                                while (mes.Length < 5)
                                {
                                    mes = ' ' + mes;
                                }
                                _serialPort.WriteLine('p'+mes);
                                pitch_prev = pitch_curr;
                                
                            }
                            work = true;
                        }
                        if (state.Offset == JoystickOffset.RotationZ)
                        {
                            roll_curr = ((state.Value - 32767) / 37) + roll0;
                            if (Math.Abs(roll_curr - roll_prev) > 100)
                            {
                                string mes = Convert.ToString(roll_curr);
                                while (mes.Length < 5)
                                {
                                    mes = ' ' + mes;
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
                                        force += 20;
                                        string mes = Convert.ToString(force);
                                        while (mes.Length < 5)
                                        {
                                            mes =  ' ' + mes;
                                        }
                                        _serialPort.WriteLine('f'+mes);
                                    }
                                    break;
                                case 9000:
                                    force = 0;
                                    _serialPort.WriteLine("f00000\n");
                                    break;
                                case 18000:
                                    if (force >19)
                                    {
                                        force -= 20;
                                        string mes = Convert.ToString(force);
                                        while (mes.Length < 5)
                                        {
                                            mes = ' ' + mes;
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
