﻿using API.Constants;
using API.Utils;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Users
{
    public class CreateUserDto
    {
        private string _username;

        [Required]
        [StringLength(30, ErrorMessage = "Tên tài khoản dài từ 3 đến 30 ký tự", MinimumLength = 3)]
        public string Username
        {
            get => _username;
            set => _username = value?.Trim();
        }

        private string _password;

        [Required]
        [StringLength(30, ErrorMessage = "Mật khẩu dài từ 3 đến 30 ký tự", MinimumLength = 3)]
        public string Password
        {
            get => _password;
            set => _password = value?.Trim();
        }

        [IntegerRange(AllowableValues = new int[] { UserTypes.Admin, UserTypes.Customer })]
        public int UserType { get; set; }
    }
}
