using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Input;
using AutoMapper;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Models.Registration;
using RVTMobileAPP.Services;
using RVTMobileAPP.Validators;
using RVTMobileAPP.Validators.Rules;
using RVTMobileAPP.Views;
using Xamarin.Forms;

namespace RVTMobileAPP.ViewModels
{
    public class RegisterViewModel: INotifyPropertyChanged
    {
        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Surname { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Gender { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<DateTime> Birth_date { get; set; } = new ValidatableObject<DateTime>() { Value = DateTime.Now };
        public ValidatableObject<string> IDNP { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Number { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Region { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<bool> TermsAndCondition { get; set; } = new ValidatableObject<bool>();

        public RegisterViewModel()
        {
            AddValidationRules();
        }

        private void AddValidationRules()
        {
            Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Introduceți numele" });
            Surname.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Întroduceți prenumele" });
            Gender.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Selectați sexul" });

            IDNP.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Introduceți IDNP-ul" });
            IDNP.Validations.Add(new IsLenghtValidRule<string> { ValidationMessage = "IDNP-ul trebuie să consiste din 13 caractere", MaximunLenght = 13, MinimunLenght = 12 });

            Number.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Introduceți numărul de telefon" });
            Number.Validations.Add(new IsLenghtValidRule<string> { ValidationMessage = "Numărul de telefon trebuie să conține minimum 8 cifre și maximum 10 cifre", MaximunLenght = 10, MinimunLenght = 8 });

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Introduceți adresa poștală" });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "Adresa poștală nu există. Introduceți o altă adresă poștală validă." });

            Birth_date.Validations.Add(new HasValidAgeRule<DateTime> { ValidationMessage = "Trebuie să aveți 18 ani sau mai mult" });

            Region.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Selectați domiciliul" });


            TermsAndCondition.Validations.Add(new IsValueTrueRule<bool> { ValidationMessage = "Vă rugăm să acceptați termenii și condițiile" });

        }
        
        public ICommand SignUpCommand => new Command(async () =>
        {
            if (AreFieldsValid())
            {
                var model = new RegistrationMessage
                {
                    IDNP = IDNP.Value,
                    Ip_address= Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault().ToString(),
                    Birth_date = Birth_date.Value,
                    Name = Name.Value,
                    Surname = Surname.Value,
                    Gender = Gender.Value,
                    Phone_Number =Number.Value,
                    Email = Email.Value,
                    RegisterDate = DateTime.Now
                }; 

                var service = DependencyService.Get<IUser>().Registration(model);
                await App.Current.MainPage.DisplayAlert("Înregistrare", service.Result.Message, "Ok");
                if(service.Result.Status)
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
        });

        bool AreFieldsValid()
        {
            bool isNameValid = Name.Validate();
            bool isSurnameValid = Surname.Validate();
            bool isGenderValid = Gender.Validate();
            bool isIdnpValid = IDNP.Validate();
            bool isBirthDayValid = Birth_date.Validate();
            bool isPhoneNumberValid = Number.Validate();
            bool isEmailValid = Email.Validate();
            bool isTermsAndConditionValid = TermsAndCondition.Validate();
            bool isRegionValid = Region.Validate();

            return isNameValid && isSurnameValid && isGenderValid && isBirthDayValid && isIdnpValid
                   && isPhoneNumberValid && isEmailValid && isTermsAndConditionValid && isRegionValid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
