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
    public partial class Secondpage : ContentPage
    {
        Label label;
        public Secondpage()
        {
            Editor editor= new Editor
            {
                Placeholder="Put the text here",
                BackgroundColor= Color.DeepSkyBlue,
                TextColor= Color.RosyBrown,
            };
            editor.TextChanged += Editor_TextChanged;
            label = new Label
            {
                Text="MainTitle",
                HorizontalOptions =LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.SandyBrown,
                BackgroundColor = Color.DeepSkyBlue,
            };
            Button b = new Button
            {
                Text = "To Start Page",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor= Color.SandyBrown,
            };
            b.Clicked += B_Clicked;  
            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { label, editor,b },
                BackgroundColor = Color.AliceBlue,
            };
            Content=st;
            //st.Children.Add(b);
        }
        
        int i = 0;
        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = e.NewTextValue ?? "";
            string oldText = e.OldTextValue ?? "";

            if (newText.Length < oldText.Length) // Текст был удален
            {
                char deletedChar = oldText[oldText.Length - 1];
                if (deletedChar == 'A')
                {
                    i--;
                }
            }
            else if (newText.Length > oldText.Length) // Текст был добавлен
            {
                char addedChar = newText[newText.Length - 1];
                if (addedChar == 'A')
                {
                    i++;
                }
            }

            label.Text = "A: " + i.ToString();
        }

        private async void B_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}