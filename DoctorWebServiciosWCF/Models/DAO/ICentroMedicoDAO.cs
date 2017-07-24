using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = CentroMedico;

    public interface ICentroMedicoDAO
    {
        Modelo ObtenerCC(int centroMedicoId);

        List<Modelo> ObtenerTodosCC();

        void CrearCC(Modelo centroMedico);

        void ActualizarCC(Modelo centroMedico);

        void BorrarCC(Modelo centroMedico);
    }
}
