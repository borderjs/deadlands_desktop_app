using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Deadlands_Desktop_App.Classes;
using MahApps.Metro.Controls;

namespace Deadlands_Desktop_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            
            int hitLocation = Helpers.GetRandomNumber(rnd, 1, 20);
            this.PopulateChart(hitLocation);
            
        }

        private void BtnMinus2_Click(object sender, RoutedEventArgs e)
        {
            int hitLocation = Helpers.GetRandomNumber(rnd, 1, 20);
            hitLocation = (hitLocation - 2) < 1 ? 1 : hitLocation - 2;
            this.PopulateChart(hitLocation);
        }

        private void BtnPlus2_Click(object sender, RoutedEventArgs e)
        {
            int hitLocation = Helpers.GetRandomNumber(rnd, 1, 20);
            hitLocation = (hitLocation + 2) > 20 ? 20 : hitLocation + 2;
            this.PopulateChart(hitLocation);
        }

        private void BtnPlus6_Click(object sender, RoutedEventArgs e)
        {
            int hitLocation = Helpers.GetRandomNumber(rnd, 1, 20);
            hitLocation = (hitLocation + 6) > 20 ? 20 : hitLocation + 6;
            this.PopulateChart(hitLocation);
        }

        private void BtnPlus4_Click(object sender, RoutedEventArgs e)
        {
            int hitLocation = Helpers.GetRandomNumber(rnd, 1, 20);
            hitLocation = (hitLocation + 4) > 20 ? 20 : hitLocation + 4;
            this.PopulateChart(hitLocation);
        }

        private void PopulateChart(int hitLocation)
        {
            int evenOdd = Helpers.GetRandomNumber(rnd, 1, 2);
            int armor = Helpers.GetRandomNumber(rnd, 1, 2);
            LblResult.Content = "Result: " + hitLocation;
            TxtLocation.Text = Helpers.GetLocation(hitLocation, 0, evenOdd);
            Txt1Raise.Text = Helpers.GetLocation(hitLocation, 1, evenOdd);
            Txt2Raises.Text = Helpers.GetLocation(hitLocation, 2, evenOdd);
            Txt3Raises.Text = Helpers.GetLocation(hitLocation, 3, evenOdd);
            if (armor == 1)
            {
                TxtArmor.Text = "Deflected";
            }
            else
            {
                TxtArmor.Text = "Bustin' Through";
            }
        }

        private void BtnCustomRoll_Click(object sender, RoutedEventArgs e)
        {
            string roll = TxtCustomRoll.Text.ToLower().Trim();
            string [] parts = roll.Split('d');
            if (parts.Count() == 2)
            {
                int numberOfDice;
                int dieValue;
                if (Int32.TryParse(parts[0], out numberOfDice))
                {
                    if (Int32.TryParse(parts[1], out dieValue))
                    {
                        if (numberOfDice >= 1 && dieValue > 1)
                        {
                            List<int> results = Helpers.RollDice(numberOfDice, dieValue);
                            results.Sort();
                            string values = string.Empty;
                            string resultString = Helpers.ParseResults(results, ref values);
                            DateTime time = DateTime.Now;
                            CheckBlockCustomRollResult();
                            BlockCustomRollResult.Inlines.InsertBefore(BlockCustomRollResult.Inlines.FirstInline, (new Run(values)));
                            BlockCustomRollResult.Inlines.InsertBefore(BlockCustomRollResult.Inlines.FirstInline, (new Run(resultString) { FontWeight = FontWeights.Bold }));
                            BlockCustomRollResult.Inlines.InsertBefore(BlockCustomRollResult.Inlines.FirstInline, (new Run(time.ToString("HH:mm:ss") + " - Rolling " + roll + "! - ")));
                            
                        }
                    }
                }
            }
        }

        private void BtnRolLScart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int numDice = Convert.ToInt32(button.Tag);
            List<int> results = Helpers.RollDice(numDice, 6);
            results.Sort();
            string values = string.Empty;
            string resultString = Helpers.ParseResults(results, ref values);
            DateTime time = DateTime.Now;
            CheckBlockCustomRollResult();
            BlockCustomRollResult.Inlines.InsertBefore(BlockCustomRollResult.Inlines.FirstInline, (new Run(values)));
            BlockCustomRollResult.Inlines.InsertBefore(BlockCustomRollResult.Inlines.FirstInline, (new Run(resultString) { FontWeight = FontWeights.Bold }));
            BlockCustomRollResult.Inlines.InsertBefore(BlockCustomRollResult.Inlines.FirstInline, (new Run(time.ToString("HH:mm:ss") + " - Rolling Scart " + numDice + "d6! - ")));
        }

        private void CheckBlockCustomRollResult()
        {
            if (BlockCustomRollResult.Inlines.FirstInline == null)
            {
                BlockCustomRollResult.Inlines.Add(new Run(Environment.NewLine));
            }
            else
            {
                BlockCustomRollResult.Inlines.InsertBefore(BlockCustomRollResult.Inlines.FirstInline, (new Run(Environment.NewLine)));
            }
        }
    }
}
