
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using register.Entities;

namespace register.Models
{
   public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
       
        public AppDBContext(DbContextOptions options): base(options)
        { 
        }
        //  public DbSet<RegistrationModel> Register { get; set; }
          
          
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
 }   }