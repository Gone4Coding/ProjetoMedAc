using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AlertSystem.ServiceReferenceHealth;


namespace AlertSystem
{
    public partial class FormAlertSystem : Form
    {
        private const string REQUIRED = "Required field";
        private const string OXYSAT = "Oxygen Saturation(%)";
        private const string HRATE = "Heart Rate";
        private const string DIASTOLIC = "Diastolic - BP";
        private const string SYSTOLIC = "Systolic - BP";
        private const string LINES = "Lines";
        private const string POINTS = "Points";
        private const string COLUMNS = "Columns";
        private const string cPATIENTSNS = "PatientSNS";
        private const string TIME = "Time";
        private const string RESULTS = "Results: ";
        private const string AREA1 = "area";
        private const string AREA2 = "area2";

        private static ServiceHealthAlertClient client;
        private List<Countries.Country> countries;
        private List<Patient> patients;
        private Patient patientToEdit;
        private Patient patientOnMonitoring;
        private List<BloodPressure> patientsRecordBloodPressure;
        private List<HeartRate> patientsRecordHeartRate;
        private List<OxygenSaturation> patientsRecordOxySat;
        private List<BloodPressure> warningListBloodPressure;
        private List<HeartRate> warningListHeartRate;
        private List<OxygenSaturation> warningListOxygenSaturation;
        private List<BloodPressureWarning> warningListBPALL;
        private List<HeartRateWarning> warningListHRALL;
        private List<OxygenSaturationWarning> warningListOXYSATALL;
        private DateTime fromDate;
        private DateTime toDate;
        private bool asc;
        private bool fromSelection;
        private bool firstTime;
        private Event eventType;
        public FormAlertSystem()
        {

            InitializeComponent();
            client = new ServiceHealthAlertClient();
            eventType = new Event();
            timer1.Start();
            timer1.Interval = 1000;
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

            load(null, false);
            if (patients?.Count == 0)
            {
                toolStripButtonAdd_Click(sender, e);
            }

            #endregion

            #region Monitoring

            firstTime = true;

            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.CustomFormat = "dd / MM / yyyy HH: mm: ss";

            dateTimePickerTO.Format = DateTimePickerFormat.Custom;
            dateTimePickerTO.CustomFormat = "dd / MM / yyyy HH: mm: ss";

            dateTimePickerFrom.Value = DateTime.Now.AddDays(-1);
            dateTimePickerTO.Value = DateTime.Now;

            comboBoxChartType.Items.Add(LINES);
            comboBoxChartType.Items.Add(COLUMNS);
            comboBoxChartType.Items.Add(POINTS);

            checkBoxDiastolicSeries.Text = DIASTOLIC;
            checkBoxSystolicSeries.Text = SYSTOLIC;
            checkBoxHeartRateSeries.Text = HRATE;
            checkBoxOxySatSeries.Text = OXYSAT;

            checkBoxesChecked(true);

            #endregion
        }
        #region Eventos
        private void tabControlRecors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControlRecors.SelectedTab.Text.Equals("View Records"))
                {
                    load(patientToEdit, true);
                    if (patientToEdit.Ativo)
                    {
                        radioButtonBloodPressure.Checked = true;
                        firstTime = false;
                        radioButtonAll.Checked = true;
                        readRadioButtons(patientOnMonitoring);
                        startGraphics();
                        comboBoxChartType.SelectedIndex = 0;
                        readComboChartTyper();
                        readCheckBoxValue();
                    }
                }
            }
            catch (NullReferenceException x)
            {
                MessageBox.Show("First, you have to add a Patient to the system, and then you can monitorize it", "INFO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #region PatientsTab

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                readSearch();
            }
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
            groupBoxPatientMonitoring.Hide();
            clearFields();
            enableSearch(false);
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            load(null, false);
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

                    load(p, false);
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
            load(p, false);
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
                    load(p, false);
                    enableSearch(true);
                    groupBoxPatientMonitoring.Show();
                }
            }
        }

        private void dataGridViewPatients_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 10:
                    if (!asc)
                    {                       
                        fillGridView(patients.OrderBy(i => i.Name).ToList());
                        asc = true;
                    }
                    else
                    {
                        fillGridView(patients.OrderByDescending(i => i.Name).ToList());
                        asc = false;
                    }
                    break;
                case 8:
                    if (!asc)
                    {
                        fillGridView(patients.OrderBy(i => i.Gender).ToList());
                        asc = true;
                    }
                    else
                    {
                        fillGridView(patients.OrderByDescending(i => i.Gender).ToList());
                        asc = false;
                    }
                    break;
                case 14:
                    if (!asc)
                    {
                        fillGridView(patients.OrderBy(i => i.Sns).ToList());
                        asc = true;
                    }
                    else
                    {
                        fillGridView(patients.OrderByDescending(i => i.Sns).ToList());
                        asc = false;
                    }
                    break;
                case 15:
                    if (!asc)
                    {
                        fillGridView(patients.OrderBy(i => i.Surname).ToList());
                        asc = true;
                    }
                    else
                    {
                        fillGridView(patients.OrderByDescending(i => i.Surname).ToList());
                        asc = false;
                    }
                    break;
            }

        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            load(null, false);
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
            if (radioButtonBloodPressure.Checked)
            {
                readRadioButtons(patientOnMonitoring);
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void radioButtonHeartRate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonHeartRate.Checked)
            {
                readRadioButtons(patientOnMonitoring);
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void radioButtonOxygenSat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOxygenSat.Checked)
            {
                readRadioButtons(patientOnMonitoring);
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = ".PNG | *.png";
            saveFileDialog1.Title = "Save Graphic Image";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                chart1.SaveImage(fs, System.Drawing.Imaging.ImageFormat.Png);

                fs.Close();
                MessageBox.Show("File saved!\n" + fs.Name, "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void bt_OK_Click(object sender, EventArgs e)
        {
            readDateTimeGraphics();
            readRadioButtons(patientOnMonitoring);
            readRadioButtonsAlerts(patientOnMonitoring, eventType);
            startGraphics();
            readComboChartTyper();
        }
        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            readComboChartTyper();
        }
        private void toolStripButtonSearchMonitor_Click(object sender, EventArgs e)
        {
            FormSearch search = new FormSearch();
            search.ShowDialog();
            Patient patientSearch = search.getPatientSearched();
            if (patientSearch != null)
            {
                patientOnMonitoring = patientSearch;
                load(patientOnMonitoring, true);
                firstTime = false;
                radioButtonEAC.Checked = true;
                readRadioButtons(patientOnMonitoring);
                startGraphics();
                readComboChartTyper();

            }
        }
        private void checkBoxesChecked(bool state)
        {
            checkBoxDiastolicSeries.Checked = state;
            checkBoxSystolicSeries.Checked = state;
            checkBoxHeartRateSeries.Checked = state;
            checkBoxOxySatSeries.Checked = state;

        }
        private void checkBoxDiastolicSeries_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDiastolicSeries.Checked)
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
            else
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
        }
        private void checkBoxSystolicSeries_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSystolicSeries.Checked)
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
            else
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
        }
        private void checkBoxHeartRateSeries_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHeartRateSeries.Checked)
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
            else
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
        }
        private void checkBoxOxySatSeries_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOxySatSeries.Checked)
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
            else
            {
                startGraphics();
                readComboChartTyper();
                readCheckBoxValue();
            }
        }
        private void radioButtonEAC_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEAC.Checked)
            {
                eventType.EvenType = Event.Type.EAC;
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void radioButtonEAI_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEAI.Checked)
            {
                eventType.EvenType = Event.Type.EAI;
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void radioButtonECC_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonECC.Checked)
            {
                eventType.EvenType = Event.Type.ECC;
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void radioButtonECI_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonECI.Checked)
            {
                eventType.EvenType = Event.Type.ECI;
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void radioButtonECA_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonECA.Checked)
            {
                eventType.EvenType = Event.Type.ECA;
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAll.Checked)
            {
                readRadioButtonsAlerts(patientOnMonitoring, eventType);
            }
        }
        private void dataGridViewAlerts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {          
            switch (e.ColumnIndex)
            {
                case 0:
                    if (!asc)
                    {
                        if (radioButtonBloodPressure.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBPALL.OrderBy(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonBloodPressure.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBloodPressure.OrderBy(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHRALL.OrderBy(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHeartRate.OrderBy(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOXYSATALL.OrderBy(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOxygenSaturation.OrderBy(i => i.Date);
                            setGridViewAlerts();
                        }
                        asc = true;
                    }
                    else
                    {
                        if (radioButtonBloodPressure.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBPALL.OrderByDescending(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonBloodPressure.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBloodPressure.OrderByDescending(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHRALL.OrderByDescending(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHeartRate.OrderByDescending(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOXYSATALL.OrderByDescending(i => i.Date);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOxygenSaturation.OrderByDescending(i => i.Date);
                            setGridViewAlerts();
                        }
                        //fiquei aqui
                        asc = false;
                    }
                    break;
                case 1:
                    if (!asc)
                    {
                        if (radioButtonBloodPressure.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBPALL.OrderBy(i => i.Diastolic);
                            setGridViewAlerts();
                        }

                        if (radioButtonBloodPressure.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBloodPressure.OrderBy(i => i.Diastolic);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHRALL.OrderBy(i => i.Rate);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHeartRate.OrderBy(i => i.Rate);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOXYSATALL.OrderBy(i => i.Saturation);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOxygenSaturation.OrderBy(i => i.Saturation);
                            setGridViewAlerts();
                        }
                        asc = true;
                    }
                    else
                    {
                        if (radioButtonBloodPressure.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBPALL.OrderByDescending(i => i.Diastolic);
                            setGridViewAlerts();
                        }

                        if (radioButtonBloodPressure.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListBloodPressure.OrderByDescending(i => i.Diastolic);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHRALL.OrderByDescending(i => i.Rate);
                            setGridViewAlerts();
                        }

                        if (radioButtonHeartRate.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListHeartRate.OrderByDescending(i => i.Rate);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked && radioButtonAll.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOXYSATALL.OrderByDescending(i => i.Saturation);
                            setGridViewAlerts();
                        }

                        if (radioButtonOxygenSat.Checked)
                        {
                            dataGridViewAlerts.DataSource = warningListOxygenSaturation.OrderByDescending(i => i.Saturation);
                            setGridViewAlerts();
                        }
                        asc = false;
                    }
                    break;
                case 2:
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
                case 15:
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
        #endregion

        #endregion

        #region Metodos
        #region PatientsTab
        private void load(Patient p, bool monitoring)
        {
            try
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
                        if (p.Ativo)
                        {
                            fillMonitorPatientInfo(p);
                        }
                        else
                        {
                            MessageBox.Show("Patient is not active on monitoring!", "INFO", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            tabControlRecors.SelectedTab = tabPagePatients;
                        }
                    }
                }

                enableTextBoxes(false);
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("No service found!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("GOOD BYE", "BYE BYE", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }

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
            //else
            //{
            //    if (dataGridViewPatientsMonitor.Rows.Count != 0)
            //    {
            //        int sns = Convert.ToInt32(dataGridViewPatientsMonitor.Rows[0].Cells["Sns"].Value);
            //        Patient patientSelected = client.GetPatient(sns);

            //        fillMonitorPatientInfo(patientSelected);
            //    }
            //}
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
            if (patient.EmergencyNumberCountryCode != null)
                comboBoxEmergencyCode.Text =
                    countries.First(i => i.CallingCodes == patient.EmergencyNumberCountryCode).ToString();
            if (patient.PhoneCountryCode != null)
                comboBoxCode.Text = countries.First(i => i.CallingCodes == patient.PhoneCountryCode).ToString();
            readMonitoring(patient);
            // fillMonitorPatientInfo(patient);


        }
        private void fillComboBoxCountries()
        {
            countries = Countries.getAllCountries();

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
            Regex regexEmail =
                new Regex(
                    @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            List<Patient> listPatients = new List<Patient>(client.GetPatientList());

            if (tb_firstname.Text.Equals("") || tb_lastName.Text.Equals("") ||
                dateTimePicker_birthdate.Format == DateTimePickerFormat.Custom || tb_nif.Text.Equals("") ||
                tb_sns.Text.Equals("") || tb_emergencyContact.Text.Equals("") || comboBoxGender.SelectedIndex == -1 ||
                comboBoxEmergencyCode.SelectedIndex == -1)
            {
                errors = 0;

                if (tb_firstname.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_firstname, REQUIRED);
                    errors++;
                }
                if (tb_lastName.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_lastName
                        , REQUIRED);
                    errors++;
                }
                if (dateTimePicker_birthdate.Format == DateTimePickerFormat.Custom)
                {
                    errorProvider1.SetError(dateTimePicker_birthdate, REQUIRED);
                    errors++;
                }
                if (tb_nif.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_nif, REQUIRED);
                    errors++;
                }
                if (tb_sns.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_sns, REQUIRED);
                    errors++;
                }
                if (tb_emergencyContact.Text.Equals(""))
                {
                    errorProvider1.SetError(tb_emergencyContact, REQUIRED);
                    errors++;
                }
                if (comboBoxGender.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBoxGender, REQUIRED);
                    errors++;
                }
                if (comboBoxEmergencyCode.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBoxEmergencyCode, REQUIRED);
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
                string[] alpha = comboBoxEmergencyCode.SelectedItem.ToString().Split(' ');
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
                }
                else
                {
                    MessageBox.Show("No results!", "INFO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            if (type.Equals("NIF"))
            {
                pSearched = patients.FirstOrDefault(i => i.Nif == Convert.ToInt32(toolStripTextBox.Text));

                if (pSearched != null)
                {
                    load(pSearched, false);
                }
                else
                {
                    MessageBox.Show("No results!", "INFO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            if (type.Equals("NAME"))
            {
                string[] splited = toolStripTextBox.Text.Split(' ');
                List<Patient> pByName =
                    patients.Where(i => i.Name.Contains(splited[0]) || i.Surname.Contains(splited[1])).ToList();

                dataGridViewPatients.DataSource = pByName;
            }
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
                    if (toolStripTextBox.Text.Length < 9 && isNumber(toolStripTextBox.Text))
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
        #endregion
        #region Monitor
        private void fillMonitorPatientInfo(Patient patient)
        {
            patientOnMonitoring = patient;
            toolStripPatientLabel.Text = "PATIENT: " + patientOnMonitoring.Name + " " + patientOnMonitoring.Surname + " SNS: " +
                                         patientOnMonitoring.Sns + " AGE: " +
                                         getAge(patientOnMonitoring.BirthDate).ToString();
        }
        private void readRadioButtons(Patient patient)
        {
            patientsRecordBloodPressure =
                    new List<BloodPressure>(
                        client.BloodPressureList(patient.Sns)
                            .Where(i => i.Date >= fromDate && i.Date <= toDate)
                            .OrderByDescending(i => i.Date));

            patientsRecordHeartRate =
                    new List<HeartRate>(client.HeartRateList(patient.Sns)
                        .Where(i => i.Date >= fromDate && i.Date <= toDate)
                        .OrderByDescending(i => i.Date));

            patientsRecordOxySat =
                   new List<OxygenSaturation>(
                       client.OxygenSaturationList(patient.Sns)
                           .Where(i => i.Date >= fromDate && i.Date <= toDate)
                           .OrderByDescending(i => i.Date));

            if (radioButtonBloodPressure.Checked)
            {
                if (readDateTimeGraphics())
                {
                    dataGridViewHistory.DataSource = patientsRecordBloodPressure;
                    dataGridViewHistory.RowHeadersVisible = false;
                    dataGridViewHistory.Columns[cPATIENTSNS].Visible = false;
                    dataGridViewHistory.Columns[TIME].Visible = false;

                    labelResultsRecords.Text = RESULTS + patientsRecordBloodPressure.Count;

                    if (patientsRecordBloodPressure.Count == 0 && !firstTime)
                    {
                        MessageBox.Show("No results for timeline selected!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Timeline selected is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

            if (radioButtonHeartRate.Checked)
            {
                if (readDateTimeGraphics())
                {
                    dataGridViewHistory.DataSource = patientsRecordHeartRate;
                    dataGridViewHistory.RowHeadersVisible = false;
                    dataGridViewHistory.Columns[cPATIENTSNS].Visible = false;
                    dataGridViewHistory.Columns[TIME].Visible = false;

                    labelResultsRecords.Text = RESULTS + patientsRecordHeartRate.Count;
                    if (patientsRecordHeartRate.Count == 0)
                    {
                        MessageBox.Show("No results for timeline selected!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Timeline selected is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

            if (radioButtonOxygenSat.Checked)
            {
                if (readDateTimeGraphics())
                {
                    dataGridViewHistory.DataSource = patientsRecordOxySat;
                    dataGridViewHistory.RowHeadersVisible = false;
                    dataGridViewHistory.Columns[cPATIENTSNS].Visible = false;
                    dataGridViewHistory.Columns[TIME].Visible = false;

                    labelResultsRecords.Text = RESULTS + patientsRecordOxySat.Count;
                    if (patientsRecordOxySat.Count == 0)
                    {
                        MessageBox.Show("No results for timeline selected!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Timeline selected is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


        }
        private bool readDateTimeGraphics()
        {
            fromDate = dateTimePickerFrom.Value;
            toDate = dateTimePickerTO.Value;

            if (fromDate > toDate)
            {
                return false;
            }
            return true;
        }
        private void checkBoxValues_CheckedChanged(object sender, EventArgs e)
        {
            readCheckBoxValue();
        }
        private void readCheckBoxValue()
        {
            try
            {
                bool sDiastolic = false;
                bool sSystolic = false;
                bool sHrate = false;
                bool sOxySat = false;

                foreach (Series a in chart1.Series)
                {
                    if (a.Name.Equals(DIASTOLIC))
                        sDiastolic = true;
                    if (a.Name.Equals(SYSTOLIC))
                        sSystolic = true;
                    if (a.Name.Equals(HRATE))
                        sHrate = true;
                    if (a.Name.Equals(OXYSAT))
                        sOxySat = true;
                }

                if (checkBoxValues.Checked)
                {
                    if (sDiastolic)
                        chart1.Series[DIASTOLIC].IsValueShownAsLabel = true;
                    if (sSystolic)
                        chart1.Series[SYSTOLIC].IsValueShownAsLabel = true;
                    if (sHrate)
                        chart1.Series[HRATE].IsValueShownAsLabel = true;
                    if (sOxySat)
                        chart1.Series[OXYSAT].IsValueShownAsLabel = true;
                }
                else
                {
                    if (sDiastolic)
                        chart1.Series[DIASTOLIC].IsValueShownAsLabel = false;
                    if (sSystolic)
                        chart1.Series[SYSTOLIC].IsValueShownAsLabel = false;
                    if (sHrate)
                        chart1.Series[HRATE].IsValueShownAsLabel = false;
                    if (sOxySat)
                        chart1.Series[OXYSAT].IsValueShownAsLabel = false;
                }
            }
            catch (ArgumentException a)
            {
                MessageBox.Show("No graphic", "INFO",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException c)
            {
                this.Close();
            }
        }
        public void startGraphics()
        {
            try
            {
                chart1?.Series.Clear();
                chart1?.ChartAreas.Clear();

                chart1.ChartAreas.Add(AREA1);
                List<int> totalValues = new List<int>();
                if (checkBoxSystolicSeries.Checked)
                {
                    List<string> horaBsysList = readValuesForBloodPressureS();
                    if (horaBsysList != null)
                        totalValues.Add(horaBsysList.Count);
                }

                if (checkBoxDiastolicSeries.Checked)
                {
                    List<string> horaBdiasList = readValuesForBloodPressureD();
                    if (horaBdiasList != null)
                        totalValues.Add(horaBdiasList.Count);
                }
                if (checkBoxHeartRateSeries.Checked)
                {
                    List<string> horaHrateList = readValuesForHeartRate();
                    if (horaHrateList != null)
                        totalValues.Add(horaHrateList.Count);
                }

                if (checkBoxOxySatSeries.Checked)
                {
                    List<string> horaOxySatList = readValuesForOxySat();
                    if (horaOxySatList != null)
                        totalValues.Add(horaOxySatList.Count);
                }
                totalValues.Sort();

                chart1.ChartAreas[AREA1].AxisX.Minimum = 1;
                if (totalValues.Count > 0)
                    chart1.ChartAreas[AREA1].AxisX.Maximum = totalValues.Last();
                //chart1.ChartAreas[AREA1].AxisX.Interval = 1;
                chart1.ChartAreas[AREA1].AxisX.LabelStyle.ForeColor = Color.Red;

                chart1.ChartAreas[AREA1].AxisY.Maximum = 200;
                chart1.ChartAreas[AREA1].AxisY.Interval = 10;
                chart1.ChartAreas[AREA1].AxisY.Title = "#Value";
                chart1.ChartAreas[AREA1].AxisX.Title = "Time";

                chart1.ChartAreas[AREA1].BackColor = Color.White;
                chart1.ChartAreas[AREA1].BackSecondaryColor = Color.LightBlue;
                chart1.ChartAreas[AREA1].BackGradientStyle =
                    System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
                chart1.ChartAreas[AREA1].AxisX.MajorGrid.LineColor = Color.LightSlateGray;
                chart1.ChartAreas[AREA1].AxisY.MajorGrid.LineColor = Color.LightSteelBlue;
            }
            catch (NullReferenceException c)
            {
                this.Close();
            }

        }
        private List<string> readValuesForOxySat()
        {
            List<int> valoresOxy = new List<int>();
            List<string> horaList = new List<string>();

            chart1.Series.Add(OXYSAT);

            chart1.Series[OXYSAT].Color = Color.Chartreuse;

            if (patientsRecordOxySat != null)
            {
                List<OxygenSaturation> valores =
                    patientsRecordOxySat.Where(i => i.Date >= fromDate && i.Date <= toDate)
                        .OrderBy(i => i.Date)
                        .ToList();

                double valorMedioSat = 0;
                int nrOfvalues = 0;

                for (int i = 0; i < valores.Count; i++)
                {
                    if (i > 0 && valores[i].Time.Minutes != valores[i - 1].Time.Minutes)
                    {
                        valorMedioSat = valorMedioSat / nrOfvalues;

                        valoresOxy.Add(Convert.ToInt32(valorMedioSat));
                        int day = valores[i].Date.Day - valores[i - 1].Date.Day;
                        string[] hour;
                        if (day != 0)
                        {
                            hour = valores[i].Date.ToString().Split(' ');
                            horaList.Add(hour[0]);
                        }
                        else
                        {
                            hour = valores[i].Time.ToString().Split(':');
                            horaList.Add(hour[0] + ":" + hour[1]);
                        }
                        valorMedioSat = 0;

                        nrOfvalues = 0;
                        nrOfvalues++;
                        valorMedioSat += valores[i].Saturation;
                    }
                    else
                    {
                        nrOfvalues++;
                        valorMedioSat += valores[i].Saturation;
                    }
                }
                chart1.Series[OXYSAT].Points.DataBindXY(horaList, valoresOxy);

                return horaList;
            }
            return null;
        }
        private List<string> readValuesForHeartRate()
        {
            List<int> valoresRate = new List<int>();
            List<string> horaList = new List<string>();

            chart1.Series.Add(HRATE);

            chart1.Series[HRATE].Color = Color.Purple;

            if (patientsRecordHeartRate != null)
            {
                List<HeartRate> valores =
                    patientsRecordHeartRate.Where(i => i.Date >= fromDate && i.Date <= toDate)
                        .OrderBy(i => i.Date)
                        .ToList();

                double valorMedioRate = 0;
                int nrOfvalues = 0;

                for (int i = 0; i < valores.Count; i++)
                {
                    if (i > 0 && valores[i].Time.Minutes != valores[i - 1].Time.Minutes)
                    {
                        valorMedioRate = valorMedioRate / nrOfvalues;

                        valoresRate.Add(Convert.ToInt32(valorMedioRate));
                        int day = valores[i].Date.Day - valores[i - 1].Date.Day;
                        string[] hour;
                        if (day != 0)
                        {
                            hour = valores[i].Date.ToString().Split(' ');
                            horaList.Add(hour[0]);
                        }
                        else
                        {
                            hour = valores[i].Time.ToString().Split(':');
                            horaList.Add(hour[0] + ":" + hour[1]);
                        }

                        valorMedioRate = 0;

                        nrOfvalues = 0;
                        nrOfvalues++;
                        valorMedioRate += valores[i].Rate;
                    }
                    else
                    {
                        nrOfvalues++;
                        valorMedioRate += valores[i].Rate;
                    }
                }
                chart1.Series[HRATE].Points.DataBindXY(horaList, valoresRate);
                return horaList;
            }
            return null;
        }
        private List<string> readValuesForBloodPressureD()
        {
            chart1.Series.Add(DIASTOLIC);
            chart1.Series[DIASTOLIC].Color = Color.Red;

            if (patientsRecordBloodPressure != null)
            {
                List<BloodPressure> valores =
                    patientsRecordBloodPressure.Where(i => i.Date >= fromDate && i.Date <= toDate)
                        .OrderBy(i => i.Date)
                        .ToList();

                List<double> valoresDistolic = new List<double>();

                List<string> horaList = new List<string>();
                double valorMedioDistolic = 0;

                int nrOfvalues = 0;

                for (int i = 0; i < valores.Count; i++)
                {

                    if (i > 0 && valores[i].Time.Minutes != valores[i - 1].Time.Minutes)
                    {
                        valorMedioDistolic = valorMedioDistolic / nrOfvalues;
                        valoresDistolic.Add(Convert.ToInt32(valorMedioDistolic));
                        int day = valores[i].Date.Day - valores[i - 1].Date.Day;
                        string[] hour;
                        if (day != 0)
                        {
                            hour = valores[i].Date.ToString().Split(' ');
                            horaList.Add(hour[0]);
                        }
                        else
                        {
                            hour = valores[i].Time.ToString().Split(':');
                            horaList.Add(hour[0] + ":" + hour[1]);
                        }
                        valorMedioDistolic = 0;
                        nrOfvalues = 0;
                        nrOfvalues++;
                        valorMedioDistolic += valores[i].Diastolic;
                    }
                    else
                    {
                        nrOfvalues++;
                        valorMedioDistolic += valores[i].Diastolic;
                    }
                }
                chart1.Series[DIASTOLIC].Points.DataBindXY(horaList, valoresDistolic);

                return horaList;
            }
            return null;
        }
        private List<string> readValuesForBloodPressureS()
        {
            chart1.Series.Add(SYSTOLIC);

            chart1.Series[SYSTOLIC].Color = Color.Blue;

            if (patientsRecordBloodPressure != null)
            {
                List<BloodPressure> valores =
                    patientsRecordBloodPressure.Where(i => i.Date >= fromDate && i.Date <= toDate)
                        .OrderBy(i => i.Date)
                        .ToList();

                List<double> valoresSystolic = new List<double>();
                List<string> horaList = new List<string>();
                double valorMedioSystolic = 0;
                int nrOfvalues = 0;

                for (int i = 0; i < valores.Count; i++)
                {

                    if (i > 0 && valores[i].Time.Minutes != valores[i - 1].Time.Minutes)
                    {
                        valorMedioSystolic = valorMedioSystolic / nrOfvalues;
                        valoresSystolic.Add(Convert.ToInt32(valorMedioSystolic));
                        int day = valores[i].Date.Day - valores[i - 1].Date.Day;
                        string[] hour;
                        if (day != 0)
                        {
                            hour = valores[i].Date.ToString().Split(' ');
                            horaList.Add(hour[0]);
                        }
                        else
                        {
                            hour = valores[i].Time.ToString().Split(':');
                            horaList.Add(hour[0] + ":" + hour[1]);
                        }

                        valorMedioSystolic = 0;
                        nrOfvalues = 0;
                        nrOfvalues++;
                        valorMedioSystolic += valores[i].Systolic;
                    }
                    else
                    {
                        nrOfvalues++;
                        valorMedioSystolic += valores[i].Systolic;
                    }
                }
                chart1.Series[SYSTOLIC].Points.DataBindXY(horaList, valoresSystolic);

                return horaList;
            }
            return null;
        }
        private void readComboChartTyper()
        {
            try
            {
                bool sDiastolic = false;
                bool sSystolic = false;
                bool sHrate = false;
                bool sOxySat = false;

                foreach (Series a in chart1.Series)
                {
                    if (a.Name.Equals(DIASTOLIC))
                        sDiastolic = true;
                    if (a.Name.Equals(SYSTOLIC))
                        sSystolic = true;
                    if (a.Name.Equals(HRATE))
                        sHrate = true;
                    if (a.Name.Equals(OXYSAT))
                        sOxySat = true;
                }

                switch (comboBoxChartType.Text)
                {
                    case LINES:
                        if (sDiastolic)
                        {
                            chart1.Series[DIASTOLIC].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        }
                        if (sSystolic)
                        {
                            chart1.Series[SYSTOLIC].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        }
                        if (sHrate)
                        {
                            chart1.Series[HRATE].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        }
                        if (sOxySat)
                        {
                            chart1.Series[OXYSAT].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        }
                        break;
                    case POINTS:
                        if (sDiastolic)
                        {
                            chart1.Series[DIASTOLIC].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                        }
                        if (sSystolic)
                        {
                            chart1.Series[SYSTOLIC].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                        }
                        if (sHrate)
                        {
                            chart1.Series[HRATE].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                        }
                        if (sOxySat)
                        {
                            chart1.Series[OXYSAT].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                        }
                        break;
                    case COLUMNS:
                        if (sDiastolic)
                        {
                            chart1.Series[DIASTOLIC].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        }
                        if (sSystolic)
                        {
                            chart1.Series[SYSTOLIC].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        }
                        if (sHrate)
                        {
                            chart1.Series[HRATE].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        }
                        if (sOxySat)
                        {
                            chart1.Series[OXYSAT].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        }
                        break;
                }
            }
            catch (NullReferenceException x)
            {
                this.Close();
            }
        }
        private void readRadioButtonsAlerts(Patient patient, Event typeEvent)
        {
            if (radioButtonBloodPressure.Checked)
            {
                if (radioButtonAll.Checked)
                {
                    warningListBPALL =
                        new List<BloodPressureWarning>(client.GetWarningListBloodPressureALL(fromDate, toDate,
                            patientOnMonitoring).OrderByDescending(i => i.Date));

                    dataGridViewAlerts.DataSource = warningListBPALL;
                    setGridViewAlerts();

                    labelResultsAlerts.Text = RESULTS + warningListBPALL.Count;
                }
                else
                {
                    warningListBloodPressure =
                   new List<BloodPressure>(client.GetWarningListBloodPressure(typeEvent, fromDate, toDate)
                       .Where(i => i.PatientSNS == patient.Sns
                       ).OrderByDescending(i => i.Date));

                    dataGridViewAlerts.DataSource = warningListBloodPressure;
                    setGridViewAlerts();

                    labelResultsAlerts.Text = RESULTS + warningListBloodPressure.Count;
                }
            }

            if (radioButtonHeartRate.Checked)
            {
                if (radioButtonAll.Checked)
                {
                    warningListHRALL =
                        new List<HeartRateWarning>(client.GetWarningListHeartRateALL(fromDate, toDate,
                            patientOnMonitoring).OrderByDescending(i => i.Date));

                    dataGridViewAlerts.DataSource = warningListHRALL;
                    setGridViewAlerts();

                    labelResultsAlerts.Text = RESULTS + warningListHRALL.Count;
                }
                else
                {
                    warningListHeartRate =
                        new List<HeartRate>(client.GetWarningListHeartRate(typeEvent, fromDate, toDate)
                            .Where(i => i.PatientSNS == patient.Sns
                            ).OrderByDescending(i => i.Date));

                    dataGridViewAlerts.DataSource = warningListHeartRate;
                    setGridViewAlerts();

                    labelResultsAlerts.Text = RESULTS + warningListHeartRate.Count;
                }
            }

            if (radioButtonOxygenSat.Checked)
            {
                if (radioButtonAll.Checked)
                {
                    warningListOXYSATALL =
                        new List<OxygenSaturationWarning>(client.GetWarningListOxygenSaturationALL(fromDate, toDate,
                            patientOnMonitoring).OrderByDescending(i => i.Date));

                    dataGridViewAlerts.DataSource = warningListOXYSATALL;
                    setGridViewAlerts();

                    labelResultsAlerts.Text = RESULTS + warningListOXYSATALL.Count;
                }
                else
                {
                    warningListOxygenSaturation =
                        new List<OxygenSaturation>(client.GetWarningListOxygenSaturation(typeEvent, fromDate, toDate)
                            .Where(i => i.PatientSNS == patient.Sns
                            ).OrderByDescending(i => i.Date));

                    dataGridViewAlerts.DataSource = warningListOxygenSaturation;
                    setGridViewAlerts();

                    labelResultsAlerts.Text = RESULTS + warningListOxygenSaturation.Count;
                }
            }
        }

        private void setGridViewAlerts()
        {
            dataGridViewAlerts.RowHeadersVisible = false;
            if (!radioButtonAll.Checked)
            {
                dataGridViewAlerts.Columns[cPATIENTSNS].Visible = false;
                dataGridViewAlerts.Columns[TIME].Visible = false;
                dataGridViewAlerts.Columns["Date"].Width = 125;
            }
        }
     
        #endregion

        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToLongTimeString();
        }

        public static ServiceHealthAlertClient GetClient()
        {
            return client;
        }

    }
}
