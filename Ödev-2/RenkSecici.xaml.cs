using Microsoft.Maui.Controls;

namespace Ödev_2
{
    public partial class RenkSecici : ContentPage
    {
        public RenkSecici()
        {
            InitializeComponent();
            UpdateColor();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            int red = (int)RedSlider.Value;
            int green = (int)GreenSlider.Value;
            int blue = (int)BlueSlider.Value;

            Color color = Color.FromRgb(red, green, blue);
            ColorPreview.BackgroundColor = color;

            string hexCode = $"#{red:X2}{green:X2}{blue:X2}";
            ColorCodeEntry.Text = hexCode;
        }

        private void OnRandomButtonClicked(object sender, EventArgs e)
        {
            var random = new Random();
            RedSlider.Value = random.Next(0, 256);
            GreenSlider.Value = random.Next(0, 256);
            BlueSlider.Value = random.Next(0, 256);
        }
    }
}
