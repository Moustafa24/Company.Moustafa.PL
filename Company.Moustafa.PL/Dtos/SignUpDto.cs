﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Company.Moustafa.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName is Required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName is Required !!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastNamme is Required !!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)] // ****  
        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [Compare(nameof(Password), ErrorMessage = "Confirm password does not match the password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
