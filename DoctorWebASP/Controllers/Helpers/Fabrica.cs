using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Controllers.Helpers
{
    public class Fabrica
    {
        internal static Exception CrearExcepcion(string mensaje = "Excepcion no controlada.", Exception interna = null)
        {
            return new DoctorWebException(mensaje, interna);
        }
    }
}