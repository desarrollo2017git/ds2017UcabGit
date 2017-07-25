using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    // Interface de DAO para Clase ResultadoExamenMedico
    public interface IResultadoExamenMedicoDAO
	{
        // Metodo del DAO para guardar Resultados de Examenes Medicos en la Base de datos
        void GuardarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);

        // Metodo del DAO para obtener una lista de resultados de examenes Medicos
        List<ResultadoExamenMedico> ObtenerSelectListResultadoExamenMedico();

        // Metodo del Data Access Object utilizado para eliminar resultados de examenes Medicos
        void EliminarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);

    }
}