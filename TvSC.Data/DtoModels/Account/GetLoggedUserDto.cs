using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.Account
{
    public class GetLoggedUserDto : BaseModelDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }
}
