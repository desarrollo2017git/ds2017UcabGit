using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    public class Persona
    {
        [DataMember]
        [Key]
        public int PersonaId { get; set; }
        [DataMember]
        [Required]
        public string Nombre { get; set; }
        [DataMember]
        [Required]
        public string Apellido { get; set; }
        [DataMember]
        [Required]
        public string Cedula { get; set; }
        [DataMember]
        [Required]
        public string Genero { get; set; }
        [DataMember]
        [Required]
        public string Telefono { get; set; }
        [DataMember]
        public DateTime FechaNacimiento { get; set; }
        [DataMember]
        public DateTime FechaCreacion { get; set; }
        [DataMember]
        [EmailAddress]
        public string Email { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string ApplicationUserId { get; set; }


        public string ConcatUserName
        {
            get
            {
                return Nombre +" "+ Apellido;
            }
        }


        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + Apellido;
            }
        }

    }
}