﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    interface ICalendariosDAO
    {
        void Actualizar(Calendario calendario, int calendarioId);
    }
}
