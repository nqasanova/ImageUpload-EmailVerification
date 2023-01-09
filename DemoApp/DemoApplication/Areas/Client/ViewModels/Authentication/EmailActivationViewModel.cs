using System;
namespace DemoApplication.Areas.Client.ViewModels.Authentication
{
    public class EmailActivationViewModel
    {
        public string Email { get; set; }

        public EmailActivationViewModel(string email)
        {
            Email = email;
        }
    }
}