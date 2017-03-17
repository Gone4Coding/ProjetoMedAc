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

            toolStripComboBox.Items.Add("Sns");
            toolStripComboBox.Items.Add("Phone");
            toolStripComboBox.Items.Add("Nome");

            toolStripComboBox.SelectedIndex = 0;

            load();

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
            clearFields();
            bt_edit.Hide();
            bt_cancelEdit.Hide();
            bt_save.Hide();
            buttonAdd.Show();
            bt_cancelAdd.Show();
            dataGridViewPatients.ClearSelection();
            dataGridViewPatients.Enabled = false;
            enableTextBoxes(true);

        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            load();
        }

        private void bt_edit_Click(object sender, EventArgs e)
        {
            enableTextBoxes(true);
            bt_save.Enabled = true;
            bt_edit.Enabled = false;
            bt_cancelEdit.Show();
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
            bt_edit.Enabled = true;
            bt_save.Enabled = false;
            bt_cancelEdit.Hide();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.Name =tb_firstname.Text;
            p.Surname= tb_lastName.Text;
            p.BirthDate= dateTimePicker_birthdate.Value;
            p.Nif = Convert.ToInt32(tb_nif.Text) ;
            p.Sns = Convert.ToInt32(tb_sns.Text);
            if (tb_phone.Text.Equals(" "))   
                p.Phone = Convert.ToInt32(tb_phone.Text);
            p.Email = tb_email.Text;
            p.EmergencyNumber = Convert.ToInt32(tb_emergencyContact.Text);
            p.EmergencyName = tb_emergencyContactName.Text ;
            p.Adress = richTextBox_address.Text ;
            p.Gender = comboBoxGender.Text ;
            if(tb_height.Text.Equals(" "))
                p.Height = Convert.ToInt32(tb_height.Text) ;
            if (tb_weight.Text.Equals(" "))
                p.Weight = Convert.ToDouble(tb_weight.Text);
            p.Alergies= richTextBoxAlergies.Text;

            bool res = client.InsertPatient(p);

            if (!res)
            {
                MessageBox.Show("erro");
            }
            else
            {
                clearFields();
                load();
            }
        }

        private void dataGridViewPatients_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName;

            switch (e.ColumnIndex)
            {
                   case 
            }

        }

        #endregion

        #region Metodos

        private void load()
        {
            bt_edit.Show();
            bt_edit.Enabled = true;
            bt_save.Show();
            bt_save.Enabled = false;
            bt_cancelAdd.Hide();
            bt_cancelEdit.Hide();
            buttonAdd.Hide();
            dataGridViewPatients.Enabled = true;
            fillGridView();
            fillFirstSelected();
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

       
        private void fillGridView()
        {
            List<Patient> patients = new List<Patient>(client.GetPatientList());
          
            dataGridViewPatients.DataSource = patients;

            for (int i = 0; i < dataGridViewPatients.ColumnCount; i++)
            {
                if (i != 6 && i != 9 && i != 10 && i != 11)
                {
                    dataGridViewPatients.Columns[i].Visible = false;
                }              
            }

            dataGridViewPatients.Columns[6].DisplayIndex = 0;
            dataGridViewPatients.Columns[11].DisplayIndex = 1;
            dataGridViewPatients.Columns[9].DisplayIndex = 2;
            dataGridViewPatients.Columns[10].DisplayIndex = 3;

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
            //dateTimePicker_birthdate.Value = patient.
            tb_nif.Text = patient.Nif.ToString();
            tb_sns.Text = patient.Sns.ToString();
            tb_phone.Text = patient.Phone.ToString();
            tb_email.Text = patient.Email;
            tb_emergencyContact.Text = patient.EmergencyNumber.ToString();
            tb_emergencyContactName.Text = patient.EmergencyName;
            richTextBox_address.Text = patient.Adress;
            comboBoxGender.Text = patient.Gender;
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
