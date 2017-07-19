using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface IDAO<T>
    {
        IQueryable<T> ObtenerTodos();
        IQueryable<T> ObtenerTodosLosQue(Expression<Func<T, bool>> condicion);
        T ObtenerPrimero(params object[] keys);
        int Contar();
        void Borrar(T datos);
        void Crear(T datos);
        void Actualizar(object datos, params object[] keys);
    }
}
