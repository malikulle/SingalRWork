using Microsoft.EntityFrameworkCore;
using SignalRWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRWork.Domain.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {                
        }


        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Covid> Covids { get; set; }
    }
}
