using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using AlpiniCalculatorWPF.ConfigModels;

namespace AlpiniCalculatorWPF
{
    public partial class MainWindow : Window
    {
        MixtureManager mixtureManager = new MixtureManager();
        public MainWindow()
        {
            mixtureManager.LoadConfig();
            InitializeComponent();
        }
        public void AboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
        public void ConfigMenuItem_Click(object sender, EventArgs e)
        {
            ConfigWindow configWindow = new ConfigWindow();
            configWindow.ConfigSaved += () => mixtureManager.LoadConfig();
            configWindow.ShowDialog();
        }
        public void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            string coffeeRecipe = ConstructPrintText();
            PrintResultsDirectly(coffeeRecipe);
        }

        public void PrintResultsDirectly(string content)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument document = new FlowDocument(new Paragraph(new Run(content)))
                {
                    FontFamily = new FontFamily("Verdana"),
                    FontSize = 20,
                    PagePadding = new Thickness(50)
                };
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Рецепт кофейной смеси");
            }
        }

        private string ConstructPrintText()
        {
            StringBuilder sb = new StringBuilder($"Рецепты для смеси Alpini:\n\n");

            if (CremaSidamo.Text != "" && CremaX.Text == "")
                sb.Append($"CREMA {CremaTotalWeight.Text} кг:\n" +
                $"• Сидамо - {CremaSidamo.Text}\n" +
                $"• Сантос - {CremaSantos.Text}\n" +
                $"• Можиана - {CremaMogiana.Text}\n" +
                $"• Робуста - {CremaRobusta.Text}\n\n");
            if (CremaSidamo.Text != "" && CremaX.Text != "")
                sb.Append($"CREMA {CremaTotalWeight.Text} - {CremaX.Text}:\n" +
                $"• Сидамо - {CremaSidamo.Text}\n" +
                $"• Сантос - {CremaSantos.Text}\n" +
                $"• Можиана - {CremaMogiana.Text}\n" +
                $"• Робуста - {CremaRobusta.Text}\n\n");

            if (IntensoSidamo.Text != "" && IntensoX.Text == "")
                sb.Append($"INTENSO {IntensoTotalWeight.Text} кг:\n" +
                $"• Сидамо - {IntensoSidamo.Text}\n" +
                $"• Сантос - {IntensoSantos.Text}\n" +
                $"• Можиана - {IntensoMogiana.Text}\n" +
                $"• Робуста - {IntensoRobusta.Text}\n\n");
            if (IntensoSidamo.Text != "" && IntensoX.Text != "")
                sb.Append($"INTENSO {IntensoTotalWeight.Text} - {IntensoX.Text}:\n" +
                $"• Сидамо - {IntensoSidamo.Text}\n" +
                $"• Сантос - {IntensoSantos.Text}\n" +
                $"• Можиана - {IntensoMogiana.Text}\n" +
                $"• Робуста - {IntensoRobusta.Text}\n\n");


            if (BaristaSidamo.Text != "" && BaristaX.Text == "")
                sb.Append($"Barista {BaristaTotalWeight.Text} кг:\n" +
                $"• Сидамо - {BaristaSidamo.Text}\n" +
                $"• Сантос - {BaristaSantos.Text}\n" +
                $"• Робуста - {BaristaRobusta.Text}\n\n");
            if (BaristaSidamo.Text != "" && BaristaX.Text != "")
                sb.Append($"Barista {BaristaTotalWeight.Text} - {BaristaX.Text}:\n" +
                $"• Сидамо - {BaristaSidamo.Text}\n" +
                $"• Сантос - {BaristaSantos.Text}\n" +
                $"• Робуста - {BaristaRobusta.Text}\n\n");

            if (BrazilSantos.Text != "" && BrazilX.Text == "")
                sb.Append($"BRAZIL {BrazilTotalWeight.Text} кг:\n" +
                $"• Сантос - {BrazilSantos.Text}\n" +
                $"• Можиана - {BrazilMogiana.Text}\n" +
                $"• Робуста - {BrazilRobusta.Text}\n\n");
            if (BrazilSantos.Text != "" && BrazilX.Text != "")
                sb.Append($"BRAZIL {BrazilTotalWeight.Text} - {BrazilX.Text}:\n" +
                $"• Сантос - {BrazilSantos.Text}\n" +
                $"• Можиана - {BrazilMogiana.Text}\n" +
                $"• Робуста - {BrazilRobusta.Text}\n\n");
            return sb.ToString();
        }

        #region Input
        private void Crema_Input(object sender, KeyEventArgs e)
        {
            CremaClear();
            string weight = CremaTotalWeight.Text;
            if (e.Key == Key.Enter)
            {
                try { CremaCalculation(Convert.ToDouble(weight)); }
                catch { }
            }
        }
        private void Intenso_Input(object sender, KeyEventArgs e)
        {
            IntensoClear();
            string weight = IntensoTotalWeight.Text;
            if (e.Key == Key.Enter)
            {
                try { IntensoCalculation(Convert.ToDouble(weight)); }
                catch { }
            }
        }
        private void Barista_Input(object sender, KeyEventArgs e)
        {
            BaristaClear();
            string weight = BaristaTotalWeight.Text;
            if (e.Key == Key.Enter)
            {
                try { BaristaCalculation(Convert.ToDouble(weight)); }
                catch { }
            }
        }
        private void Brazil_Input(object sender, KeyEventArgs e)
        {
            BrazilClear();
            string weight = BrazilTotalWeight.Text;
            if (e.Key == Key.Enter)
            {
                try { BrazilCalculation(Convert.ToDouble(weight)); }
                catch { }
            }
        }
        #endregion

        #region Calculation
        private void CremaCalculation(double input)
        {
            CremaSidamo.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Sidamo").Percentage) / 100, 2)).ToString()} кг";
            CremaSantos.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Santos").Percentage) / 100, 2)).ToString()} кг";
            CremaMogiana.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Mogiana").Percentage) / 100, 2)).ToString()} кг";
            CremaRobusta.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage) / 100, 2)).ToString()} кг";
        }
        private void IntensoCalculation(double input)
        {
            IntensoSidamo.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Sidamo").Percentage) / 100, 2)).ToString()} кг";
            IntensoSantos.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Santos").Percentage) / 100, 2)).ToString()} кг";
            IntensoMogiana.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Mogiana").Percentage) / 100, 2)).ToString()} кг";
            IntensoRobusta.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage) / 100, 2)).ToString()} кг";
        }
        private void BaristaCalculation(double input)
        {
            BaristaSidamo.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Barista").Components.FirstOrDefault(m => m.Name == "Sidamo").Percentage) / 100, 2)).ToString()} кг";
            BaristaSantos.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Barista").Components.FirstOrDefault(m => m.Name == "Santos").Percentage) / 100, 2)).ToString()} кг";
            BaristaRobusta.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Barista").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage) / 100, 2)).ToString()} кг";
        }
        private void BrazilCalculation(double input)
        {
            BrazilSantos.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Brazil").Components.FirstOrDefault(m => m.Name == "Santos").Percentage) / 100, 2)).ToString()} кг";
            BrazilMogiana.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Brazil").Components.FirstOrDefault(m => m.Name == "Mogiana").Percentage) / 100, 2)).ToString()} кг";
            BrazilRobusta.Text = $"{(Math.Round(input * (mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == "Brazil").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage) / 100, 2)).ToString()} кг";
        }
        #endregion

        #region Clear
        private void CremaClear()
        {
            CremaSidamo.Text = "";
            CremaSantos.Text = "";
            CremaMogiana.Text = "";
            CremaRobusta.Text = "";
            CremaX.Text = "";
        }
        private void IntensoClear()
        {
            IntensoSidamo.Text = "";
            IntensoSantos.Text = "";
            IntensoMogiana.Text = "";
            IntensoRobusta.Text = "";
            IntensoX.Text = "";
        }
        private void BaristaClear()
        {
            BaristaSidamo.Text = "";
            BaristaRobusta.Text = "";
            BaristaSantos.Text = "";
            BaristaX.Text = "";
        }
        private void BrazilClear()
        {
            BrazilSantos.Text = "";
            BrazilMogiana.Text = "";
            BrazilRobusta.Text = "";
            BrazilX.Text = "";
        }
        #endregion

        #region Divide_Logic
        private void DivideIntenso(double input)
        {
            if (IntensoSidamo.Text == "" || IntensoSantos.Text == "" || IntensoMogiana.Text == "" || IntensoRobusta.Text == "")
                IntensoClear();
            else
            {
                double sidamo = double.Parse((IntensoSidamo.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double santos = double.Parse((IntensoSantos.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double mogiana = double.Parse((IntensoMogiana.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double robusta = double.Parse((IntensoRobusta.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);

                IntensoSidamo.Text = $"{Math.Round(sidamo / double.Parse(IntensoDivide.Text), 2)} кг";
                IntensoSantos.Text = $"{Math.Round(santos / double.Parse(IntensoDivide.Text), 2)} кг";
                IntensoMogiana.Text = $"{Math.Round(mogiana / double.Parse(IntensoDivide.Text), 2)} кг";
                IntensoRobusta.Text = $"{Math.Round(robusta / double.Parse(IntensoDivide.Text), 2)} кг";
            }
        }

        private void Intenso_Divide_Input(object sender, KeyEventArgs e)
        {
            if (IntensoSidamo.Text == "" || IntensoSantos.Text == "" || IntensoMogiana.Text == "" || IntensoRobusta.Text == "")
                IntensoClear();
            else
            {
                if (IntensoTotalWeight.Text != "")
                {
                    IntensoCalculation(Convert.ToDouble(IntensoTotalWeight.Text));
                    if (IntensoDivide.Text == "")
                        IntensoX.Text = "";
                    else
                    {
                        try
                        {
                            DivideIntenso(double.Parse(IntensoDivide.Text));
                            IntensoX.Text = $"{IntensoDivide.Text.ToString()}x по {Math.Round((double.Parse(IntensoTotalWeight.Text) / double.Parse(IntensoDivide.Text)),2)}кг";
                        }
                        catch { }
                    }
                }
            }
        }

        private void DivideCrema(double input)
        {
            if (CremaSidamo.Text == "" || CremaSantos.Text == "" || CremaMogiana.Text == "" || CremaRobusta.Text == "")
                CremaClear();
            else
            {
                double sidamo = double.Parse((CremaSidamo.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double santos = double.Parse((CremaSantos.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double mogiana = double.Parse((CremaMogiana.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double robusta = double.Parse((CremaRobusta.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);

                CremaSidamo.Text = $"{Math.Round(sidamo / double.Parse(CremaDivide.Text), 2)} кг";
                CremaSantos.Text = $"{Math.Round(santos / double.Parse(CremaDivide.Text), 2)} кг";
                CremaMogiana.Text = $"{Math.Round(mogiana / double.Parse(CremaDivide.Text), 2)} кг";
                CremaRobusta.Text = $"{Math.Round(robusta / double.Parse(CremaDivide.Text), 2)} кг";
            }
        }
        private void Crema_Divide_Input(object sender, KeyEventArgs e)
        {
            if (CremaSidamo.Text == "" || CremaSantos.Text == "" || CremaMogiana.Text == "" || CremaRobusta.Text == "")
                CremaClear();
            else
            {
                if (CremaTotalWeight.Text != "")
                {
                    CremaCalculation(Convert.ToDouble(CremaTotalWeight.Text));
                    if (CremaDivide.Text == "")
                        CremaX.Text = "";
                    else
                    {
                        try
                        {
                            DivideCrema(double.Parse(CremaDivide.Text));
                            CremaX.Text = $"{CremaDivide.Text.ToString()}x по {Math.Round((double.Parse(CremaTotalWeight.Text) / double.Parse(CremaDivide.Text)), 2)}кг";
                        }
                        catch { }
                    }
                }
            }
        }

        private void DivideBarista(double input)
        {
            if (BaristaSidamo.Text == "" || BaristaSantos.Text == "" || BaristaRobusta.Text == "")
                BaristaClear();
            else
            {
                double sidamo = double.Parse((BaristaSidamo.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double santos = double.Parse((BaristaSantos.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double robusta = double.Parse((BaristaRobusta.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);

                BaristaSidamo.Text = $"{Math.Round(sidamo / double.Parse(BaristaDivide.Text), 2)} кг";
                BaristaSantos.Text = $"{Math.Round(santos / double.Parse(BaristaDivide.Text), 2)} кг";
                BaristaRobusta.Text = $"{Math.Round(robusta / double.Parse(BaristaDivide.Text), 2)} кг";
            }
        }

        private void Barista_Divide_Input(object sender, KeyEventArgs e)
        {
            if (BaristaSidamo.Text == "" || BaristaSantos.Text == "" || BaristaRobusta.Text == "")
                BaristaClear();
            else
            {
                if (BaristaTotalWeight.Text != "")
                {
                    BaristaCalculation(Convert.ToDouble(BaristaTotalWeight.Text));
                    if (BaristaDivide.Text == "")
                        BaristaX.Text = "";
                    else
                    {
                        try
                        {
                            DivideBarista(double.Parse(BaristaDivide.Text));
                            BaristaX.Text = $"{BaristaDivide.Text.ToString()}x по {Math.Round((double.Parse(BaristaTotalWeight.Text) / double.Parse(BaristaDivide.Text)), 2)}кг";
                        }
                        catch { }
                    }
                }
            }
        }
        private void DivideBrazil(double input)
        {
            if (BrazilSantos.Text == "" || BrazilMogiana.Text == "" || BrazilRobusta.Text == "")
                BrazilClear();
            else
            {
                double santos = double.Parse((BrazilSantos.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double mogiana = double.Parse((BrazilMogiana.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);
                double robusta = double.Parse((BrazilRobusta.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0]);

                BrazilSantos.Text = $"{Math.Round(santos / double.Parse(BrazilDivide.Text), 2)} кг";
                BrazilMogiana.Text = $"{Math.Round(mogiana / double.Parse(BrazilDivide.Text), 2)} кг";
                BrazilRobusta.Text = $"{Math.Round(robusta / double.Parse(BrazilDivide.Text), 2)} кг";
            }
        }

        private void Brazil_Divide_Input(object sender, KeyEventArgs e)
        {
            if (BrazilSantos.Text == "" || BrazilMogiana.Text == "" || BrazilRobusta.Text == "")
                BrazilClear();
            else
            {
                if (BrazilTotalWeight.Text != "")
                {
                    BrazilCalculation(Convert.ToDouble(BrazilTotalWeight.Text));
                    if (BrazilDivide.Text == "")
                        BrazilX.Text = "";
                    else
                    {
                        try
                        {
                            DivideBrazil(double.Parse(BrazilDivide.Text));
                            BrazilX.Text = $"{BrazilDivide.Text.ToString()}x по {Math.Round((double.Parse(BrazilTotalWeight.Text) / double.Parse(BrazilDivide.Text)), 2)}кг";
                        }
                        catch { }
                    }
                }
            }
        }
        
        #endregion
    }
}