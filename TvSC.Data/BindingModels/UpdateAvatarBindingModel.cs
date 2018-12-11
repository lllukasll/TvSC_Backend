using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TvSC.Data.BindingModels
{
    public class UpdateAvatarBindingModel
    {
        public IFormFile Avatar { get; set; }
    }
}
