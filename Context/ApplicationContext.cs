using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReminderApplication.Entities;
using System;
using System.Collections.Generic;

namespace ReminderApplication.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
    }
}