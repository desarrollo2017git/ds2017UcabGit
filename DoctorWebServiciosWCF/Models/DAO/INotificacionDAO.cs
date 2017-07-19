using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface INotificacionDAO
    {
        List<Notificacion> ObtenerTodos(out int cantidadPaginas, string nombre = null, int pagina = 0, int numeroFilas = 30);
        Notificacion Obtener(int codigo);
        Notificacion Obtener(string nombre);
        bool Guardar(Notificacion notificacion);
        bool Borrar(out string message, int codigo);
    }
}
