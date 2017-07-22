using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Interfaz que permite definir un comando.
    /// </summary>
    interface IComandoDAO
    {
        /// <summary>
        /// Metodo que permite implementar la accion de un comando.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        void Ejecutar<T>(params object[] args)
            where T : class;
    }
}
