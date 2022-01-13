using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ReservaSitio.Entities;



namespace ReservaSitio.DataAccess
{
    public class ApiDbContext : IdentityDbContext
    {

      
        //public DbSet<PlantillaCorreo.PCLT_PLANTILLA_CORREO> PCLT_PLANTILLA_CORREO { get; set; }
        //public DbSet<PCLT_LOG_TABLA> PCLT_LOG_TABLA { get; set; }
        //public DbSet<PCLT_LOG_ERROR> PCLT_LOG_ERROR { get; set; }
        //public DbSet<PCLT_OPCION> PCLT_OPCION { get; set; }
      

       // public DbSet<PCLT_COTIZACION> Cotizacion { get; set; }
        //public DbSet<PCLT_DET_COTIZACION> CotizacionDetalle { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();

            base.OnModelCreating(modelBuilder);
        }*/
    }
}
