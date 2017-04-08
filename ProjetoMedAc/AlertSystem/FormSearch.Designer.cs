namespace AlertSystem
{
    partial class FormSearch
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
            this.dataGridViewActivePatients = new System.Windows.Forms.DataGridView();
            this.groupBoxPatientsList = new System.Windows.Forms.GroupBox();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.comboBoxSearch = new System.Windows.Forms.ComboBox();
            this.buttonokSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActivePatients)).BeginInit();
            this.groupBoxPatientsList.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewActivePatients
            // 
            this.dataGridViewActivePatients.AllowUserToAddRows = false;
            this.dataGridViewActivePatients.AllowUserToDeleteRows = false;
            this.dataGridViewActivePatients.AllowUserToOrderColumns = true;
            this.dataGridViewActivePatients.AllowUserToResizeColumns = false;
            this.dataGridViewActivePatients.AllowUserToResizeRows = false;
            this.dataGridViewActivePatients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewActivePatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewActivePatients.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewActivePatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewActivePatients.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewActivePatients.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewActivePatients.MultiSelect = false;
            this.dataGridViewActivePatients.Name = "dataGridViewActivePatients";
            this.dataGridViewActivePatients.ReadOnly = true;
            this.dataGridViewActivePatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewActivePatients.Size = new System.Drawing.Size(491, 270);
            this.dataGridViewActivePatients.TabIndex = 8;
            this.dataGridViewActivePatients.SelectionChanged += new System.EventHandler(this.dataGridViewActivePatients_SelectionChanged);
            // 
            // groupBoxPatientsList
            // 
            this.groupBoxPatientsList.Controls.Add(this.dataGridViewActivePatients);
            this.groupBoxPatientsList.Location = new System.Drawing.Point(12, 92);
            this.groupBoxPatientsList.Name = "groupBoxPatientsList";
            this.groupBoxPatientsList.Size = new System.Drawing.Size(503, 300);
            this.groupBoxPatientsList.TabIndex = 9;
            this.groupBoxPatientsList.TabStop = false;
            this.groupBoxPatientsList.Text = "Patients active on monitoring";
            // 
            // groupBoxFilters
            // 
            this.groupBoxFilters.Controls.Add(this.buttonokSearch);
            this.groupBoxFilters.Controls.Add(this.comboBoxSearch);
            this.groupBoxFilters.Controls.Add(this.textBoxSearch);
            this.groupBoxFilters.Location = new System.Drawing.Point(12, 4);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(503, 65);
            this.groupBoxFilters.TabIndex = 10;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Filters";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(12, 398);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 29);
            this.buttonOk.TabIndex = 11;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(440, 398);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 29);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(6, 25);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(343, 20);
            this.textBoxSearch.TabIndex = 0;
            // 
            // comboBoxSearch
            // 
            this.comboBoxSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearch.FormattingEnabled = true;
            this.comboBoxSearch.Location = new System.Drawing.Point(355, 25);
            this.comboBoxSearch.Name = "comboBoxSearch";
            this.comboBoxSearch.Size = new System.Drawing.Size(77, 21);
            this.comboBoxSearch.TabIndex = 1;
            // 
            // buttonokSearch
            // 
            this.buttonokSearch.Location = new System.Drawing.Point(438, 25);
            this.buttonokSearch.Name = "buttonokSearch";
            this.buttonokSearch.Size = new System.Drawing.Size(59, 21);
            this.buttonokSearch.TabIndex = 12;
            this.buttonokSearch.Text = "OK";
            this.buttonokSearch.UseVisualStyleBackColor = true;
            this.buttonokSearch.Click += new System.EventHandler(this.buttonokSearch_Click);
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 439);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.groupBoxPatientsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormSearch";
            this.ShowIcon = false;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.FormSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActivePatients)).EndInit();
            this.groupBoxPatientsList.ResumeLayout(false);
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewActivePatients;
        private System.Windows.Forms.GroupBox groupBoxPatientsList;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonokSearch;
        private System.Windows.Forms.ComboBox comboBoxSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
    }
}