using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
	public interface IResultadoExamenMedicoDAO
	{
        // Metodo del DAO para guardar Observaciones Medias en la Base de datos
        void GuardarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);

        // Metodo del DAO para obtener una lista de las observaciones medicas
        List<ResultadoExamenMedico> ObtenerSelectListResultadoExamenMedico();

        // Metodo del Data Access Object utilizado para eliminar observaciones medicas. 
        void EliminarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);

    }
}