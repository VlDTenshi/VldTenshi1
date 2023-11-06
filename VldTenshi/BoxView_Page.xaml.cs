using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VldTenshi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoxView_Page : ContentPage
    {
        Slider redSlider, greenSlider, blueSlider;
        BoxView box;
        public BoxView_Page()
        {
            box = new BoxView
            {
                Color=Color.FromRgb(0, 0, 0),
                CornerRadius=20,
                WidthRequest=300, HeightRequest=500,
                HorizontalOptions=LayoutOptions.Center,
                VerticalOptions=LayoutOptions.Center,
            };
            redSlider = new Slider
            {
                Maximum = 255,
                Value = 0,
                BackgroundColor=Color.Brown
            };

            greenSlider = new Slider
            {
                Maximum = 255,
                Value = 0,
                BackgroundColor = Color.GreenYellow
            };

            blueSlider = new Slider
            {
                Maximum = 255,
                Value = 0,
                BackgroundColor = Color.HotPink
            };
            redSlider.ValueChanged += Slider_ValueChanged;
            greenSlider.ValueChanged += Slider_ValueChanged;
            blueSlider.ValueChanged += Slider_ValueChanged;
            //TapGestureRecognizer tap = new TapGestureRecognizer();
            //tap.Tapped += Tap_Tapped;
            //box.GestureRecognizers.Add(tap);
            StackLayout st = new StackLayout { Children = { box, button1 } };
            st.Children.Add(redSlider);
            st.Children.Add(greenSlider);
            st.Children.Add(blueSlider);
            Content = st;
            button1.Clicked += Button1_Clicked;
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        Button button1 = new Button
        {
            Text = "To Start Page",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Color.SandyBrown,
        };
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.Color = Color.FromRgb((int)redSlider.Value, (int)greenSlider.Value, (int)blueSlider.Value);
        }
        Random rnd;
        //private void Tap_Tapped(object sender, EventArgs e)
        //{
        //    rnd = new Random();
        //    box.Color = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next());
        //}
    }
}