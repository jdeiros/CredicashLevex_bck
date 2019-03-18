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
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluación> Evaluaciones { get; set; }
        
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

            var escuela = new Escuela();
            escuela.AñoDeCreación = 2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "Platzi School";
            escuela.Ciudad = "Bogota";
            escuela.Pais = "Colombia";
            escuela.Direccion = "Avd Siempre viva";
            escuela.TipoEscuela = TiposEscuela.Secundaria;

            //Cargar Cursos de la escuela
            var cursos = CargarCursos(escuela);

            //x cada curso cargar asignaturas
            var asignaturas = CargarAsignaturas(cursos);

            //x cada curso cargar alumnos
            var alumnos = CargarAlumnos(cursos);

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());

            /******************************************************************/
            /************* Carga del sistema Picash ***************************/

            var debtCollector = new DebtCollector();
            debtCollector.Birthdate = Convert.ToDateTime("12/4/1980 12:10:15 PM", new CultureInfo("en-US"));
            debtCollector.Id = Guid.NewGuid().ToString();
            debtCollector.CellPhone = "+54 9 11 5521 3345";
            debtCollector.Name = "Juan";
            debtCollector.SurName = "Perez";
            debtCollector.OptionalContact = "juanperez@perezcompany.com";

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
                        Id = Guid.NewGuid().ToString(),
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
                                   RouteId = route.Id,
                                   Name = $"{n1} {n2}",
                                   SurName = $"{sn}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return customerList.OrderBy((cus) => cus.Id).Take(cant).ToList();
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
                            Id = Guid.NewGuid().ToString(),
                            DebtCollectorId = debtCollector.Id,
                            Code = "101"
                        },
                        new Route() {Id = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.Id, Code = "201"},
                        new Route() {Id = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.Id, Code = "301"},
                        new Route() {Id = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.Id, Code = "401"},
                        new Route() {Id = Guid.NewGuid().ToString(), DebtCollectorId = debtCollector.Id, Code = "501"},
            };
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();

            Random rnd = new Random();
            foreach (var curso in cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                var tmplist = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tmplist);
            }
            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var curso in cursos)
            {
                var tmpList = new List<Asignatura> {
                            new Asignatura{
                                Id = Guid.NewGuid().ToString(),
                                CursoId = curso.Id,
                                Nombre="Matemáticas"} ,
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Educación Física"},
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Castellano"},
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Ciencias Naturales"},
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Programación"}

                };
                listaCompleta.AddRange(tmpList);
                //curso.Asignaturas = tmpList;
            }

            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                        new Curso() {
                            Id = Guid.NewGuid().ToString(),
                            EscuelaId = escuela.Id,
                            Nombre = "101",
                            Dirección = "Avd Siempre viva",
                            Jornada = TiposJornada.Mañana
                        },
                        new Curso() {Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "201", Dirección = "Pasco 637", Jornada = TiposJornada.Mañana},
                        new Curso   {Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "301", Dirección = "sourignes 2020", Jornada = TiposJornada.Mañana},
                        new Curso() {Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "401", Dirección = "rosales 156", Jornada = TiposJornada.Tarde },
                        new Curso() {Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "501", Dirección = "marconi 1245", Jornada = TiposJornada.Tarde},
            };
        }

        private List<Alumno> GenerarAlumnosAlAzar(
            Curso curso,
            int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   CursoId = curso.Id,
                                   Nombre = $"{n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }
    }
}
