using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Text.RegularExpressions;

namespace TimeCalculator
{
    internal class ParseTime
    {
        public static (string time10, string time01, string ampm1, string time20, string time02, string ampm2, byte operation, bool mode) Parse(string input)
        {

            string time10 = "", time01 = "", time20 = "", time02 = "", ampm1 = "", ampm2 = "";
            //string ampm1, ampm2;
            byte operation = 3;
            bool mode = false;

            if (Regex.IsMatch(input, "^\\s*(?:[01]?\\d|2[0-3]):[0-5]\\d\\s*(?:[AP][M])?\\s*[+-]\\s*(?:[01]?\\d|2[0-3]):[0-5]\\d\\s*(?:[AP][M])?\\s*(?:no\\s?wrap)?\\s*$", RegexOptions.IgnoreCase))
            {
                // Explaination for Regex.Matches found here: https://stackoverflow.com/questions/15421651/splitting-a-string-in-c-sharp
                string[] times = Regex.Matches(input, "(?:[01]?\\d|2[0-3]):[0-5]\\d", RegexOptions.IgnoreCase).Cast<Match>().Select(m => m.Value).ToArray();
                time10 = times[0].Split(':')[0];
                time01 = times[0].Split(':')[1];
                time20 = times[1].Split(':')[0];
                time02 = times[1].Split(':')[1];
                string[] ampms = Regex.Matches(input, "(?:[AP][M])", RegexOptions.IgnoreCase).Cast<Match>().Select(m => m.Value).ToArray();
                if (ampms.Length == 2)
                {
                    ampm1 = ampms[0];
                    ampm2 = ampms[1];
                }
                else if (Regex.IsMatch(input, "^\\s*(?:[01]?\\d|2[0-3]):[0-5]\\d\\s*(?:[AP][M])\\s*[+-]\\s*(?:[01]?\\d|2[0-3]):[0-5]\\d\\s*\\s*(?:no\\s?wrap)?\\s*$", RegexOptions.IgnoreCase))
                {
                    ampm1 = ampms[0];
                }
                else if (Regex.IsMatch(input, "^\\s*(?:[01]?\\d|2[0-3]):[0-5]\\d\\s*\\s*[+-]\\s*(?:[01]?\\d|2[0-3]):[0-5]\\d\\s*(?:[AP][M])\\s*(?:no\\s?wrap)?\\s*$", RegexOptions.IgnoreCase))
                {
                    ampm1 = ampms[0];
                }
                else
                {
                    ampm1 = "";
                    ampm2 = "";
                }
                if (input.Contains('+'))
                    operation = 1;
                else
                    operation = 0;
                if (input.Contains("no wrap") || input.Contains("nowrap"))
                    mode = true;
            }

            return (time10, time01, ampm1, time20, time02, ampm2, operation, mode);
        }
    }
}
