using Microsoft.EntityFrameworkCore;

namespace PrioniaApp.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
        :base(options)
        {

        }


    }
}
