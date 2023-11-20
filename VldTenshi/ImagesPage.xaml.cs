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
	public partial class ImagesPage : ContentPage
	{
		Switch sw;
		Image img;
		public ImagesPage ()
		{
			img = new Image { Source = "scr.jfif" };
			var tap = new TapGestureRecognizer();
			tap.Tapped += Tap_Tapped;
			tap.NumberOfTapsRequired = 2;
			img.GestureRecognizers.Add(tap);

			sw = new Switch
			{
				IsToggled = false,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
			};
			sw.Toggled += Sw_Toggled;
			Content = new StackLayout { Children = {sw, img}};
			// Добавляем кнопку возврата в верхний тулбар
			ToolbarItems.Add(new ToolbarItem("Back", null, async () => await Navigation.PopAsync()));
		}
		int taps = 0;

		private void Tap_Tapped(object sender, EventArgs e)
		{
			taps++;
			Image img = (Image)sender;
			if (taps % 2 == 0)
			{
				img.Source = "scr2.jfif";
			}
			else
			{
				img.Source = "scr.jfif";
			}
		}

		private void Sw_Toggled(object sender, ToggledEventArgs e)
		{
			if (e.Value)
			{
				img.IsVisible = true;
			}
			else
			{
				img.IsVisible = false;
			}
		}
	}
}