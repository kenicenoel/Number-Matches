using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NumberMatches.UI
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

        private string GetClipBoardData()
        {
            bool IsTextDataOnClipboard = Clipboard.ContainsData(DataFormats.Text);
            var clipboardText = "";
            if(IsTextDataOnClipboard)
            {
                clipboardText = Clipboard.GetText(TextDataFormat.Text);
            }
            return clipboardText;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.V)
            {
                var clipboardText = GetClipBoardData();
                if (string.IsNullOrEmpty(clipboardText))
                {
                    MessageBox.Show("Could not find anything in the clipboard");
                }
                else
                { 
                    var numbers = clipboardText.Split('\n');
                    FoundMatches.Items.Clear();
                    foreach(var number in numbers)
                    {
                        PastedNumbers.Items.Add(number);
                    }
                    FindMatches(numbers);
                    
                }
            }
        }

        private void FindMatches(string[] numbers)
        {
           var matches = new List<string>();
            var numbersList = numbers.ToList().Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            var duplicatedList = numbersList;
            numbersList.ForEach(number =>
            {
                duplicatedList.ForEach(element =>
                {
                   var match = number.Except(element).Any();
                   if (!number.Equals(element) && !match)
                    {
                        if(!FoundMatches.Items.Contains(number))
                        {
                            //FoundMatches.Items.Add(element);
                            FoundMatches.Items.Add(number);
                        }
                    }
                });
            });
            
        }

    
        private void ClearListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            PastedNumbers.Items.Clear();
        }

        private void ExportContentsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool Compare(string first, string second)
        {
            if(first.Contains(second))
            {
                return true;
            }
            return false;
        }
    }
}
