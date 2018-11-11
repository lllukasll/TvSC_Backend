using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TvSC.Data.BindingModels.Actor
{
    public class AddActorBindingModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile Photo { get; set; }
    }
}
