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
    public partial class TrafficLight : ContentPage
    {
        private bool isTrafficLightOn = false;

        public TrafficLight()
        {
            InitializeComponent();
            // Запуск таймера при инициализации страницы
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                // Этот код будет выполняться каждые 10 секунд
                ToggleTrafficLight();
                return true; // Возвращаем true для повторного выполнения таймера
            });
        }

        private void OnSisseClicked(object sender, EventArgs e)
        {
            isTrafficLightOn = true;
            UpdateTrafficLight();
        }

        private void OnValjaClicked(object sender, EventArgs e)
        {
            isTrafficLightOn = false;
            UpdateTrafficLight();
        }

        private void UpdateTrafficLight()
        {
            RedFrame.BackgroundColor = isTrafficLightOn ? Color.Red : Color.Gray;
            YellowFrame.BackgroundColor = isTrafficLightOn ? Color.Yellow : Color.Gray;
            GreenFrame.BackgroundColor = isTrafficLightOn ? Color.Green : Color.Gray;

            if (!isTrafficLightOn)
            {
                RedFrame.IsEnabled = false;
                YellowFrame.IsEnabled = false;
                GreenFrame.IsEnabled = false;
            }
            else
            {
                RedFrame.IsEnabled = true;
                YellowFrame.IsEnabled = true;
                GreenFrame.IsEnabled = true;
            }
        }

        private void OnFrameTapped(object sender, EventArgs e)
        {
            if (!isTrafficLightOn)
            {
                DisplayAlert("Сначала включи светофор", "", "OK");
                return;
            }

            Frame tappedFrame = (Frame)sender;

            if (tappedFrame == RedFrame)
            {
                DisplayAlert("Стой", "", "OK");
            }
            else if (tappedFrame == YellowFrame)
            {
                DisplayAlert("Готовься", "", "OK");
            }
            else if (tappedFrame == GreenFrame)
            {
                DisplayAlert("Иди", "", "OK");
            }
        }
        private void ToggleTrafficLight()
        {
            if (isTrafficLightOn)
            {
                // Переключаем светофор
                isTrafficLightOn = false;
                UpdateTrafficLight();
            }
            else
            {
                // Включаем светофор
                isTrafficLightOn = true;
                UpdateTrafficLight();
            }
        }
    }
}