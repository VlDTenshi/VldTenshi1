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
	public partial class HoroscopePage : ContentPage
	{
		public HoroscopePage ()
		{
			InitializeComponent();

			// Добавляем элементы на страницу
			var dateLabel = new Label
			{
				Text = "Enter Birthdate:",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var datePicker = new DatePicker
			{
				Format = "MM/dd/yyyy",
				MaximumDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day),
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};

			var resultLabel = new Label
			{
				Text = "Zodiac Sign: ",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			var zodiacImage = new Image
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var checkButton = new Button
			{
				Text = "Check",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			checkButton.Clicked += (sender, e) =>
			{
				// Обработка нажатия кнопки
				DateTime selectedDate = datePicker.Date;

				string zodiacSign = GetZodiacSign(selectedDate);
				resultLabel.Text = "Zodiac Sign: " + zodiacSign;

				DisplayZodiacImage(zodiacSign, zodiacImage);
			};

			var zodiacLabel = new Label
			{
				Text = "Enter Zodiac Sign:",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var zodiacEntry = new Entry
			{
				Placeholder = "Enter Zodiac Sign",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var dateResultLabel = new Label
			{
				Text = "Date Range: ",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var checkZodiacButton = new Button
			{
				Text = "Check",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			checkZodiacButton.Clicked += (sender, e) =>
			{
				// Обработка нажатия кнопки
				string enteredZodiac = zodiacEntry.Text.ToUpper();
				DateTime dateRangeStart, dateRangeEnd;

				GetZodiacDateRange(enteredZodiac, out dateRangeStart, out dateRangeEnd);
				dateResultLabel.Text = $"Date Range: {dateRangeStart:MM/dd/yyyy} - {dateRangeEnd:MM/dd/yyyy}";

				//Отображение изображения для знака зодиака
				DisplayZodiacImage(enteredZodiac, zodiacImage);
			};

			// Создаем разметку
			var stackLayout = new StackLayout
			{
				Padding = new Thickness(20),
				Spacing = 10,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { dateLabel, datePicker, resultLabel, checkButton, zodiacLabel, zodiacEntry, dateResultLabel, checkZodiacButton }
			};

			Content = stackLayout;
			// Добавляем кнопку возврата в верхний тулбар
			ToolbarItems.Add(new ToolbarItem("Back", null, async () => await Navigation.PopAsync()));
		}

		// Метод для отображения изображения для знака зодиака
		private void DisplayZodiacImage(string zodiac, Image zodiacImage)
		{
			// Создайте коллекцию изображений для каждого знака зодиака
			Dictionary<string, string> zodiacImages = new Dictionary<string, string>
		{
			{ "ARIES", "Aries.jpg" },
			{ "TAURUS", "Taurus.jpg" },
			{ "GEMINI", "Gemini.jpg" },
			{ "CANCER", "Cancer.jpg" },
			{ "LEO", "Leo.jpg" },
			{ "VIRGO", "Virgo.jpg" },
			{ "LIBRA", "Libra.jpg" },
			{ "SCORPIO", "Scorpio.jpg" },
			{ "SAGITTARIUS", "Sagittarius.jpg" },
			{ "CAPRICORN", "Capricorn.jpg" },
			{ "AQUARIUS", "Aquarius.jpg" },
			{ "PISCES", "Pisces.jpg" },
		};

			// Проверьте, есть ли изображение для данного знака зодиака
			if (zodiacImages.ContainsKey(zodiac))
			{
				// Отобразите изображение
				string resourceName = zodiacImages[zodiac];
				zodiacImage.Source = ImageSource.FromResource("VldTenshi.Resources.drawable" + resourceName + ".jpg")
			}
			else
			{
					// Очистите изображение, если знак зодиака не найден
					zodiacImage.Source = null;
			}
		}
	}
		// Метод для определения знака зодиака по дате
		private string GetZodiacSign(DateTime date)
		{
			if ((date.Month == 3 && date.Day >= 21) || (date.Month == 4 && date.Day <= 19))
				return "Aries";
			else if ((date.Month == 4 && date.Day >= 20) || (date.Month == 5 && date.Day <= 20))
				return "Taurus";
			else if ((date.Month == 5 && date.Day >= 21) || (date.Month == 6 && date.Day <= 20))
				return "Gemini";
			else if ((date.Month == 6 && date.Day >= 21) || (date.Month == 7 && date.Day <= 22))
				return "Cancer";
			else if ((date.Month == 7 && date.Day >= 23) || (date.Month == 8 && date.Day <= 22))
				return "Leo";
			else if ((date.Month == 8 && date.Day >= 23) || (date.Month == 9 && date.Day <= 22))
				return "Virgo";
			else if ((date.Month == 9 && date.Day >= 23) || (date.Month == 10 && date.Day <= 22))
				return "Libra";
			else if ((date.Month == 10 && date.Day >= 23) || (date.Month == 11 && date.Day <= 21))
				return "Scorpio";
			else if ((date.Month == 11 && date.Day >= 22) || (date.Month == 12 && date.Day <= 21))
				return "Sagittarius";
			else if ((date.Month == 12 && date.Day >= 22) || (date.Month == 1 && date.Day <= 19))
				return "Capricorn";
			else if ((date.Month == 1 && date.Day >= 20) || (date.Month == 2 && date.Day <= 18))
				return "Aquarius";
			else if ((date.Month == 2 && date.Day >= 19) || (date.Month == 3 && date.Day <= 20))
				return "Pisces";
			else
				return "Unknown";
		}

		// Метод для определения промежутка времени по знаку зодиака
		private void GetZodiacDateRange(string zodiac, out DateTime startDate, out DateTime endDate)
		{
			// Пример промежутков времени для знаков зодиака
			switch (zodiac.ToUpper())
			{
				case "ARIES":
					startDate = new DateTime(DateTime.Now.Year, 3, 21);
					endDate = new DateTime(DateTime.Now.Year, 4, 19);
					break;
				case "TAURUS":
					startDate = new DateTime(DateTime.Now.Year, 4, 20);
					endDate = new DateTime(DateTime.Now.Year, 5, 20);
					break;
				case "GEMINI":
					startDate = new DateTime(DateTime.Now.Year, 5, 21);
					endDate = new DateTime(DateTime.Now.Year, 6, 20);
					break;
				case "CANCER":
					startDate = new DateTime(DateTime.Now.Year, 6, 21);
					endDate = new DateTime(DateTime.Now.Year, 7, 22);
					break;
				case "LEO":
					startDate = new DateTime(DateTime.Now.Year, 7, 23);
					endDate = new DateTime(DateTime.Now.Year, 8, 22);
					break;
				case "VIRGO":
					startDate = new DateTime(DateTime.Now.Year, 8, 23);
					endDate = new DateTime(DateTime.Now.Year, 9, 22);
					break;
				case "LIBRA":
					startDate = new DateTime(DateTime.Now.Year, 9, 23);
					endDate = new DateTime(DateTime.Now.Year, 10, 22);
					break;
				case "SCORPIO":
					startDate = new DateTime(DateTime.Now.Year, 10, 23);
					endDate = new DateTime(DateTime.Now.Year, 11, 21);
					break;
				case "SAGITTARIUS":
					startDate = new DateTime(DateTime.Now.Year, 11, 22);
					endDate = new DateTime(DateTime.Now.Year, 12, 21);
					break;
				case "CAPRICORN":
					startDate = new DateTime(DateTime.Now.Year, 12, 22);
					endDate = new DateTime(DateTime.Now.Year, 1, 19);
					break;
				case "AQUARIUS":
					startDate = new DateTime(DateTime.Now.Year, 1, 20);
					endDate = new DateTime(DateTime.Now.Year, 2, 18);
					break;
				case "PISCES":
					startDate = new DateTime(DateTime.Now.Year, 2, 19);
					endDate = new DateTime(DateTime.Now.Year, 3, 20);
					break;
				default:
					startDate = DateTime.MinValue;
					endDate = DateTime.MinValue;
					break;
			}
		}
	}
}