using DoctorWebServiciosWCF.Models.Command;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class DAO<T> : IDAO<T>
        where T : class
    {
        public ContextoBD db { get; set; }
        protected DbSet<T> coleccion { get; set; }

        public DAO(): this(coleccion: null)
        {
        }

        public DAO(DbSet<T> coleccion)
        {
            db = new ContextoBD();
            this.coleccion = coleccion;
        }

        public IQueryable<T> ObtenerTodos()
        {
            return coleccion;
        }

        public IQueryable<T> ObtenerTodosLosQue(Expression<Func<T, bool>> condicion)
        {
            return coleccion.Where(condicion);
        }

        public T ObtenerPrimero(params object[] keys)
        {
            return coleccion.Find(keys);
        }

        public int Contar()
        {
            return coleccion.Count();
        }

        public void Borrar(T datos)
        {
            IComandoDAO crear = new ComandoDAOBorrar();
            crear.Ejecutar<T>(db, coleccion, datos);
        }

        public void Crear(T datos)
        {
            IComandoDAO crear = new ComandoDAOCrear();
            crear.Ejecutar<T>(db, coleccion, datos);
        }

        public void Actualizar(object datos, params object[] keys)
        {
            IComandoDAO actualizar = new ComandoDAOActualizar();
            actualizar.Ejecutar<T>(db, coleccion, datos, keys);
        }

        public List<T> Listar<T>(DbSet<T> coleccion)
            where T : class // <== add this constraint
        {
            return coleccion.ToList();
        }

    }
}