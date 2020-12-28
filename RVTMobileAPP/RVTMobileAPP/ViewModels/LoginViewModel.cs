using RVTMobileAPP.Validators;
using RVTMobileAPP.Validators.Rules;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RVTMobileAPP.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        public ValidatableObject<string> IDNP { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();


        public LoginViewModel()
        {
            AddValidationRules();

        }

        public void AddValidationRules()
        {
            IDNP.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Introduceți IDNP-ul." });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Introduceți parola." });
           
        }

        public ICommand LogInCommand => new Command(async () =>
        {
            if (AreFieldsValid())
            {
                var test = IDNP.Value;
                await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
            }
        });


        bool AreFieldsValid()
        {
            bool isIDNPValid = IDNP.Validate();
            bool isPasswordValid = Password.Validate();
           

            return isIDNPValid && isPasswordValid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
