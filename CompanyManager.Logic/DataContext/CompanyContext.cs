﻿using CompanyManager.Logic.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Logic.DataContext
{
    internal class CompanyContext : DbContext, IContext
    {
        #region fields
        private static readonly string DatabaseType = "";
        private static readonly string ConnectionString = "";
        #endregion fields

        static CompanyContext()
        {
            var appSettings = Common.Modules.Configuration.AppSettings.Instance;

            DatabaseType = appSettings["Database:Type"] ?? DatabaseType;
            ConnectionString = appSettings[$"ConnectionStrings:{DatabaseType}ConnectionString"] ?? ConnectionString;
        }

        #region properties
        public DbSet<Entities.Company> CompanySet { get; set; }
        public DbSet<Entities.Customer> CustomerSet { get; set; }
        public DbSet<Entities.Employee> EmployeeSet { get; set; }
        #endregion properties

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (DatabaseType == "Sqlite")
            {
                optionsBuilder.UseSqlite(ConnectionString);
            }
            else if (DatabaseType == "SqlServer")
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
