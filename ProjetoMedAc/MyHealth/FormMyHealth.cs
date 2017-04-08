using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using MyHealth.ServiceReferenceHealth;
using MyHealth.AppSettings;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace MyHealth
{
    public partial class FormMyHealth : Form
    {
        #region Variables 

        private ServiceHealthClient clientHealth;
        private ServiceHealthAlertClient clientHealthAlert;

        private SpeechSynthesizer synth;
        private PromptBuilder pBuilder;
        private SpeechRecognitionEngine sReconEngine;
        private PhysiologicParametersDll.PhysiologicParametersDll dll;
        private string queryForMedline = @"query?db=healthTopics&term=";
        private Image success, error;

        private Patient patient;
        private bool bloodPressure_checked, saturation_checked, heartRate_checked;
        private bool speechActive, serviceActive = false;

        private enum Question
        {
            Close,
            GotoMedLine,
            NULL
        }

        private bool closeQuestion, goToMedlineQuestion = false;

        private static string speechDeactivated = "Speech: Deactivated";
        private static string speechSemiActive = "Speech: Active. Say \"Hello Alice to fully activate\"";
        private static string speechFullyActive = "Speech: Active";
        #endregion

        #region General

        public FormMyHealth()
        {
            this.dll = new PhysiologicParametersDll.PhysiologicParametersDll();
            this.sReconEngine = new SpeechRecognitionEngine();
            this.synth = new SpeechSynthesizer();
            this.pBuilder = new PromptBuilder();
            this.clientHealth = new ServiceHealthClient();
            this.clientHealthAlert = new ServiceHealthAlertClient();
            this.success = Image.FromFile(@"../../Images/success.png");
            this.error = Image.FromFile(@"../../Images/cancel.png");
            InitializeComponent();
        }

        private void MyHealth_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            cb_speechActivation.Text = speechDeactivated;
            cb_speechActivation.ForeColor = Color.Firebrick;
            HideAll();
            HideLabels(false);
            tb_patientSNS.Text = ApplicationSettings.Get_Patient_Id().ToString();
        }

        private void CommandsWithS(string speech)
        {
            #region Monitoring Related

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
            #endregion
            else if (speech.Contains(VoiceRecognition.VoiceRecognition.Code.SetId.ToString())
                      || speech.Contains(VoiceRecognition.VoiceRecognition.Code.ChangeId.ToString()))
            {
                tb_patientSNS.Focus();
                Speak("Now, please type the number because I'm too stupid to understand numbers");
            }
            else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.SearchInMedline.ToString()))
            {
                //TODO ABRIR O FORM PARA PROCURAR NO MEDLINE
                mainTabing.SelectedTab = medline;
            }
            else if (speech.Contains(VoiceRecognition.VoiceRecognition.Code.Search.ToString()))
            {
                if (mainTabing.SelectedTab == medline)
                {
                    string terms = speech.Replace(VoiceRecognition.VoiceRecognition.Code.Search.ToString(), "");

                    tb_url.Text = terms;

                    UseBrowser(terms);
                    goToMedlineQuestion = false;
                }
                else
                {
                    Speak("Do you want to search in MedLine?");
                    ChangeQuestionActive(false, Question.GotoMedLine);
                }
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

            if (!firstTime)
                MessageBox.Show("The Service is Not Active", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
        }

        private void EveryThingOk()
        {
            gb_monitoringParametrs.Visible = true;
            gb_BloodPressure.Visible = true;
            gb_HeatRate.Visible = true;
            gb_Saturation.Visible = true;
            lb_userName.Visible = true;
            lb_userAge.Visible = true;

            foreach (TabPage tabPage in mainTabing.TabPages)
            {
                tabPage.Enabled = true;
            }
        }

        private void WrongUser()
        {
            gb_monitoringParametrs.Visible = false;
            lb_userName.Visible = false;
            lb_userAge.Visible = false;
        }

        private void HideAll()
        {
            lb_userName.Visible = false;
            lb_userAge.Visible = false;
            lb_serviceError.Visible = false;
            gb_monitoringParametrs.Visible = false;
            gb_BloodPressure.Visible = false;
            gb_HeatRate.Visible = false;
            gb_Saturation.Visible = false;

            foreach (TabPage tabPage in mainTabing.TabPages)
            {
                if (tabPage != home)
                    tabPage.Enabled = false;
            }
        }

        private void InitializeSpeech()
        {
            sReconEngine.RecognizeAsyncStop();
            sReconEngine.UnloadAllGrammars();
            sReconEngine.LoadGrammar(VoiceRecognition.VoiceRecognition.GetGrammar());
            sReconEngine.SetInputToDefaultAudioDevice();
            sReconEngine.RecognizeAsync(RecognizeMode.Multiple);
            sReconEngine.SpeechRecognized += Srecon_SpeechRecognized;
        }

        private void MyHealth_FormClosing(object sender, FormClosingEventArgs e)
        {
            dll.Stop();
            dll = null;
        }
    
        private void ChangeQuestionActive(bool allFalse, Question question)
        {
            if (allFalse)
            {
                closeQuestion = false;
                goToMedlineQuestion = false;

                return;
            }

            switch (question)
            {
                case Question.Close:
                    closeQuestion = true;
                    goToMedlineQuestion = false;
                    break;
                    
                case Question.GotoMedLine:
                    closeQuestion = false;
                    goToMedlineQuestion = true;
                    break;
            }

        }

        private void Srecon_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            if (cb_speechActivation.Checked)
            {
                #region Begin/End

                if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.HelloAlice.ToString()) && !speechActive)
                {
                    speechActive = true;
                    cb_speechActivation.Text = speechFullyActive;
                    cb_speechActivation.ForeColor = Color.ForestGreen;
                    Speak("Hi.");
                }
                else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Bye.ToString()))
                {
                    speechActive = false;
                    cb_speechActivation.Text = speechSemiActive;
                    cb_speechActivation.ForeColor = Color.Yellow;
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
                        else
                        {
                            ChangeQuestionActive(true, Question.NULL);
                        }
                    }

                    #endregion "CLOSE" QUESTION

                    #region GOTO MEDLINE

                    if (goToMedlineQuestion)
                    {
                        mainTabing.SelectedTab = medline;
                    }

                    #endregion

                    if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.FindTerms.ToString()))
                    {
                        /*
                         * Ask if user wants to go to MedLine
                         * If yes => GO 
                         */
                        Speak("Do you want to search in MedLine?");
                        ChangeQuestionActive(false, Question.GotoMedLine);

                    }
                    else if (speech.Equals(VoiceRecognition.VoiceRecognition.Code.Close.ToString()))
                    {
                        Speak("You sure?");
                        ChangeQuestionActive(false, Question.Close);
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
            if (check)
            {
                bt_HRActivation.Text = "Active";
                bt_HRActivation.ForeColor = Color.LimeGreen;
            }
            else
            {
                bt_HRActivation.Text = "Deactive";
                bt_HRActivation.ForeColor = Color.Firebrick;
            }
            lb_date_HR.Visible = check;
            lb_date_HR.Text = "Receiving...";
            lb_dataHR.Visible = check;
            lb_dataHR.Text = "Receiving...";
            pb_successErrorHR.Visible = check;
            StartMonitoring();
        }

        private void ActivateSaturationMonitoring(bool check)
        {
            if (!check) lb_dataSPO2.Text = "";
            if (check)
            {
                bt_satActivation.Text = "Active";
                bt_satActivation.ForeColor = Color.LimeGreen;
            }
            else
            {
                bt_satActivation.Text = "Deactive";
                bt_satActivation.ForeColor = Color.Firebrick;
            }
            lb_date_o2.Visible = check;
            lb_date_o2.Text = "Receiving...";
            lb_dataSPO2.Visible = check;
            lb_dataSPO2.Text = "Receiving...";
            pb_successErrorSPO2.Visible = check;
            StartMonitoring();
        }

        private void ActivateBloodPressureMonitoring(bool check)
        {
            if (!check) lb_dataBP.Text = "";
            if (check)
            {
                bt_BPActivation.Text = "Active";
                bt_BPActivation.ForeColor = Color.LimeGreen;
            }
            else
            {
                bt_BPActivation.Text = "Deactive";
                bt_BPActivation.ForeColor = Color.Firebrick;
            }
            lb_dateBP.Visible = check;
            lb_dateBP.Text = "Receiving...";
            lb_dataBP.Visible = check;
            lb_dataBP.Text = "Receiving...";
            pb_successErrorBP.Visible = check;
            StartMonitoring();
        }
        
        private void StartMonitoring()
        {
            int rate = ApplicationSettings.Get_DLL_Rate();
            dll.InitializeWithAlerts(DataParser, rate, bloodPressure_checked, saturation_checked, heartRate_checked);
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
                DateTime date = dateTime;

                int snsUser = patient.Sns;

                switch (type)
                {
                    #region Case BP
                    case "BP":
                        string[] bpData = data.Split('-');
                        int systolic = Convert.ToInt32(bpData[0]);
                        int diastolic = Convert.ToInt32(bpData[1]);
                        lb_dataBP.Text = diastolic + "/" + systolic;
                        lb_dateBP.Text = time.ToString();

                        if (serviceActive)
                        {
                            try
                            {
                                BloodPressure bp = new BloodPressure();
                                bp.PatientSNS = snsUser;
                                bp.Date = date;
                                bp.Time = time;
                                bp.Diastolic = diastolic;
                                bp.Systolic = systolic;
                                bool resBP = clientHealth.InsertBloodPressureRecord(bp);

                                pb_successErrorBP.BackgroundImage = (resBP) ? success : error;
                            }
                            catch (Exception e)
                            {
                                serviceActive = false;
                                pb_successErrorBP.BackgroundImage = error;
                            }
                        }
                        else
                        {
                            pb_successErrorBP.BackgroundImage = error;
                        }

                        break;
                    #endregion Case BP
                    #region Case SPO2
                    case "SPO2":
                        int spo2Data = Convert.ToInt32(data);
                        lb_dataSPO2.Text = spo2Data + "%";
                        lb_date_o2.Text = time.ToString();

                        if (serviceActive)
                        {
                            try
                            {
                                OxygenSaturation spo2 = new OxygenSaturation();
                                spo2.PatientSNS = snsUser;
                                spo2.Date = date;
                                spo2.Time = time;
                                spo2.Saturation = spo2Data;
                                bool resSPO2 = clientHealth.InsertOxygenSaturationRecord(spo2);

                                pb_successErrorSPO2.Image = (resSPO2) ? success : error;
                            }
                            catch (Exception e)
                            {
                                serviceActive = false;
                                pb_successErrorSPO2.Image = error;
                            }
                        }
                        else
                        {
                            pb_successErrorSPO2.Image = error;
                        }
                        break;
                    #endregion Case SPO2
                    #region Case HR
                    case "HR":
                        int hrData = Convert.ToInt32(data);
                        lb_dataHR.Text = hrData + "bpm";
                        lb_date_HR.Text = time.ToString();

                        if (serviceActive)
                        {
                            try
                            {
                                HeartRate hr = new HeartRate();
                                hr.PatientSNS = snsUser;
                                hr.Date = date;
                                hr.Time = time;
                                hr.Rate = hrData;
                                bool resHR = clientHealth.InsertHeartRateRecord(hr);

                                pb_successErrorHR.Image = (resHR) ? success : error;
                            }
                            catch (Exception e)
                            {
                                serviceActive = false;
                                pb_successErrorHR.Image = error;
                            }
                        }
                        else
                        {
                            pb_successErrorHR.Image = error;
                        }
                        break;
                        #endregion Case HR
                }

            }));
        }

        private void bt_insert_Click(object sender, EventArgs e)
        {
            MyHealth_Load(sender, e);
            int sns;
            if (int.TryParse(tb_patientSNS.Text, out sns))
            {
                try
                {
                    if (!clientHealth.ValidatePatient(sns))
                    {
                        MessageBox.Show("The SNS is not valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        WrongUser();
                    }
                    else
                    {
                        if (clientHealth.ValidatePatientState(sns))
                        {
                            ApplicationSettings.Set_Patient_Id(sns);
                            patient = clientHealthAlert.GetPatient(sns);
                            FillUserInfo();
                            serviceActive = true;

                            EveryThingOk();
                            InitializeSpeech();
                        }
                        else
                        {
                            MessageBox.Show("Currently You Are Not Being Monitored.\nTalk to your physician", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            WrongUser();
                        }

                    }
                }
                catch (Exception ex)
                {
                    ServiceNotAvailable(false);
                }

            }
            else
            {
                MessageBox.Show("Wrong format!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bt_BPActivation_Click(object sender, EventArgs e)
        {
            bloodPressure_checked = !bloodPressure_checked;
            ActivateBloodPressureMonitoring(bloodPressure_checked);
        }

        private void bt_satActivation_Click(object sender, EventArgs e)
        {
            saturation_checked = !saturation_checked;
            ActivateSaturationMonitoring(saturation_checked);
        }

        private void bt_HRActivation_Click(object sender, EventArgs e)
        {
            heartRate_checked = !heartRate_checked;
            ActivateHeartRateMonitoring(heartRate_checked);
        }

        private void cb_speechActivation_CheckedChanged(object sender, EventArgs e)
        {

            if (cb_speechActivation.Checked)
            {
                cb_speechActivation.Text = speechSemiActive;
                cb_speechActivation.ForeColor = Color.DarkGoldenrod;
            }
            else
            {
                cb_speechActivation.Text = speechDeactivated;
                cb_speechActivation.ForeColor = Color.Firebrick;
            }
        }

        private void tb_patientSNS_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bt_insert_Click(sender, e);
        }

        #endregion

        #region TabUserInformation

        private void FillUserInfo()
        {
            tb_firstname.Text = patient.Name;
            tb_lastName.Text = patient.Surname;
            lb_userName.Text = patient.Name + " " + patient.Surname;
            int age = DateTime.Now.Year - patient.BirthDate.Year;
            if (patient.BirthDate.Month > DateTime.Now.Month)
            {
                age--;
            }
            lb_userAge.Text = age.ToString();
            dateTimePicker_birthdate.Value = patient.BirthDate;
            tb_nif.Text = patient.Nif.ToString();
            tb_sns.Text = patient.Sns.ToString();
            tb_phone.Text = patient.Phone.ToString();
            tb_email.Text = patient.Email;
            richTextBox_address.Text = patient.Adress;
            tb_gender.Text = patient.Gender;
            tb_emergencyContactName.Text = patient.EmergencyName;
            tb_emergencyContactNum.Text = patient.EmergencyNumber.ToString();
            tb_height.Text = patient.Height.ToString();
            tb_weight.Text = patient.Weight.ToString();
            tb_phoneCountryCode.Text = patient.PhoneCountryCode;
            tb_emContactNumCountryCode.Text = patient.EmergencyNumberCountryCode;
        }

        #endregion

        #region TabMedLine
        
        private void Navigate()
        {
            string terms = tb_url.Text;

            UseBrowser(terms);
        }

        private void UseBrowser(string terms)
        {
            string finalURL = ApplicationSettings.Get_MedLine_URL() + queryForMedline + terms + "&retmax=" +
                              ApplicationSettings.Get_Retmax().ToString();
            WebClient webClient = new WebClient();
            webClient.DownloadStringAsync(new Uri(finalURL));
            webClient.DownloadStringCompleted += Result_DownloadStringCompleted;

            browser.DocumentText = "<h4>Retreiving Data...</h4>";
        }

        private void Result_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            browser.DocumentText = WebPage.LoadPage(e.Result);
        }

        private void tb_url_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Navigate();
        }

        private void bt_searchMedLine_Click(object sender, EventArgs e)
        {
            Navigate();
        }

        #endregion TabMedLine

        #region TabConfiguration

        private void RetreivePropertiesInfo()
        {
            tb_urlMedline.Text = ApplicationSettings.Get_MedLine_URL();

            if (ApplicationSettings.Get_Gender_Voice().Equals("Male"))
                rb_male.Checked = true;
            else
                rb_female.Checked = true;

            numberRatingVoice.Value = ApplicationSettings.Get_Voice_Rate();

            if (ApplicationSettings.Get_DLL_Rate() >= 3000)
                numberRatingDLL.Value = Convert.ToDecimal(ApplicationSettings.Get_DLL_Rate());

            tb_retmax.Text = ApplicationSettings.Get_Retmax().ToString();

            List<string> termsList = ApplicationSettings.Get_Terms();
            rtb_terms.Text = "";
            foreach (string term in termsList)
            {
                rtb_terms.Text += term + "\n";
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (!rb_male.Checked && !rb_female.Checked)
                MessageBox.Show("Please select a gender for the voice", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (rb_male.Checked)
                ApplicationSettings.Set_Gender_Voice("Male");
            else if (rb_female.Checked)
                ApplicationSettings.Set_Gender_Voice("Female");

            ApplicationSettings.Set_Voice_Rate(Convert.ToInt32(numberRatingVoice.Value));

            ApplicationSettings.Set_MedLine_URL(tb_urlMedline.Text);

            ApplicationSettings.Set_DLL_Rate(Convert.ToInt32(numberRatingDLL.Value));

            int retmax;
            if (int.TryParse(tb_retmax.Text, out retmax))
            {
                ApplicationSettings.Set_Retmax(retmax);
            }
            else
            {
                MessageBox.Show("Nº of Received Documents must be a whole number", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

            List<string> termsList = rtb_terms.Text.Split('\n').ToList();
            ApplicationSettings.Set_Terms(termsList);
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            RetreivePropertiesInfo();
        }

        private void mainTabing_MouseClick(object sender, MouseEventArgs e)
        {
            if (mainTabing.SelectedTab == configurations)
            {
                RetreivePropertiesInfo();
            }
        }

        #endregion TabConfiguration

    }
}
