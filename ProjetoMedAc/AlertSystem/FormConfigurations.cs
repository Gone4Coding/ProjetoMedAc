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
        private int bpNormalDiastolic;
        private int bpNormalSystolic;
        private int bpAnyTimeDiastolic;
        private int bpAnyTimeSystolic;
        private int satNormal;
        private int satAnyTime;
        private int rateNormalMin;
        private int rateNormalMax;
        private int rateAnyTimeMin;
        private int rateAnyTimeMax;
        private int eac;
        private int eaiMin;
        private int eaiMax;
        private int ecc;
        private int eciMin;
        private int eciMax;
        private ServiceHealthAlertClient client;

        public FormConfigurations()
        {
            this.client = FormAlertSystem.GetClient();
            InitializeComponent();
        }

        private void FormConfigurations_Load(object sender, EventArgs e)
        {
            FillValues();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (ValdiadeParameters())
            {
                //ConfigurationLimitType bp = client.GetConfigurationLimit(ConfigurationLimitType.Type.BP);

            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FillValues()
        {
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

            foreach (Event eventType in eventsList)
            {
                switch (eventType.EvenType)
                {
                    case Event.Type.ECC:
                        numConCritical.Value = eventType.MinimumTime;
                        break;

                    case Event.Type.ECI:
                        numIntCriticalMin.Value = eventType.MinimumTime;
                        numIntCriticalMax.Value = eventType.MaximumTime;
                        break;

                    case Event.Type.EAC:
                        numConWarning.Value = eventType.MinimumTime;
                        break;

                    case Event.Type.EAI:
                        numIntWarningMin.Value = eventType.MinimumTime;
                        numIntWarningMax.Value = eventType.MaximumTime;
                        break;
                }
            }
        }

        private bool ValdiadeParameters()
        {
            bpNormalDiastolic = Convert.ToInt32(numbNormalDiastolic.Value);
            bpNormalSystolic = Convert.ToInt32(numbNormalSystolic.Value);
            bpAnyTimeDiastolic = Convert.ToInt32(numbAnyTimeDiastolic);
            bpAnyTimeSystolic = Convert.ToInt32(numbAnyTimeSystolic);
            satNormal = Convert.ToInt32(numbNormalSaturation.Value);
            satAnyTime = Convert.ToInt32(numbAnyTimeSaturation.Value);
            rateNormalMin = Convert.ToInt32(numbNormalRateMin.Value);
            rateNormalMax = Convert.ToInt32(numbNormalRateMax.Value);
            rateAnyTimeMin = Convert.ToInt32(numbAnyTimeRateMin.Value);
            rateAnyTimeMax = Convert.ToInt32(numbAnyTimeRateMax.Value);
            eac = Convert.ToInt32(numConWarning.Value);
            eaiMin = Convert.ToInt32(numIntWarningMin.Value);
            eaiMax = Convert.ToInt32(numIntWarningMax.Value);
            ecc = Convert.ToInt32(numConCritical.Value);
            eciMin = Convert.ToInt32(numIntCriticalMin.Value);
            eciMax = Convert.ToInt32(numIntCriticalMax.Value);

            if (bpNormalDiastolic == 0 || bpNormalSystolic == 0 || bpAnyTimeDiastolic == 0 || bpAnyTimeSystolic == 0 ||
                satNormal == 0 || satAnyTime == 0 || rateNormalMin == 0 || rateNormalMax == 0 || rateAnyTimeMin == 0 ||
                rateAnyTimeMax == 0 || eac == 0 || eaiMin == 0 || eaiMax == 0 || ecc == 0 || eciMin == 0 || eciMax == 0)
            {
                GiveWarning("It can´t be values equal to 0");
                return false;
            }

            if (bpNormalDiastolic >= bpNormalSystolic)
            {
                GiveWarning("The Normal Diastolic value can´t be greater than Normal Systolic value");
                return false;
            }

            if (bpAnyTimeDiastolic >= bpAnyTimeSystolic)
            {
                GiveWarning("The Any Time Diastolic value can´t be greater than Any Time Systolic value");
                return false;
            }

            if (bpAnyTimeDiastolic >= bpNormalSystolic || bpAnyTimeDiastolic >= bpNormalDiastolic)
            {
                GiveWarning("None of Any Time values of Blood Pressure can be greater\nthan Normal values of Blood Pressure");
                return false;
            }

            if (satAnyTime >= satNormal)
            {
                GiveWarning("Any Time Saturation value can´t be greater than Normal Saturation value");
                return false;
            }

            if (rateNormalMin >= rateNormalMax)
            {
                GiveWarning("Heart Rate Normal Rate minimum value can´t be greater than the maximum value");
                return false;
            }

            if (rateAnyTimeMin >= rateAnyTimeMax)
            {
                GiveWarning("Heart Rate Any Time Rate minimum value can´t be greater than the maximum value");
                return false;
            }

            if (rateAnyTimeMin >= rateNormalMin || rateAnyTimeMax <= rateNormalMax)
            {
                GiveWarning("None of Any Time Heart Rate values can be\ninside Normal Heart Rate values");
                return false;
            }

            if (eaiMin >= eaiMax)
            {
                GiveWarning("The minimum value for an Intermittent Warning\ncan´t be greater than the full range (maximum)");
                return false;
            }

            if (eciMin >= eciMax)
            {
                GiveWarning("The minimum value for an Intermittent Critical\ncan´t be greater than the full range (maximum)");
                return false;
            }

            return true;
        }

        private void GiveWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        
    }
}
