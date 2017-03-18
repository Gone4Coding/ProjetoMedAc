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
            this.bt_validate = new System.Windows.Forms.Button();
            this.bt_startMonitoring = new System.Windows.Forms.Button();
            this.cb_bloodPressure = new System.Windows.Forms.CheckBox();
            this.cb_saturations = new System.Windows.Forms.CheckBox();
            this.cb_heartRate = new System.Windows.Forms.CheckBox();
            this.tb_bloodPressure = new System.Windows.Forms.TextBox();
            this.tb_saturation = new System.Windows.Forms.TextBox();
            this.tb_heartRate = new System.Windows.Forms.TextBox();
            this.lb_dataBP = new System.Windows.Forms.Label();
            this.lb_data_o2 = new System.Windows.Forms.Label();
            this.lb_data_HR = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tb_third = new System.Windows.Forms.TextBox();
            this.tb_second = new System.Windows.Forms.TextBox();
            this.tb_first = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lb_patientId
            // 
            this.lb_patientId.AutoSize = true;
            this.lb_patientId.Location = new System.Drawing.Point(13, 23);
            this.lb_patientId.Name = "lb_patientId";
            this.lb_patientId.Size = new System.Drawing.Size(52, 13);
            this.lb_patientId.TabIndex = 0;
            this.lb_patientId.Text = "Patient Id";
            // 
            // tb_patientId
            // 
            this.tb_patientId.Location = new System.Drawing.Point(71, 20);
            this.tb_patientId.Name = "tb_patientId";
            this.tb_patientId.Size = new System.Drawing.Size(128, 20);
            this.tb_patientId.TabIndex = 1;
            // 
            // bt_validate
            // 
            this.bt_validate.Location = new System.Drawing.Point(205, 18);
            this.bt_validate.Name = "bt_validate";
            this.bt_validate.Size = new System.Drawing.Size(75, 23);
            this.bt_validate.TabIndex = 2;
            this.bt_validate.Text = "Validate";
            this.bt_validate.UseVisualStyleBackColor = true;
            this.bt_validate.Click += new System.EventHandler(this.bt_validate_Click);
            // 
            // bt_startMonitoring
            // 
            this.bt_startMonitoring.Location = new System.Drawing.Point(16, 82);
            this.bt_startMonitoring.Name = "bt_startMonitoring";
            this.bt_startMonitoring.Size = new System.Drawing.Size(111, 23);
            this.bt_startMonitoring.TabIndex = 3;
            this.bt_startMonitoring.Text = "Start Monitoring";
            this.bt_startMonitoring.UseVisualStyleBackColor = true;
            this.bt_startMonitoring.Click += new System.EventHandler(this.bt_startMonitoring_Click);
            // 
            // cb_bloodPressure
            // 
            this.cb_bloodPressure.AutoSize = true;
            this.cb_bloodPressure.Location = new System.Drawing.Point(16, 125);
            this.cb_bloodPressure.Name = "cb_bloodPressure";
            this.cb_bloodPressure.Size = new System.Drawing.Size(97, 17);
            this.cb_bloodPressure.TabIndex = 4;
            this.cb_bloodPressure.Text = "Blood Pressure";
            this.cb_bloodPressure.UseVisualStyleBackColor = true;
            this.cb_bloodPressure.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_bloodPressure_MouseClick);
            // 
            // cb_saturations
            // 
            this.cb_saturations.AutoSize = true;
            this.cb_saturations.Location = new System.Drawing.Point(16, 157);
            this.cb_saturations.Name = "cb_saturations";
            this.cb_saturations.Size = new System.Drawing.Size(91, 17);
            this.cb_saturations.TabIndex = 4;
            this.cb_saturations.Text = "O2 Saturation";
            this.cb_saturations.UseVisualStyleBackColor = true;
            this.cb_saturations.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_saturations_MouseClick);
            // 
            // cb_heartRate
            // 
            this.cb_heartRate.AutoSize = true;
            this.cb_heartRate.Location = new System.Drawing.Point(16, 187);
            this.cb_heartRate.Name = "cb_heartRate";
            this.cb_heartRate.Size = new System.Drawing.Size(78, 17);
            this.cb_heartRate.TabIndex = 4;
            this.cb_heartRate.Text = "Heart Rate";
            this.cb_heartRate.UseVisualStyleBackColor = true;
            this.cb_heartRate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_heartRate_MouseClick);
            // 
            // tb_bloodPressure
            // 
            this.tb_bloodPressure.Enabled = false;
            this.tb_bloodPressure.Location = new System.Drawing.Point(120, 125);
            this.tb_bloodPressure.Name = "tb_bloodPressure";
            this.tb_bloodPressure.Size = new System.Drawing.Size(148, 20);
            this.tb_bloodPressure.TabIndex = 5;
            // 
            // tb_saturation
            // 
            this.tb_saturation.Enabled = false;
            this.tb_saturation.Location = new System.Drawing.Point(120, 154);
            this.tb_saturation.Name = "tb_saturation";
            this.tb_saturation.Size = new System.Drawing.Size(148, 20);
            this.tb_saturation.TabIndex = 5;
            // 
            // tb_heartRate
            // 
            this.tb_heartRate.Enabled = false;
            this.tb_heartRate.Location = new System.Drawing.Point(120, 185);
            this.tb_heartRate.Name = "tb_heartRate";
            this.tb_heartRate.Size = new System.Drawing.Size(148, 20);
            this.tb_heartRate.TabIndex = 5;
            // 
            // lb_dataBP
            // 
            this.lb_dataBP.AutoSize = true;
            this.lb_dataBP.Location = new System.Drawing.Point(274, 128);
            this.lb_dataBP.Name = "lb_dataBP";
            this.lb_dataBP.Size = new System.Drawing.Size(50, 13);
            this.lb_dataBP.TabIndex = 6;
            this.lb_dataBP.Text = "Data_BP";
            // 
            // lb_data_o2
            // 
            this.lb_data_o2.AutoSize = true;
            this.lb_data_o2.Location = new System.Drawing.Point(274, 157);
            this.lb_data_o2.Name = "lb_data_o2";
            this.lb_data_o2.Size = new System.Drawing.Size(64, 13);
            this.lb_data_o2.TabIndex = 6;
            this.lb_data_o2.Text = "Data_Sat02";
            // 
            // lb_data_HR
            // 
            this.lb_data_HR.AutoSize = true;
            this.lb_data_HR.Location = new System.Drawing.Point(274, 188);
            this.lb_data_HR.Name = "lb_data_HR";
            this.lb_data_HR.Size = new System.Drawing.Size(52, 13);
            this.lb_data_HR.TabIndex = 6;
            this.lb_data_HR.Text = "Data_HR";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(319, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Speech";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tb_third
            // 
            this.tb_third.Location = new System.Drawing.Point(353, 259);
            this.tb_third.Name = "tb_third";
            this.tb_third.Size = new System.Drawing.Size(20, 20);
            this.tb_third.TabIndex = 8;
            // 
            // tb_second
            // 
            this.tb_second.Location = new System.Drawing.Point(327, 259);
            this.tb_second.Name = "tb_second";
            this.tb_second.Size = new System.Drawing.Size(20, 20);
            this.tb_second.TabIndex = 8;
            // 
            // tb_first
            // 
            this.tb_first.Enabled = false;
            this.tb_first.Location = new System.Drawing.Point(301, 259);
            this.tb_first.Name = "tb_first";
            this.tb_first.Size = new System.Drawing.Size(20, 20);
            this.tb_first.TabIndex = 8;
            // 
            // MyHealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 291);
            this.Controls.Add(this.tb_first);
            this.Controls.Add(this.tb_second);
            this.Controls.Add(this.tb_third);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lb_data_HR);
            this.Controls.Add(this.lb_data_o2);
            this.Controls.Add(this.lb_dataBP);
            this.Controls.Add(this.tb_heartRate);
            this.Controls.Add(this.tb_saturation);
            this.Controls.Add(this.tb_bloodPressure);
            this.Controls.Add(this.cb_heartRate);
            this.Controls.Add(this.cb_saturations);
            this.Controls.Add(this.cb_bloodPressure);
            this.Controls.Add(this.bt_startMonitoring);
            this.Controls.Add(this.bt_validate);
            this.Controls.Add(this.tb_patientId);
            this.Controls.Add(this.lb_patientId);
            this.Name = "MyHealth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyHealth";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyHealth_FormClosing);
            this.Load += new System.EventHandler(this.MyHealth_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_patientId;
        private System.Windows.Forms.TextBox tb_patientId;
        private System.Windows.Forms.Button bt_validate;
        private System.Windows.Forms.Button bt_startMonitoring;
        private System.Windows.Forms.CheckBox cb_bloodPressure;
        private System.Windows.Forms.CheckBox cb_saturations;
        private System.Windows.Forms.CheckBox cb_heartRate;
        private System.Windows.Forms.TextBox tb_bloodPressure;
        private System.Windows.Forms.TextBox tb_saturation;
        private System.Windows.Forms.TextBox tb_heartRate;
        private System.Windows.Forms.Label lb_dataBP;
        private System.Windows.Forms.Label lb_data_o2;
        private System.Windows.Forms.Label lb_data_HR;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox tb_third;
        private System.Windows.Forms.TextBox tb_second;
        private System.Windows.Forms.TextBox tb_first;
    }
}

