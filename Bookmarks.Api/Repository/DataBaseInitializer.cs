using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Repository
{
    public class DataBaseInitializer
    {

        public static void Initialize(DataBase context)
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
