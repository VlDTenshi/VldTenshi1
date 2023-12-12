using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VldTenshi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Catalog : ContentPage
	{
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		public Catalog()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			UpdateFileList();
		}
		private void UpdateFileList()
		{
			//receiving all the files
			filesList.ItemsSource = Directory.GetFiles(folderPath).Select(f => Path.GetFileName(f));
			//Deselect
			filesList.SelectedItem = null;
		}
		private async void Button_Clicked(object sender, EventArgs e)
		{
			string filename = fileNameEntry.Text;
			if (String.IsNullOrEmpty(filename)) return;
			//if file exists
			if(File.Exists(Path.Combine(folderPath, filename)))
			{
				//request permission to rewrite
				bool isRewrited = await DisplayAlert("Kinnitus", "Fail on juba olemas, Kas salvestada ümber?", "Jah", "Ei");
				if(isRewrited == false) return;
			}
			File.WriteAllText(Path.Combine(folderPath, filename), textEditor.Text);
			//updating list of files
			UpdateFileList();
        }

		private void filesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null) return;
			//receiving selected element
			string filename = (string)e.SelectedItem;
			//uploading a text in a textfield
			textEditor.Text = File.ReadAllText(Path.Combine(folderPath, (string)e.SelectedItem));
			//set the name of file
			fileNameEntry.Text = filename;
			//deselect
			filesList.SelectedItem = null;
        }

		private void MenuItem_Clicked(object sender, EventArgs e)
		{
			//receiving file name
			string filename = (string)((MenuItem)sender).BindingContext;
			//Deleting file from the list
			File.Delete(Path.Combine(folderPath, filename));
			//Updating list of files
			UpdateFileList();
        }

		private void ToList_Clicked(object sender, EventArgs e)
		{
			// Получаем имя выбранного файла
			string selectedFilename = (string)((MenuItem)sender).BindingContext;

			// Формируем полный путь к выбранному файлу
			string selectedFilePath = Path.Combine(folderPath, selectedFilename);

			// Проверяем, существует ли файл
			if (File.Exists(selectedFilePath))
			{
				// Читаем содержимое файла и разбиваем его на строки (каждый элемент в своей строке)
				string[] elements = File.ReadAllLines(selectedFilePath);

				// Устанавливаем элементы в ListView
				list.ItemsSource = elements;
			}
		}
	}
}