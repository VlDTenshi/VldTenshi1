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
		Label resultLabel;
		Image zodiacImage;
		Label zodiacDescriptionLabel;
		

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

			resultLabel = new Label
			{
				Text = "Zodiac Sign: ",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			zodiacImage = new Image
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
			zodiacDescriptionLabel = new Label();
			checkButton.Clicked += (sender, e) =>
			{
				ShowHoroscope(datePicker.Date);
			};
			var stackLayout = new StackLayout
			{
				Padding = new Thickness(20),
				Spacing = 10,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { dateLabel, datePicker, resultLabel, checkButton, zodiacImage, zodiacDescriptionLabel }
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

				}
			};

			// Создаем разметку
			var stackLayout1 = new StackLayout
			{
				Padding = new Thickness(20),
				Spacing = 10,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { zodiacLabel, zodiacEntry, dateResultLabel, checkZodiacButton, zodiacImage, zodiacDescriptionLabel }
			};
			var mainStackLayout = new StackLayout
			{
				Spacing = 10,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { stackLayout, stackLayout1 }
			};
			
			Content = mainStackLayout;
			// Добавляем кнопку возврата в верхний тулбар
			ToolbarItems.Add(new ToolbarItem("Back", null, async () => await Navigation.PopAsync()));
		}

		// Метод для отображения изображения для знака зодиака
		private void ShowHoroscope(DateTime date)
		{

			string zodiacSign = GetZodiacSign(date);


			resultLabel.Text = $"Your zodiac sign is {zodiacSign}.";


			DisplayZodiacInfo(zodiacSign);


			zodiacDescriptionLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
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
		private void DisplayZodiacInfo(string zodiacSign)
		{

			switch (zodiacSign.ToLower())
			{
				case "aries":
					zodiacImage.Source = "Aries.jpg";
					zodiacDescriptionLabel.Text = " людей, родившихся под знаком Овна, характер непростой. Их целеустремлённость однобока — её хватает ненадолго: " +
						"они быстро увлекаются и настолько же быстро теряют интерес. Часто это становится причиной развития не вполне позитивных сценариев их жизни — " +
						"участие в сомнительных сделках, погружение в рискованные профессии.";
					break;
				case "taurus":
					zodiacImage.Source = "Taurus.jpg";
					zodiacDescriptionLabel.Text = "У родившихся под покровительством Тельца характер, несмотря на всё упорство, описанное выше, отличается пассивностью. " +
						"Да, он готов работать до седьмого и далее пота. Но активно искать каких-то дополнительных " +
						" возможностей он не станет, а просто продолжит уверенно заниматься своим делом. ";
					break;
				case "gemini":
					zodiacImage.Source = "Gemini.jpg";
					zodiacDescriptionLabel.Text = "Близнецы — самый многогранный знак зодиака с рядом выдающихся качеств, которые окружающими не всегда воспринимаются верно." +
						" Это очень тонкие, сложные и интересные люди. Их можно познавать всю жизнь и всякий раз открывать что-то новое.  " +
						"Характер их легок и интересен. Они логичны, интеллектуальны, коммуникативны, позитивны, легки в жизни и общении, а еще имеют энциклопедический склад ума.";
					break;
				case "cancer":
					zodiacImage.Source = "Cancer.jpg";
					zodiacDescriptionLabel.Text = "Раки поражают глубиной и красотой своего внутреннего мира." +
						"Раки не склонны к открытому противостоянию, стараются сгладить острые углы и не допустить конфликта.  " +
						"Но если прижать к стенке, то будут ожесточенно бороться. Представителям этого знака бывает довольно сложно делиться своими эмоциями и переживаниями с другими людьми. ";
					break;
				case "leo":
					zodiacImage.Source = "Leo.jpg";
					zodiacDescriptionLabel.Text = "У Львов есть склонность к управлению — это сильные личности, настроенные на завоевание мира. " +
						"Лев стремится к вершине успеха и достигает ее потому, что знает, чего хочет.   " +
						"Его характер настолько непредсказуем, насколько и силен. Знак невероятно обаятельный, яркий, любящий внимание. ";
					break;
				case "virgo":
					zodiacImage.Source = "Virgo.jpg";
					zodiacDescriptionLabel.Text = "Не бойтесь довериться Деве — представитель знака всегда поддержит и встанет на вашу сторону, если что-то пойдет не так. " +
						"Дева — настоящий реалист. Она смотрит на жизнь так, что та иногда кажется ей немного страшной.  " +
						"Но при этом Дева не боится брать ответственность, умеет управляться с деньгами и знает им цену. ";
					break;
				case "libra":
					zodiacImage.Source = "Libro.jpg";
					zodiacDescriptionLabel.Text = "Рожденные под покровительство Венеры Весы — утонченные эстеты, творцы, обладатели особого взгляда на мир. " +
						"Они спокойны, рассудительны, дипломатичны, обладают острым чувством справедливости. " +
						"Это один из самых комфортных в общении знаков зодиака. Весам часто не хватает уверенности в себе и решительности..";
					break;
				case "scorpio":
					zodiacImage.Source = "Scorpio.jpg";
					zodiacDescriptionLabel.Text = "Человек, рожденный под знаком Скорпиона, сочетает в себе противоречивые черты. Он может быть сдержанным в проявлении чувств и даже казаться холодным, но в душе это очень эмоциональный человек." +
						"н прямолинеен, умен, не лезет за словом в карман. " +
						" Обладает невероятной стойкостью, с достоинством выдерживает любые испытания и идет к цели несмотря ни на что. Честолюбив и амбициозен. ";
					break;

				case "sagittarius":
					zodiacImage.Source = "Sagittarius.jpg";
					zodiacDescriptionLabel.Text = "Огненная стихия оказывает сильное воздействие на характер Стрельца. " +
						"ти излучающие оптимизм люди заряжают окружающих своей энергией. " +
						" Стрельцы прекрасно проявляют себя на ниве преподавательской деятельности, но могут с успехом податься и в политику.";
					break;
				case "capricorn":
					zodiacImage.Source = "Capricorn.jpg";
					zodiacDescriptionLabel.Text = "Они выделяются амбициозностью и стойкостью. Козероги стараются избегать двусмысленных ситуаций и прямых конфликтов. " +
						" Такая позиция помогает им обходиться в жизни без явных врагов. Козероги предпочитают все держать в себе: не делиться душевным состоянием с окружающими, также как и не высказывать открыто негатив. " +
						" Внешне они спокойные, немногословные, отстраненные. Как никто, умеют совладать с эмоциями и подчинять их логике и разуму. ";
					break;
				case "aquarius":
					zodiacImage.Source = "Aquarius.jpg";
					zodiacDescriptionLabel.Text = "Водолей — реалист, который стремится изменить мир к лучшему. " +
						" В нем всегда бурлит творческая энергия, гениальные мысли, планы и задумки." +
						"При этом Водолей никогда не остановится на половине пути к намеченной цели и всегда будет идти до победного конца. ";
					break;
				case "pisces":
					zodiacImage.Source = "Pisces.jpg";
					zodiacDescriptionLabel.Text = "Рыбы — утончённые натуры, отличающиеся чувственностью и проницательностью. " +
						" Они всегда готовы прийти на помощь и поддержать в трудную минуту. Иногда отзывчивость делает их жертвами манипуляторов, " +
						" но благодаря врождённой интуиции удаётся обернуть ситуацию в свою пользу, взяв всё под контроль.";
					break;




				default:
					zodiacImage.Source = null;
					zodiacDescriptionLabel.Text = "No information available for this zodiac sign.";

					break;
			}
		}
	}
}