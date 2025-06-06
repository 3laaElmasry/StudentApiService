﻿
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "The Name cann't be blank")]
        public string PersonName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The User Name cann't be blank")]
        public string UserName { get; set; } = string.Empty ;

        [Required(ErrorMessage = "The User Name cann't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number Should Contain Only Numbers")]
        public string PhoneNumber { get; set; } = string.Empty ;

        [Required(ErrorMessage = "The Email cann't be blank")]
        [EmailAddress(ErrorMessage = "The Email must be valid with email constraints")]
        [Remote(action: "IsEmailAlreadyExist",
            controller: "Account", ErrorMessage = "Email is Already Use")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Password cann't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Password cann't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
