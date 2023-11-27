using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace VldTenshi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        Picker picker;
        WebView webView;
        StackLayout st;
        StackLayout st2;
        StackLayout st3;
        Frame fr;
        Entry addressEntry;
        string[] lehed = new string[4] { "https://moodle.edu.ee", "https://www.tthk.ee/", "https://tahvel.edu.ee/#/", "https://mail.google.com/" };
        int currentPageIndex = 0;
        public PickerPage()
        {
            picker = new Picker
            {
                Title ="Lehed"
            };
            picker.Items.Add("Moodle");
            picker.Items.Add("TTHK");
            picker.Items.Add("Tahvel");
            picker.Items.Add("Gmail");
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

            addressEntry = new Entry
            {
                Placeholder = "Enter URL and press Enter",
                ReturnType = ReturnType.Go
            };
            addressEntry.Completed += AddressEntry_Completed;


            webView = new WebView();
            SwipeGestureRecognizer swipe = new SwipeGestureRecognizer();
            swipe.Swiped += Swipe_Swiped;
            swipe.Direction = SwipeDirection.Right;
            fr= new Frame
            {
                BorderColor = Color.GreenYellow,
                CornerRadius = 20,
                HeightRequest = 20, WidthRequest = 400,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HasShadow = true,
            };
            var backButton = new Button
            {
                Text = "Back",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            backButton.Clicked += BackButton_Clicked;

            var forwardButton = new Button
            {
                Text = "Forward",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            forwardButton.Clicked += ForwardButton_Clicked;

            var homeButton = new Button
            {
                Text = "Home",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            homeButton.Clicked += HomeButton_Clicked;
            st = new StackLayout { Children = { picker, fr } };
            st2 = new StackLayout { Children = { addressEntry, backButton, forwardButton, homeButton } };
            st3 = new StackLayout { Children = { st, st2 } };
            fr.GestureRecognizers.Add(swipe);
            Content = st3;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            currentPageIndex = (currentPageIndex - 1 + lehed.Length) % lehed.Length;
            webView.Source = new UrlWebViewSource { Url = lehed[currentPageIndex] };
        }

        private void ForwardButton_Clicked(object sender, EventArgs e)
        {
            currentPageIndex = (currentPageIndex + 1) % lehed.Length;
            webView.Source = new UrlWebViewSource { Url = lehed[currentPageIndex] };
        }

        private void HomeButton_Clicked(object sender, EventArgs e)
        {
            // Задайте домашнюю страницу по вашему выбору
            string homePageUrl = "https://example.com";
            webView.Source = new UrlWebViewSource { Url = homePageUrl };
        }

        private async void AddressEntry_Completed(object sender, EventArgs e)
        {
            string enteredUrl = addressEntry.Text;

            if (Uri.TryCreate(enteredUrl, UriKind.Absolute, out Uri result))
            {
                webView.Source = new UrlWebViewSource { Url = enteredUrl };
            }
            else
            {
                bool isUserWantsToAdd = await DisplayAlert("Invalid URL", "Do you want to add this URL to the list?", "Yes", "No");

                if (isUserWantsToAdd)
                {
                    {
                        // Всплывающее окно для ввода пользовательского URL
                        var customUrlPopup = new CustomUrlPopup();
                        customUrlPopup.OnUrlEntered += CustomUrlPopup_OnUrlEntered;
                        customUrlPopup.Show();
                    }
                }
                else
                {
                    DisplayAlert("Error", "Invalid URL format. Please enter a valid URL.", "OK");
                }
            }
        }

        private void CustomUrlPopup_OnUrlEntered(object sender, string enteredUrl)
        {
            // Закрыть всплывающее окно
            var customUrlPopup = (CustomUrlPopup)sender;
            customUrlPopup.Close();

            // Добавление нового URL в список
            Array.Resize(ref lehed, lehed.Length + 1);
            lehed[lehed.Length - 1] = enteredUrl;

            // Обновление Picker
            picker.Items.Add("Custom");
            picker.SelectedIndex = picker.Items.Count - 1;
            currentPageIndex = picker.SelectedIndex;
            webView.Source = new UrlWebViewSource { Url = enteredUrl };
        }

        //int ind=0;
        private void Swipe_Swiped(object sender, SwipedEventArgs e)
        {
            currentPageIndex = (currentPageIndex + 1) % lehed.Length;
            webView.Source = new UrlWebViewSource { Url = lehed[currentPageIndex] };
            //webView.Source = new UrlWebViewSource { Url = lehed[ind] };
            //ind++
            //if (ind == lehed.Length)
            //{
            //  ind = 0;
            //}
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (webView != null) 
            {
                st.Children.Remove(webView);
            }
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = lehed[picker.SelectedIndex] },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            currentPageIndex = picker.SelectedIndex;
            st.Children.Add(webView);
        }
    }
}