﻿using Quiz.Dependencies.Enums;
using Speckoz.MobileQuiz.Dependencies.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace Quiz.API.Models
{
    public class UserModel : IUser
    {
        public int UserID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserTypeEnum UserType { get; set; }
    }
}