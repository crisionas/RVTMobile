using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RVTMobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void Send_clicked(object sender, System.EventArgs e)
        {
            var email = Email.Text;
            var emailPattern =
                "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
            if (Regex.IsMatch(email,emailPattern))
            {
                try
                {
                    // Who send?
                    MailAddress From = new MailAddress("rvtvote@gmail.com", "RVT Vote");
                    string utilizator = "rvtvote@gmail.com";
                    //Change your password
                    string password = "**********";
                    // where to send?
                    MailAddress To = new MailAddress("rvtvote@gmail.com");
                    MailMessage msg = new MailMessage(From, To);
                    msg.Subject = $"RVT|ContactForm - {Email.Text} ";
                    msg.Body = "Nume: "+Name.Text+"Email: "+Email.Text+"Mesaj: "+ Message.Text;
                    msg.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential(utilizator, password);
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                    DisplayAlert("Info", "Mesajul dumneavostră a fost transmis cu succes.", "Ok");
                }
                catch
                {

                }
            }
            else
            {
                DisplayAlert("Alert", "Email-ul nu este valid. Introduceți altă adresă.", "Ok");
            }

        }
    }
}