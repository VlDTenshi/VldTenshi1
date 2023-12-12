using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VldTenshi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Secondpage : ContentPage
    {
        Label label, label2;
        Editor editor;
        public Secondpage()
        {
            editor= new Editor
            {
                Placeholder="Put the text here",
                BackgroundColor= Color.DeepSkyBlue,
                TextColor= Color.RosyBrown,
            };
            //editor.TextChanged += Editor_TextChanged;
            label = new Label
            {
                Text="MainTitle",
                HorizontalOptions =LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.SandyBrown,
                BackgroundColor = Color.DeepSkyBlue,
            };
			label2 = new Label
			{
				Text = Preferences.Get("key2", "Ei ole veel key2"),
				HorizontalOptions = LayoutOptions.Start,
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
            Button c = new Button
            {
                Text = "Salvesta omadus",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
			c.Clicked += C_Clicked;
            Button d = new Button
            {
                Text = "Salvesta Preferences",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
			d.Clicked += D_Clicked;
            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { label, editor,b,c,d, label2 },
                BackgroundColor = Color.AliceBlue,
            };
            Content=st;
            //st.Children.Add(b);
        }
        int j = 1;
		private void D_Clicked(object sender, EventArgs e)
		{
			string value2 = editor.Text;
            Preferences.Set("key"+j.ToString(), value2);
            label2.Text = " "+value2;
            j++;
		}

		private void C_Clicked(object sender, EventArgs e)
		{
			
				string value = editor.Text;

				if (App.Current.Properties.ContainsKey("key"))
				{
					// Свойство с ключом "key" уже существует
					string existingValue = (string)App.Current.Properties["key"];

					if (existingValue == value)
					{
						// Текст не изменился, выводите сообщение об ошибке или выполняйте необходимые действия
						DisplayAlert("Ошибка", "Текст не изменился. Введите другой текст или удалите текущий.", "OK");
					}
					else
					{
						// Текст изменился, обновите значение свойства
						App.Current.Properties["key"] = value;
						label.Text = value;
					}
				}
				else
				{
					// Свойства с ключом "key" нет, просто добавьте его
					App.Current.Properties.Add("key", value);
					label.Text = value;
				}
			
			//string value = editor.Text;
			//         App.Current.Properties.Remove("key");
			//         App.Current.Properties.Add("key", value);
			//         label.Text = (string)App.Current.Properties["key"];
		}
		private void DeleteProperty()
		{
			if (App.Current.Properties.ContainsKey("key"))
			{
				// Свойство с ключом "key" существует, удаляем его
				App.Current.Properties.Remove("key");
				label.Text = "Свойство удалено";
			}
			else
			{
				// Свойства с ключом "key" нет, выводим сообщение
				DisplayAlert("Ошибка", "Свойства с ключом 'key' не существует.", "OK");
			}
		}

		protected override void OnAppearing()
		{
            object key = "";
            if (App.Current.Properties.TryGetValue("key", out key))
            {
                label.Text = (string)App.Current.Properties["key"];
            }
			base.OnAppearing();
		}

		int i = 0;
        //private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    string newText = e.NewTextValue ?? "";
        //    string oldText = e.OldTextValue ?? "";

        //    if (newText.Length < oldText.Length) // Текст был удален
        //    {
        //        char deletedChar = oldText[oldText.Length - 1];
        //        if (deletedChar == 'A')
        //        {
        //            i--;
        //        }
        //    }
        //    else if (newText.Length > oldText.Length) // Текст был добавлен
        //    {
        //        char addedChar = newText[newText.Length - 1];
        //        if (addedChar == 'A')
        //        {
        //            i++;
        //        }
        //    }

        //    label.Text = "A: " + i.ToString();
        //}

        private async void B_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}