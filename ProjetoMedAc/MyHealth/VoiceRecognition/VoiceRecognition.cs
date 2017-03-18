using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyHealth.VoiceRecognition
{
    public static class VoiceRecognition
    {
        private static string[] allWords = File.ReadAllLines(@"../../VoiceRecognition/EnglishWords.txt");
        
        public enum Code
        {
            Find,
            FindTerms,
            Search,
            SearchInMedline,
            StartMonitoring,
            StopMonitoring,
            EnableOxygenSaturation,
            DisableOxygenSaturation,
            EnableHeartRate,
            DisableHeartRate,
            EnableBloodPressure,
            DisableBloodPressure,
            SetId,
            ChangeId,
            Close,
            HelloMyHealth,
            Bye,
            Yes,
            No
        }
        
        public static Grammar GetGrammar()
        {
            List<string> comms = new List<string>();
            
            foreach (Code code in Enum.GetValues(typeof(Code)))
            {
                comms.Add(ToString(code));
            }

            Choices choices = new Choices();
            choices.Add(comms.ToArray());

            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(choices);

            Grammar grammar = new Grammar(gBuilder);

            return grammar;
        }
        
        public static string ToString(this Code code)
        {
            return code.ToString();
        }
    }
}
