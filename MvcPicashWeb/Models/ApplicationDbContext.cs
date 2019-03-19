using Microsoft.EntityFrameworkCore;
using mvcPicash.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MvcPicashWeb.Models
{
    public class ApplicationDbContext : DbContext
    {        
        //picash
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DebtCollector> DebtCollectors { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<PaymentCommitment> PaymentCommitments { get; set; }
        public DbSet<Route> Routes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /******************************************************************/
            /************* Carga del sistema Picash ***************************/

            var debtCollector = new DebtCollector
            {
                Birthdate = Convert.ToDateTime("12/4/1980 12:10:15 PM", new CultureInfo("en-US")),
                DebtCollectorId = Guid.NewGuid().ToString(),
                CellPhone = "+54 9 11 5521 3345",
                Name = "Juan",
                SurName = "Perez",
                OptionalContact = "juanperez@perezcompany.com"
            };

            List<Route> routes = LoadRoutes(debtCollector);
            List<Customer> customers = LoadCustomers(routes);
            List<Address> addresses = LoadAddresses(customers);

            modelBuilder.Entity<DebtCollector>().HasData(debtCollector);
            modelBuilder.Entity<Route>().HasData(routes.ToArray());
            modelBuilder.Entity<Customer>().HasData(customers.ToArray());
            modelBuilder.Entity<Address>().HasData(addresses.ToArray());
        }

        private List<Address> LoadAddresses(List<Customer> customers)
        {
            string[] street = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] number = { "1243", "666", "895", "397", "1236", "1789", "2765" };

            Random rnd = new Random();

            var completeList = new List<Address>();
            foreach (Customer cus in customers)
            {
                int rndIndex1 = rnd.Next(0, 6);
                int rndIndex2 = rnd.Next(0, 6);
                completeList.Add(
                    new Address()
                    {
                        AddressId = Guid.NewGuid().ToString(),
                        IsMain = true,
                        Description = $"{street[rndIndex1]} {number[rndIndex2]}"
                    }
                );
            }
            return completeList;
        }

        private List<Customer> LoadCustomers(List<Route> routes)
        {
            var customerList = new List<Customer>();

            Random rnd = new Random();
            foreach (var route in routes)
            {
                int cantRandom = rnd.Next(5, 20);
                var tmplist = RandomCustomerGenerator(route, cantRandom);
                customerList.AddRange(tmplist);
            }
            return customerList;
        }
        private List<Customer> RandomCustomerGenerator(Route route, int cant)
        {
            string[] name1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] surname = { "Ruiz", "Sarmiento", "Uribe", "Sosa", "Pérez", "Toledo", "Herrera" };
            string[] name2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var customerList = from n1 in name1
                               from n2 in name2
                               from sn in surname
                               select new Customer
                               {
                                   RouteId = route.RouteId,
                                   Name = $"{n1} {n2}",
                                   SurName = $"{sn}",
                                   CustomerId = Guid.NewGuid().ToString()
                               };

            return customerList.OrderBy((cus) => cus.CustomerId).Take(cant).ToList();
        }
        /*
        private List<Customer> LoadCustomers(List<Route> routes)
        {
            var completeList = new List<Customer>();
            foreach (var route in routes)
            {
                var tmpList = new List<Customer> {
                            new Customer{
                                Id = Guid.NewGuid().ToString(),
                                RouteId = route.Id,
                                Name ="Federico",
                                SurName = "Lopez",
                                Birthdate = Convert.ToDateTime("12/5/1985 12:10:15 PM", new CultureInfo("en-US")),
                                CellPhone = "+54 9 11 2626 5344",
                                OptionalContact = "fedeLopez@outlook.com.ar"
                            } ,
                            new Customer{
                                Id = Guid.NewGuid().ToString(),
                                RouteId = route.Id,
                                Name ="Roberto",
                                SurName = "Salomone",
                                Birthdate = Convert.ToDateTime("12/6/1983 12:10:15 PM", new CultureInfo("en-US")),
                                CellPhone = "+54 9 11 2626 6666",
                                OptionalContact = "robert@gmail.com.ar"
                            } ,
                            new Customer{
                                Id = Guid.NewGuid().ToString(),
                                RouteId = route.Id,
                                Name ="Roberto",
                                SurName = "Salomone",
                                Birthdate = Convert.ToDateTime("12/6/1983 12:10:15 PM", new CultureInfo("en-US")),
                                CellPhone = "+54 9 11 2626 6666",
                                OptionalContact = "robert@gmail.com.ar"
                            }

                };
                completeList.AddRange(tmpList);
            }
            return completeList;
        }
        */
        private List<Route> LoadRoutes(DebtCollector debtCollector)
        {
            return new List<Route>(){
                        new Route() {
                            RouteId = Guid.NewGuid().ToString(),
                            DebtCollectorId = debtCollector.DebtCollectorId,
                            Code = "101"
                        },
                        new Route() {RouteId = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.DebtCollectorId, Code = "201"},
                        new Route() {RouteId = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.DebtCollectorId, Code = "301"},
                        new Route() {RouteId = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.DebtCollectorId, Code = "401"},
                        new Route() {RouteId = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.DebtCollectorId, Code = "501"},
            };
        }

       

       


        
    }
}
