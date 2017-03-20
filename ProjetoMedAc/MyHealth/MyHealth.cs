using System;
using System.Drawing;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Xml;
using MyHealth.ServiceReferenceHealth;
using System.Threading.Tasks;

namespace MyHealth
{
    public partial class MyHealth : Form
    {
        #region Variables 

        private ServiceHealthClient clientHealth;
        private ServiceHealthAlertClient clientHealthAlert;

        private SpeechSynthesizer synth;
        private PromptBuilder pBuilder;
        private SpeechRecognitionEngine sReconEngine;
        private PhysiologicParametersDll.PhysiologicParametersDll dll;

        private Patient patient;
        private bool bloodPressure_checked;
        private bool saturation_checked;
        private bool heartRate_checked;
        private bool speechActive = false;
        private bool closeQuestion = false;
        private bool setId = false;
        private bool serviceActive = false;

        #endregion

        #region General

        public MyHealth()
        {
            this.dll = new PhysiologicParametersDll.PhysiologicParametersDll();
            this.sReconEngine = new SpeechRecognitionEngine();
            this.synth = new SpeechSynthesizer();
            this.pBuilder = new PromptBuilder();
            this.clientHealth = new ServiceHealthClient();
            this.clientHealthAlert = new ServiceHealthAlertClient();

            InitializeComponent();
        }

        private void MyHealth_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            HideAll();
            HideLabels(false);
            gb_user.Location = new Point(192,71);
            tb_patientSNS.Text = Properties.Settings.Default.Patient_Id.ToString();
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
                if (speech.StartsWith("S")) CommandsWithS(speech);

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

        private void ServiceNotAvailable(bool firstTime)
        {
            lb_serviceError.Visible = true;

            foreach (TabPage tabPage in mainTabing.TabPages)
            {
                if (tabPage != home)
                    tabPage.Enabled = false;
            }

            if(!firstTime)
                MessageBox.Show("The Service is Not Active", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
        }

        private void EveryThingOk()
        {
            MoveGroupBoxUser();
            gb_monitoringParametrs.Visible = true;
            gb_physiologicDataNormal.Visible = true;
            lb_userName.Visible = true;

            foreach (TabPage tabPage in mainTabing.TabPages)
            {
                tabPage.Enabled = true;
            }
        }

        private void WrongUser()
        {
            gb_monitoringParametrs.Visible = false;
            gb_physiologicDataNormal.Visible = false;
            lb_userName.Visible = false;
        }

        private void HideAll()
        {
            lb_userName.Visible = false;
            lb_serviceError.Visible = false;
            gb_monitoringParametrs.Visible = false;
            gb_physiologicDataNormal.Visible = false;

            foreach (TabPage tabPage in mainTabing.TabPages)
            {
                if (tabPage != home)
                    tabPage.Enabled = false;
            }
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
            if (int.TryParse(tb_patientSNS.Text, out sns))
            {
                try
                {
                    bool verifiedSNS = clientHealth.ValidatePatient(sns);

                    if (!verifiedSNS)
                    {
                        MessageBox.Show("The SNS is not valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        WrongUser();
                    }
                    else
                    {
                        Properties.Settings.Default.Patient_Id = sns;
                        Properties.Settings.Default.Save();
                        patient = clientHealthAlert.GetPatient(sns);
                        serviceActive = true;

                        EveryThingOk();
                        InitializeSpeech();
                    }
                }
                catch (Exception)
                {
                    ServiceNotAvailable(false);
                }

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
                DateTime dateTime = Convert.ToDateTime(info[2]);

                TimeSpan time = dateTime.TimeOfDay;
                DateTime date = dateTime.Date;

                int snsUser = patient.Sns;

                switch (type)
                {
                    case "BP":
                        string[] bpData = data.Split('-');
                        int systolic = Convert.ToInt32(bpData[0]);
                        int diastolic = Convert.ToInt32(bpData[1]);
                        lb_dataBP.Text = diastolic + "/" + systolic;
                        lb_dateBP.Text = time.ToString();

                        if (serviceActive)
                        {

                            BloodPressure bp = new BloodPressure();
                            bp.PatientSNS = snsUser;
                            bp.Date = date;
                            bp.Time = time;
                            bp.Diastolic = diastolic;
                            bp.Systolic = systolic;
                            clientHealth.InsertBloodPressureRecord(bp);
                        }
                        break;

                    case "SPO2":
                        int spo2Data = Convert.ToInt32(data);
                        lb_dataSPO2.Text = spo2Data + "%";
                        lb_date_o2.Text = time.ToString();

                        if (serviceActive)
                        {
                            OxygenSaturation spo2 = new OxygenSaturation();
                            spo2.PatientSNS = snsUser;
                            spo2.Date = date;
                            spo2.Time = time;
                            spo2.Saturation = spo2Data;
                            clientHealth.InsertOxygenSaturationRecord(spo2);
                        }
                        break;

                    case "HR":
                        int hrData = Convert.ToInt32(data);
                        lb_dataHR.Text = hrData + "bpm";
                        lb_date_HR.Text = time.ToString();

                        if (serviceActive)
                        {
                            HeartRate hr = new HeartRate();
                            hr.PatientSNS = snsUser;
                            hr.Date = date;
                            hr.Time = time;
                            hr.Rate = hrData;
                            clientHealth.InsertHeartRateRecord(hr);
                        }
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

        private void MoveGroupBoxUser()
        { 
            Control destination = new Control();
            destination.Location = new Point(6, 6);

            slideToDestination(destination, gb_user, 2, null);
        }

        private void slideToDestination(Control destination, Control control, int delay, Action onFinish)
        {
            new Task(() =>
            {
                int directionX = destination.Left > control.Left ? 1 : -1;
                int directionY = destination.Bottom > control.Top ? 1 : -1;

                while (control.Left != destination.Left || control.Top != destination.Bottom)
                {
                    try
                    {
                        if (control.Left != destination.Left)
                        {
                            this.Invoke((Action)delegate ()
                            {
                                control.Left += directionX;
                            });
                        }
                        if (control.Top != destination.Bottom)
                        {
                            this.Invoke((Action)delegate ()
                            {
                                control.Top += directionY;
                            });
                        }
                        Thread.Sleep(delay);
                    }
                    catch
                    {
                        // form could be disposed
                        break;
                    }
                }

                if (onFinish != null) onFinish();

            }).Start();
        }

        #endregion

        #region TabUserInformation

        private void FillUserInfo()
        {
            tb_firstname.Text = patient.Name;
            tb_lastName.Text = patient.Surname;
            lb_userName.Text = patient.Name + " " + patient.Surname;
            dateTimePicker_birthdate.Value = patient.BirthDate;
            tb_nif.Text = patient.Nif.ToString();
            tb_sns.Text = patient.Sns.ToString();
            tb_phone.Text = patient.Phone.ToString();
            tb_email.Text = patient.Email;
            richTextBox_address.Text = patient.Adress;
            comboBoxGender.Text = patient.Gender;
            tb_emergencyContactName.Text = patient.EmergencyName;
            tb_emergencyContactNum.Text = patient.EmergencyNumber.ToString();
            tb_height.Text = patient.Height.ToString();
            tb_weight.Text = patient.Weight.ToString();
        }

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
