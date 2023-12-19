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
	
	public partial class StartPage1 : ContentPage
	{
		List<ContentPage> pages = new List<ContentPage>() { new Startpage(), new BoxView_Page(), new Timerpage(), new DateTime_Page(), new StepperSlider_Page(), new TrafficLight(), new FrameGridPage(), new ImagesPage(), new HoroscopePage(), new PickerPage(), new Table_Page(), new Catalog(), new WordGameApp() };
		List<string> tekst = new List<string> { "Ava Startpage leht", "Ava BoxView leht", "Ava Timer leht", "Ava DateTime leht", "Ava StepperSlider leht", "Ava TrafficLight leht", "Ava Grid leht", "Ava Image leht", "Ava Horoscope leht", "Ava List", "Ava tabel", "Ava leht", "Ava mäng" };
		StackLayout st;
		public StartPage1()
		{
			st = new StackLayout();
			{
				//Orientation = StackOrientation.Vertical,
				BackgroundColor = Color.YellowGreen;
			};
			for (int i = 0; i < pages.Count; i++)
			{ 
				Button button = new Button
				{
					Text = tekst[i],
					TabIndex = i,
					BackgroundColor = Color.Red,
					TextColor = Color.White,
					FontFamily = "MyFont.ttf"
				};
					st.Children.Add(button);
					button.Clicked += Button_Clicked1;
			}
			ScrollView sv = new ScrollView {Content = st };
			Content =sv;
		}

		private async void Button_Clicked1(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			await Navigation.PushAsync(pages[btn.TabIndex]);
		}
	}
}