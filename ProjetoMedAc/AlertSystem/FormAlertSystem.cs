using System;
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
        private static string pagePatient = "Patients";
        private static string pageViewRecord = "View Records";
        private static string pageManageRecords = "Manage Records";

        private ServiceHealthAlertClient client;
        public FormAlertSystem()
        {        
            InitializeComponent();
            client = new ServiceHealthAlertClient();
        }

        #region Eventos

        private void tabControlRecors_SelectedIndexChanged(object sender, EventArgs e)
        {

            MessageBox.Show(tabControlRecors.SelectedTab.TabIndex.ToString());

        }

        private void FormAlertSystem_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            #region PatientsPage

            comboBoxGender.Items.Add("Female");
            comboBoxGender.Items.Add("Male");

            fillGridView();
            fillFirstSelected();

            buttonAdd.Hide();
            bt_cancel.Hide();
            enableTextBoxes(false);

            #endregion
        }

        private void dataGridViewPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sns = Convert.ToInt32(dataGridViewPatients.Rows[0].Cells["Sns"].Value);
            Patient patientSelected = client.GetPatient(sns);

            fillFields(patientSelected);
        }
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            clearFields();
            bt_edit.Hide();
            buttonAdd.Show();
            bt_cancel.Show();
            dataGridViewPatients.ClearSelection();
            dataGridViewPatients.Enabled = false;
            enableTextBoxes(true);

        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            bt_edit.Show();
            bt_cancel.Hide();
            buttonAdd.Hide();
            dataGridViewPatients.Enabled = true;
            fillGridView();
            enableTextBoxes(false);
        }

        #endregion

        #region Metodos

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

        private void bt_edit_Click(object sender, EventArgs e)
        {
            enableTextBoxes(true);
        }
        private void fillGridView()
        {
            List<Patient> patients = new List<Patient>(client.GetPatientList());
          
            dataGridViewPatients.DataSource = patients;

            for (int i = 0; i < dataGridViewPatients.ColumnCount; i++)
            {
                if(i!=6 && i!= 9 && i!=10 && i!=11)
                    dataGridViewPatients.Columns[i].Visible = false;
            }

            dataGridViewPatients.Columns[6].DisplayIndex = 0;
            dataGridViewPatients.Columns[11].DisplayIndex = 1;
            dataGridViewPatients.Columns[9].DisplayIndex = 2;
            dataGridViewPatients.Columns[10].DisplayIndex = 3;

            dataGridViewPatients.RowHeadersVisible = false;

           // dataGridViewPatients.ClearSelection();
        }

        private void fillFirstSelected()
        {
            int sns = Convert.ToInt32(dataGridViewPatients.Rows[0].Cells["Sns"].Value);
            Patient patientSelected = client.GetPatient(sns);

            fillFields(patientSelected);
        }

        private void fillFields(Patient patient)
        {
            tb_firstname.Text = patient.Name;
            tb_lastName.Text = patient.Surname;
            //dateTimePicker_birthdate.Value = patient.
            tb_nif.Text = patient.Nif.ToString();
            tb_sns.Text = patient.Sns.ToString();
            tb_phone.Text = patient.Phone;
            tb_email.Text = patient.Email;
            tb_emergencyContact.Text = patient.EmergencyNumber;
            tb_emergencyContactName.Text = patient.EmergencyName;
            richTextBox_address.Text = patient.Adress;
            comboBoxGender.Text = patient.Sex;
            tb_height.Text = patient.Height.ToString();
            tb_weight.Text = patient.Weight.ToString();
            richTextBoxAlergies.Text = patient.Alergies;
        }

        private void clearFields()
        {
            tb_firstname.Clear();
            tb_lastName.Clear();
            //dateTimePicker_birthdate.Value = patient.
            tb_nif.Clear();
            tb_sns.Clear();
            tb_phone.Clear();
            tb_email.Clear();
            tb_emergencyContact.Clear();
            tb_emergencyContactName.Clear();
            richTextBox_address.Clear();
            comboBoxGender.Text = " ";
            tb_height.Clear();
            tb_weight.Clear();
            richTextBoxAlergies.Clear();
        }
        #endregion

      
    }
}
