using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlertSystem
{
    public static class Countries
    {
        private class Country
        {
            private string alpha2Code;
            private string callingCodes;

            public Country(string alpha2Code, string callingCodes)
            {
                this.alpha2Code = alpha2Code;
                this.callingCodes = callingCodes;
            }

            public string Alpha2Code
            {
                get { return alpha2Code; }
                set { alpha2Code = value; }
            }
            public string CallingCodes
            {
                get { return CallingCodes; }
                set { CallingCodes = value; }
            }

            public override string ToString()
            {
                return alpha2Code + " (" + callingCodes + ")";
            }
        }
        
        public static bool getAllCountries()
        {
            var result = new WebClient();
            

           result.DownloadStringAsync(new Uri("https://restcountries.eu/rest/v2/all"));
          
            result.DownloadStringCompleted += Result_DownloadStringCompleted;

            return true;
        }

        private static void Result_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
           // MessageBox.Show(e.Result);
           // JObject json = JObject.Parse(e.Result);
            List<Country> countryCodeList = new List<Country>();
             //string [] s= e.ToString().Split('{');
            countryCodeList = JsonConvert.DeserializeObject<List<Country>>(e.Result);
          
            foreach (Country country in countryCodeList)
            {
                MessageBox.Show(country.Alpha2Code);
            }

        }
    }
}
