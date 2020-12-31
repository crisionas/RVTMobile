using System;
using RVTMobileAPP.Validators;
using RVTMobileAPP.Validators.Rules;
using System.ComponentModel;
using System.Windows.Input;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Models.AuthUser;
using RVTMobileAPP.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
            IDNP.Validations.Add(new IsLenghtValidRule<string> { ValidationMessage = "IDNP-ul trebuie să consiste din 13 caractere", MaximunLenght = 13, MinimunLenght = 12 });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Introduceți parola." });
           
        }

        public ICommand LogInCommand => new Command(async () =>
        {
            if (AreFieldsValid())
            {
                var model = new AuthMessage
                {
                    IDNP = IDNP.Value,
                    VnPassword = Password.Value
                };
                var response = DependencyService.Get<IUser>().Login(model).Result;
                await App.Current.MainPage.DisplayAlert("Logare", response.Message, "Ok");
                if (response.Status)
                    await Application.Current.MainPage.Navigation.PushAsync(new VotePage(response.IDVN));
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
