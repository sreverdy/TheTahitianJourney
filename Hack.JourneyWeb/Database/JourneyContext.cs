using Hack.JourneyWeb.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hack.JourneyWeb.Database
{
    public class JourneyContext : DbContext
    {
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<User> Users { get; set; }
    }
}