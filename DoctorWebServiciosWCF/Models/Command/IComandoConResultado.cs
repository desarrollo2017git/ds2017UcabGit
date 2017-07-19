using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.Command
{
    interface IComandoConResultado
    {
        T Ejecutar<T>(params object[] args);
    }
}
