using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TvSC.Data.BindingModels
{
    public class LoginBindingModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
