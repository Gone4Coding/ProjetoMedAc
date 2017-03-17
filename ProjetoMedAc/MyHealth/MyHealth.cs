using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PhysiologicParametersDll;
using System.Speech;
using System.Speech.Recognition;
using MyHealth.VoiceRecognition;
using System.Speech.Synthesis;
using System.Threading.Tasks;


namespace MyHealth
{
    public partial class MyHealth : Form
    {
        private SpeechSynthesizer synth;
        private PromptBuilder pBuilder;
        private SpeechRecognitionEngine sReconEngine;
        private PhysiologicParametersDll.PhysiologicParametersDll dll;
        private bool bloodPressure_checked;
        private bool saturation_checked;
        private bool heartRate_checked;
        private bool speechActive = false;
        private Stopwatch iteration = new Stopwatch();

        public MyHealth()
        {
            InitializeComponent();
        }

        private void MyHealth_Load(object sender, EventArgs e)
        {
            this.dll = new PhysiologicParametersDll.PhysiologicParametersDll();
            this.sReconEngine = new SpeechRecognitionEngine();
            this.synth = new SpeechSynthesizer();
            this.pBuilder = new PromptBuilder();
            InitializeSpeech();

            tb_patientId.Text = Properties.Settings.Default.Patient_Id.ToString();
            DataLabels(false);
        }

        #region Events

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

        private void cb_bloodPressure_MouseClick(object sender, MouseEventArgs e)
        {
            ActivateBloodPressureMonitoring(cb_bloodPressure.Checked);
        }

        private void cb_saturations_MouseClick(object sender, MouseEventArgs e)
        {
            ActivateSaturationMonitoring(cb_saturations.Checked);
        }

        private void cb_heartRate_MouseClick(object sender, MouseEventArgs e)
        {
            ActivateHeartRateMonitoring(cb_heartRate.Checked);
        }

        #endregion Events

        #region Methods

        private void DataLabels(bool visible)
        {
            lb_dataBP.Visible = visible;
            lb_data_o2.Visible = visible;
            lb_data_HR.Visible = visible;
        }

        private void InitializeSpeech()
        {
            sReconEngine.LoadGrammar(VoiceRecognition.VoiceRecognition.GetGrammar());
            sReconEngine.SetInputToDefaultAudioDevice();
            sReconEngine.RecognizeAsync(RecognizeMode.Multiple);
            sReconEngine.SpeechRecognized += Srecon_SpeechRecognized;
        }

        private void Srecon_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.HelloMyHealth.ToString()))
            {
                speechActive = true;
                checkBox1.Checked = true;
                //RecognitionWaiting();
            }

            if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Bye.ToString()))
            {
                speechActive = false;
                checkBox1.Checked = false;
            }

            if (speechActive)
            {
                if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.EnableAll.ToString()))
                {
                    ActivateHeartRateMonitoring(true);
                    ActivateSaturationMonitoring(true);
                    ActivateBloodPressureMonitoring(true);
                }
                else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.DisableAll.ToString()))
                {
                    ActivateHeartRateMonitoring(false);
                    ActivateSaturationMonitoring(false);
                    ActivateBloodPressureMonitoring(false);
                }
                
            }
        }

        private void ActivateHeartRateMonitoring(bool check)
        {
            cb_heartRate.Checked = check;
            heartRate_checked = check;
            lb_data_HR.Visible = check;
            lb_data_HR.Text = "Receiving...";
            StartMonitoring();
        }

        private void ActivateSaturationMonitoring(bool check)
        {
            cb_saturations.Checked = check;
            saturation_checked = check;
            lb_data_o2.Visible = check;
            lb_data_o2.Text = "Receiving...";
            StartMonitoring();
        }

        private void ActivateBloodPressureMonitoring(bool check)
        {
            cb_bloodPressure.Checked = check;
            bloodPressure_checked = check;
            lb_dataBP.Visible = check;
            lb_dataBP.Text = "Receiving...";
            StartMonitoring();
        }

        /*private void RecognitionWaiting()
        {
            while (speechActive)
            {
                iteration.Start();

                tb_first.BackColor = Color.Blue;

                if(iteration.ElapsedMilliseconds/60 % 2 == 0)
                    tb_first.BackColor = Color.Black;

            }
        }*/

        /*private Task RecognitionWaiting()
        {
            return MethodAsyncInternal();
        }

        private async Task MethodAsyncInternal()
        {
            if (speechActive)
            {
                
            }
        }*/

        #endregion Methods


    }
}
