using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VldTenshi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Timerpage : ContentPage
    {
        public Timerpage()
        {
            InitializeComponent();
        }

        private async void btn_back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        bool onoff= false;
        CancellationTokenSource cts;
        private async void ShowTime()
        {
            while (onoff)
            {
                timer_value.Text = DateTime.Now.ToString("T");
                await Task.Delay(1000);
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (onoff)
            {
                onoff = false;
                timer_start.Text = "Start";
                cts?.Cancel();
            }
            else
            {
                onoff = true;
                timer_start.Text = "Stop";
                cts = new CancellationTokenSource();
                ShowTime();
            }
        }
    }
}