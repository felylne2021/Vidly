using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models {
    public class CheckMembershipAge : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            else {
                if (customer.Birthdate == null)
                    return new ValidationResult("Birthdate is required");
                var age = DateTime.Today.Year - customer.Birthdate.Value.Year;
                return (age >= 17) ?
                    ValidationResult.Success
                    : new ValidationResult("Customer must be at least 17 years old for paid membership.");
            }

        }
    }
}