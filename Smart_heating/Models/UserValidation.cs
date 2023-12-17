using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_heating.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User 
    {
        [Bind(Exclude = "UserID")]
        public class UserMetaData
        {
            [ScaffoldColumn(false)]
            public int UserID { get; set; }

            // checking the accuracy of entered login
            [DisplayName("Логін")]
            [Required(ErrorMessage = "Потрібно ввести свій логін")]
            [MinLength(10, ErrorMessage = "Логін замалий. Мінімум 10 символів")]
            [MaxLength(20, ErrorMessage = "Логін завеликий. Максимум 20 символів")]

            public string UserLogin { get; set; }

            // checking the accuracy of entered password
            [DisplayName("Пароль")]
            [Required(ErrorMessage = "Потрібно ввести свій пароль")]
            [MinLength(10, ErrorMessage = "Пароль замалий. Мінімум 10 символів")]
            [MaxLength(20, ErrorMessage = "Пароль завеликий. Максимум 20 символів")]

            public string UserPassword { get; set; }
        }
    }
}