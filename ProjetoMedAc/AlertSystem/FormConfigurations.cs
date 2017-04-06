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
    public partial class FormConfigurations : Form
    {
        public FormConfigurations()
        {
            InitializeComponent();
        }

        private void FormConfigurations_Load(object sender, EventArgs e)
        {

        }

        private void bt_save_Click(object sender, EventArgs e)
        {

        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {

        }

        private void FillValues()
        {
            ServiceHealthAlertClient client = FormAlertSystem.GetClient();
            List<ConfigurationLimitType> configurationLimitList = client.GetConfigurationLimitList();
            List<Event> eventsList = client.GetEventList();

            foreach (ConfigurationLimitType limitType in configurationLimitList)
            {
                switch (limitType.ConfigurationType.ToString())
                {
                    case "HR":
                        numbNormalRateMin.Value = limitType.MinimumValue;
                        numbNormalRateMax.Value = limitType.MaximumValue;
                        numbAnyTimeRateMin.Value = limitType.MinimumCriticalValue;
                        numbAnyTimeRateMax.Value = limitType.MaximumCriticalValue;
                        break;

                    case "SPO2":
                        numbNormalSaturation.Value = limitType.MinimumValue;
                        numbAnyTimeSaturation.Value = limitType.MinimumCriticalValue;
                        break;

                    case "BP":
                        numbNormalDiastolic.Value = limitType.MinimumValue;
                        numbNormalSystolic.Value = limitType.MaximumValue;
                        numbAnyTimeDiastolic.Value = limitType.MinimumCriticalValue;
                        numbAnyTimeSystolic.Value = limitType.MaximumCriticalValue;
                        break;
                }
            }
        }

        private bool ValdiadeParameters()
        {
            return true;
        }
        
    }
}
