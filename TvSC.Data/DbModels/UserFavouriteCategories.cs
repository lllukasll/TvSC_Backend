using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class UserFavouriteCategories : BaseModel
    {
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
