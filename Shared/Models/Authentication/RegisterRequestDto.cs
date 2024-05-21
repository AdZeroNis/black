using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Authentication
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "لطفا شماره همراه را وارد نمایید")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را وارد نمایید")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا نام و نام خانوادگی را وارد نمایید")]
        public string FullName { get; set; }

    }
}