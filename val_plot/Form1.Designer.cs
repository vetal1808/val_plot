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
            this.components = new System.ComponentModel.Container();
            this.CanvasPanel = new System.Windows.Forms.Panel();
            this.diff_limit = new System.Windows.Forms.NumericUpDown();
            this.inegr_limit = new System.Windows.Forms.NumericUpDown();
            this.prop_limit = new System.Windows.Forms.NumericUpDown();
            this.diff_gain = new System.Windows.Forms.NumericUpDown();
            this.integr_gain = new System.Windows.Forms.NumericUpDown();
            this.prop_gain = new System.Windows.Forms.NumericUpDown();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.drawingSwitcherButton = new System.Windows.Forms.Button();
            this.forwardOffsetButton = new System.Windows.Forms.Button();
            this.leftOffsetButton = new System.Windows.Forms.Button();
            this.backOffsetButton = new System.Windows.Forms.Button();
            this.rightOffsetButton = new System.Windows.Forms.Button();
            this.resetOffsetButton = new System.Windows.Forms.Button();
            this.upThrustButton = new System.Windows.Forms.Button();
            this.offThrustButton = new System.Windows.Forms.Button();
            this.downThrustButton = new System.Windows.Forms.Button();
            this.applyMaskButton = new System.Windows.Forms.Button();
            this.comPortNameBox = new System.Windows.Forms.TextBox();
            this.connectToComPortButton = new System.Windows.Forms.Button();
            this.calculationTime = new System.Windows.Forms.TextBox();
            this.flRotorSwitcherButton = new System.Windows.Forms.Button();
            this.blRotorSwitcherButton = new System.Windows.Forms.Button();
            this.frRotorSwitcherButton = new System.Windows.Forms.Button();
            this.brRotorSwitcherButton = new System.Windows.Forms.Button();
            this.thrustValueTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.scale_of_line = new System.Windows.Forms.NumericUpDown();
            this.scaling_line = new System.Windows.Forms.NumericUpDown();
            this.joysticSense = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.PID_list = new System.Windows.Forms.ComboBox();
            this.flightModeButton = new System.Windows.Forms.ComboBox();
            this.battetyVoltage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.gpsAccuracy = new System.Windows.Forms.TextBox();
            this.sattelitesUsed = new System.Windows.Forms.TextBox();
            this.altitudeMetod = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.diff_limit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inegr_limit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prop_limit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diff_gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integr_gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prop_gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale_of_line)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaling_line)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.joysticSense)).BeginInit();
            this.SuspendLayout();
            // 
            // CanvasPanel
            // 
            this.CanvasPanel.Location = new System.Drawing.Point(12, 12);
            this.CanvasPanel.Name = "CanvasPanel";
            this.CanvasPanel.Size = new System.Drawing.Size(789, 650);
            this.CanvasPanel.TabIndex = 0;
            this.CanvasPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CanvasPanel_Paint);
            // 
            // diff_limit
            // 
            this.diff_limit.Location = new System.Drawing.Point(959, 509);
            this.diff_limit.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.diff_limit.Name = "diff_limit";
            this.diff_limit.Size = new System.Drawing.Size(40, 20);
            this.diff_limit.TabIndex = 5;
            this.diff_limit.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.diff_limit.ValueChanged += new System.EventHandler(this.diff_limit_ValueChanged);
            // 
            // inegr_limit
            // 
            this.inegr_limit.Location = new System.Drawing.Point(903, 509);
            this.inegr_limit.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.inegr_limit.Name = "inegr_limit";
            this.inegr_limit.Size = new System.Drawing.Size(40, 20);
            this.inegr_limit.TabIndex = 4;
            this.inegr_limit.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.inegr_limit.ValueChanged += new System.EventHandler(this.inegr_limit_ValueChanged);
            // 
            // prop_limit
            // 
            this.prop_limit.Location = new System.Drawing.Point(847, 509);
            this.prop_limit.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.prop_limit.Name = "prop_limit";
            this.prop_limit.Size = new System.Drawing.Size(43, 20);
            this.prop_limit.TabIndex = 3;
            this.prop_limit.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.prop_limit.ValueChanged += new System.EventHandler(this.prop_limit_ValueChanged);
            // 
            // diff_gain
            // 
            this.diff_gain.Location = new System.Drawing.Point(959, 483);
            this.diff_gain.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.diff_gain.Name = "diff_gain";
            this.diff_gain.Size = new System.Drawing.Size(40, 20);
            this.diff_gain.TabIndex = 2;
            this.diff_gain.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.diff_gain.ValueChanged += new System.EventHandler(this.diff_gain_ValueChanged);
            // 
            // integr_gain
            // 
            this.integr_gain.Location = new System.Drawing.Point(903, 483);
            this.integr_gain.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.integr_gain.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.integr_gain.Name = "integr_gain";
            this.integr_gain.Size = new System.Drawing.Size(40, 20);
            this.integr_gain.TabIndex = 1;
            this.integr_gain.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.integr_gain.ValueChanged += new System.EventHandler(this.integr_gain_ValueChanged);
            // 
            // prop_gain
            // 
            this.prop_gain.Location = new System.Drawing.Point(847, 483);
            this.prop_gain.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.prop_gain.Name = "prop_gain";
            this.prop_gain.Size = new System.Drawing.Size(43, 20);
            this.prop_gain.TabIndex = 0;
            this.prop_gain.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.prop_gain.ValueChanged += new System.EventHandler(this.prop_gain_ValueChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "pitch",
            "roll",
            "yaw",
            "Prop pitch",
            "Integr pitch",
            "Diff pitch",
            "Prop roll",
            "Integr roll",
            "Diff roll",
            "Prop yaw",
            "Integr yaw",
            "Diff yaw",
            "baro altitude",
            "sonar distanse",
            "altitude",
            "altitude velocity",
            "calcualtion time",
            "battety voltage",
            "system flags",
            "GPS accuarcy & statellits used",
            "reserved 0",
            "reserved 1",
            "reserved 2",
            "reserved 3",
            "reserved 4",
            "reserved 5"});
            this.checkedListBox1.Location = new System.Drawing.Point(811, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(188, 169);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // drawingSwitcherButton
            // 
            this.drawingSwitcherButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.drawingSwitcherButton.Location = new System.Drawing.Point(811, 213);
            this.drawingSwitcherButton.Name = "drawingSwitcherButton";
            this.drawingSwitcherButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.drawingSwitcherButton.Size = new System.Drawing.Size(79, 25);
            this.drawingSwitcherButton.TabIndex = 2;
            this.drawingSwitcherButton.Text = "drawing";
            this.drawingSwitcherButton.UseVisualStyleBackColor = false;
            this.drawingSwitcherButton.Click += new System.EventHandler(this.drawingSwitcherButton_Click);
            // 
            // forwardOffsetButton
            // 
            this.forwardOffsetButton.Location = new System.Drawing.Point(913, 300);
            this.forwardOffsetButton.Name = "forwardOffsetButton";
            this.forwardOffsetButton.Size = new System.Drawing.Size(40, 40);
            this.forwardOffsetButton.TabIndex = 3;
            this.forwardOffsetButton.Text = "↑";
            this.forwardOffsetButton.UseVisualStyleBackColor = true;
            this.forwardOffsetButton.Click += new System.EventHandler(this.forwardOffsetButton_Click);
            // 
            // leftOffsetButton
            // 
            this.leftOffsetButton.Location = new System.Drawing.Point(867, 346);
            this.leftOffsetButton.Name = "leftOffsetButton";
            this.leftOffsetButton.Size = new System.Drawing.Size(40, 40);
            this.leftOffsetButton.TabIndex = 4;
            this.leftOffsetButton.Text = "←";
            this.leftOffsetButton.UseVisualStyleBackColor = true;
            this.leftOffsetButton.Click += new System.EventHandler(this.leftOffsetButton_Click);
            // 
            // backOffsetButton
            // 
            this.backOffsetButton.Location = new System.Drawing.Point(913, 392);
            this.backOffsetButton.Name = "backOffsetButton";
            this.backOffsetButton.Size = new System.Drawing.Size(40, 40);
            this.backOffsetButton.TabIndex = 5;
            this.backOffsetButton.Text = "↓";
            this.backOffsetButton.UseVisualStyleBackColor = true;
            this.backOffsetButton.Click += new System.EventHandler(this.backOffsetButton_Click);
            // 
            // rightOffsetButton
            // 
            this.rightOffsetButton.Location = new System.Drawing.Point(959, 346);
            this.rightOffsetButton.Name = "rightOffsetButton";
            this.rightOffsetButton.Size = new System.Drawing.Size(40, 40);
            this.rightOffsetButton.TabIndex = 6;
            this.rightOffsetButton.Text = "→";
            this.rightOffsetButton.UseVisualStyleBackColor = true;
            this.rightOffsetButton.Click += new System.EventHandler(this.rightOffsetButton_Click);
            // 
            // resetOffsetButton
            // 
            this.resetOffsetButton.Location = new System.Drawing.Point(913, 346);
            this.resetOffsetButton.Name = "resetOffsetButton";
            this.resetOffsetButton.Size = new System.Drawing.Size(40, 40);
            this.resetOffsetButton.TabIndex = 7;
            this.resetOffsetButton.Text = "S";
            this.resetOffsetButton.UseVisualStyleBackColor = true;
            this.resetOffsetButton.Click += new System.EventHandler(this.resetOffsetButton_Click);
            // 
            // upThrustButton
            // 
            this.upThrustButton.Location = new System.Drawing.Point(811, 300);
            this.upThrustButton.Name = "upThrustButton";
            this.upThrustButton.Size = new System.Drawing.Size(50, 40);
            this.upThrustButton.TabIndex = 8;
            this.upThrustButton.Text = "UP";
            this.upThrustButton.UseVisualStyleBackColor = true;
            this.upThrustButton.Click += new System.EventHandler(this.upThrustButton_Click);
            // 
            // offThrustButton
            // 
            this.offThrustButton.Location = new System.Drawing.Point(811, 346);
            this.offThrustButton.Name = "offThrustButton";
            this.offThrustButton.Size = new System.Drawing.Size(50, 40);
            this.offThrustButton.TabIndex = 9;
            this.offThrustButton.Text = "STOP";
            this.offThrustButton.UseVisualStyleBackColor = true;
            this.offThrustButton.Click += new System.EventHandler(this.offThrustButton_Click);
            // 
            // downThrustButton
            // 
            this.downThrustButton.BackColor = System.Drawing.SystemColors.Control;
            this.downThrustButton.Location = new System.Drawing.Point(811, 392);
            this.downThrustButton.Name = "downThrustButton";
            this.downThrustButton.Size = new System.Drawing.Size(50, 40);
            this.downThrustButton.TabIndex = 10;
            this.downThrustButton.Text = "DOWN";
            this.downThrustButton.UseVisualStyleBackColor = false;
            this.downThrustButton.Click += new System.EventHandler(this.downThrustButton_Click);
            // 
            // applyMaskButton
            // 
            this.applyMaskButton.Location = new System.Drawing.Point(896, 213);
            this.applyMaskButton.Name = "applyMaskButton";
            this.applyMaskButton.Size = new System.Drawing.Size(103, 25);
            this.applyMaskButton.TabIndex = 27;
            this.applyMaskButton.Text = "apply mask";
            this.applyMaskButton.UseVisualStyleBackColor = true;
            this.applyMaskButton.Click += new System.EventHandler(this.applyMaskButton_Click);
            // 
            // comPortNameBox
            // 
            this.comPortNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comPortNameBox.Location = new System.Drawing.Point(811, 248);
            this.comPortNameBox.Name = "comPortNameBox";
            this.comPortNameBox.Size = new System.Drawing.Size(79, 21);
            this.comPortNameBox.TabIndex = 31;
            this.comPortNameBox.Text = "COM4";
            this.comPortNameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comPortNameBox.TextChanged += new System.EventHandler(this.comPortNameBox_TextChanged);
            // 
            // connectToComPortButton
            // 
            this.connectToComPortButton.BackColor = System.Drawing.Color.Red;
            this.connectToComPortButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.connectToComPortButton.Location = new System.Drawing.Point(896, 244);
            this.connectToComPortButton.Name = "connectToComPortButton";
            this.connectToComPortButton.Size = new System.Drawing.Size(103, 30);
            this.connectToComPortButton.TabIndex = 32;
            this.connectToComPortButton.Text = "connect";
            this.connectToComPortButton.UseVisualStyleBackColor = false;
            this.connectToComPortButton.Click += new System.EventHandler(this.connectToComPortButton_Click);
            // 
            // calculationTime
            // 
            this.calculationTime.Location = new System.Drawing.Point(905, 561);
            this.calculationTime.Name = "calculationTime";
            this.calculationTime.Size = new System.Drawing.Size(94, 20);
            this.calculationTime.TabIndex = 36;
            this.calculationTime.Text = "computing time";
            this.calculationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.calculationTime.TextChanged += new System.EventHandler(this.textBox10_TextChanged);
            // 
            // flRotorSwitcherButton
            // 
            this.flRotorSwitcherButton.BackColor = System.Drawing.Color.Lime;
            this.flRotorSwitcherButton.Location = new System.Drawing.Point(867, 300);
            this.flRotorSwitcherButton.Name = "flRotorSwitcherButton";
            this.flRotorSwitcherButton.Size = new System.Drawing.Size(40, 40);
            this.flRotorSwitcherButton.TabIndex = 38;
            this.flRotorSwitcherButton.Text = "FL";
            this.flRotorSwitcherButton.UseVisualStyleBackColor = false;
            this.flRotorSwitcherButton.Click += new System.EventHandler(this.flRotorSwitcher_Click);
            // 
            // blRotorSwitcherButton
            // 
            this.blRotorSwitcherButton.BackColor = System.Drawing.Color.Lime;
            this.blRotorSwitcherButton.Location = new System.Drawing.Point(867, 392);
            this.blRotorSwitcherButton.Name = "blRotorSwitcherButton";
            this.blRotorSwitcherButton.Size = new System.Drawing.Size(40, 40);
            this.blRotorSwitcherButton.TabIndex = 39;
            this.blRotorSwitcherButton.Text = "BL";
            this.blRotorSwitcherButton.UseVisualStyleBackColor = false;
            this.blRotorSwitcherButton.Click += new System.EventHandler(this.blRotorSwitcher_Click);
            // 
            // frRotorSwitcherButton
            // 
            this.frRotorSwitcherButton.BackColor = System.Drawing.Color.Lime;
            this.frRotorSwitcherButton.Location = new System.Drawing.Point(959, 300);
            this.frRotorSwitcherButton.Name = "frRotorSwitcherButton";
            this.frRotorSwitcherButton.Size = new System.Drawing.Size(40, 40);
            this.frRotorSwitcherButton.TabIndex = 40;
            this.frRotorSwitcherButton.Text = "FR";
            this.frRotorSwitcherButton.UseVisualStyleBackColor = false;
            this.frRotorSwitcherButton.Click += new System.EventHandler(this.frRotorSwitcher_Click);
            // 
            // brRotorSwitcherButton
            // 
            this.brRotorSwitcherButton.BackColor = System.Drawing.Color.Lime;
            this.brRotorSwitcherButton.Location = new System.Drawing.Point(959, 392);
            this.brRotorSwitcherButton.Name = "brRotorSwitcherButton";
            this.brRotorSwitcherButton.Size = new System.Drawing.Size(40, 40);
            this.brRotorSwitcherButton.TabIndex = 41;
            this.brRotorSwitcherButton.Text = "BR";
            this.brRotorSwitcherButton.UseVisualStyleBackColor = false;
            this.brRotorSwitcherButton.Click += new System.EventHandler(this.brRotorSwitcher_Click);
            // 
            // thrustValueTextbox
            // 
            this.thrustValueTextbox.Location = new System.Drawing.Point(811, 277);
            this.thrustValueTextbox.Name = "thrustValueTextbox";
            this.thrustValueTextbox.Size = new System.Drawing.Size(50, 20);
            this.thrustValueTextbox.TabIndex = 42;
            this.thrustValueTextbox.TextChanged += new System.EventHandler(this.thrustValueTextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(858, 464);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 16);
            this.label1.TabIndex = 43;
            this.label1.Text = "P";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(915, 464);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "I";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(967, 464);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 16);
            this.label4.TabIndex = 45;
            this.label4.Text = "D";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(813, 487);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "gain";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(813, 513);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 47;
            this.label6.Text = "limit";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(813, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 16);
            this.label2.TabIndex = 50;
            this.label2.Text = "line";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(902, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 16);
            this.label7.TabIndex = 51;
            this.label7.Text = "scale";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scale_of_line
            // 
            this.scale_of_line.Location = new System.Drawing.Point(949, 187);
            this.scale_of_line.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.scale_of_line.Name = "scale_of_line";
            this.scale_of_line.Size = new System.Drawing.Size(50, 20);
            this.scale_of_line.TabIndex = 53;
            this.scale_of_line.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.scale_of_line.ValueChanged += new System.EventHandler(this.scale_of_line_ValueChanged);
            // 
            // scaling_line
            // 
            this.scaling_line.Location = new System.Drawing.Point(853, 187);
            this.scaling_line.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.scaling_line.Name = "scaling_line";
            this.scaling_line.Size = new System.Drawing.Size(44, 20);
            this.scaling_line.TabIndex = 54;
            this.scaling_line.ValueChanged += new System.EventHandler(this.scaling_line_ValueChanged);
            // 
            // joysticSense
            // 
            this.joysticSense.Location = new System.Drawing.Point(898, 537);
            this.joysticSense.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.joysticSense.Name = "joysticSense";
            this.joysticSense.Size = new System.Drawing.Size(44, 20);
            this.joysticSense.TabIndex = 56;
            this.joysticSense.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.joysticSense.ValueChanged += new System.EventHandler(this.joysticSense_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(813, 539);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 16);
            this.label8.TabIndex = 55;
            this.label8.Text = "joystic sens";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PID_list
            // 
            this.PID_list.DisplayMember = "0";
            this.PID_list.FormattingEnabled = true;
            this.PID_list.Items.AddRange(new object[] {
            "pitch",
            "roll",
            "yaw",
            "Ox",
            "Oy",
            "Oz"});
            this.PID_list.Location = new System.Drawing.Point(891, 440);
            this.PID_list.Name = "PID_list";
            this.PID_list.Size = new System.Drawing.Size(108, 21);
            this.PID_list.TabIndex = 57;
            this.PID_list.SelectedIndexChanged += new System.EventHandler(this.PID_list_SelectedIndexChanged);
            // 
            // flightModeButton
            // 
            this.flightModeButton.DisplayMember = "0";
            this.flightModeButton.FormattingEnabled = true;
            this.flightModeButton.Items.AddRange(new object[] {
            "STAND BY",
            "MANUAL",
            "ALT HOLD",
            "POS HOLD"});
            this.flightModeButton.Location = new System.Drawing.Point(867, 276);
            this.flightModeButton.Name = "flightModeButton";
            this.flightModeButton.Size = new System.Drawing.Size(132, 21);
            this.flightModeButton.TabIndex = 58;
            this.flightModeButton.SelectedIndexChanged += new System.EventHandler(this.flightModeButton_SelectedIndexChanged);
            // 
            // battetyVoltage
            // 
            this.battetyVoltage.Location = new System.Drawing.Point(905, 587);
            this.battetyVoltage.Name = "battetyVoltage";
            this.battetyVoltage.Size = new System.Drawing.Size(94, 20);
            this.battetyVoltage.TabIndex = 59;
            this.battetyVoltage.Text = "battery voltage";
            this.battetyVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(946, 539);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 16);
            this.label9.TabIndex = 60;
            this.label9.Text = "degree";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(813, 445);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 61;
            this.label10.Text = "PID setting";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpsAccuracy
            // 
            this.gpsAccuracy.Location = new System.Drawing.Point(816, 587);
            this.gpsAccuracy.Name = "gpsAccuracy";
            this.gpsAccuracy.Size = new System.Drawing.Size(81, 20);
            this.gpsAccuracy.TabIndex = 62;
            this.gpsAccuracy.Text = "accuracy";
            this.gpsAccuracy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gpsAccuracy.TextChanged += new System.EventHandler(this.gpsAccuracy_TextChanged);
            // 
            // sattelitesUsed
            // 
            this.sattelitesUsed.Location = new System.Drawing.Point(816, 561);
            this.sattelitesUsed.Name = "sattelitesUsed";
            this.sattelitesUsed.Size = new System.Drawing.Size(81, 20);
            this.sattelitesUsed.TabIndex = 63;
            this.sattelitesUsed.Text = "sattelites";
            this.sattelitesUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // altitudeMetod
            // 
            this.altitudeMetod.Location = new System.Drawing.Point(816, 613);
            this.altitudeMetod.Name = "altitudeMetod";
            this.altitudeMetod.Size = new System.Drawing.Size(81, 20);
            this.altitudeMetod.TabIndex = 64;
            this.altitudeMetod.Text = "altitude method";
            this.altitudeMetod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(905, 613);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(94, 20);
            this.textBox2.TabIndex = 65;
            this.textBox2.Text = "reserved";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 669);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.altitudeMetod);
            this.Controls.Add(this.sattelitesUsed);
            this.Controls.Add(this.gpsAccuracy);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.battetyVoltage);
            this.Controls.Add(this.flightModeButton);
            this.Controls.Add(this.PID_list);
            this.Controls.Add(this.joysticSense);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.scaling_line);
            this.Controls.Add(this.scale_of_line);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.diff_limit);
            this.Controls.Add(this.thrustValueTextbox);
            this.Controls.Add(this.inegr_limit);
            this.Controls.Add(this.brRotorSwitcherButton);
            this.Controls.Add(this.prop_limit);
            this.Controls.Add(this.diff_gain);
            this.Controls.Add(this.upThrustButton);
            this.Controls.Add(this.integr_gain);
            this.Controls.Add(this.prop_gain);
            this.Controls.Add(this.frRotorSwitcherButton);
            this.Controls.Add(this.offThrustButton);
            this.Controls.Add(this.blRotorSwitcherButton);
            this.Controls.Add(this.calculationTime);
            this.Controls.Add(this.downThrustButton);
            this.Controls.Add(this.flRotorSwitcherButton);
            this.Controls.Add(this.forwardOffsetButton);
            this.Controls.Add(this.leftOffsetButton);
            this.Controls.Add(this.connectToComPortButton);
            this.Controls.Add(this.resetOffsetButton);
            this.Controls.Add(this.comPortNameBox);
            this.Controls.Add(this.rightOffsetButton);
            this.Controls.Add(this.backOffsetButton);
            this.Controls.Add(this.applyMaskButton);
            this.Controls.Add(this.drawingSwitcherButton);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.CanvasPanel);
            this.Name = "Form1";
            this.Text = "Draw";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.diff_limit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inegr_limit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prop_limit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diff_gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integr_gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prop_gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale_of_line)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaling_line)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.joysticSense)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel CanvasPanel;
        private System.Windows.Forms.Button drawingSwitcherButton;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button forwardOffsetButton;
        private System.Windows.Forms.Button leftOffsetButton;
        private System.Windows.Forms.Button backOffsetButton;
        private System.Windows.Forms.Button rightOffsetButton;
        private System.Windows.Forms.Button resetOffsetButton;
        private System.Windows.Forms.Button upThrustButton;
        private System.Windows.Forms.Button offThrustButton;
        private System.Windows.Forms.Button downThrustButton;
        private System.Windows.Forms.Button applyMaskButton;
        private System.Windows.Forms.TextBox comPortNameBox;
        private System.Windows.Forms.Button connectToComPortButton;
        private System.Windows.Forms.TextBox calculationTime;
        private System.Windows.Forms.Button flRotorSwitcherButton;
        private System.Windows.Forms.Button blRotorSwitcherButton;
        private System.Windows.Forms.Button frRotorSwitcherButton;
        private System.Windows.Forms.Button brRotorSwitcherButton;
        private System.Windows.Forms.TextBox thrustValueTextbox;
        private System.Windows.Forms.NumericUpDown prop_gain;
        private System.Windows.Forms.NumericUpDown diff_limit;
        private System.Windows.Forms.NumericUpDown inegr_limit;
        private System.Windows.Forms.NumericUpDown prop_limit;
        private System.Windows.Forms.NumericUpDown diff_gain;
        private System.Windows.Forms.NumericUpDown integr_gain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown scale_of_line;
        private System.Windows.Forms.NumericUpDown scaling_line;
        private System.Windows.Forms.NumericUpDown joysticSense;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox PID_list;
        private System.Windows.Forms.ComboBox flightModeButton;
        private System.Windows.Forms.TextBox battetyVoltage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox gpsAccuracy;
        private System.Windows.Forms.TextBox sattelitesUsed;
        private System.Windows.Forms.TextBox altitudeMetod;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer timer1;
    }
}

