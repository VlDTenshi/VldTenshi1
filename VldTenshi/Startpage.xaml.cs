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
    public partial class Startpage : ContentPage
    {
        public Startpage()
        {
            Button Ent_btn = new Button
            {
                Text = "Entry page",
                BackgroundColor = Color.DeepSkyBlue,
            };

            Button Time_btn = new Button
            {
                Text = "Timer page",
                BackgroundColor = Color.DeepSkyBlue,
            };
            StackLayout st = new StackLayout
            {
                Children = { Ent_btn, Time_btn },
                BackgroundColor= Color.AliceBlue,
            };
            Content= st;
            Ent_btn.Clicked += Ent_btn_Clicked;
            Time_btn.Clicked += Time_btn_Clicked;
        }

        private async void Time_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Timerpage());
        }

        private async void Ent_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Secondpage());
        }
    }
}