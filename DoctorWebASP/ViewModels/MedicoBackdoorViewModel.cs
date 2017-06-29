using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.ViewModels
{
    public class MedicoBackdoorViewModel
    {
        public SelectList EspecialidadesMedicas { get; set; }
        public SelectList CentrosMedicos { get; set; }

        public Medico Medico { get; set; }
        public int CentroMedicoId { get; set; }
        public int EspecialidadMedicaId { get; set; }
    }
}