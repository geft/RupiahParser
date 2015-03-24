using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupiahParser
{
    class Parser
    {
        public static string getTextFromHundreds(string number)
        {
            if (number.Equals(new String('0', number.Length)))
            {
                return "";
            }

            string one = number[0].ToString();
            string hundred = number[1].ToString() + number[2].ToString();
            string firstDigit = (number.StartsWith("1")) ? "Seratus " : getTextFromOnes(one) + "Ratus ";

            if (hundred.Equals("00"))
            {
                return firstDigit;
            }
            else if (hundred.StartsWith("0"))
            {
                return firstDigit + getTextFromOnes(hundred[1].ToString());
            }
            else
            {
                return firstDigit + getTextFromTens(hundred);
            }
        }

        public static string getTextFromTens(string number)
        {
            if (number.Equals(new String('0', number.Length)))
            {
                return "";
            }

            string first = number[0].ToString();
            string second = number[1].ToString();
            string firstDigit = (number.StartsWith("1")) ? "Sepuluh " : getTextFromOnes(first) + "Puluh ";

            if (number.StartsWith("1") && !number.Equals("10"))
            {
                return getTextFromOnes(second) + "Belas ";
            }
            else
            {
                return firstDigit + getTextFromOnes(second);
            }
        }

        public static string getTextFromOnes(string number)
        {
            switch (number)
            {
                case "1":
                    return "Satu ";
                case "2":
                    return "Dua ";
                case "3":
                    return "Tiga ";
                case "4":
                    return "Empat ";
                case "5":
                    return "Lima ";
                case "6":
                    return "Enam ";
                case "7":
                    return "Tujuh ";
                case "8":
                    return "Delapan ";
                case "9":
                    return "Sembilan ";
                default:
                    return "";
            }
        }
    }
}
