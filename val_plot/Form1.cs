using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

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
        static Pen pRed = new Pen(Color.Red, 1);
        static Pen pGreen = new Pen(Color.Green, 1);
        static Pen pBlue = new Pen(Color.Blue, 1);
        static Pen pBlack = new Pen(Color.Black, 1);
        static Pen pGray = new Pen(Color.Gray, 1);
        static int j = 0;
        static bool[] _isDrawing = new bool[12];
        static Graphics g;

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
            _serialPort.Write("w");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _serialPort.Write("q");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _serialPort.Write("a");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _serialPort.Write("d");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _serialPort.Write("s");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _serialPort.Write("U");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _serialPort.Write("Q");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _serialPort.Write("D");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _serialPort.Write("B");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _serialPort.Write("N");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _serialPort.Write("M");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            _serialPort.Write("b");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            _serialPort.Write("n");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            _serialPort.Write("m");
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            _serialPort = new SerialPort
            {
                PortName = "COM10",
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
                    if(message[1] == ':' && message[0] >='A' && message[0]<='W')
                    {
                        char lol = message[0];
                        message = message.Remove(0, 2);
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
    }
}
