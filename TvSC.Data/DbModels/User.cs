using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TvSC.Data.DbModels
{
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<TvSeriesUserRating> TvSeriesRatings { get; set; }
    }
}
