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

        public void Actualizar(Calendario calendario, int calendarioId)
        {
            throw new NotImplementedException();
        }
    }
}