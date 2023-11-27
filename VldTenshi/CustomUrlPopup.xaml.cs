using Rg.Plugins.Popup.Services;
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
    public partial class CustomUrlPopup : ContentView
    {
        public event EventHandler<string> OnUrlEntered;

        Entry urlEntry;
        Button okButton;
        public CustomUrlPopup()
        {
            InitializeComponent();
        }
        private void InitializeUI()
        {
            urlEntry = new Entry
            {
                Placeholder = "Enter URL",
                Keyboard = Keyboard.Url,
                Margin = new Thickness(10),
            };

            okButton = new Button
            {
                Text = "OK",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(10),
            };
            okButton.Clicked += OkButton_Clicked;

            Content = new StackLayout
            {
                Children = { urlEntry, okButton },
            };
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            string enteredUrl = urlEntry.Text;
            OnUrlEntered?.Invoke(this, enteredUrl);
        }

        public void Show()
        {
            PopupNavigation.Instance.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage
            {
                Content = this,
                CloseWhenBackgroundIsClicked = true,
            });
        }

        public void Close()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}