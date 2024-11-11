using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Ödev_2
{
    public partial class KrediHesaplama : ContentPage
    {
        private double KKDF = 0;
        private double BSMV = 0;

        public KrediHesaplama()
        {
            InitializeComponent();
        }

        private void OnKrediTuruChanged(object sender, EventArgs e)
        {
            switch (KrediTuruPicker.SelectedItem as string)
            {
                case "İhtiyaç Kredisi":
                    KKDF = 0.15;
                    BSMV = 0.10;
                    break;
                case "Konut Kredisi":
                    KKDF = 0;
                    BSMV = 0;
                    break;
                case "Taşıt Kredisi":
                    KKDF = 0.15;
                    BSMV = 0.05;
                    break;
                case "Ticari Kredi":
                    KKDF = 0.06;
                    BSMV = 0.05;
                    break;
                default:
                    KKDF = 0;
                    BSMV = 0;
                    break;
            }
        }

        private void OnVadeSliderChanged(object sender, ValueChangedEventArgs e)
        {
            // Slider kaydırıldıkça vade değeri Entry'ye yansır ve hesaplama yapılır.
            int vade = (int)VadeSlider.Value;
            VadeEntry.Text = vade.ToString();  // VadeEntry'yi güncelle
            Hesapla(vade);  // Hesaplamayı yeniden yap
        }

        private void OnVadeEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // Manuel yazılan değeri al ve slider'ı güncelle
            if (int.TryParse(VadeEntry.Text, out int vade))
            {
                if (vade >= VadeSlider.Minimum && vade <= VadeSlider.Maximum)
                {
                    VadeSlider.Value = vade;  // Slider'ı manuel girilen değere göre ayarla
                    Hesapla(vade);  // Hesaplama yapılır
                }
                else
                {
                    // Girilen değer slider'ın aralığından dışında ise hata mesajı ya da farklı bir işlem yapılabilir
                    // Örneğin, maksimum ve minimum sınırlarına çekebiliriz
                    VadeEntry.Text = VadeSlider.Minimum.ToString();  // Örnek olarak minimum değer gösterilebilir
                }
            }
        }

        private void OnHesaplaClicked(object sender, EventArgs e)
        {
            int vade = (int)VadeSlider.Value;  // Slider değeri alınır
            Hesapla(vade);  // Hesaplamayı yap
        }

        private void Hesapla(int vade)
        {
            double krediTutari = 0;
            double faizOrani = 0;

            string krediTutariText = KrediTutariEntry.Text.Replace(',', '.');
            string faizOraniText = FaizOraniEntry.Text.Replace(',', '.');

            double.TryParse(krediTutariText, NumberStyles.Any, CultureInfo.InvariantCulture, out krediTutari);
            double.TryParse(faizOraniText, NumberStyles.Any, CultureInfo.InvariantCulture, out faizOrani);

            double brutFaiz = faizOrani * (1 + BSMV + KKDF);

            double taksit = ((Math.Pow(1 + brutFaiz / 100, vade) * brutFaiz / 100) / (Math.Pow(1 + brutFaiz / 100, vade) - 1)) * krediTutari;

            double toplamOdeme = (taksit * vade);
            double toplamFaiz = toplamOdeme - krediTutari;

            AylikTaksitLabel.Text = $"{taksit:F2} TL";
            ToplamOdemeLabel.Text = $"{toplamOdeme:F2} TL";
            ToplamFaizLabel.Text = $"{toplamFaiz:F2} TL";
        }
    }
}
