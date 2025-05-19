﻿using System.ComponentModel.DataAnnotations;

namespace TT1.Handle.Email
{
    public class Validate
    {
        public static bool IsValidEmail(string email) 
        {
            var checkEmail = new EmailAddressAttribute();
            return checkEmail.IsValid(email);
        }
    }
}
