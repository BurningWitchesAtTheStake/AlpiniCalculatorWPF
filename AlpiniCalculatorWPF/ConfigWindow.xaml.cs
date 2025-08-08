using AlpiniCalculatorWPF.ConfigModels;
using System;
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
using System.Windows.Shapes;

namespace AlpiniCalculatorWPF
{
    public partial class ConfigWindow : Window
    {
        MixtureManager mixtureManager = new MixtureManager();

        public event Action ConfigSaved;
        public ConfigWindow()
        {
            InitializeComponent();
            mixtureManager.LoadConfig();
            StartConfig(mixtureManager);
        }

        public void StartConfig(MixtureManager mixtureManager)
        {
            IntensoSidamoPerc.Text = InitPercentage(mixtureManager, "Intenso", "Sidamo");
            IntensoSantosPerc.Text = InitPercentage(mixtureManager, "Intenso", "Santos");
            IntensoMogianaPerc.Text = InitPercentage(mixtureManager, "Intenso", "Mogiana");
            IntensoRobustaPerc.Text = InitPercentage(mixtureManager, "Intenso", "Robusta");

            CremaSidamoPerc.Text = InitPercentage(mixtureManager, "Crema", "Sidamo");
            CremaSantosPerc.Text = InitPercentage(mixtureManager, "Crema", "Santos");
            CremaMogianaPerc.Text = InitPercentage(mixtureManager, "Crema", "Mogiana");
            CremaRobustaPerc.Text = InitPercentage(mixtureManager, "Crema", "Robusta");

            BaristaSidamoPerc.Text = InitPercentage(mixtureManager, "Barista", "Sidamo");
            BaristaSantosPerc.Text = InitPercentage(mixtureManager, "Barista", "Santos");
            BaristaRobustaPerc.Text = InitPercentage(mixtureManager, "Barista", "Robusta");

            BrazilSantosPerc.Text = InitPercentage(mixtureManager, "Brazil", "Santos");
            BrazilMogianaPerc.Text = InitPercentage(mixtureManager, "Brazil", "Mogiana");
            BrazilRobustaPerc.Text = InitPercentage(mixtureManager, "Brazil", "Robusta");
        }

        public string InitPercentage(MixtureManager mixtureManager, string mix, string sort)
        {
            if (mixtureManager?.Config?.Mixtures == null)
            {
                return "0";
            }

            var mixture = mixtureManager.Config.Mixtures.FirstOrDefault(m => m.Name == mix);
            if (mixture?.Components == null)
            {
                return "0";
            }

            var component = mixture.Components.FirstOrDefault(c => c.Name == sort);
            return component?.Percentage.ToString() ?? "0";
        }

        #region Check
        public bool IntensoCheck()
        {
            try
            {
                if (NewIntensoSidamoPerc.Text == "" || NewIntensoSantosPerc.Text == "" || NewIntensoMogianaPerc.Text == "" || NewIntensoRobustaPerc.Text == "")
                {
                    return false;
                }
                if (((int.Parse(NewIntensoSidamoPerc.Text)) + (int.Parse(NewIntensoSantosPerc.Text)) + (int.Parse(NewIntensoMogianaPerc.Text)) + (int.Parse(NewIntensoRobustaPerc.Text))) != 100)
                {
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        public bool CremaCheck()
        {
            try
            {
                if (NewCremaSidamoPerc.Text == "" || NewCremaSantosPerc.Text == "" || NewCremaMogianaPerc.Text == "" || NewCremaRobustaPerc.Text == "")
                {
                    return false;
                }
                if (((int.Parse(NewCremaSidamoPerc.Text)) + (int.Parse(NewCremaSantosPerc.Text)) + (int.Parse(NewCremaMogianaPerc.Text)) + (int.Parse(NewCremaRobustaPerc.Text))) != 100)
                {
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        public bool BaristaCheck()
        {
            try
            {
                if (NewBaristaSidamoPerc.Text == "" || NewBaristaSantosPerc.Text == "" || NewBaristaRobustaPerc.Text == "")
                {
                    return false;
                }
                if (((int.Parse(NewBaristaSidamoPerc.Text)) + (int.Parse(NewBaristaSantosPerc.Text)) + (int.Parse(NewBaristaRobustaPerc.Text))) != 100)
                {
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        public bool BrazilCheck()
        {
            try
            {
                if (NewBrazilSantosPerc.Text == "" || NewBrazilMogianaPerc.Text == "" || NewBrazilRobustaPerc.Text == "")
                {
                    return false;
                }
                if (((int.Parse(NewBrazilSantosPerc.Text)) + (int.Parse(NewBrazilMogianaPerc.Text)) + (int.Parse(NewBrazilRobustaPerc.Text))) != 100)
                {
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        #endregion

        private void Button_Save_Click(object sender, EventArgs e)
        {
            MixtureManager temp = mixtureManager;
            StartConfig(temp);
            if (IntensoCheck())
            {
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Sidamo").Percentage = (int.Parse(NewIntensoSidamoPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Santos").Percentage = (int.Parse(NewIntensoSantosPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Mogiana").Percentage = (int.Parse(NewIntensoMogianaPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Intenso").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage = (int.Parse(NewIntensoRobustaPerc.Text));
                temp.SaveConfig();
                StartConfig(temp);
            }
            if (CremaCheck())
            {
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Sidamo").Percentage = (int.Parse(NewCremaSidamoPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Santos").Percentage = (int.Parse(NewCremaSantosPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Mogiana").Percentage = (int.Parse(NewCremaMogianaPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Crema").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage = (int.Parse(NewCremaRobustaPerc.Text));
                temp.SaveConfig();
                StartConfig(temp);
            }
            if (BaristaCheck())
            {
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Barista").Components.FirstOrDefault(m => m.Name == "Sidamo").Percentage = (int.Parse(NewBaristaSidamoPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Barista").Components.FirstOrDefault(m => m.Name == "Santos").Percentage = (int.Parse(NewBaristaSantosPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Barista").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage = (int.Parse(NewBaristaRobustaPerc.Text));
                temp.SaveConfig();
                StartConfig(temp);
            }
            if (BrazilCheck())
            {
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Brazil").Components.FirstOrDefault(m => m.Name == "Santos").Percentage = (int.Parse(NewBrazilSantosPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Brazil").Components.FirstOrDefault(m => m.Name == "Mogiana").Percentage = (int.Parse(NewBrazilMogianaPerc.Text));
                temp.Config.Mixtures.FirstOrDefault(m => m.Name == "Brazil").Components.FirstOrDefault(m => m.Name == "Robusta").Percentage = (int.Parse(NewBrazilRobustaPerc.Text));
                temp.SaveConfig();
                StartConfig(temp);
            }
            if (IntensoCheck() || CremaCheck() || BaristaCheck() || BrazilCheck())
                ConfigSaved?.Invoke();
        }
    }
}
