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
	public partial class FrameGridPage : ContentPage
	{
		Frame frame;
		Grid grid;
		Random rnd;
		Button button;
		Label label;
		public FrameGridPage()
		{
			//frame = new Frame
			//{
			//    BorderColor = Color.White,
			//    CornerRadius = 2,
			//    BackgroundColor = Color.FromRgb(rnd.Next(0,255), 155, 55),
			//};
			rnd = new Random();
			grid = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				//RowDefinitions =
				//{
				//    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
				//    new RowDefinition {Height = new GridLength(2, GridUnitType.Star)},
				//    new RowDefinition {Height = new GridLength(3, GridUnitType.Star)}
				//},
				//ColumnDefinitions =
				//{
				//    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
				//    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)}
				//}
			};
			//grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			//grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					//grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
					grid.Children.Add(
					button = new Button
					{
						Text = i.ToString() + j.ToString(),
						BorderColor = Color.Green,
						CornerRadius = 2,
						BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
						VerticalOptions = LayoutOptions.FillAndExpand,
						HorizontalOptions = LayoutOptions.FillAndExpand,
					}, j, i);
					button.Clicked += Button_Clicked;
				}
			}
			//grid.Children.Add(frame, 0, 0);
			//grid.Children.Add(frame, 1, 0);
			//grid.Children.Add(frame, 0, 1);
			//grid.Children.Add(frame, 1, 1);
			//grid.Children.Add(frame, 0, 2);
			//grid.Children.Add(frame, 1, 2);
			label = new Label { Text = "Tekst", BackgroundColor = Color.AliceBlue };
			grid.Children.Add( label, 0, 3 );
			Grid.SetColumnSpan(label, 2);
			//StackLayout st = new StackLayout { VerticalOptions=LayoutOptions.FillAndExpand };
			//st.Children.Add( label );
			//st.Children.Add( grid );
			Content = grid;
			// Добавляем кнопку возврата в верхний тулбар
			ToolbarItems.Add(new ToolbarItem("Back", null, async () => await Navigation.PopAsync()));
		}
		private void Button_Clicked(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if(button.BindingContext == null || (Color)button.BindingContext == Color.Red)
			{
				button.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
				button.BindingContext = button.BackgroundColor;
			}
			else
			{ 
				button.BackgroundColor = Color.Red;
				button.BindingContext = Color.Red;
		    }
			label.Text = button.Text;
		}
	}
}