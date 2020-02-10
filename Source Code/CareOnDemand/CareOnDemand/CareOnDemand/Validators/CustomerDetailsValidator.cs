using CareOnDemandRest.Models;
using FluentValidation;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareOnDemand.Validators
{
    public class CustomerDetailsValidator   : AbstractValidator<Account>
    {
        public CustomerDetailsValidator()
        {
            RuleFor(account => account.Email).EmailAddress().WithMessage("Not a valid email address").NotNull().WithMessage("Email is required");
            RuleFor(account => account.Password).NotNull().WithMessage("Password is required").Equal(account => account.PasswordConfirmation).WithMessage("Passwords must match");
            RuleFor(account => account.Password).Matches("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})$").WithMessage("Password must be atleast 8 characters and must contain uppercase letter, lowercase letter, and number");
            RuleFor(account => account.PasswordConfirmation).NotNull().WithMessage("Password confirmation is required");
            RuleFor(account => account.FirstName).NotNull().WithMessage("First Name is required");
            RuleFor(account => account.LastName).NotNull().WithMessage("Last Name is required");
            RuleFor(account => account.PhoneNumber).NotNull().WithMessage("Phone Number is required");
        }
    }
}
