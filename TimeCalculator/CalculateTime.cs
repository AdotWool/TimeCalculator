using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.UI.Xaml.Controls;

namespace TimeCalculator
{
    internal class CalculateTime
    {
        public static (string result, byte status) Calculate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return ("Enter a time calculation...", 0);
            // Tuple takes the place of the struct originally conceived. Fulfills the purpose exactly as needed.
            (string time10, string time01, string ampm1, string time20, string time02, string ampm2, byte operation, bool mode) timeStream = ParseTime.Parse(input);
            // Pulling the values out of the tuple makes them easier to deal with.
            string time10 = timeStream.time10, time01 = timeStream.time01, time20 = timeStream.time20, time02 = timeStream.time02, ampm1 = timeStream.ampm1.ToUpper(), ampm2 = timeStream.ampm2.ToUpper();
            byte operation = timeStream.operation;
            bool mode = timeStream.mode;
            bool ampmode = (!string.IsNullOrEmpty(ampm1) || !string.IsNullOrEmpty(ampm2));
            // Subtraction Mode
            if (operation == 0)
            {
                // Converting everything to minutes for easier calculation.
                int time1 = int.Parse(time01) + (int.Parse(time10) * 60);
                int time2 = int.Parse(time02) + (int.Parse(time20) * 60);
                int time = time1 - time2;
                // Must account for oddities with AM/PM time.
                if (ampmode)
                {
                    if (ampm1.CompareTo("AM") == 0 && (time1 >= 720 && time1 < 780))
                    {
                        time1 -= 720;
                    }
                    else if ((ampm1.CompareTo("PM") == 0) && time1 < 720)
                    {
                        time1 += 720;
                    }
                    if (ampm2.CompareTo("AM") == 0 && (time2 >= 720 && time2 < 780))
                    {
                        time2 -= 720;
                    }
                    else if ((ampm2.CompareTo("PM") == 0) && time2 < 720)
                    {
                        time1 += 720;
                    }
                    time = time1 - time2;
                }
                // Adjusting for 24-hour wraparound if in that mode.
                if (!mode)
                {
                    // Shifts time back into 0-24 range.
                    while (time < 0)
                        time += 1440;
                    if (ampmode)
                    {
                        if (time > 780)
                        {
                            return ($"{(time / 60) - 12}:{(time % 60).ToString().PadLeft(2, '0')} PM", 1);
                        }
                        else if (time < 60)
                        {
                            return ($"{(time / 60) + 12}:{(time % 60).ToString().PadLeft(2, '0')} AM", 1);
                        }
                        else if (time >= 720 && time < 780)
                        {
                            return ($"{(time / 60)}:{(time % 60).ToString().PadLeft(2, '0')} PM", 1);
                        }
                        else
                        {
                            return ($"{time / 60}:{(time % 60).ToString().PadLeft(2, '0')} AM", 1);
                        }
                    }
                }
                if (time < 0 && time > -60) // Handling negative times between 0 and -1 hour.
                {
                    return ($"-{(time / 60).ToString().PadLeft(2, '0')}:{(time % 60 == 0 ? 0 : 60 + time % 60).ToString().PadLeft(2, '0')} {time}", 1);
                }
                else if (time < 0)
                {
                    return ($"{(time / 60).ToString().PadLeft(2, '0')}:{(time % 60 == 0 ? 0 : 60 + time % 60).ToString().PadLeft(2, '0')} {time}", 1);
                }
                return ($"{(time / 60).ToString().PadLeft(2, '0')}:{(time % 60).ToString().PadLeft(2, '0')}", 1);

            }
            // Addition Mode
            else if (operation == 1)
            {
                int time1 = int.Parse(time01) + (int.Parse(time10) * 60);
                int time2 = int.Parse(time02) + (int.Parse(time20) * 60);
                int time = time1 + time2;
                if (ampmode)
                {
                    if (ampm1.CompareTo("AM") == 0 && (time1 >= 720 && time1 < 780))
                    {
                        time1 -= 720;
                    }
                    else if ((ampm1.CompareTo("PM") == 0) && time1 < 720)
                    {
                        time1 += 720;
                    }
                    if (ampm2.CompareTo("AM") == 0 && (time2 >= 720 && time2 < 780))
                    {
                        time2 -= 720;
                    }
                    else if ((ampm2.CompareTo("PM") == 0) && time2 < 720)
                    {
                        time1 += 720;
                    }
                    time = time1 + time2;
                }
                if (!mode)
                {
                    // Shifts time back into 0-24 range.
                    while (time >= 1440)
                        time -= 1440;
                    if (ampmode)
                    {
                        if (time > 780)
                        {
                            return ($"{(time / 60) - 12}:{(time % 60).ToString().PadLeft(2, '0')} PM", 1);
                        }
                        else if (time < 60)
                        {
                            return ($"{(time / 60) + 12}:{(time % 60).ToString().PadLeft(2, '0')} AM", 1);
                        }
                        else if (time >= 720 && time < 780)
                        {
                            return ($"{(time / 60)}:{(time % 60).ToString().PadLeft(2, '0')} PM", 1);
                        }
                        else
                        {
                            return ($"{time / 60}:{(time % 60).ToString().PadLeft(2, '0')} AM", 1);
                        }
                    }
                }
                return ($"{(time / 60).ToString().PadLeft(2, '0')}:{(time % 60).ToString().PadLeft(2, '0')}", 1);
            }
            // Example if the string is incorrect.
            return ("<hh:mm> [am|pm] <+|-> <hh:mm> [am|pm]", 2);
        }
    }
}
