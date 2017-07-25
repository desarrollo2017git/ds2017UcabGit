using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models.Services
{
    public interface IServicioResultadoE2
    {
        // Metodo del cliente que realiza el llamado para Guardar un resultado de examen
        void GuardarResultadoE2(ResultadoE2 resultadoE2);


        // Metodo en el cliente utilizado para obtener una lista de todos
        // los resultados
        List<ResultadoE2> ObtenerSelectListResultadoE2();


        // Metodo del cliente que realiza el llamado para eliminar un resultado
        void EliminarResultadoE2(ResultadoE2 resultadoE2);
    }
}