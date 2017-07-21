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
            IComandoDAOConResultado comando = new ComandoDAOObtenerTodos();
            return comando.Ejecutar<IQueryable<T>>(coleccion);
        }

        public IQueryable<T> ObtenerTodosLosQue(Expression<Func<T, bool>> condicion)
        {
            IComandoDAOConResultado comando = new ComandoDAOObtenerTodosLosQue<T>();
            return comando.Ejecutar<IQueryable<T>>(coleccion, condicion);
        }

        public T ObtenerPrimero(params object[] keys)
        {
            IComandoDAOConResultado comando = new ComandoDAOObtenerPrimero();
            return comando.Ejecutar<T>(coleccion, keys);
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