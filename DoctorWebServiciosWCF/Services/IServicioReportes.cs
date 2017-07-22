using DoctorWebServiciosWCF.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReporteService" in both code and config file together.
    [ServiceContract]
    public interface IServicioReportes
    {
        [OperationContract]
        [WebGet(UriTemplate = "/obtener/{codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DoWork(string codigo);

        [OperationContract]
        [WebGet(UriTemplate = "/reportes/{tipo}/{codigo}?fechaInicio={fechaInicio}&fechaFin={fechaFin}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso Reportes(string tipo, string codigo, string fechaInicio, string fechaFin);
    }
}
