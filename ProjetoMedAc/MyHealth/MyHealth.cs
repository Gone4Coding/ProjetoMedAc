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
        #region Variables 

        private SpeechSynthesizer synth;
        private PromptBuilder pBuilder;
        private SpeechRecognitionEngine sReconEngine;
        private PhysiologicParametersDll.PhysiologicParametersDll dll;
        private bool bloodPressure_checked;
        private bool saturation_checked;
        private bool heartRate_checked;
        private bool speechActive = false;
        private Stopwatch iteration = new Stopwatch();
        private bool closeQuestion = false;
        private bool setId = false;

        #endregion

        #region General

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
            HideLabels(false);
        }

        private void MyHealth_FormClosing(object sender, FormClosingEventArgs e)
        {
            dll.Stop();
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

            #region Begin/End
            if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.HelloAlice.ToString()) && !speechActive)
            {
                speechActive = true;
                checkBox1.Checked = true;
                Speak("Hi.");
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Bye.ToString()))
            {
                speechActive = false;
                checkBox1.Checked = false;
            }
            #endregion Begin/End

            if (speechActive)
            {
                if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Alice.ToString()))
                    Speak("Yes?");

                // Commands that starts with an S
                if(speech.StartsWith("S")) CommandsWithS(speech);

                #region "CLOSE" QUESTION
                if (closeQuestion)
                {
                    if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Yes.ToString()))
                    {
                        Close();
                    }
                    else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.No.ToString()))
                    {
                        closeQuestion = false;
                    }
                }
                #endregion "CLOSE" QUESTION
                

                #region "SET ID"/"CHANGE ID" QUESTION
                if (setId)
                {
                    int sns;
                    if (int.TryParse(speech, out sns))
                    {
                        Speak(sns.ToString());
                    }
                    else
                    {
                        Speak("That's not a number");
                        setId = false;
                    }
                }
                #endregion "SET ID"/"CHANGE ID" QUESTION


                if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Find.ToString()))
                {

                }
                else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.FindTerms.ToString()))
                {

                }
                else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Close.ToString()))
                {
                    Speak("You sure?");
                    closeQuestion = true;
                }
                else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.GotoHome.ToString()))
                {
                    mainTabing.SelectedTab = home;
                }
                else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.GotoUserInformation.ToString()))
                {
                    mainTabing.SelectedTab = personalData;
                }
                else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.GotoConfigurations.ToString()))
                {
                    mainTabing.SelectedTab = configurations;
                }
            }
        }

        private void CommandsWithS(string speech)
        {
            if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StartMonitoring.ToString()))
            {
                ActivateHeartRateMonitoring(true);
                ActivateSaturationMonitoring(true);
                ActivateBloodPressureMonitoring(true);
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StopMonitoring.ToString()))
            {
                ActivateHeartRateMonitoring(false);
                ActivateSaturationMonitoring(false);
                ActivateBloodPressureMonitoring(false);
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StartBloodPressure.ToString()))
            {
                ActivateBloodPressureMonitoring(true);
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StopBloodPressure.ToString()))
            {
                ActivateBloodPressureMonitoring(false);

            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StartHeartRate.ToString()))
            {
                ActivateHeartRateMonitoring(true);
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StopHeartRate.ToString()))
            {
                ActivateHeartRateMonitoring(false);
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StartOxygenSaturation.ToString()))
            {
                ActivateSaturationMonitoring(true);
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.StopOxygenSaturation.ToString()))
            {
                ActivateSaturationMonitoring(false);
            }
            else if (speech.Contains(VoiceRecognition.VoiceRecognition.Code.SetId.ToString())
                      || speech.Contains(VoiceRecognition.VoiceRecognition.Code.ChangeId.ToString()))
            {
                //TODO MUDAR SNS
                Speak("Tell me the number.");
                setId = true;
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.SearchInMedline.ToString()))
            {
                //TODO ABRIR O FORM PARA PROCURAR NO MEDLINE
                mainTabing.SelectedTab = medline;
            }
        }

        private void Speak(string phrase)
        {
            string genderProperties = Properties.Settings.Default.Gender_Voice;
            VoiceGender gender = (genderProperties.Equals("Male")) ? VoiceGender.Male : VoiceGender.Female;

            int rate = Properties.Settings.Default.Voice_Rate;

            pBuilder.ClearContent();
            pBuilder.AppendText(phrase);
            synth.SelectVoiceByHints(gender, VoiceAge.Adult);
            synth.Rate = rate;
            synth.SpeakAsync(pBuilder);
        }

        #endregion General

        #region TabHome

        private void HideLabels(bool visible)
        {
            lb_dateBP.Visible = visible;
            lb_date_o2.Visible = visible;
            lb_date_HR.Visible = visible;
            lb_dataBP.Visible = visible;
            lb_dataHR.Visible = visible;
            lb_dataSPO2.Visible = visible;
            lb_userName.Visible = visible;
        }

        private void ActivateHeartRateMonitoring(bool check)
        {
            if (!check) lb_dataHR.Text = "";
            cb_heartRate.Checked = check;
            heartRate_checked = check;
            lb_date_HR.Visible = check;
            lb_date_HR.Text = "Receiving...";
            lb_dataHR.Visible = check;
            lb_dataHR.Text = "Receiving...";
            StartMonitoring();
        }

        private void ActivateSaturationMonitoring(bool check)
        {
            if (!check) lb_dataSPO2.Text = "";
            cb_saturations.Checked = check;
            saturation_checked = check;
            lb_date_o2.Visible = check;
            lb_date_o2.Text = "Receiving...";
            lb_dataSPO2.Visible = check;
            lb_dataSPO2.Text = "Receiving...";
            StartMonitoring();
        }

        private void ActivateBloodPressureMonitoring(bool check)
        {
            if (!check) lb_dataBP.Text = "";
            cb_bloodPressure.Checked = check;
            bloodPressure_checked = check;
            lb_dateBP.Visible = check;
            lb_dateBP.Text = "Receiving...";
            lb_dataBP.Visible = check;
            lb_dataBP.Text = "Receiving...";
            StartMonitoring();
        }

        private void bt_insert_Click(object sender, EventArgs e)
        {
            int sns;
            if (int.TryParse(tb_patientId.Text, out sns))
            {

            }
            else
            {
                MessageBox.Show("Wrong format!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void StartMonitoring()
        {
            dll.Initialize(DataParser, 1000, bloodPressure_checked, saturation_checked, heartRate_checked);
        }

        private void DataParser(string message)
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

                        lb_dataBP.Text = diastolic + "/" + systolic;
                        lb_dateBP.Text = date.ToString();
                        break;

                    case "SPO2":
                        int sat = Convert.ToInt32(data);
                        lb_dataSPO2.Text = sat + "%";
                        lb_date_o2.Text = date.ToString();
                        break;

                    case "HR":
                        int hr = Convert.ToInt32(data);
                        lb_dataHR.Text = hr + "bpm";
                        lb_date_HR.Text = date.ToString();
                        break;
                }

            }));

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

        #endregion

        #region TabUserInformation

        #endregion

        #region TabMedLine

        private void bt_searchMedLine_Click(object sender, EventArgs e)
        {
            Navigate();
        }

        private void Navigate()
        {
            string url = tb_url.Text;
            browser.Navigate(url);
        }

        #endregion TabMedLine

        #region TabConfiguration

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (!rb_male.Checked && !rb_female.Checked)
                MessageBox.Show("Please select a gender for the voice", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (rb_male.Checked)
                Properties.Settings.Default.Gender_Voice = "Male";
            else if (rb_female.Checked)
                Properties.Settings.Default.Gender_Voice = "Female";

            Properties.Settings.Default.Voice_Rate = Convert.ToInt32(numberRating.Value);

            Properties.Settings.Default.MedLine_URL = tb_url.Text;
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            tb_url.Text = Properties.Settings.Default.MedLine_URL;

            if (Properties.Settings.Default.Gender_Voice.Equals("Male"))
                rb_male.Checked = true;
            else
                rb_female.Checked = true;

            numberRating.Value = Properties.Settings.Default.Voice_Rate;
        }

        #endregion
        
    }
}
