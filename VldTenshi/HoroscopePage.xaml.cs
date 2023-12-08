using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VldTenshi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HoroscopePage : ContentPage
	{
        //public enum ZodiacSign
        //{
        //    Aries,
        //    Taurus,
        //    Gemini,
        //    Cancer,
        //    Leo,
        //    Virgo,
        //    Libra,
        //    Scorpio,
        //    Sagittarius,
        //    Capricorn,
        //    Aquarius,
        //    Pisces,
        //    Unknown
        //}
        public HoroscopePage()
		{
            //InitializeComponent();

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
				BindingContext = "Zodiac Photo: ",
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
				string enteredZodiac = zodiacEntry.Text?.ToUpper();
                string placeholderText = "Enter Zodiac Sign";
                // Обработка нажатия кнопки
                if (string.IsNullOrWhiteSpace(enteredZodiac) || enteredZodiac == placeholderText)
				{
					// Если значение не было введено, выведите сообщение пользователю
					DisplayAlert("Error", "Please enter a Zodiac sign.", "OK");
					return; // Прекратить выполнение дальнейших действий
				}
				else
				{
					DateTime dateRangeStart, dateRangeEnd;

					GetZodiacDateRange(enteredZodiac, out dateRangeStart, out dateRangeEnd);
					dateResultLabel.Text = $"Date Range: {dateRangeStart:MM/dd/yyyy} - {dateRangeEnd:MM/dd/yyyy}";

					//Отображение изображения для знака зодиака
					DisplayZodiacImage(enteredZodiac, zodiacImage);
				}
				//if (Enum.TryParse(enteredZodiac, true, out ZodiacSign zodiacSign))
				//{
				//    DateTime dateRangeStart, dateRangeEnd;
				//    GetZodiacDateRange(zodiacSign, out dateRangeStart, out dateRangeEnd);
				//    dateResultLabel.Text = $"Date Range: {dateRangeStart:MM/dd/yyyy} - {dateRangeEnd:MM/dd/yyyy}";

				//    DisplayZodiacImage(zodiacSign, zodiacImage);
				//}
				//else
				//{
				//    DisplayAlert("Error", "Please enter a valid Zodiac sign.", "OK");
				//}
			};


			// Создаем разметку
			var stackLayout = new StackLayout
			{
				Padding = new Thickness(20),
				Spacing = 10,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { dateLabel, datePicker, resultLabel, checkButton, zodiacLabel, zodiacEntry, dateResultLabel, checkZodiacButton, zodiacImage }
			};

			Content = stackLayout;
			// Добавляем кнопку возврата в верхний тулбар
			ToolbarItems.Add(new ToolbarItem("Back", null, async () => await Navigation.PopAsync()));

		}

		//Метод для отображения изображения для знака зодиака
		private void DisplayZodiacImage(string enteredZodiac, Image zodiacImage)
		{
            // Создаём коллекцию изображений для каждого знака зодиака
            Dictionary<string, string> zodiacImages = new Dictionary<string, string>
        {
            { "Aries", "Aries.jpg" },
            { "Taurus", "Taurus.jpg" },
            { "Gemini", "Gemini.jpg" },
            { "Cancer", "Cancer.jpg" },
            { "Leo", "Leo.jpg" },
            { "Virgo", "Virgo.jpg" },
            { "Libra", "Libra.jpg" },
            { "Scorpio", "Scorpio.jpg" },
            { "Sagittarius", "Sagittarius.jpg" },
            { "Capricorn", "Capricorn.jpg" },
            { "Aquarius", "Aquarius.jpg" },
            { "Pisces", "Pisces.jpg" },
        };
            if (!string.IsNullOrEmpty(enteredZodiac) && enteredZodiac.ToLower() != "unknown")
			{
				if(zodiacImages.TryGetValue(enteredZodiac, out string imageName)) 
				{
					// Путь к изображению
					string imagePath = $"VldTenshi.Resources.drawable.{imageName}";

					// Устанавливаем источник изображения
					zodiacImage.Source = ImageSource.FromResource(imagePath, typeof(HoroscopePage).GetTypeInfo().Assembly);
				}
				else
                {
                        // Если знак зодиака не найден, очищаем изображение
                        zodiacImage.Source = null;
                }
            }

            else
			{
				// Если знак зодиака не найден, очищаем изображение
				zodiacImage.Source = null;
			}
		}
		//private void DisplayZodiacImage(ZodiacSign zodiac, Image zodiacImage)
  //      {
  //          if (zodiac != ZodiacSign.Unknown)
  //          {
  //              string zodiacName = zodiac.ToString();
  //              string imagePath = $"VldTenshi.Resources.drawable.{zodiacName}.jpg";

  //              zodiacImage.Source = ImageSource.FromResource(imagePath, typeof(HoroscopePage).GetTypeInfo().Assembly);
  //          }
  //          else
  //          {
  //              zodiacImage.Source = null;
  //          }
  //      }

        // Метод для определения знака зодиака по дате
        private /*ZodiacSign*/ string GetZodiacSign(DateTime date)
		{
			if ((date.Month == 3 && date.Day >= 21) || (date.Month == 4 && date.Day <= 19))
				return /*ZodiacSign.*/"Aries";
			else if ((date.Month == 4 && date.Day >= 20) || (date.Month == 5 && date.Day <= 20))
				return /*ZodiacSign.*/"Taurus";
			else if ((date.Month == 5 && date.Day >= 21) || (date.Month == 6 && date.Day <= 20))
				return /*ZodiacSign.*/"Gemini";
			else if ((date.Month == 6 && date.Day >= 21) || (date.Month == 7 && date.Day <= 22))
				return /*ZodiacSign.*/"Cancer";
			else if ((date.Month == 7 && date.Day >= 23) || (date.Month == 8 && date.Day <= 22))
				return /*ZodiacSign.*/"Leo";
			else if ((date.Month == 8 && date.Day >= 23) || (date.Month == 9 && date.Day <= 22))
				return /*ZodiacSign.*/"Virgo";
			else if ((date.Month == 9 && date.Day >= 23) || (date.Month == 10 && date.Day <= 22))
				return /*ZodiacSign.*/"Libra";
			else if ((date.Month == 10 && date.Day >= 23) || (date.Month == 11 && date.Day <= 21))
				return /*ZodiacSign.*/"Scorpio";
			else if ((date.Month == 11 && date.Day >= 22) || (date.Month == 12 && date.Day <= 21))
				return /*ZodiacSign.*/"Sagittarius";
			else if ((date.Month == 12 && date.Day >= 22) || (date.Month == 1 && date.Day <= 19))
				return /*ZodiacSign.*/"Capricorn";
			else if ((date.Month == 1 && date.Day >= 20) || (date.Month == 2 && date.Day <= 18))
				return /*ZodiacSign.*/"Aquarius";
			else if ((date.Month == 2 && date.Day >= 19) || (date.Month == 3 && date.Day <= 20))
				return /*ZodiacSign.*/"Pisces";
			else
				return /*ZodiacSign.*/"Unknown";
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