using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class CalendariosDAO : DAO<Calendario>, ICalendariosDAO
    {
        public CalendariosDAO() : base()
        {
            coleccion = db.Calendarios;
        }


    }
}