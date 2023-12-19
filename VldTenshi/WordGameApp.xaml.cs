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
	public partial class WordGameApp : ContentPage
	{
		private string secretWord = "APPLE"; // загаданное слово
		private List<Entry> letterEntries; // список элементов ввода букв

		public WordGameApp()
		{
			InitializeComponent();

			// Инициализация элементов интерфейса
			InitializeUI();
		}

		private void InitializeUI()
		{
			// Создание списка элементов ввода букв
			letterEntries = new List<Entry>();

			// Создание сетки для ячеек
			Grid grid = new Grid
			{
				RowDefinitions = new RowDefinitionCollection
				{
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
				},
				ColumnDefinitions = new ColumnDefinitionCollection
				{
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};

			// Заполнение сетки элементами ввода букв
			for (int row = 0; row < 6; row++)
			{
				for (int col = 0; col < 5; col++)
				{
					Entry entry = new Entry
					{
						Text = "",
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.FillAndExpand,
						Keyboard = Keyboard.Text,
						MaxLength = 1,
						CharacterSpacing = 5,
						Margin = new Thickness(1),
					};

					// Добавление обработчика события изменения текста для проверки слова
					entry.TextChanged += Entry_TextChanged1; ;

					// Добавление элемента ввода в список и в сетку
					letterEntries.Add(entry);
					grid.Children.Add(entry, col, row);
				}
			}

			// Добавление обработчика события для управления фокусом
			foreach (var entry in letterEntries)
			{
				entry.TextChanged += (s, e) => { MoveToNextEntry(s as Entry); };
			}

			// Кнопка для проверки слова
			Button checkButton = new Button
			{
				Text = "Check Word",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Margin = new Thickness(10),
			};

			// Добавление обработчика события для кнопки
			checkButton.Clicked += CheckButton_Clicked1; ;

			// Добавление элементов интерфейса на страницу
			Content = new StackLayout
			{
				Children = {
					grid,
					checkButton
				}
			};
		}
		// Метод для автоматического переключения фокуса на следующую ячейку
		private void MoveToNextEntry(Entry currentEntry)
		{
			var index = letterEntries.IndexOf(currentEntry);
			if (index < letterEntries.Count - 1 && currentEntry.Text.Length == 1)
			{
				letterEntries[index + 1].Focus();
			}
			else if (index == letterEntries.Count - 1 && currentEntry.Text.Length == 1)
			{
				// Если это последняя ячейка в строке, переходите к следующей строке и первой ячейке
				letterEntries[(index + 1) % 5].Focus();
			}
		}

		private void CheckButton_Clicked1(object sender, EventArgs e)
		{
			// Проверка введенного слова
			string guessedWord = "";
			foreach (var entry in letterEntries)
			{
				guessedWord += entry.Text;
			}
			Console.WriteLine($"Secret Word: {secretWord}");
			// Вывод в консоль для проверки
			Console.WriteLine($"Guessed Word: {guessedWord}");

			// Подсветка букв в зависимости от правильности
			for (int i = 0; i < guessedWord.Length; i++)
			{
				if (i < secretWord.Length)
				{
					if (guessedWord[i] == secretWord[i])
					{
						letterEntries[i].BackgroundColor = Color.Green; // буква на правильном месте
					}
					else if (secretWord.Contains(guessedWord[i].ToString()))
					{
						int index = secretWord.IndexOf(guessedWord[i]);
						if (index != -1 && index != i)
						{
							letterEntries[i].BackgroundColor = Color.Yellow; // буква есть, но не на правильном месте
						}
					}
						Console.WriteLine($"Comparing {guessedWord[i]} with {secretWord[i]}");
				}
			}

			// Добавьте логику для отображения сообщения пользователю
			DisplayAlert("Result", "Word Checked!", "OK");
		
		}

		private void Entry_TextChanged1(object sender, TextChangedEventArgs e)
		{
			// Очищение подсветки при изменении текста в ячейке
			Entry entry = (Entry)sender;
			entry.BackgroundColor = Color.Default;
		}
	}
}