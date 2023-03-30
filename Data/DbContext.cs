using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollegehelperAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace CollegehelperAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
    }
}