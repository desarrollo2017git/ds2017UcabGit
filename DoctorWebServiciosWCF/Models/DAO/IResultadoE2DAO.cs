using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface IResultadoE2DAO
    {
        // Metodo del DAO para guardar resultados de examenes en la Base de datos
        void GuardarResultadoE2(ResultadoE2 resultadoE2);

        // Metodo del DAO para obtener una lista de los resultados de examenes
        List<ResultadoE2> ObtenerSelectListResultadoE2();

        // Metodo del DAO utilizado para eliminar resultados de examenes medicos
        void EliminarResultadoE2(ResultadoE2 resultadoE2);
    }
}