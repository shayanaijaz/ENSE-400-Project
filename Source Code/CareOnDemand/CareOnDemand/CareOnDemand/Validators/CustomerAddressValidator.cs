using System;
using System.Collections.Generic;
using System.Text;
using CareOnDemand.Models;
using FluentValidation;

namespace CareOnDemand.Validators
{
    public class CustomerAddressValidator   : AbstractValidator<Address>
    {
        public CustomerAddressValidator()
        {
            RuleFor(address => address.AddrLine1).NotNull().WithMessage("Address Line 1 is required");
            //RuleFor(address => address.City).NotNull().WithMessage("City is required");
            //RuleFor(address => address.Province).NotNull().WithMessage("Province is required");
            RuleFor(address => address.PostalCode).NotNull().WithMessage("Postal Code is required");
        }
    }
}
