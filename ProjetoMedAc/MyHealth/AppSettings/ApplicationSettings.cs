using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyHealth.AppSettings
{
    public static class ApplicationSettings
    {
        private static XmlDocument doc = new XmlDocument();
        private static string settingsFile = @"../../AppSettings/AppSettings.xml";

        private static int patient_Id;
        private static string gender_Voice;
        private static int voice_Rate;
        private static string medLine_URL;
        private static int dLL_Rate;
        private static int retmax;

        public static void Set_Patient_Id(int value)
        {
            doc.Load(settingsFile);
            XmlNode patientNode = doc.SelectSingleNode("/settings/patientId");
            if (patientNode != null)
                patientNode.InnerText = value.ToString();
        }

        public static int Get_Patient_Id()
        {
            doc.Load(settingsFile);
            XmlNode patientNode = doc.SelectSingleNode("/settings/patientId");
            return (patientNode == null || patientNode.InnerText.Equals("")) ? 0 : Convert.ToInt32(patientNode.InnerText);
        }

        public static void Set_Gender_Voice(string value)
        {
            doc.Load(settingsFile);
            XmlNode genderNode = doc.SelectSingleNode("/settings/genderVoice");
            if (genderNode != null)
                genderNode.InnerText = value;
        }

        public static string Get_Gender_Voice()
        {
            doc.Load(settingsFile);
            XmlNode genderNode = doc.SelectSingleNode("/settings/genderVoice");
            return (genderNode == null) ? "" : genderNode.InnerText;
        }

        public static void Set_Voice_Rate(int value)
        {
            doc.Load(settingsFile);
            XmlNode voiceRateNode = doc.SelectSingleNode("/settings/voiceRate");
            if (voiceRateNode != null)
                voiceRateNode.InnerText = value.ToString();
        }

        public static int Get_Voice_Rate()
        {
            doc.Load(settingsFile);
            XmlNode voiceRateNode = doc.SelectSingleNode("/settings/voiceRate");
            return (voiceRateNode == null || voiceRateNode.InnerText.Equals("")) ? 0 : Convert.ToInt32(voiceRateNode.InnerText);
        }

        public static void Set_MedLine_URL(string value)
        {
            doc.Load(settingsFile);
            XmlNode medURLNode = doc.SelectSingleNode("/settings/medlineURL");
            if (medURLNode != null)
                medURLNode.InnerText = value;
        }

        public static string Get_MedLine_URL()
        {
            doc.Load(settingsFile);
            XmlNode medURLNode = doc.SelectSingleNode("/settings/medlineURL");
            return (medURLNode == null) ? "" : medURLNode.InnerText;
        }

        public static void Set_DLL_Rate(int value)
        {
            doc.Load(settingsFile);
            XmlNode dllNode = doc.SelectSingleNode("/settings/DLLRate");
            if (dllNode != null)
                dllNode.InnerText = value.ToString();
        }

        public static int Get_DLL_Rate()
        {
            doc.Load(settingsFile);
            XmlNode dllNode = doc.SelectSingleNode("/settings/DLLRate");
            return (dllNode == null || dllNode.InnerText.Equals("")) ? 0 : Convert.ToInt32(dllNode.InnerText);
        }

        public static void Set_Retmax(int value)
        {
            doc.Load(settingsFile);
            XmlNode retmaxNode = doc.SelectSingleNode("/settings/retmax");
            if (retmaxNode != null)
                retmaxNode.InnerText = value.ToString();
        }

        public static int Get_Retmax()
        {
            doc.Load(settingsFile);
            XmlNode retmaxNode = doc.SelectSingleNode("/settings/retmax");
            return (retmaxNode == null || retmaxNode.InnerText.Equals("")) ? 0 : Convert.ToInt32(retmaxNode.InnerText);
        }


    }
}
