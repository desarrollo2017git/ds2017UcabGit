using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface IDao
    {
        void Borrar<T>(DbSet<T> coleccion, T datos)
            where T : class; // <== add this constraint;

        void Crear<T>(DbSet<T> coleccion, T datos)
            where T : class;

        void Actualizar<T>(DbSet<T> coleccion, object datos, params object[] keys)
            where T : class;

    }
}
