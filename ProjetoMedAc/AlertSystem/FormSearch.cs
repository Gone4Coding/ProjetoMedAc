using System;
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
    public partial class FormSearch : Form
    {
        private List<Patient> activePatients;
        private ServiceHealthAlertClient client;
        private Patient patientSelected;
        public FormSearch()
        {
            InitializeComponent();
            client = new ServiceHealthAlertClient();           
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            this.CenterToParent();

            activePatients = new List<Patient>(client.GetPatientList().Where(i=> i.Ativo).ToList());

            fillGridView(activePatients);

            comboBoxSearch.Items.Add("SNS");
            comboBoxSearch.Items.Add("NAME");
            comboBoxSearch.Items.Add("NIF");

            comboBoxSearch.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            patientSelected = null;
            this.Close();
        }

        private void fillGridView(List<Patient> patients)
        {
            dataGridViewActivePatients.DataSource = patients;

            for (int i = 0; i < dataGridViewActivePatients.ColumnCount; i++)
            {
                if (i != 14 && i != 10 && i != 15 && i != 2 && i != 8)
                {
                    dataGridViewActivePatients.Columns[i].Visible = false;
                }
            }

            foreach (DataGridViewRow row in dataGridViewActivePatients.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Ativo"].Value))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }
            }

            dataGridViewActivePatients.Columns[14].DisplayIndex = 0;
            dataGridViewActivePatients.Columns[10].DisplayIndex = 1;
            dataGridViewActivePatients.Columns[15].DisplayIndex = 2;
            dataGridViewActivePatients.Columns[8].DisplayIndex = 3;
            dataGridViewActivePatients.Columns[2].DisplayIndex = 4;

            dataGridViewActivePatients.RowHeadersVisible = false;

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            readSelected();
            this.Close();
        }

        private void dataGridViewActivePatients_SelectionChanged(object sender, EventArgs e)
        {
           readSelected();
        }

        private void readSelected()
        {
            if (dataGridViewActivePatients.Rows.Count > 0 & dataGridViewActivePatients.CurrentCell != null)
            {
                int index = dataGridViewActivePatients.CurrentCell.RowIndex;
                int sns = Convert.ToInt32(dataGridViewActivePatients.Rows[index].Cells["Sns"].Value);
                patientSelected = activePatients.First(i => i.Sns == sns);
            }
        }

        public Patient getPatientSearched()
        {
            return patientSelected;
        }

        private void buttonokSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                readSearch();
            }
        }

        private bool validateSearch()
        {
            if (textBoxSearch.Text.Equals("") || comboBoxSearch.SelectedIndex == -1)
            {
                if (textBoxSearch.Text.Equals(""))
                {
                    MessageBox.Show("Field with value can't be empty", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                if (comboBoxSearch.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a search type", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (comboBoxSearch.Text.Equals("SNS") && !isNumber(textBoxSearch.Text) ||
                    comboBoxSearch.Text.Equals("NIF") && !isNumber(textBoxSearch.Text))
                {
                    MessageBox.Show("For this type of search, value has to be a number ", "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    if (textBoxSearch.Text.Length < 9 && isNumber(textBoxSearch.Text))
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

        private void readSearch()
        {
            string type = comboBoxSearch.Text;
            List<Patient> pSearched;

            if (type.Equals("SNS"))
            {
                pSearched = activePatients.Where(i => i.Sns == Convert.ToInt32(textBoxSearch.Text)).ToList();
                if (pSearched != null)
                {
                    dataGridViewActivePatients.DataSource = pSearched;
                }
                else
                {
                    MessageBox.Show("No results!", "INFO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            if (type.Equals("NIF"))
            {
                pSearched = activePatients.Where(i => i.Nif == Convert.ToInt32(textBoxSearch.Text)).ToList();

                if (pSearched != null)
                {
                    dataGridViewActivePatients.DataSource = pSearched;
                }
                else
                {
                    MessageBox.Show("No results!", "INFO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            if (type.Equals("NAME"))
            {
                string[] splited = textBoxSearch.Text.Split(' ');
                List<Patient> pByName;
                if (splited.Length > 1)
                {
                     pByName =
                        activePatients.Where(i => i.Name.ToLower().Contains(splited[0].ToLower()) || i.Surname.ToLower().Contains(splited[1].ToLower()))
                            .ToList();
                }
                else
                {
                    pByName =
                       activePatients.Where(i => i.Name.ToLower().Contains(splited[0].ToLower()) || i.Surname.ToLower().Contains(splited[0].ToLower()))
                           .ToList();
                }

                dataGridViewActivePatients.DataSource = pByName;
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
    }
}
