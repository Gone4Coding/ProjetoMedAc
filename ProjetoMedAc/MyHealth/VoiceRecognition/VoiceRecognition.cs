using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            SearchInMedLine,
            EnableAll,
            DisableAll,
            EnableOxygenSaturation,
            DisableOxygenSaturation,
            EnableHeartRate,
            DisableHeartRate,
            EnableBloodPressure,
            DisableBloodPressure,
            SetId,
            ChangeId,
            Close
        }

        public static string Recognition(Code code, string args)
        {
            switch (code)
            {
                case Code.Find:
                    break;

                case Code.FindTerms:
                    break;

                case Code.Search:
                    break;

                case Code.SearchInMedLine:
                    break;

                case Code.EnableAll:
                    break;

                case Code.DisableAll:
                    break;

                case Code.EnableOxygenSaturation:
                    break;

                case Code.DisableOxygenSaturation:
                    break;

                case Code.EnableHeartRate:
                    break;

                case Code.DisableHeartRate:
                    break;

                case Code.EnableBloodPressure:
                    break;

                case Code.DisableBloodPressure:
                    break;

                case Code.SetId:
                    break;

                case Code.ChangeId:
                    break;

                case Code.Close:
                    break;

            }
            return "";
        }
    }
}
