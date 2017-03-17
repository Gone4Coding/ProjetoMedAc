﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlertSystem.ServiceReferenceHealth;


namespace AlertSystem
{
    public partial class FormAlertSystem : Form
    {
        private ServiceHealthAlertClient client;
        private bool asc;
        private int patientAge;
        private DateTime dateValue;
        public FormAlertSystem()
        {
            InitializeComponent();
            client = new ServiceHealthAlertClient();
            dateValue = new DateTime(6969, 01, 01);
        }
        #region Eventos
        private void tabControlRecors_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(tabControlRecors.SelectedTab.TabIndex.ToString());
        }
        private void FormAlertSystem_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            #region PatientsPage
            comboBoxGender.Items.Add(" ");
            comboBoxGender.Items.Add("Female");
            comboBoxGender.Items.Add("Male");

            toolStripComboBox.Items.Add("Sns");
            toolStripComboBox.Items.Add("Phone");
            toolStripComboBox.Items.Add("Nome");

            toolStripComboBox.SelectedIndex = 0;

            load(null);

            #endregion
        }
        private void dataGridViewPatients_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int sns = Convert.ToInt32(dataGridViewPatients.Rows[e.RowIndex].Cells["Sns"].Value);
                Patient patientSelected = client.GetPatient(sns);

                fillFields(patientSelected);
            }
            catch (ArgumentOutOfRangeException x)
            {
                Console.WriteLine(x.Message);
            }
        }
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {          
            bt_edit.Hide();
            bt_cancelEdit.Hide();
            bt_save.Hide();
            buttonAdd.Show();
            bt_cancelAdd.Show();
            dataGridViewPatients.ClearSelection();
            dataGridViewPatients.Enabled = false;
            enableTextBoxes(true);
            dateTimePicker_birthdate.Value = dateValue;
            clearFields();
            enableSearch(false);
                 
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            load(null);
            enableSearch(true);
            dateTimePicker_birthdate.Format = DateTimePickerFormat.Short;
        }
        private void dateTimePicker_birthdate_ValueChanged(object sender, EventArgs e)
        {
                dateTimePicker_birthdate.Value = DateTime.Now;
                dateTimePicker_birthdate.Format = DateTimePickerFormat.Short;          
        }
        private void bt_edit_Click(object sender, EventArgs e)
        {
            enableTextBoxes(true);
            bt_save.Enabled = true;
            bt_edit.Enabled = false;
            bt_cancelEdit.Show();
            dataGridViewPatients.ClearSelection();
            dataGridViewPatients.Enabled = false;
            enableSearch(false);
        }
        private void bt_save_Click(object sender, EventArgs e)
        {
            enableTextBoxes(false);
            bt_edit.Enabled = true;
            bt_save.Enabled = false;
            bt_cancelEdit.Hide();
        }
        private void bt_cancelEdit_Click(object sender, EventArgs e)
        {
            enableTextBoxes(false);
            enableSearch(true);
            bt_edit.Enabled = true;
            bt_save.Enabled = false;
            bt_cancelEdit.Hide();

            dataGridViewPatients.ClearSelection();
            dataGridViewPatients.Enabled = true;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (validateFields())
            {
                Patient p = readFields();

                bool res = client.InsertPatient(p);

                if (!res)
                {
                    MessageBox.Show("erro");
                }
                else
                {
                    int rowIndex = 0;

                    MessageBox.Show(p.Name + " " + p.Surname + " sucessfully added!", "SUCESS", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    clearFields();
                    load(p);
                    enableSearch(true);
                }
            }
        }
        private void dataGridViewPatients_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<Patient> patients;

            switch (e.ColumnIndex)
            {
                case 8:
                    if (!asc)
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderBy(i => i.Name));
                        fillGridView(patients);
                        asc = true;
                    }
                    else
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderByDescending(i => i.Name));
                        fillGridView(patients);
                        asc = false;
                    }
                    break;
                case 9:
                    if (!asc)
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderBy(i => i.Nif));
                        fillGridView(patients);
                        asc = true;
                    }
                    else
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderByDescending(i => i.Nif));
                        fillGridView(patients);
                        asc = false;
                    }
                    break;
                case 11:
                    if (!asc)
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderBy(i => i.Sns));
                        fillGridView(patients);
                        asc = true;
                    }
                    else
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderByDescending(i => i.Sns));
                        fillGridView(patients);
                        asc = false;
                    }
                    break;
                case 12:
                    if (!asc)
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderBy(i => i.Surname));
                        fillGridView(patients);
                        asc = true;
                    }
                    else
                    {
                        patients = new List<Patient>(client.GetPatientList().OrderByDescending(i => i.Surname));
                        fillGridView(patients);
                        asc = false;
                    }
                    break;
            }

        }
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            load(null);
        }
        private void dataGridViewPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPatients.Rows.Count > 0 & dataGridViewPatients.CurrentCell!=null)
            {
                int index = dataGridViewPatients.CurrentCell.RowIndex;
                int sns = Convert.ToInt32(dataGridViewPatients.Rows[index].Cells["Sns"].Value);
                Patient patientSelected = client.GetPatient(sns);

                fillFields(patientSelected);
            }
        }
        #endregion

        #region Metodos

        private void load(Patient p)
        {
            int rowIndex = 0;

            bt_edit.Show();
            bt_edit.Enabled = true;
            bt_save.Show();
            bt_save.Enabled = false;
            bt_cancelAdd.Hide();
            bt_cancelEdit.Hide();
            buttonAdd.Hide();
            dataGridViewPatients.Enabled = true;
            List<Patient> patients = new List<Patient>(client.GetPatientList());
            fillGridView(patients);
            if (p == null)
            {
                fillFirstSelected();
            }
            else
            {
                foreach (DataGridViewRow row in dataGridViewPatients.Rows)
                {
                    if (p.Sns == Convert.ToInt32(row.Cells[11].Value))
                    {
                        rowIndex = row.Index;
                    }
                }

                dataGridViewPatients.Rows[rowIndex].Selected = true;

                fillFields(p);
            }

            enableTextBoxes(false);
        }
        private void enableTextBoxes(bool estado)
        {
            tb_firstname.Enabled = estado;
            tb_lastName.Enabled = estado;
            dateTimePicker_birthdate.Enabled = estado;
            tb_nif.Enabled = estado;
            tb_sns.Enabled = estado;
            tb_phone.Enabled = estado;
            tb_email.Enabled = estado;
            tb_emergencyContact.Enabled = estado;
            tb_emergencyContactName.Enabled = estado;
            richTextBox_address.Enabled = estado;
            comboBoxGender.Enabled = estado;
            tb_height.Enabled = estado;
            tb_weight.Enabled = estado;
            richTextBoxAlergies.Enabled = estado;
        }
        private void fillGridView(List<Patient> patients)
        {

            dataGridViewPatients.DataSource = patients;

            for (int i = 0; i < dataGridViewPatients.ColumnCount; i++)
            {
                if (i != 8 && i != 9 && i != 12 && i != 11)
                {
                    dataGridViewPatients.Columns[i].Visible = false;
                }
            }

            dataGridViewPatients.Columns[8].DisplayIndex = 0;
            dataGridViewPatients.Columns[12].DisplayIndex = 1;
            dataGridViewPatients.Columns[9].DisplayIndex = 2;
            dataGridViewPatients.Columns[11].DisplayIndex = 3;

            dataGridViewPatients.RowHeadersVisible = false;

        }
        private void fillFirstSelected()
        {
            if (dataGridViewPatients.Rows.Count != 0)
            {
                int sns = Convert.ToInt32(dataGridViewPatients.Rows[0].Cells["Sns"].Value);
                Patient patientSelected = client.GetPatient(sns);

                fillFields(patientSelected);
            }
        }
        private void fillFields(Patient patient)
        {
            tb_firstname.Text = patient.Name;
            tb_lastName.Text = patient.Surname;
            dateTimePicker_birthdate.Value = patient.BirthDate;
            tb_nif.Text = patient.Nif.ToString();
            tb_sns.Text = patient.Sns.ToString();
            tb_phone.Text = patient.Phone.ToString();
            tb_email.Text = patient.Email;
            tb_emergencyContact.Text = patient.EmergencyNumber.ToString();
            tb_emergencyContactName.Text = patient.EmergencyName;
            richTextBox_address.Text = patient.Adress;
            if (patient.Gender.Equals("F"))
                comboBoxGender.SelectedIndex = 2;
            if (patient.Gender.Equals("M"))
                comboBoxGender.SelectedIndex = 3;
            tb_height.Text = patient.Height.ToString();
            tb_weight.Text = patient.Weight.ToString();
            richTextBoxAlergies.Text = patient.Alergies;
        }
        private int getAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
        private void clearFields()
        {
            tb_firstname.Clear();
            tb_lastName.Clear();
            tb_nif.Clear();
            tb_sns.Clear();
            tb_phone.Clear();
            tb_email.Clear();
            tb_emergencyContact.Clear();
            tb_emergencyContactName.Clear();
            richTextBox_address.Clear();
            comboBoxGender.SelectedIndex = 0;
            tb_height.Clear();
            tb_weight.Clear();
            richTextBoxAlergies.Clear();

            dateTimePicker_birthdate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_birthdate.CustomFormat = " ";
        }
        private void enableSearch(bool estado)
        {
            toolStripComboBox.Enabled = estado;
            toolStripButtonSearch.Enabled = estado;
            toolStripTextBox.Enabled = estado;
        }

        private bool validateFields()
        {
            if (tb_firstname.Text.Equals("") || tb_lastName.Text.Equals("") ||
                dateTimePicker_birthdate.Value == dateValue || tb_nif.Text.Equals("") ||
                tb_sns.Text.Equals("") || tb_emergencyContact.Text.Equals("") || comboBoxGender.SelectedIndex == 0)
            {
                MessageBox.Show("campos vazios");
                return false;
            }
            else
            {
                MessageBox.Show("TRUE");
                return true;
            }

        }

        private Patient readFields()
        {
            Patient p = new Patient();
            p.Name = tb_firstname.Text;
            p.Surname = tb_lastName.Text;
            if (!dateTimePicker_birthdate.Value.Equals(DateTime.MaxValue))
            {
                p.BirthDate = dateTimePicker_birthdate.Value;
            }
            p.BirthDate = dateTimePicker_birthdate.Value;
            if (!tb_nif.Text.Equals(""))
                p.Nif = Convert.ToInt32(tb_nif.Text);
            if (!tb_sns.Text.Equals(""))
                p.Sns = Convert.ToInt32(tb_sns.Text);
            if (!tb_phone.Text.Equals(""))
                p.Phone = Convert.ToInt32(tb_phone.Text);
            p.Email = tb_email.Text;
            if (!tb_emergencyContact.Text.Equals(""))
                p.EmergencyNumber = Convert.ToInt32(tb_emergencyContact.Text);
            p.EmergencyName = tb_emergencyContactName.Text;
            p.Adress = richTextBox_address.Text;
            if (comboBoxGender.SelectedIndex != 0)
            {
                if (comboBoxGender.SelectedIndex == 1)
                    p.Gender = "F";
                if (comboBoxGender.SelectedIndex == 2)
                    p.Gender = "M";
            }
            if (!tb_height.Text.Equals(""))
                p.Height = Convert.ToInt32(tb_height.Text);
            if (!tb_weight.Text.Equals(""))
                p.Weight = Convert.ToDouble(tb_weight.Text);
            p.Alergies = richTextBoxAlergies.Text;

            return p;
        }
        #endregion

        
    }
}
