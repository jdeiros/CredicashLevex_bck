using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MvcPicashWeb.Models;

namespace MvcPicashWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();

            /*el scope creado con CreateScope() se puede quedar vivo en memoria
             * por eso lo metemos dentro de un using para definirle una zona donde el 
             * objeto pueda "vivir"
             */
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    /*Esto de recuperar el contexto de la base de datos puede fallar
                     * porque pueda que no tenga conexion a la db
                    */
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    //traigo el servicio del mecanismo de log que vamos a usar en toda 
                    //la app y lo uso para loguear el error
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error ocurred creatin the DB.");
                    throw;
                }

            }
            //no inicia hasta que esten todos los datos ejecutandoce, ahora si inicio (run)
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
