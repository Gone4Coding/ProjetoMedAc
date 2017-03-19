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
            // Commands With A
            Alice,

            // Commands With B
            Bye,

            // Commands With C
            ChangeId,
            Close,

            // Commands With F
            Find,
            FindTerms,

            // Commands With G
            GotoHome,
            GotoUserInformation,
            GotoConfigurations,

            // Commands With H
            HelloAlice,

            // Commands With N
            No,

            // Commands With S
            Search,
            SearchInMedline,
            StartMonitoring,
            StopMonitoring,
            StartOxygenSaturation,
            StopOxygenSaturation,
            StartHeartRate,
            StopHeartRate,
            StartBloodPressure,
            StopBloodPressure,
            SetId,

            // Commands With Y
            Yes
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
