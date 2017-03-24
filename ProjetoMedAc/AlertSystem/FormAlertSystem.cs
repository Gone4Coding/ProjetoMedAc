using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlertSystem.ServiceReferenceHealth;


namespace AlertSystem
{
    public partial class FormAlertSystem : Form
    {
        private ServiceHealthAlertClient client;
        private bool asc;
        private bool fromSelection;
        private int patientAge;
        private List<Countries.Country> countries;
        private List<Patient> patients;
        private Patient patientToEdit;
        private Patient patientOnMonitoring;

        private List<BloodPressure> patientsRecordBloodPressure;
        private List<HeartRate> patientsRecordHeartRate;
        private List<OxygenSaturation> patientsRecordOxySat;
        private enum SearchType
        {
            SNS,
            NAME,
            NIF
        }
        public FormAlertSystem()
        {
            InitializeComponent();
            client = new ServiceHealthAlertClient();
        }
        private void FormAlertSystem_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            #region PatientsPage
            comboBoxGender.Items.Add("Female");
            comboBoxGender.Items.Add("Male");

            fillComboBoxCountries();

            toolStripComboBox.Items.Add("SNS");
            toolStripComboBox.Items.Add("NAME");
            toolStripComboBox.Items.Add("NIF");

            toolStripComboBox.SelectedIndex = 0;
            //toolStripTextBox.Width = 500;

            load(null,false);

            #endregion

            #region Monitoring

            

