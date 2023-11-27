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
    public partial class Table_Page : ContentPage
    {
        TableView tableView;
        SwitchCell switchCell;
        ImageCell imageCell;
        TableSection fotosection;
        Button callButton;
        Button smsButton;
        Button emailButton;
        //ViewCell viewCell;
        public Table_Page()
        {
            fotosection = new TableSection();
            switchCell = new SwitchCell { Text = "Näita veel..."};
            switchCell.OnChanged += SwitchCell_OnChanged;
            callButton = new Button
            {
                Text = "Call",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            callButton.Clicked += CallButton_Clicked;

            smsButton = new Button
            {
                Text = "Send SMS",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            smsButton.Clicked += SmsButton_Clicked;

            emailButton = new Button
            {
                Text = "Send Email",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            emailButton.Clicked += EmailButton_Clicked;
            imageCell = new ImageCell { Text = "Foto:", ImageSource="scr.jfif", Detail = "Kirjeldus"};
            tableView = new TableView
            {

                Intent = TableIntent.Form,
                Root = new TableRoot("Andmed:")
                {
                    new TableSection("Põhiandmed:")
                    {
                        new EntryCell
                        {
                            Label = "Nimi:",
                            Placeholder = "Siia tuleb nimi",
                            Keyboard = Keyboard.Text
                        }
                    },
                    new TableSection("Kontaktandmed:")
                    {
                        new EntryCell
                        {
                            Label = "Telefon:",
                            Placeholder = "Kirjuta telefoni nr.",
                            Keyboard = Keyboard.Telephone
                        }
                    },
                    new TableSection("Kontaktandmed:")
                    {
                        new EntryCell
                        {
                            Label = "Email:",
                            Placeholder = "Siia tuleb email",
                            Keyboard = Keyboard.Email
                        }
                    },
                    new TableSection("Lisavõimalused:") 
                    {
                        new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children = { callButton, smsButton, emailButton }
                            }
                        },
                        switchCell
                    },
                    fotosection
                }
            };
            Content = tableView;
        }

        private void SwitchCell_OnChanged(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                fotosection.Title = "Foto: ";
                fotosection.Add(imageCell);
                switchCell.Text = "Peida";
            }
            else
            {
                fotosection.Title = "";
                fotosection.Remove(imageCell);
                switchCell.Text = "Näita veel...";
            }
        }
        private void CallButton_Clicked(object sender, EventArgs e)
        {
            // Реализуйте логику вызова по номеру телефона
            string phoneNumber = GetPhoneNumber(); // Получите телефонный номер из соответствующего поля
            // Ваш код для вызова
        }

        private void SmsButton_Clicked(object sender, EventArgs e)
        {
            // Реализуйте логику отправки SMS
            string phoneNumber = GetPhoneNumber(); // Получите телефонный номер из соответствующего поля
            string message = GetMessage(); // Получите текст сообщения из соответствующего поля
            // Ваш код для отправки SMS
        }

        private void EmailButton_Clicked(object sender, EventArgs e)
        {
            // Реализуйте логику отправки Email
            string email = GetEmail(); // Получите email из соответствующего поля
            // Ваш код для отправки Email
        }

        private string GetPhoneNumber()
        {
            // Верните телефонный номер из соответствующего поля
            return "123456789";
        }

        private string GetMessage()
        {
            // Верните текст сообщения из соответствующего поля
            return "Hello, this is a test message.";
        }

        private string GetEmail()
        {
            // Верните email из соответствующего поля
            return "test@example.com";
        }
    }
}