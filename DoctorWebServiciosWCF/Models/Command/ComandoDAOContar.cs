using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    public class ComandoDAOContar : IComandoDAO
    {
        public void Ejecutar<T>(params object[] args) where T : class
        {
            throw new NotImplementedException();
        }
    }
}