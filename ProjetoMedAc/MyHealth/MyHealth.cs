using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhysiologicParametersDll;

namespace MyHealth
{
    public partial class MyHealth : Form
    {
        private PhysiologicParametersDll.PhysiologicParametersDll dll;
        private bool bloodPressure_checked;
        private bool saturation_checked;
        private bool heartRate_checked;

        public MyHealth()
        {
            InitializeComponent();
        }

        #region Events

        private void MyHealth_Load(object sender, EventArgs e)
        {
            this.dll = new PhysiologicParametersDll.PhysiologicParametersDll();
            tb_patientId.Text = Properties.Settings.Default.Patient_Id.ToString();
            DataLabels(false);
        }

        private void bt_validate_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(tb_patientId.Text, out id))
            {
                
            }
        }

        private void bt_startMonitoring_Click(object sender, EventArgs e)
        {
        }

        private void StartMonitoring()
        {
            dll.Initialize(MyProcessMethod, 1000, bloodPressure_checked, saturation_checked, heartRate_checked);

        }

        private void MyProcessMethod(string message)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                string[] info = message.Split(';');
                string type = info[0];
                string data = info[1];
                TimeSpan date = Convert.ToDateTime(info[2]).TimeOfDay;

                switch (type)
                {
                    case "BP":
                        string[] bp = data.Split('-');
                        int systolic = Convert.ToInt32(bp[0]);
                        int diastolic = Convert.ToInt32(bp[1]);

                        tb_bloodPressure.Text = "Systolic: " + systolic + " - Diastolic: " + diastolic;
                        lb_dataBP.Text = date.ToString();
                        break;

                    case "SPO2":
                        int sat = Convert.ToInt32(data);
                        tb_saturation.Text = sat + "%";
                        lb_data_o2.Text = date.ToString();
                        break;

                    case "HR":
                        int hr = Convert.ToInt32(data);
                        tb_heartRate.Text = hr + "bpm";
                        lb_data_HR.Text = date.ToString();
                        break;
                }

            }));
           
        }

        private void MyHealth_FormClosing(object sender, FormClosingEventArgs e)
        {
            dll.Stop();
        }

        private void cb_bloodPressure_Click(object sender, EventArgs e)
        {
            

        }

        private void cb_saturations_Click(object sender, EventArgs e)
        {
        }

        private void cb_heartRate_Click(object sender, EventArgs e)
        {
            
        }


        #endregion Events

        #region Methods

        private void DataLabels(bool visible)
        {
            lb_dataBP.Visible = visible;
            lb_data_o2.Visible = visible;
            lb_data_HR.Visible = visible;
        }


        #endregion Methods

        private void cb_bloodPressure_MouseClick(object sender, MouseEventArgs e)
        {
            bloodPressure_checked = cb_bloodPressure.Checked;
            lb_dataBP.Visible = cb_bloodPressure.Checked;
            lb_dataBP.Text = "Receiving...";
            StartMonitoring();
        }

        private void cb_saturations_MouseClick(object sender, MouseEventArgs e)
        {
            saturation_checked = cb_saturations.Checked;
            lb_data_o2.Visible = cb_saturations.Checked;
            lb_data_o2.Text = "Receiving...";
            StartMonitoring();
        }

        private void cb_heartRate_MouseClick(object sender, MouseEventArgs e)
        {
            heartRate_checked = cb_heartRate.Checked;
            lb_data_HR.Visible = cb_heartRate.Checked;
            lb_data_HR.Text = "Receiving...";
            StartMonitoring();
        }

    }
}
