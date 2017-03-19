namespace MyHealth
{
    partial class MyHealth
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_patientId = new System.Windows.Forms.Label();
            this.tb_patientId = new System.Windows.Forms.TextBox();
            this.bt_Insert = new System.Windows.Forms.Button();
            this.cb_bloodPressure = new System.Windows.Forms.CheckBox();
            this.cb_saturations = new System.Windows.Forms.CheckBox();
            this.cb_heartRate = new System.Windows.Forms.CheckBox();
            this.lb_dateBP = new System.Windows.Forms.Label();
            this.lb_date_o2 = new System.Windows.Forms.Label();
            this.lb_date_HR = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mainTabing = new System.Windows.Forms.TabControl();
            this.home = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gb_physiologicDataNormal = new System.Windows.Forms.GroupBox();
            this.lb_dataHR = new System.Windows.Forms.Label();
            this.lb_dataBP = new System.Windows.Forms.Label();
            this.lb_dataSPO2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.personalData = new System.Windows.Forms.TabPage();
            this.medline = new System.Windows.Forms.TabPage();
            this.configurations = new System.Windows.Forms.TabPage();
            this.cb_medlineURL = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gb_voice = new System.Windows.Forms.GroupBox();
            this.numberRating = new System.Windows.Forms.NumericUpDown();
            this.lb_rate = new System.Windows.Forms.Label();
            this.rb_female = new System.Windows.Forms.RadioButton();
            this.rb_male = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.bt_saveURL = new System.Windows.Forms.Button();
            this.mainTabing.SuspendLayout();
            this.home.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gb_physiologicDataNormal.SuspendLayout();
            this.configurations.SuspendLayout();
            this.cb_medlineURL.SuspendLayout();
            this.gb_voice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberRating)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_patientId
            // 
            this.lb_patientId.AutoSize = true;
            this.lb_patientId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lb_patientId.Location = new System.Drawing.Point(11, 15);
            this.lb_patientId.Name = "lb_patientId";
            this.lb_patientId.Size = new System.Drawing.Size(47, 22);
            this.lb_patientId.TabIndex = 0;
            this.lb_patientId.Text = "SNS";
            // 
            // tb_patientId
            // 
            this.tb_patientId.Location = new System.Drawing.Point(64, 12);
            this.tb_patientId.Name = "tb_patientId";
            this.tb_patientId.Size = new System.Drawing.Size(128, 27);
            this.tb_patientId.TabIndex = 1;
            // 
            // bt_Insert
            // 
            this.bt_Insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.bt_Insert.Location = new System.Drawing.Point(64, 45);
            this.bt_Insert.Name = "bt_Insert";
            this.bt_Insert.Size = new System.Drawing.Size(91, 34);
            this.bt_Insert.TabIndex = 2;
            this.bt_Insert.Text = "Insert";
            this.bt_Insert.UseVisualStyleBackColor = true;
            this.bt_Insert.Click += new System.EventHandler(this.bt_insert_Click);
            // 
            // cb_bloodPressure
            // 
            this.cb_bloodPressure.AutoSize = true;
            this.cb_bloodPressure.Location = new System.Drawing.Point(22, 39);
            this.cb_bloodPressure.Name = "cb_bloodPressure";
            this.cb_bloodPressure.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_bloodPressure.Size = new System.Drawing.Size(193, 26);
            this.cb_bloodPressure.TabIndex = 4;
            this.cb_bloodPressure.Text = "(BP) Blood Pressure";
            this.cb_bloodPressure.UseVisualStyleBackColor = true;
            this.cb_bloodPressure.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_bloodPressure_MouseClick);
            // 
            // cb_saturations
            // 
            this.cb_saturations.AutoSize = true;
            this.cb_saturations.Location = new System.Drawing.Point(234, 39);
            this.cb_saturations.Name = "cb_saturations";
            this.cb_saturations.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_saturations.Size = new System.Drawing.Size(205, 26);
            this.cb_saturations.TabIndex = 4;
            this.cb_saturations.Text = "(SPO2) O2 Saturation";
            this.cb_saturations.UseVisualStyleBackColor = true;
            this.cb_saturations.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_saturations_MouseClick);
            // 
            // cb_heartRate
            // 
            this.cb_heartRate.AutoSize = true;
            this.cb_heartRate.Location = new System.Drawing.Point(464, 39);
            this.cb_heartRate.Name = "cb_heartRate";
            this.cb_heartRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_heartRate.Size = new System.Drawing.Size(159, 26);
            this.cb_heartRate.TabIndex = 4;
            this.cb_heartRate.Text = "(HR) Heart Rate";
            this.cb_heartRate.UseVisualStyleBackColor = true;
            this.cb_heartRate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_heartRate_MouseClick);
            // 
            // lb_dateBP
            // 
            this.lb_dateBP.AutoSize = true;
            this.lb_dateBP.Location = new System.Drawing.Point(494, 27);
            this.lb_dateBP.Name = "lb_dateBP";
            this.lb_dateBP.Size = new System.Drawing.Size(82, 22);
            this.lb_dateBP.TabIndex = 6;
            this.lb_dateBP.Text = "Date_BP";
            // 
            // lb_date_o2
            // 
            this.lb_date_o2.AutoSize = true;
            this.lb_date_o2.Location = new System.Drawing.Point(494, 88);
            this.lb_date_o2.Name = "lb_date_o2";
            this.lb_date_o2.Size = new System.Drawing.Size(105, 22);
            this.lb_date_o2.TabIndex = 6;
            this.lb_date_o2.Text = "Date_Sat02";
            // 
            // lb_date_HR
            // 
            this.lb_date_HR.AutoSize = true;
            this.lb_date_HR.Location = new System.Drawing.Point(494, 152);
            this.lb_date_HR.Name = "lb_date_HR";
            this.lb_date_HR.Size = new System.Drawing.Size(84, 22);
            this.lb_date_HR.TabIndex = 6;
            this.lb_date_HR.Text = "Date_HR";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 452);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Speech";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // mainTabing
            // 
            this.mainTabing.Controls.Add(this.home);
            this.mainTabing.Controls.Add(this.personalData);
            this.mainTabing.Controls.Add(this.medline);
            this.mainTabing.Controls.Add(this.configurations);
            this.mainTabing.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.mainTabing.Location = new System.Drawing.Point(12, 12);
            this.mainTabing.Name = "mainTabing";
            this.mainTabing.SelectedIndex = 0;
            this.mainTabing.Size = new System.Drawing.Size(661, 438);
            this.mainTabing.TabIndex = 8;
            // 
            // home
            // 
            this.home.Controls.Add(this.label1);
            this.home.Controls.Add(this.groupBox1);
            this.home.Controls.Add(this.gb_physiologicDataNormal);
            this.home.Controls.Add(this.bt_Insert);
            this.home.Controls.Add(this.tb_patientId);
            this.home.Controls.Add(this.lb_patientId);
            this.home.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.home.Location = new System.Drawing.Point(4, 29);
            this.home.Name = "home";
            this.home.Padding = new System.Windows.Forms.Padding(3);
            this.home.Size = new System.Drawing.Size(653, 405);
            this.home.TabIndex = 0;
            this.home.Text = "Home";
            this.home.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "UserName";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_bloodPressure);
            this.groupBox1.Controls.Add(this.cb_saturations);
            this.groupBox1.Controls.Add(this.cb_heartRate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.groupBox1.Location = new System.Drawing.Point(7, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 91);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Monitoring Parameters";
            // 
            // gb_physiologicDataNormal
            // 
            this.gb_physiologicDataNormal.Controls.Add(this.lb_dataHR);
            this.gb_physiologicDataNormal.Controls.Add(this.lb_dataBP);
            this.gb_physiologicDataNormal.Controls.Add(this.lb_dataSPO2);
            this.gb_physiologicDataNormal.Controls.Add(this.button2);
            this.gb_physiologicDataNormal.Controls.Add(this.button1);
            this.gb_physiologicDataNormal.Controls.Add(this.label4);
            this.gb_physiologicDataNormal.Controls.Add(this.label3);
            this.gb_physiologicDataNormal.Controls.Add(this.label2);
            this.gb_physiologicDataNormal.Controls.Add(this.lb_date_o2);
            this.gb_physiologicDataNormal.Controls.Add(this.lb_date_HR);
            this.gb_physiologicDataNormal.Controls.Add(this.lb_dateBP);
            this.gb_physiologicDataNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.gb_physiologicDataNormal.Location = new System.Drawing.Point(6, 189);
            this.gb_physiologicDataNormal.Name = "gb_physiologicDataNormal";
            this.gb_physiologicDataNormal.Size = new System.Drawing.Size(641, 210);
            this.gb_physiologicDataNormal.TabIndex = 7;
            this.gb_physiologicDataNormal.TabStop = false;
            this.gb_physiologicDataNormal.Text = "Physiologic Data";
            // 
            // lb_dataHR
            // 
            this.lb_dataHR.AutoSize = true;
            this.lb_dataHR.Location = new System.Drawing.Point(298, 152);
            this.lb_dataHR.Name = "lb_dataHR";
            this.lb_dataHR.Size = new System.Drawing.Size(74, 22);
            this.lb_dataHR.TabIndex = 9;
            this.lb_dataHR.Text = "DataHR";
            // 
            // lb_dataBP
            // 
            this.lb_dataBP.AutoSize = true;
            this.lb_dataBP.Location = new System.Drawing.Point(298, 27);
            this.lb_dataBP.Name = "lb_dataBP";
            this.lb_dataBP.Size = new System.Drawing.Size(72, 22);
            this.lb_dataBP.TabIndex = 9;
            this.lb_dataBP.Text = "DataBP";
            // 
            // lb_dataSPO2
            // 
            this.lb_dataSPO2.AutoSize = true;
            this.lb_dataSPO2.Location = new System.Drawing.Point(298, 88);
            this.lb_dataSPO2.Name = "lb_dataSPO2";
            this.lb_dataSPO2.Size = new System.Drawing.Size(96, 22);
            this.lb_dataSPO2.TabIndex = 9;
            this.lb_dataSPO2.Text = "DataSPO2";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(6, 85);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 52);
            this.button2.TabIndex = 8;
            this.button2.Text = "Simple View";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 52);
            this.button1.TabIndex = 8;
            this.button1.Text = "Simple View";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "HR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "SPO2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "BP";
            // 
            // personalData
            // 
            this.personalData.Location = new System.Drawing.Point(4, 29);
            this.personalData.Name = "personalData";
            this.personalData.Padding = new System.Windows.Forms.Padding(3);
            this.personalData.Size = new System.Drawing.Size(653, 405);
            this.personalData.TabIndex = 1;
            this.personalData.Text = "User Information";
            this.personalData.UseVisualStyleBackColor = true;
            // 
            // medline
            // 
            this.medline.Location = new System.Drawing.Point(4, 29);
            this.medline.Name = "medline";
            this.medline.Size = new System.Drawing.Size(653, 405);
            this.medline.TabIndex = 2;
            this.medline.Text = "MedLine";
            this.medline.UseVisualStyleBackColor = true;
            // 
            // configurations
            // 
            this.configurations.Controls.Add(this.cb_medlineURL);
            this.configurations.Controls.Add(this.gb_voice);
            this.configurations.Location = new System.Drawing.Point(4, 29);
            this.configurations.Name = "configurations";
            this.configurations.Padding = new System.Windows.Forms.Padding(3);
            this.configurations.Size = new System.Drawing.Size(653, 405);
            this.configurations.TabIndex = 3;
            this.configurations.Text = "Configurations";
            this.configurations.UseVisualStyleBackColor = true;
            // 
            // cb_medlineURL
            // 
            this.cb_medlineURL.Controls.Add(this.bt_saveURL);
            this.cb_medlineURL.Controls.Add(this.label5);
            this.cb_medlineURL.Controls.Add(this.textBox1);
            this.cb_medlineURL.Location = new System.Drawing.Point(267, 16);
            this.cb_medlineURL.Name = "cb_medlineURL";
            this.cb_medlineURL.Size = new System.Drawing.Size(366, 134);
            this.cb_medlineURL.TabIndex = 1;
            this.cb_medlineURL.TabStop = false;
            this.cb_medlineURL.Text = "URL MedLine";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(58, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(302, 27);
            this.textBox1.TabIndex = 0;
            // 
            // gb_voice
            // 
            this.gb_voice.Controls.Add(this.numberRating);
            this.gb_voice.Controls.Add(this.lb_rate);
            this.gb_voice.Controls.Add(this.rb_female);
            this.gb_voice.Controls.Add(this.rb_male);
            this.gb_voice.Location = new System.Drawing.Point(21, 16);
            this.gb_voice.Name = "gb_voice";
            this.gb_voice.Size = new System.Drawing.Size(228, 134);
            this.gb_voice.TabIndex = 0;
            this.gb_voice.TabStop = false;
            this.gb_voice.Text = "Voice ";
            // 
            // numberRating
            // 
            this.numberRating.Location = new System.Drawing.Point(113, 77);
            this.numberRating.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numberRating.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numberRating.Name = "numberRating";
            this.numberRating.Size = new System.Drawing.Size(50, 27);
            this.numberRating.TabIndex = 2;
            // 
            // lb_rate
            // 
            this.lb_rate.AutoSize = true;
            this.lb_rate.Location = new System.Drawing.Point(59, 79);
            this.lb_rate.Name = "lb_rate";
            this.lb_rate.Size = new System.Drawing.Size(48, 22);
            this.lb_rate.TabIndex = 1;
            this.lb_rate.Text = "Rate";
            // 
            // rb_female
            // 
            this.rb_female.AutoSize = true;
            this.rb_female.Location = new System.Drawing.Point(113, 35);
            this.rb_female.Name = "rb_female";
            this.rb_female.Size = new System.Drawing.Size(87, 26);
            this.rb_female.TabIndex = 0;
            this.rb_female.TabStop = true;
            this.rb_female.Text = "Female";
            this.rb_female.UseVisualStyleBackColor = true;
            // 
            // rb_male
            // 
            this.rb_male.AutoSize = true;
            this.rb_male.Location = new System.Drawing.Point(27, 35);
            this.rb_male.Name = "rb_male";
            this.rb_male.Size = new System.Drawing.Size(66, 26);
            this.rb_male.TabIndex = 0;
            this.rb_male.TabStop = true;
            this.rb_male.Text = "Male";
            this.rb_male.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 22);
            this.label5.TabIndex = 1;
            this.label5.Text = "URL";
            // 
            // bt_saveURL
            // 
            this.bt_saveURL.Location = new System.Drawing.Point(267, 93);
            this.bt_saveURL.Name = "bt_saveURL";
            this.bt_saveURL.Size = new System.Drawing.Size(93, 35);
            this.bt_saveURL.TabIndex = 2;
            this.bt_saveURL.Text = "Save";
            this.bt_saveURL.UseVisualStyleBackColor = true;
            // 
            // MyHealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 476);
            this.Controls.Add(this.mainTabing);
            this.Controls.Add(this.checkBox1);
            this.Name = "MyHealth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyHealth";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyHealth_FormClosing);
            this.Load += new System.EventHandler(this.MyHealth_Load);
            this.mainTabing.ResumeLayout(false);
            this.home.ResumeLayout(false);
            this.home.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gb_physiologicDataNormal.ResumeLayout(false);
            this.gb_physiologicDataNormal.PerformLayout();
            this.configurations.ResumeLayout(false);
            this.cb_medlineURL.ResumeLayout(false);
            this.cb_medlineURL.PerformLayout();
            this.gb_voice.ResumeLayout(false);
            this.gb_voice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberRating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_patientId;
        private System.Windows.Forms.TextBox tb_patientId;
        private System.Windows.Forms.Button bt_Insert;
        private System.Windows.Forms.CheckBox cb_bloodPressure;
        private System.Windows.Forms.CheckBox cb_saturations;
        private System.Windows.Forms.CheckBox cb_heartRate;
        private System.Windows.Forms.Label lb_dateBP;
        private System.Windows.Forms.Label lb_date_o2;
        private System.Windows.Forms.Label lb_date_HR;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabControl mainTabing;
        private System.Windows.Forms.TabPage home;
        private System.Windows.Forms.TabPage personalData;
        private System.Windows.Forms.TabPage medline;
        private System.Windows.Forms.GroupBox gb_physiologicDataNormal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_dataHR;
        private System.Windows.Forms.Label lb_dataBP;
        private System.Windows.Forms.Label lb_dataSPO2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage configurations;
        private System.Windows.Forms.GroupBox gb_voice;
        private System.Windows.Forms.Label lb_rate;
        private System.Windows.Forms.RadioButton rb_female;
        private System.Windows.Forms.RadioButton rb_male;
        private System.Windows.Forms.NumericUpDown numberRating;
        private System.Windows.Forms.GroupBox cb_medlineURL;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bt_saveURL;
        private System.Windows.Forms.Label label5;
    }
}

