using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class Dao : IDao
    {
        public ContextoBD db { get; set; }

        public Dao()
        {
            db = new ContextoBD();
        }

        public void Borrar<T>(DbSet<T> coleccion, T datos)
            where T : class // <== add this constraint
        {
            coleccion.Remove(datos);
            db.SaveChanges();

        }

        public void Crear<T>(DbSet<T> coleccion, T datos)
            where T : class // <== add this constraint
        {
            coleccion.Add(datos);
            db.SaveChanges();

        }

        public void Actualizar<T>(DbSet<T> coleccion, object datos, params object[] keys)
            where T : class // <== add this constraint
        {
            var registrada = db.Notificaciones.Find(keys);
            db.Entry(registrada).CurrentValues.SetValues(datos);
            db.SaveChanges();

        }

        public List<T> Listar<T>(DbSet<T> coleccion)
            where T : class // <== add this constraint
        {
            return coleccion.ToList();
        }

    }
}