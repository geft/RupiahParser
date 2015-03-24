using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RupiahParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textEnter_GotFocus(object sender, RoutedEventArgs e)
        {
            textEnter.Text = "";
        }

        private void buttonParse_Click(object sender, RoutedEventArgs e)
        {
            beginFormatting();
        }

        private void textEnter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                beginFormatting();
            }
        }

        private void beginFormatting()
        {
            string text = clearFormatting(textEnter.Text);
            string error = "";

            if (sanitizeInput(text, out error))
            {
                textResult.Text = getTextFromNumber(text);
            }
            else
            {
                textResult.Text = error;
            }
        }

        private bool sanitizeInput(string text, out string error)
        {
            bool success = false;
            long result = 0;
            error = "";

            if (text.Length > 15)
            {
                error = "Angka terlalu besar";
            }
            else if (text.StartsWith("0"))
            {
                error = "Tidak boleh mulai dengan 0";
            }
            else if (!Int64.TryParse(text, out result))
            {
                error = "Format tidak didukung";
            } else {
                success = true;
            }

            return success;
        }

        private string getTextFromNumber(string number)
        {
            number = padWithZeros(number);
            List<string> numGroups = getGroupedNumbers(number);
            string finalText = getParsedNumbers(numGroups);
            return finalFormatting(finalText);
        }

        private string getParsedNumbers(List<string> numGroups)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < numGroups.Count; i++ )
            {
                string number = removePadding(numGroups[i]);
                string parsed = "";

                if (number.Length == 3)
                {
                    parsed = Parser.getTextFromHundreds(number);
                }
                else if (number.Length == 2)
                {
                    parsed = Parser.getTextFromTens(number);
                }
                else if (number.Length == 1)
                {
                    parsed = Parser.getTextFromOnes(number);
                }

                builder.Append(parsed);

                if (!String.IsNullOrWhiteSpace(number))
                {
                    builder.Append(getDenomination(numGroups.Count - i - 1));
                }
            }

            return builder.ToString();
        }

        private string getDenomination(int denomination)
        {
            switch (denomination)
            {
                case 4:
                    return "Triliun ";
                case 3:
                    return "Milyar ";
                case 2:
                    return "Juta ";
                case 1:
                    return "Ribu ";
                default:
                    return "";
            }
        }

        private string removePadding(string numGroup)
        {
            while (numGroup.StartsWith("0"))
            {
                numGroup = numGroup.Substring(1);
            }

            return numGroup;
        }

        private List<string> getGroupedNumbers(string number)
        {
            List<string> list = new List<string>();

            while (number.Length != 0)
            {
                list.Add(number.Substring(0, 3));
                number = number.Substring(3);
            }

            return list;
        }

        private string padWithZeros(string number)
        {
            while (number.Length % 3 != 0)
            {
                number = "0" + number;
            }

            return number;
        }


        private string clearFormatting(string number)
        {
            return number
                .Replace(".", "")
                .Replace(",", "")
                .Replace("-", "");
        }

        private string finalFormatting(string text)
        {
            string keyWord = "Satu Ribu";

            if (text.StartsWith(keyWord))
            {
                int index = text.IndexOf(keyWord) + keyWord.Length;
                text = "Seribu" + text.Substring(index);
            }

            text = text.Replace("Juta Satu Ribu", "Juta Seribu");

            return text + "Rupiah";
        }

        private void buttonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textResult.Text);
        }
    }
}
