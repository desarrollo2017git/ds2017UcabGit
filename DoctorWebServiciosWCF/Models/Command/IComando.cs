using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    public interface IComando
    {
        void Ejecutar(params object[] args);
    }
}