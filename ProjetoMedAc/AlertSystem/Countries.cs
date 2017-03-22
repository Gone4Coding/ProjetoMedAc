using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlertSystem
{
    public static partial class Countries
    {
        private static List<Country> countryCodeList = new List<Country>();
        public partial class Country
        {
            private string name;
            private string[] topLevelDomain;
            private string alpha2Code;
            private string alpha3Code;
            private string[] callingCodes;
            private string capital;
            private string[] altSpellings;
            private string region;
            private string population;
            private string[] latlng;
            private string demonym;
            private string area;
            private string gini;
            private string[] timezones;
            private string[] borders;
            private string nativeName;
            private string numericCode;
            private string[] languages;
            private string[] currencies;
            private string[] translations;
            private string flag;

            public Country(string name, string[] topLevelDomain, string alpha2Code, string alpha3Code,
                string [] callingCodes,
                string capital, string [] altSpellings, string region, string[] latlng, string demonym, string area,
                string gini, string[] timezones, string[] borders,
                string nativeName, string numericCode)
            {
                this.name = name;
                this.alpha2Code = alpha2Code;               
                this.callingCodes = callingCodes;
               // this.capital = capital;
               // this.altSpellings = altSpellings;
               // this.region = region;
               // this.latlng = latlng;
               // this.demonym = demonym;
               // this.area = area;
               // this.gini = gini;
               // this.timezones = timezones;
               // this.borders = borders;
               // this.nativeName = nativeName;
               // this.numericCode = numericCode;
               // //this.currencies = currencies;
               //// this.languages = languages;
               // //this.translations = translations;
               //// this.flag = flag;
            }

            public string Alpha2Code
            {
                get { return alpha2Code; }
                set { alpha2Code = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string CallingCodes
            {
                get
                {
                    if (callingCodes.Length != 0 )
                    {
                        return callingCodes[0];
                    }
                    else
                    {
                        return "";
                    }
                }             
            }

            public override string ToString()
            {
                return alpha2Code +  " (+ " + CallingCodes + " )";
            }
        }

        public static List<Country> getAllCountries()
        {
            string output;
            var result = new WebClient();
            output = result.DownloadString(new Uri("https://restcountries.eu/rest/v2/all"));
            countryCodeList = JsonConvert.DeserializeObject<List<Country>>(output);

            return countryCodeList;
        }  
    }
}
