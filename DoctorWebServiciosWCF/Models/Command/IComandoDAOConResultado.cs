using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Interfaz que permite definir un comando con resultado.
    /// </summary>
    interface IComandoDAOConResultado
    {
        /// <summary>
        /// Metodo que permite implementar la accion de un comando y retornar un resultado.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        /// <returns>Resultado trans ejecutar el comando.</returns>
        T Ejecutar<T>(params object[] args)
            where T : class;
    }
}