            #endregion
        }
        #region Eventos
        //      
        private void tabControlRecors_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControlRecors.SelectedTab.Text.Equals("View Records"))
            {
                load(patientToEdit, true);
                radioButtonBloodPressure.Checked = true;
                startGraphics();
            }
        }
        #region PatientsTab

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
            groupBoxPatientMonitoring.Hide();
            clearFields();
            enableSearch(false);
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            load(null,false);
            enableSearch(true);
            dateTimePicker_birthdate.Format = DateTimePickerFormat.Short;
            errorProvider1.Clear();
            groupBoxPatientMonitoring.Show();
        }
        private void dateTimePicker_birthdate_ValueChanged(object sender, EventArgs e)
        {
            // dateTimePicker_birthdate.Value = DateTime.Now;
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
            if (validateFields(true))
            {
                Patient p = readFields();

                bool res = client.UpdatePatient(p, patientToEdit.Sns);

                if (!res)
                {
                    MessageBox.Show("erro");
                }
                else
                {
                    enableTextBoxes(false);
                    bt_edit.Enabled = true;
                    bt_save.Enabled = false;
                    bt_cancelEdit.Hide();
                    MessageBox.Show("suss");

                    load(p,false);
                }
            }

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
            errorProvider1.Clear();

            Patient p = client.GetPatient(patientToEdit.Sns);
            clearFields();
            load(p,false);
            enableSearch(true);
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (validateFields(false))
            {
                Patient p = readFields();

                bool res = client.InsertPatient(p);

                if (!res)
                {
                    MessageBox.Show("erro");
                }
                else
                {

                    MessageBox.Show(p.Name + " " + p.Surname + " sucessfully added!", "SUCESS", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    clearFields();
                    load(p,false);
                    enableSearch(true);
                    groupBoxPatientMonitoring.Show();
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
            load(null,false);
        }
        private void dataGridViewPatients_SelectionChanged(object sender, EventArgs e)
        {
            fromSelection = false;
            if (dataGridViewPatients.Rows.Count > 0 & dataGridViewPatients.CurrentCell != null)
            {
                int index = dataGridViewPatients.CurrentCell.RowIndex;
                int sns = Convert.ToInt32(dataGridViewPatients.Rows[index].Cells["Sns"].Value);
                Patient patientSelected = client.GetPatient(sns);

                fillFields(patientSelected);
                
            }
        }
       
        private void checkBoxPatientMonitoring_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBoxPatientMonitoring.Checked)
                {
                    patientToEdit.Ativo = false;
                    bool res = client.UpdateStatePatient(patientToEdit);

                    if (!res)
                    {
                        MessageBox.Show("erro");
                    }
                    else
                    {
                        readMonitoring(patientToEdit);
                        load(patientToEdit, false);
                        MessageBox.Show("MONITORIZAÇAO DESATIVADA ");                        
                    }
                }
                else
                {                   
                        patientToEdit.Ativo = true;
                        bool res = client.UpdateStatePatient(patientToEdit);

                        if (!res)
                        {
                            MessageBox.Show("erro");
                        }
                        else
                        {
                            readMonitoring(patientToEdit);
                            load(patientToEdit, false);
                            MessageBox.Show("SOB MONITORIZAÇAO");
                        }   
                }
            }
            catch (NullReferenceException x)
            {
                MessageBox.Show("No patient to set active!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBoxPatientMonitoring.Checked = false;
            }
        }
        #endregion


        #region Monitoring
        private void radioButtonBloodPressure_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonBloodPressure.Checked)
            readRadioButtons(patientOnMonitoring);
        }

        private void radioButtonHeartRate_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonHeartRate.Checked)
            readRadioButtons(patientOnMonitoring);
        }

        private void radioButtonOxygenSat_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonOxygenSat.Checked)
            readRadioButtons(patientOnMonitoring);
        }
        private void dataGridViewPatientsMonitor_SelectionChanged(object sender, EventArgs e)
        {
            fromSelection = false;
            if (dataGridViewPatientsMonitor.Rows.Count > 0 & dataGridViewPatientsMonitor.CurrentCell != null)
            {
                int index = dataGridViewPatientsMonitor.CurrentCell.RowIndex;
                int sns = Convert.ToInt32(dataGridViewPatientsMonitor.Rows[index].Cells["Sns"].Value);
                Patient patientSelected = client.GetPatient(sns);
                patientOnMonitoring = patientSelected;
                fillMonitorPatientInfo(patientSelected);
                fillFields(patientSelected);
                selectPatientDataGridView(patientSelected);

                dataGridViewHistory.DataSource = null;
                dataGridViewHistory.Rows.Clear();
                
                readRadioButtons(patientOnMonitoring);
            }
        }
        #endregion
        // 
        #endregion

        #region Metodos
        // 
        #region PatientsTab
        private void load(Patient p, bool monitoring)
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
            patients = new List<Patient>(client.GetPatientList());
            if (!monitoring)
            {
                fillGridView(patients);
            }
            else
            {
                List<Patient> activePatients = patients.Where(i => i.Ativo).ToList();
                fillGridViewMonitoring(activePatients);
            }

            if (p == null && monitoring)
            {
                fillFirstSelected(false);
            }

            if (p == null && !monitoring)
            {
                fillFirstSelected(true);
            }
            else
            {
                if (!monitoring)
                {
                    selectPatientDataGridView(p);
                    fillFields(p);
                }
                else
                {
                    if (dataGridViewPatientsMonitor.RowCount != 0)
                    {
                        foreach (DataGridViewRow row in dataGridViewPatientsMonitor.Rows)
                        {
                            if (p.Sns == Convert.ToInt32(row.Cells[14].Value))
                            {
                                rowIndex = row.Index;
                            }
                        }
                        dataGridViewPatientsMonitor.Rows[rowIndex].Selected = true;
                        if (p.Ativo)
                        {
                            fillMonitorPatientInfo(p);
                        }
                        else
                        {
                            fillFirstSelected(true);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No patients on monitoring!", "INFO", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        tabControlRecors.SelectedTab = tabPagePatients;
                    }
                }
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
            comboBoxCode.Enabled = estado;
            comboBoxEmergencyCode.Enabled = estado;
        }
        private void fillGridView(List<Patient> patients)
        {

            dataGridViewPatients.DataSource = patients;


            for (int i = 0; i < dataGridViewPatients.ColumnCount; i++)
            {
                if (i != 14 && i != 10 && i != 15 && i != 2 && i != 8)
                {
                    dataGridViewPatients.Columns[i].Visible = false;
                }
            }



            foreach (DataGridViewRow row in dataGridViewPatients.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Ativo"].Value))
                {
                    row.DefaultCellStyle.BackColor = Color.Chocolate;
                }   
            }

            dataGridViewPatients.Columns[14].DisplayIndex = 0;
            dataGridViewPatients.Columns[10].DisplayIndex = 1;
            dataGridViewPatients.Columns[15].DisplayIndex = 2;
            dataGridViewPatients.Columns[8].DisplayIndex = 3;
            dataGridViewPatients.Columns[2].DisplayIndex = 4;

            dataGridViewPatients.RowHeadersVisible = false;

        }
        private void fillFirstSelected(bool monitoring)
        {
            if (!monitoring)
            {
                if (dataGridViewPatients.Rows.Count != 0)
                {
                    int sns = Convert.ToInt32(dataGridViewPatients.Rows[0].Cells["Sns"].Value);
                    Patient patientSelected = client.GetPatient(sns);

                    fillFields(patientSelected);
                }
            }
            else
            {
                if (dataGridViewPatientsMonitor.Rows.Count != 0)
                {
                    int sns = Convert.ToInt32(dataGridViewPatientsMonitor.Rows[0].Cells["Sns"].Value);
                    Patient patientSelected = client.GetPatient(sns);

                    fillMonitorPatientInfo(patientSelected);
                }
            }
        }
        private void fillFields(Patient patient)
        {
            patientToEdit = patient;
            tb_firstname.Text = patient.Name;
            tb_lastName.Text = patient.Surname;
            dateTimePicker_birthdate.Value = patient.BirthDate;
            tb_nif.Text = patient.Nif.ToString();
            tb_sns.Text = patient.Sns.ToString();
            // snsPatientEdit = patient.Sns;
            tb_phone.Text = patient.Phone.ToString();
            tb_email.Text = patient.Email;
            tb_emergencyContact.Text = patient.EmergencyNumber.ToString();
            tb_emergencyContactName.Text = patient.EmergencyName;
            richTextBox_address.Text = patient.Adress;
            if (patient.Gender.Equals("F"))
                comboBoxGender.SelectedIndex = 0;
            if (patient.Gender.Equals("M"))
                comboBoxGender.SelectedIndex = 1;
            tb_height.Text = patient.Height.ToString();
            tb_weight.Text = patient.Weight.ToString();
            richTextBoxAlergies.Text = patient.Alergies;
            if(patient.EmergencyNumberCountryCode!=null)
                comboBoxEmergencyCode.Text = countries.First(i => i.CallingCodes == patient.EmergencyNumberCountryCode).ToString();
            if (patient.PhoneCountryCode != null)
                comboBoxCode.Text = countries.First(i => i.CallingCodes == patient.PhoneCountryCode).ToString();
            readMonitoring(patient);
           // fillMonitorPatientInfo(patient);
        }

        private void fillComboBoxCountries()
        {
            countries =  Countries.getAllCountries();
        
            if (countries != null)
            {
                foreach (Countries.Country country in countries)
                {
                    if (!country.CallingCodes.Equals(""))
                    {
                        comboBoxCode.Items.Add(country.ToString());
                        comboBoxEmergencyCode.Items.Add(country.ToString());
                    }
                }
            }
            else
            {
                comboBoxCode.Items.Add("PT +351");
                comboBoxEmergencyCode.Items.Add("PT +351");
            }

            comboBoxCode.DropDownWidth = 300;
            comboBoxEmergencyCode.DropDownWidth = 300;
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
            comboBoxGender.SelectedIndex = -1;
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

        private bool validateFields(bool fromEdition)
        {
            int errors = 0;

            errorProvider1.Clear();

            Regex regexNif = new Regex(@"^[0-9]{9}$");

            Regex regexPhone = new Regex(@"^((\+351)?([1-9]{2}[0-9]{7})|(2[0-9]{8}))$");
            Regex regexEmail = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            List<Patient> listPatients = new List<Patient>(client.GetPatientList());

            if (tb_firstname.Text.Equals("") || tb_lastName.Text.Equals("") ||
                dateTimePicker_birthdate.Format == DateTimePickerFormat.Custom || tb_nif.Text.Equals("") ||
                tb_sns.Text.Equals("") || tb_emergencyContact.Text.Equals("") || comboBoxGender.SelectedIndex == -1 || comboBoxEmergencyCode.SelectedIndex == -1)
            {
                errors = 0;

                if (tb_firstname.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_firstname, "Required field");
                    errors++;
                }
                if (tb_lastName.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_lastName
                        , "Required field");
                    errors++;
                }
                if (dateTimePicker_birthdate.Format == DateTimePickerFormat.Custom)
                {
                    errorProvider1.SetError(dateTimePicker_birthdate, "Required field");
                    errors++;
                }
                if (tb_nif.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_nif, "Required field");
                    errors++;
                }
                if (tb_sns.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_sns, "Required field");
                    errors++;
                }
                if (tb_emergencyContact.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_emergencyContact, "Required field");
                    errors++;
                }
                if (comboBoxGender.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBoxGender, "Required field");
                    errors++;
                }
                if (comboBoxEmergencyCode.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBoxEmergencyCode, "Required field");
                    errors++;
                }
                if (errors > 0)
                    return false;
            }
            else
            {
                errors = 0;
                //formatos
                if (!isNumber(tb_sns.Text))
                {
                    tb_sns.Focus();
                    errorProvider1.SetError(tb_sns, "Has to be a number");
                    errors++;
                }

                if (!isNumber(tb_nif.Text))
                {
                    tb_nif.Focus();
                    errorProvider1.SetError(tb_nif, "Has to be a number");
                    errors++;
                }

                if (!regexEmail.IsMatch(tb_email.Text) && !tb_email.Text.Equals(""))
                {
                    tb_email.Focus();
                    errorProvider1.SetError(tb_email, "Wrong format");
                    errors++;
                }

                if (!regexNif.IsMatch(tb_sns.Text))
                {
                    tb_sns.Focus();
                    errorProvider1.SetError(tb_sns, "Enter at least 9 digits");
                    errors++;
                }


                if (!regexNif.IsMatch(tb_nif.Text))
                {
                    tb_nif.Focus();
                    errorProvider1.SetError(tb_nif, "Enter at least 9 digits");
                    errors++;
                }

                if (errors > 0)
                    return false;

                //ver uniques
                if (listPatients.Where(i => i.Nif == Convert.ToInt32(tb_nif.Text)).ToList().Count > 0 && !fromEdition)
                {
                    tb_nif.Focus();
                    errorProvider1.SetError(tb_nif, "NIF already exists");
                    errors++;

                }
                if (listPatients.Where(i => i.Sns == Convert.ToInt32(tb_sns.Text)).ToList().Count > 0 && !fromEdition)
                {
                    tb_sns.Focus();
                    errorProvider1.SetError(tb_sns, "SNS already exists");
                    errors++;
                }

                if (errors > 0)
                    return false;
            }
            return true;
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
            if (comboBoxGender.SelectedIndex != -1)
            {
                if (comboBoxGender.SelectedIndex == 0)
                    p.Gender = "F";
                if (comboBoxGender.SelectedIndex == 1)
                    p.Gender = "M";
            }
            if (!tb_height.Text.Equals(""))
                p.Height = Convert.ToInt32(tb_height.Text);
            if (!tb_weight.Text.Equals(""))
                p.Weight = Convert.ToDouble(tb_weight.Text);
            p.Alergies = richTextBoxAlergies.Text;
            if (comboBoxCode.SelectedIndex != -1)
            {
                string[] alpha = comboBoxCode.SelectedItem.ToString().Split(' ');
                p.PhoneCountryCode = alpha[2];
            }
            if (comboBoxEmergencyCode.SelectedIndex != -1)
            {
                string [] alpha = comboBoxEmergencyCode.SelectedItem.ToString().Split(' ');
                p.EmergencyNumberCountryCode = alpha[2];
            }

            return p;
        }

        private void readMonitoring(Patient p)
        {
            if (!p.Ativo)
            {
                checkBoxPatientMonitoring.Checked = false;
                checkBoxPatientMonitoring.ForeColor = Color.Gray;
            }
            else
            {
                checkBoxPatientMonitoring.Checked = true;
                checkBoxPatientMonitoring.ForeColor = Color.Green;
            }
        }

        private bool isNumber(string data)
        {
            bool isnumeric = true;
            char[] datachars = data.ToCharArray();

            foreach (var datachar in datachars)
                if (isnumeric == true)
                    isnumeric = Char.IsDigit(datachar);

            return isnumeric;
        }

        private void selectPatientDataGridView(Patient p)
        {
            int rowIndex = 0;
            foreach (DataGridViewRow row in dataGridViewPatients.Rows)
            {
                if (p.Sns == Convert.ToInt32(row.Cells[14].Value))
                {
                    rowIndex = row.Index;
                }
            }
            dataGridViewPatients.Rows[rowIndex].Selected = true;
        }
        #endregion
        //

        #region Monitor

        private void fillMonitorPatientInfo(Patient patient)
        {
            patientOnMonitoring = patient;

            textBoxFirstName.Text = patient.Name;
            textBoxLastName.Text = patient.Surname;
            textBoxSNS.Text = patient.Sns.ToString();
            textBoxAge.Text = getAge(patient.BirthDate).ToString();
            tb_phone.Text = patient.Phone.ToString();
            textBoxEmergencyContact.Text = patient.EmergencyNumber.ToString();
            if (patient.Gender.Equals("F"))
                textBoxGender.Text = "Female";
            if (patient.Gender.Equals("M"))
                textBoxGender.Text = "Male";
            textBoxheight.Text = patient.Height.ToString();
            textBoxWeight.Text = patient.Weight.ToString();
            richTextBox1Alergies.Text = patient.Alergies;
        }

        private void fillGridViewMonitoring(List<Patient> patients)
        {
            List<Patient> patientsActive = patients.Where(i => i.Ativo).ToList();

            dataGridViewPatientsMonitor.DataSource = patientsActive;

            for (int i = 0; i < dataGridViewPatientsMonitor.ColumnCount; i++)
            {
                if (i != 14 && i != 10 && i != 15)
                {
                    dataGridViewPatientsMonitor.Columns[i].Visible = false;
                }
            }

            foreach (DataGridViewRow row in dataGridViewPatientsMonitor.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Sns"].Value))
                {
                   // row.DefaultCellStyle.BackColor = Color.Chocolate;
                }
            }

            dataGridViewPatientsMonitor.Columns[14].DisplayIndex = 0;
            dataGridViewPatientsMonitor.Columns[10].DisplayIndex = 1;
            dataGridViewPatientsMonitor.Columns[15].DisplayIndex = 2;
            
            dataGridViewPatientsMonitor.RowHeadersVisible = false;
        }

        private void readRadioButtons(Patient patient)
        {
            
            if (radioButtonBloodPressure.Checked)
            {
                patientsRecordBloodPressure = new List<BloodPressure>(client.BloodPressureList(patient.Sns).OrderByDescending(i=> i.Date));

                dataGridViewHistory.DataSource = patientsRecordBloodPressure;
                dataGridViewHistory.RowHeadersVisible = false;
                dataGridViewHistory.Columns["PatientSNS"].Visible = false;      
                
                                       
            }

            if (radioButtonHeartRate.Checked)
            {
                patientsRecordHeartRate = new List<HeartRate>(client.HeartRateList(patient.Sns).OrderByDescending(i=> i.Date));
               
                dataGridViewHistory.DataSource = patientsRecordHeartRate;
                dataGridViewHistory.RowHeadersVisible = false;
                dataGridViewHistory.Columns["PatientSNS"].Visible = false;
                
            }

            if (radioButtonOxygenSat.Checked)
            {
                patientsRecordOxySat = new List<OxygenSaturation>(client.OxygenSaturationList(patient.Sns).OrderByDescending(i=> i.Date));
           
                dataGridViewHistory.DataSource = patientsRecordOxySat;
                dataGridViewHistory.RowHeadersVisible = false;
                dataGridViewHistory.Columns["PatientSNS"].Visible = false;
                
            }          
        }

        public void startGraphics()
        {
            //Construção da àrea do gráfico
            chart1.ChartAreas.Add("area");
            // chart1.ChartAreas.Add("area2");
            //chart1.Series[0].ChartArea = "area2";
            chart1.ChartAreas["area"].AxisX.Minimum = 2010;
            chart1.ChartAreas["area"].AxisX.Maximum = 2014;
            chart1.ChartAreas["area"].AxisX.Interval = 1;

            chart1.ChartAreas["area"].AxisY.Minimum = 5000;
            chart1.ChartAreas["area"].AxisY.Interval = 25;
            chart1.ChartAreas["area"].AxisY.Title = "#People";
            chart1.ChartAreas["area"].AxisX.Title = "Years";

            // chart1.ChartAreas.Add("area2");
            //chart1.Series[0].ChartArea = "area2";

            //definição de duas séries para o gráfico
            chart1.Series.Add("Feminino");
            chart1.Series.Add("Masculino");
            //definição da cor de cada série
            chart1.Series["Feminino"].Color = Color.Red;
            chart1.Series["Masculino"].Color = Color.Blue;
            //Pontos a aparecer no gráfico
            chart1.Series["Feminino"].Points.AddXY(2011, 5478);
            chart1.Series["Feminino"].Points.AddXY(2012, 5456);
            chart1.Series["Feminino"].Points.AddXY(2013, 5484);
            chart1.Series["Masculino"].Points.AddXY(2011, 5210);
            chart1.Series["Masculino"].Points.AddXY(2012, 5190);
            chart1.Series["Masculino"].Points.AddXY(2013, 5100);

            //chart1.Series[1].ChartArea = "area2";

            chart1.ChartAreas["area"].BackColor = Color.White;
            chart1.ChartAreas["area"].BackSecondaryColor = Color.LightBlue;
            chart1.ChartAreas["area"].BackGradientStyle =
            System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;

            chart1.ChartAreas["area"].AxisX.MajorGrid.LineColor = Color.LightSlateGray;
            chart1.ChartAreas["area"].AxisY.MajorGrid.LineColor = Color.LightSteelBlue;

            //double[] valores = { 78, 55.5, 48.8, 65, 72 };
            //string[] nome = { "utente 1", "utente 2", "utente 3", "utente 4", "utente 5" };
            //chart1.Series.Add("peso");
            //chart1.Series[0].Points.DataBindXY(nome, valores);

        }

        #endregion

        #endregion

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                readSearch();
            }
        }

        private void readSearch()
        {
            string type = toolStripComboBox.Text;
            Patient pSearched;

            if (type.Equals("SNS"))
            {
                pSearched = patients.FirstOrDefault(i => i.Sns == Convert.ToInt32(toolStripTextBox.Text));
                if (pSearched != null)
                {
                    load(pSearched, false);
                  // this.backgroundWorker1.RunWorkerAsync();
                   // Thread t = new Thread();
                   
                }
                else
                {
                    MessageBox.Show("No results!", "INFO", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                }
            }

            if (type.Equals("NIF"))
            {
                pSearched = patients.First(i => i.Nif == Convert.ToInt32(toolStripTextBox.Text));
            }

            if (type.Equals("NIF"))
            {

            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            for (int i = 0; i < 20000; i++)
            {
                Console.WriteLine(i);
            }
            Form1 fom = new Form1();
            
            fom.ShowDialog();
        }

        private bool validateSearch()
        {
            if (toolStripTextBox.Text.Equals("") || toolStripComboBox.SelectedIndex == -1)
            {
                if (toolStripTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Field with value can't be empty", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                if (toolStripComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a search type", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (toolStripComboBox.Text.Equals("SNS") && !isNumber(toolStripTextBox.Text) ||
                    toolStripComboBox.Text.Equals("NIF") && !isNumber(toolStripTextBox.Text))
                {
                    MessageBox.Show("For this type of search, value has to be a number ", "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    if (toolStripTextBox.Text.Length < 9)
                    {
                        MessageBox.Show("Introduce a number with 9 digits", "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                        return false;
                    }                       
                }
            }
            return true;
        }

        
    }
}
