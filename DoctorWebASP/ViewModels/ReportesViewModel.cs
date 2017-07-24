using DoctorWebASP.Models;
using DoctorWebASP.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.ViewModels
{
    public class ReportesViewModel
    {
    }

    public class ReportesIndexViewModel
    {
        public ResultadoProceso resultadoProcesoR2 { get; set; }
        public ResultadoProceso resultadoProcesoR3 { get; set; }
        public ResultadoProceso resultadoProcesoR5 { get; set; }
    }
}