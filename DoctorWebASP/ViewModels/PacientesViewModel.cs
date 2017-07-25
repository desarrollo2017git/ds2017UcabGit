using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorWebASP.Models;
using System.Web.Mvc;

namespace DoctorWebASP.ViewModels
{
    public class PacientesViewModel
    {
        public SelectList Paciente { get; set; }
        public SelectList Seguro { get; set; }
    }
}