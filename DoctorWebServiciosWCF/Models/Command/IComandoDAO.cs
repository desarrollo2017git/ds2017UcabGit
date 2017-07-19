using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.Command
{
    interface IComandoDAO
    {
        void Ejecutar<T>(params object[] args)
            where T : class;
    }
}
