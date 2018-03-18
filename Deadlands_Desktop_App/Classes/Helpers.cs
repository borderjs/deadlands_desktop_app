using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Deadlands_Desktop_App.Classes
{
    public static class Helpers
    {
        public static int GetRandomNumber(Random rnd, int min, int max)
        {
            int result = rnd.Next(min, max + 1); // creates a number between min and max
            return result;
        }

        public static string GetLocation(int result, int raises, int evenOdd)
        {
            if (raises == 0)
            {
                return ChartLookup(result, evenOdd);
            }
            else
            {
                if (result + raises > 20)
                {
                    return ChartLookup(result - raises, evenOdd);
                }
                else if (result - raises < 1)
                {
                    return ChartLookup(result + raises, evenOdd);
                }
                else
                {
                    return ChartLookup(result - raises, evenOdd) + " / " + ChartLookup(result + raises, evenOdd);
                }
            }
        }

        private static string ChartLookup(int result, int evenOdd)
        {
            switch (result)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    if (evenOdd == 1)
                    {
                        return "Left Leg";
                    }
                    else
                    {
                        return "Right Leg";
                    }
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return "Guts";
                case 10:
                    return "Gizzards (+1 Die)";
                case 11:
                case 12:
                case 13:
                case 14:
                    if (evenOdd == 1)
                    {
                        return "Left Arm";
                    }
                    else
                    {
                        return "Right Arm";
                    }
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    return "Guts";
                case 20:
                    return "Head (+2 Dice)";
            }
            return string.Empty;
        }

        internal static List<int> RollDice(int numberOfDice, int dieValue)
        {
            Random rnd = new Random();
            List<int> results = new List<int>();
            for (int i = 0; i < numberOfDice; i++)
            {
                int total = 0;
                int roll = rnd.Next(1, dieValue+1);
                total += roll;
                while (roll == dieValue)
                {
                    roll = rnd.Next(1, dieValue + 1);
                    total += roll;
                }
                results.Add(total);
            }
            return results;
        }

        internal static string ParseResults(List<int> results, ref string values)
        {
            values =" (";
            for (int i = results.Count - 1; i >= 0; i--)
            {
                values += results[i] + ", ";
            }
            values = values.Substring(0, values.Length - 2) + ")";
            return results.Where(z => z == 1).Count() > results.Count / 2 ? "Bust!" : results[results.Count - 1] + string.Empty;
        }
    }
}
