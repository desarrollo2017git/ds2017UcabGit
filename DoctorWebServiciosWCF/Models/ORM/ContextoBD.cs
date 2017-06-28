using DoctorWebServiciosWCF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.ORM
{
    public class ContextoBD : DbContext
    {
        public ContextoBD() :
            base("DoctorWebBD")
        {

        }

        public DbSet<Notificacion> Notificaciones { get; set; }
    }
}