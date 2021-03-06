﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    public class Calendario
    {
        [DataMember]
        public int CalendarioId { get; set; }
        [DataMember]
        [Required]
        public DateTime HoraInicio { get; set; }
        [DataMember]
        [Required]
        public DateTime HoraFin { get; set; }
        [DataMember]
        public bool Cancelada { get; set; }
        [DataMember]
        public virtual Medico Medico {get; set; }  
        
        public virtual Cita Cita { get; set; }
        // El atributo disponible indica con 1 si esta fecha esta libre para ser tomada por una cita
        // 0 indica que esta tomada

        // El atributo disponible indica con 1 si esta fecha esta libre para ser tomada por una cita
        // 0 indica que esta tomada

        [DataMember]
        public byte Disponible { get; set; }
    }
}