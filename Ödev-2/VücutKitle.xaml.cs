using Microsoft.Maui.Controls;
using System;

namespace Ödev_2
{
    public partial class VücutKitle : ContentPage
    {
        public VücutKitle()
        {
            InitializeComponent();

            
            WeightSlider.ValueChanged += (s, e) =>
            {
                WeightEntry.Text = e.NewValue.ToString("0.##");
                WeightLabel.Text = "Kilo: " + e.NewValue.ToString("0.##") + " kg";
            };

            
            HeightSlider.ValueChanged += (s, e) =>
            {
                HeightEntry.Text = e.NewValue.ToString("0.##");
                HeightLabel.Text = "Boy: " + e.NewValue.ToString("0.##") + " cm";
            };

           
            HeightEntry.TextChanged += (s, e) =>
            {
                if (double.TryParse(HeightEntry.Text, out double heightValue))
                {
                    HeightSlider.Value = heightValue;
                    HeightLabel.Text = "Boy: " + heightValue.ToString("0.##") + " cm";
                }
            };

          
            WeightEntry.TextChanged += (s, e) =>
            {
                if (double.TryParse(WeightEntry.Text, out double weightValue))
                {
                    WeightSlider.Value = weightValue;
                    WeightLabel.Text = "Kilo: " + weightValue.ToString("0.##") + " kg";
                }
            };
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
           
            if (double.TryParse(WeightEntry.Text.Replace('.', ','), out double weight) && double.TryParse(HeightEntry.Text.Replace('.', ','), out double height))
            {
                double vki = weight / ((height / 100) * (height / 100));
                string category = "";

                if (vki < 16)
                {
                    category = "İleri Düzeyde Zayıf";
                }
                else if (vki >= 16 && vki <= 16.99)
                {
                    category = "Orta Düzeyde Zayıf";
                }
                else if (vki >= 17 && vki <= 18.49)
                {
                    category = "Hafif Düzeyde Zayıf";
                }
                else if (vki >= 18.50 && vki <= 24.9)
                {
                    category = "Normal Kilolu";
                }
                else if (vki >= 25 && vki <= 29.99)
                {
                    category = "Hafif Obez / Fazla Kilolu";
                }
                else if (vki >= 30 && vki <= 34.99)
                {
                    category = "1. Derecede Obez";
                }
                else if (vki >= 35 && vki <= 39.99)
                {
                    category = "2. Derecede Obez";
                }
                else if (vki >= 40)
                {
                    category = "3. Derecede Obez / Morbid Obez";
                }

                ResultLabel.Text = $"VKI Sonucu: {vki:0.00} - {category}";
            }
            else
            {
                ResultLabel.Text = "Geçerli bir değer girin!";
            }
        }
    }
}
