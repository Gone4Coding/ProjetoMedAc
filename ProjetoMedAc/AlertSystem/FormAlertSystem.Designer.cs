﻿namespace AlertSystem
{
    partial class FormAlertSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlertSystem));
            this.tabControlRecors = new System.Windows.Forms.TabControl();
            this.tabPagePatients = new System.Windows.Forms.TabPage();
            this.toolStripPatients = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.groupBoxPatients = new System.Windows.Forms.GroupBox();
            this.dataGridViewPatients = new System.Windows.Forms.DataGridView();
            this.groupBoxPatient = new System.Windows.Forms.GroupBox();
            this.bt_save = new System.Windows.Forms.Button();
            this.bt_cancelEdit = new System.Windows.Forms.Button();
            this.bt_cancelAdd = new System.Windows.Forms.Button();
            this.bt_edit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelInfoPatient = new System.Windows.Forms.Panel();
            this.richTextBoxAlergies = new System.Windows.Forms.RichTextBox();
            this.richTextBox_address = new System.Windows.Forms.RichTextBox();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_weight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_birthdate = new System.Windows.Forms.DateTimePicker();
            this.tb_height = new System.Windows.Forms.TextBox();
            this.tb_emergencyContactName = new System.Windows.Forms.TextBox();
            this.tb_emergencyContact = new System.Windows.Forms.TextBox();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.tb_phone = new System.Windows.Forms.TextBox();
            this.tb_sns = new System.Windows.Forms.TextBox();
            this.tb_nif = new System.Windows.Forms.TextBox();
            this.tb_lastName = new System.Windows.Forms.TextBox();
            this.tb_firstname = new System.Windows.Forms.TextBox();
            this.tabPageManageRecords = new System.Windows.Forms.TabPage();
            this.tabPage_viewRecords = new System.Windows.Forms.TabPage();
            this.tabControlRecors.SuspendLayout();
            this.tabPagePatients.SuspendLayout();
            this.toolStripPatients.SuspendLayout();
            this.groupBoxPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).BeginInit();
            this.groupBoxPatient.SuspendLayout();
            this.panelInfoPatient.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlRecors
            // 
            this.tabControlRecors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlRecors.Controls.Add(this.tabPagePatients);
            this.tabControlRecors.Controls.Add(this.tabPageManageRecords);
            this.tabControlRecors.Controls.Add(this.tabPage_viewRecords);
            this.tabControlRecors.Location = new System.Drawing.Point(0, 0);
            this.tabControlRecors.Name = "tabControlRecors";
            this.tabControlRecors.SelectedIndex = 0;
            this.tabControlRecors.Size = new System.Drawing.Size(1327, 864);
            this.tabControlRecors.TabIndex = 0;
            this.tabControlRecors.SelectedIndexChanged += new System.EventHandler(this.tabControlRecors_SelectedIndexChanged);
            // 
            // tabPagePatients
            // 
            this.tabPagePatients.Controls.Add(this.toolStripPatients);
            this.tabPagePatients.Controls.Add(this.groupBoxPatients);
            this.tabPagePatients.Controls.Add(this.groupBoxPatient);
            this.tabPagePatients.Location = new System.Drawing.Point(4, 22);
            this.tabPagePatients.Name = "tabPagePatients";
            this.tabPagePatients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePatients.Size = new System.Drawing.Size(1319, 838);
            this.tabPagePatients.TabIndex = 2;
            this.tabPagePatients.Text = "Patients";
            this.tabPagePatients.UseVisualStyleBackColor = true;
            // 
            // toolStripPatients
            // 
            this.toolStripPatients.AutoSize = false;
            this.toolStripPatients.BackColor = System.Drawing.Color.Silver;
            this.toolStripPatients.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripPatients.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripSeparator1,
            this.toolStripButtonRefresh,
            this.toolStripSeparator2,
            this.toolStripTextBox,
            this.toolStripComboBox,
            this.toolStripButton2});
            this.toolStripPatients.Location = new System.Drawing.Point(3, 3);
            this.toolStripPatients.Name = "toolStripPatients";
            this.toolStripPatients.Size = new System.Drawing.Size(1313, 40);
            this.toolStripPatients.TabIndex = 3;
            this.toolStripPatients.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripButtonAdd.Size = new System.Drawing.Size(30, 37);
            this.toolStripButtonAdd.Text = "Add New Patient";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(30, 37);
            this.toolStripButtonRefresh.Text = "Refresh Patient List";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripTextBox
            // 
            this.toolStripTextBox.Name = "toolStripTextBox";
            this.toolStripTextBox.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripTextBox.Size = new System.Drawing.Size(170, 40);
            // 
            // toolStripComboBox
            // 
            this.toolStripComboBox.AutoSize = false;
            this.toolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox.Name = "toolStripComboBox";
            this.toolStripComboBox.Size = new System.Drawing.Size(70, 23);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 37);
            this.toolStripButton2.Text = "Search Patient";
            // 
            // groupBoxPatients
            // 
            this.groupBoxPatients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxPatients.AutoSize = true;
            this.groupBoxPatients.Controls.Add(this.dataGridViewPatients);
            this.groupBoxPatients.Location = new System.Drawing.Point(8, 43);
            this.groupBoxPatients.Name = "groupBoxPatients";
            this.groupBoxPatients.Size = new System.Drawing.Size(500, 779);
            this.groupBoxPatients.TabIndex = 2;
            this.groupBoxPatients.TabStop = false;
            this.groupBoxPatients.Text = "Patients";
            // 
            // dataGridViewPatients
            // 
            this.dataGridViewPatients.AllowUserToAddRows = false;
            this.dataGridViewPatients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridViewPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPatients.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPatients.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewPatients.Location = new System.Drawing.Point(27, 36);
            this.dataGridViewPatients.MultiSelect = false;
            this.dataGridViewPatients.Name = "dataGridViewPatients";
            this.dataGridViewPatients.ReadOnly = true;
            this.dataGridViewPatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPatients.Size = new System.Drawing.Size(440, 718);
            this.dataGridViewPatients.TabIndex = 2;
            this.dataGridViewPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPatients_CellClick);
           
            // 
            // groupBoxPatient
            // 
            this.groupBoxPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPatient.Controls.Add(this.bt_save);
            this.groupBoxPatient.Controls.Add(this.bt_cancelEdit);
            this.groupBoxPatient.Controls.Add(this.bt_cancelAdd);
            this.groupBoxPatient.Controls.Add(this.bt_edit);
            this.groupBoxPatient.Controls.Add(this.buttonAdd);
            this.groupBoxPatient.Controls.Add(this.panelInfoPatient);
            this.groupBoxPatient.Location = new System.Drawing.Point(512, 43);
            this.groupBoxPatient.Name = "groupBoxPatient";
            this.groupBoxPatient.Size = new System.Drawing.Size(781, 779);
            this.groupBoxPatient.TabIndex = 1;
            this.groupBoxPatient.TabStop = false;
            this.groupBoxPatient.Text = "Patient Info";
            // 
            // bt_save
            // 
            this.bt_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_save.Image = ((System.Drawing.Image)(resources.GetObject("bt_save.Image")));
            this.bt_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_save.Location = new System.Drawing.Point(221, 649);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(114, 105);
            this.bt_save.TabIndex = 6;
            this.bt_save.Text = "Save";
            this.bt_save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_cancelEdit
            // 
            this.bt_cancelEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_cancelEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_cancelEdit.Image = ((System.Drawing.Image)(resources.GetObject("bt_cancelEdit.Image")));
            this.bt_cancelEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_cancelEdit.Location = new System.Drawing.Point(553, 649);
            this.bt_cancelEdit.Name = "bt_cancelEdit";
            this.bt_cancelEdit.Size = new System.Drawing.Size(117, 105);
            this.bt_cancelEdit.TabIndex = 5;
            this.bt_cancelEdit.Text = "Cancel";
            this.bt_cancelEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_cancelEdit.UseVisualStyleBackColor = true;
            this.bt_cancelEdit.Click += new System.EventHandler(this.bt_cancelEdit_Click);
            // 
            // bt_cancelAdd
            // 
            this.bt_cancelAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_cancelAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_cancelAdd.Image = ((System.Drawing.Image)(resources.GetObject("bt_cancelAdd.Image")));
            this.bt_cancelAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_cancelAdd.Location = new System.Drawing.Point(583, 649);
            this.bt_cancelAdd.Name = "bt_cancelAdd";
            this.bt_cancelAdd.Size = new System.Drawing.Size(117, 105);
            this.bt_cancelAdd.TabIndex = 4;
            this.bt_cancelAdd.Text = "Cancel";
            this.bt_cancelAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_cancelAdd.UseVisualStyleBackColor = true;
            this.bt_cancelAdd.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // bt_edit
            // 
            this.bt_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_edit.Image = ((System.Drawing.Image)(resources.GetObject("bt_edit.Image")));
            this.bt_edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_edit.Location = new System.Drawing.Point(84, 649);
            this.bt_edit.Name = "bt_edit";
            this.bt_edit.Size = new System.Drawing.Size(114, 105);
            this.bt_edit.TabIndex = 3;
            this.bt_edit.Text = "Edit ";
            this.bt_edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_edit.UseVisualStyleBackColor = true;
            this.bt_edit.Click += new System.EventHandler(this.bt_edit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(83, 649);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(183, 105);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add New Patient";
            this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // panelInfoPatient
            // 
            this.panelInfoPatient.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelInfoPatient.Controls.Add(this.richTextBoxAlergies);
            this.panelInfoPatient.Controls.Add(this.richTextBox_address);
            this.panelInfoPatient.Controls.Add(this.comboBoxGender);
            this.panelInfoPatient.Controls.Add(this.label14);
            this.panelInfoPatient.Controls.Add(this.tb_weight);
            this.panelInfoPatient.Controls.Add(this.label13);
            this.panelInfoPatient.Controls.Add(this.label12);
            this.panelInfoPatient.Controls.Add(this.label11);
            this.panelInfoPatient.Controls.Add(this.label10);
            this.panelInfoPatient.Controls.Add(this.label9);
            this.panelInfoPatient.Controls.Add(this.label8);
            this.panelInfoPatient.Controls.Add(this.label7);
            this.panelInfoPatient.Controls.Add(this.label6);
            this.panelInfoPatient.Controls.Add(this.label5);
            this.panelInfoPatient.Controls.Add(this.label4);
            this.panelInfoPatient.Controls.Add(this.label3);
            this.panelInfoPatient.Controls.Add(this.label2);
            this.panelInfoPatient.Controls.Add(this.label1);
            this.panelInfoPatient.Controls.Add(this.dateTimePicker_birthdate);
            this.panelInfoPatient.Controls.Add(this.tb_height);
            this.panelInfoPatient.Controls.Add(this.tb_emergencyContactName);
            this.panelInfoPatient.Controls.Add(this.tb_emergencyContact);
            this.panelInfoPatient.Controls.Add(this.tb_email);
            this.panelInfoPatient.Controls.Add(this.tb_phone);
            this.panelInfoPatient.Controls.Add(this.tb_sns);
            this.panelInfoPatient.Controls.Add(this.tb_nif);
            this.panelInfoPatient.Controls.Add(this.tb_lastName);
            this.panelInfoPatient.Controls.Add(this.tb_firstname);
            this.panelInfoPatient.Location = new System.Drawing.Point(83, 36);
            this.panelInfoPatient.Name = "panelInfoPatient";
            this.panelInfoPatient.Size = new System.Drawing.Size(617, 598);
            this.panelInfoPatient.TabIndex = 1;
            // 
            // richTextBoxAlergies
            // 
            this.richTextBoxAlergies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAlergies.Location = new System.Drawing.Point(286, 474);
            this.richTextBoxAlergies.Name = "richTextBoxAlergies";
            this.richTextBoxAlergies.Size = new System.Drawing.Size(301, 87);
            this.richTextBoxAlergies.TabIndex = 59;
            this.richTextBoxAlergies.Text = "";
            // 
            // richTextBox_address
            // 
            this.richTextBox_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_address.Location = new System.Drawing.Point(286, 304);
            this.richTextBox_address.Name = "richTextBox_address";
            this.richTextBox_address.Size = new System.Drawing.Size(301, 66);
            this.richTextBox_address.TabIndex = 58;
            this.richTextBox_address.Text = "";
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Location = new System.Drawing.Point(286, 376);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(120, 28);
            this.comboBoxGender.TabIndex = 57;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(33, 446);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 22);
            this.label14.TabIndex = 56;
            this.label14.Text = "Weight";
            // 
            // tb_weight
            // 
            this.tb_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_weight.Location = new System.Drawing.Point(286, 442);
            this.tb_weight.Name = "tb_weight";
            this.tb_weight.Size = new System.Drawing.Size(120, 26);
            this.tb_weight.TabIndex = 55;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(33, 414);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 22);
            this.label13.TabIndex = 54;
            this.label13.Text = "Height";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(33, 477);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 22);
            this.label12.TabIndex = 53;
            this.label12.Text = "Alergies";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(33, 382);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 22);
            this.label11.TabIndex = 52;
            this.label11.Text = "Gender";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(33, 307);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 22);
            this.label10.TabIndex = 51;
            this.label10.Text = "Address";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(33, 276);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(219, 22);
            this.label9.TabIndex = 50;
            this.label9.Text = "Emergency Contact Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(33, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 22);
            this.label8.TabIndex = 49;
            this.label8.Text = "Emergency Contact";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 22);
            this.label7.TabIndex = 48;
            this.label7.Text = "Email";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 22);
            this.label6.TabIndex = 47;
            this.label6.Text = "Phone Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 22);
            this.label5.TabIndex = 46;
            this.label5.Text = "SNS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 22);
            this.label4.TabIndex = 45;
            this.label4.Text = "NIF";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 22);
            this.label3.TabIndex = 44;
            this.label3.Text = "Birthdate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 22);
            this.label2.TabIndex = 43;
            this.label2.Text = "Last Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 22);
            this.label1.TabIndex = 42;
            this.label1.Text = "First Name";
            // 
            // dateTimePicker_birthdate
            // 
            this.dateTimePicker_birthdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_birthdate.Location = new System.Drawing.Point(286, 77);
            this.dateTimePicker_birthdate.Name = "dateTimePicker_birthdate";
            this.dateTimePicker_birthdate.Size = new System.Drawing.Size(301, 26);
            this.dateTimePicker_birthdate.TabIndex = 41;
            // 
            // tb_height
            // 
            this.tb_height.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_height.Location = new System.Drawing.Point(286, 410);
            this.tb_height.Name = "tb_height";
            this.tb_height.Size = new System.Drawing.Size(120, 26);
            this.tb_height.TabIndex = 40;
            // 
            // tb_emergencyContactName
            // 
            this.tb_emergencyContactName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_emergencyContactName.Location = new System.Drawing.Point(286, 272);
            this.tb_emergencyContactName.Name = "tb_emergencyContactName";
            this.tb_emergencyContactName.Size = new System.Drawing.Size(301, 26);
            this.tb_emergencyContactName.TabIndex = 39;
            // 
            // tb_emergencyContact
            // 
            this.tb_emergencyContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_emergencyContact.Location = new System.Drawing.Point(286, 240);
            this.tb_emergencyContact.Name = "tb_emergencyContact";
            this.tb_emergencyContact.Size = new System.Drawing.Size(301, 26);
            this.tb_emergencyContact.TabIndex = 38;
            // 
            // tb_email
            // 
            this.tb_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_email.Location = new System.Drawing.Point(286, 208);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(301, 26);
            this.tb_email.TabIndex = 37;
            // 
            // tb_phone
            // 
            this.tb_phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_phone.Location = new System.Drawing.Point(286, 173);
            this.tb_phone.Name = "tb_phone";
            this.tb_phone.Size = new System.Drawing.Size(301, 26);
            this.tb_phone.TabIndex = 36;
            // 
            // tb_sns
            // 
            this.tb_sns.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_sns.Location = new System.Drawing.Point(286, 141);
            this.tb_sns.Name = "tb_sns";
            this.tb_sns.Size = new System.Drawing.Size(301, 26);
            this.tb_sns.TabIndex = 35;
            // 
            // tb_nif
            // 
            this.tb_nif.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_nif.Location = new System.Drawing.Point(286, 109);
            this.tb_nif.Name = "tb_nif";
            this.tb_nif.Size = new System.Drawing.Size(301, 26);
            this.tb_nif.TabIndex = 34;
            // 
            // tb_lastName
            // 
            this.tb_lastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_lastName.Location = new System.Drawing.Point(286, 45);
            this.tb_lastName.Name = "tb_lastName";
            this.tb_lastName.Size = new System.Drawing.Size(301, 26);
            this.tb_lastName.TabIndex = 33;
            // 
            // tb_firstname
            // 
            this.tb_firstname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_firstname.Location = new System.Drawing.Point(286, 13);
            this.tb_firstname.Name = "tb_firstname";
            this.tb_firstname.Size = new System.Drawing.Size(301, 26);
            this.tb_firstname.TabIndex = 32;
            // 
            // tabPageManageRecords
            // 
            this.tabPageManageRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPageManageRecords.Name = "tabPageManageRecords";
            this.tabPageManageRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManageRecords.Size = new System.Drawing.Size(1319, 838);
            this.tabPageManageRecords.TabIndex = 3;
            this.tabPageManageRecords.Text = "Manage Records";
            this.tabPageManageRecords.UseVisualStyleBackColor = true;
            // 
            // tabPage_viewRecords
            // 
            this.tabPage_viewRecords.Location = new System.Drawing.Point(4, 22);
            this.tabPage_viewRecords.Name = "tabPage_viewRecords";
            this.tabPage_viewRecords.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_viewRecords.Size = new System.Drawing.Size(1319, 838);
            this.tabPage_viewRecords.TabIndex = 4;
            this.tabPage_viewRecords.Text = "View Records";
            this.tabPage_viewRecords.UseVisualStyleBackColor = true;
            // 
            // FormAlertSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 865);
            this.Controls.Add(this.tabControlRecors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormAlertSystem";
            this.Text = "Alert System";
            this.Load += new System.EventHandler(this.FormAlertSystem_Load);
            this.tabControlRecors.ResumeLayout(false);
            this.tabPagePatients.ResumeLayout(false);
            this.tabPagePatients.PerformLayout();
            this.toolStripPatients.ResumeLayout(false);
            this.toolStripPatients.PerformLayout();
            this.groupBoxPatients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).EndInit();
            this.groupBoxPatient.ResumeLayout(false);
            this.panelInfoPatient.ResumeLayout(false);
            this.panelInfoPatient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlRecors;
        private System.Windows.Forms.TabPage tabPagePatients;
        private System.Windows.Forms.GroupBox groupBoxPatient;
        private System.Windows.Forms.TabPage tabPageManageRecords;
        private System.Windows.Forms.TabPage tabPage_viewRecords;
        private System.Windows.Forms.GroupBox groupBoxPatients;
        private System.Windows.Forms.DataGridView dataGridViewPatients;
        private System.Windows.Forms.Panel panelInfoPatient;
        private System.Windows.Forms.RichTextBox richTextBoxAlergies;
        private System.Windows.Forms.RichTextBox richTextBox_address;
        private System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_weight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_birthdate;
        private System.Windows.Forms.TextBox tb_height;
        private System.Windows.Forms.TextBox tb_emergencyContactName;
        private System.Windows.Forms.TextBox tb_emergencyContact;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.TextBox tb_phone;
        private System.Windows.Forms.TextBox tb_sns;
        private System.Windows.Forms.TextBox tb_nif;
        private System.Windows.Forms.TextBox tb_lastName;
        private System.Windows.Forms.TextBox tb_firstname;
        private System.Windows.Forms.ToolStrip toolStripPatients;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox;
        private System.Windows.Forms.Button bt_edit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button bt_cancelAdd;
        private System.Windows.Forms.Button bt_cancelEdit;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox;
    }
}

