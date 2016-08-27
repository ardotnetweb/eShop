
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public class FavoriteUser
    {
        public int Id { get; set; }

        public int Id_User { get; set; }
        public int Id_Technology { get; set; }
        public Favorite CategoryFavorite { get; set; }
    }  
}
