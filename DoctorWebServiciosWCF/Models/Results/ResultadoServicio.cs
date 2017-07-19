﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Results
{
    public class ResultadoServicio<T> where T : class
    {
        public bool SinProblemas { get; set; }
        public string Mensaje { get; set; }

        public T Contenido { get; set; }

        public ResultadoServicio()
        {
            SinProblemas = false;
            Contenido = null;
        }

        internal void Inicializar(T Contenido)
        {
            this.SinProblemas = true;
            this.Contenido = Contenido;
        }
    }
}