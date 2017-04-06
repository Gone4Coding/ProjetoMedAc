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
                    row.DefaultCellStyle.BackColor = Color.Chocolate;
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
    }
}
