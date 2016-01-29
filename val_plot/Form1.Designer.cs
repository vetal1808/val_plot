namespace val_plot
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CanvasPanel = new System.Windows.Forms.Panel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CanvasPanel
            // 
            this.CanvasPanel.Location = new System.Drawing.Point(12, 12);
            this.CanvasPanel.Name = "CanvasPanel";
            this.CanvasPanel.Size = new System.Drawing.Size(800, 650);
            this.CanvasPanel.TabIndex = 0;
            this.CanvasPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CanvasPanel_Paint);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "pitch",
            "roll",
            "yaw",
            "P_pitch",
            "I_pitch",
            "D_pitch",
            "P_roll",
            "I_roll",
            "D_roll",
            "P_yaw",
            "I_yaw",
            "D_yaw"});
            this.checkedListBox1.Location = new System.Drawing.Point(827, 23);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(164, 184);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(826, 218);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(164, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "Drawing";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(921, 282);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 32);
            this.button2.TabIndex = 3;
            this.button2.Text = "↑";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(881, 320);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(33, 33);
            this.button3.TabIndex = 4;
            this.button3.Text = "←";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(921, 359);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(34, 31);
            this.button4.TabIndex = 5;
            this.button4.Text = "↓";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(961, 320);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(35, 33);
            this.button5.TabIndex = 6;
            this.button5.Text = "→";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(920, 320);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(35, 33);
            this.button6.TabIndex = 7;
            this.button6.Text = "S";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(827, 282);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(32, 32);
            this.button7.TabIndex = 8;
            this.button7.Text = "U";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(827, 320);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(32, 33);
            this.button8.TabIndex = 9;
            this.button8.Text = "S";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.Control;
            this.button9.Location = new System.Drawing.Point(827, 359);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(31, 31);
            this.button9.TabIndex = 10;
            this.button9.Text = "D";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Lime;
            this.button10.Location = new System.Drawing.Point(879, 411);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(35, 33);
            this.button10.TabIndex = 13;
            this.button10.Text = "I↑";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Blue;
            this.button11.Location = new System.Drawing.Point(921, 450);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(35, 33);
            this.button11.TabIndex = 12;
            this.button11.Text = "D↓";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Red;
            this.button12.Location = new System.Drawing.Point(840, 411);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(33, 33);
            this.button12.TabIndex = 11;
            this.button12.Text = "P↑";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.Lime;
            this.button13.Location = new System.Drawing.Point(878, 450);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(35, 33);
            this.button13.TabIndex = 16;
            this.button13.Text = "I↓";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.Blue;
            this.button14.Location = new System.Drawing.Point(921, 411);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(35, 33);
            this.button14.TabIndex = 15;
            this.button14.Text = "D↑";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Red;
            this.button15.Location = new System.Drawing.Point(839, 450);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(33, 33);
            this.button15.TabIndex = 14;
            this.button15.Text = "P↓";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.Location = new System.Drawing.Point(818, 256);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 20);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "POWER";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(898, 256);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(81, 20);
            this.textBox4.TabIndex = 20;
            this.textBox4.Text = "Normal vector";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(827, 602);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(52, 33);
            this.button16.TabIndex = 21;
            this.button16.Text = "P lim↓";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(944, 602);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(56, 33);
            this.button17.TabIndex = 22;
            this.button17.Text = "D lim↓";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(944, 563);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(56, 33);
            this.button18.TabIndex = 23;
            this.button18.Text = "D lim↑";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(827, 563);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(51, 33);
            this.button19.TabIndex = 24;
            this.button19.Text = "P lim↑";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(887, 563);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(51, 33);
            this.button20.TabIndex = 25;
            this.button20.Text = "I lim↑";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(886, 602);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(52, 33);
            this.button21.TabIndex = 26;
            this.button21.Text = "I lim↓";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(856, 507);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(100, 33);
            this.button22.TabIndex = 27;
            this.button22.Text = "send mask";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 657);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.CanvasPanel);
            this.Name = "Form1";
            this.Text = "Draw";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel CanvasPanel;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
    }
}

