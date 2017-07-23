using DoctorWebServiciosWCF.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DoctorWebServiciosWCF.Models.DAO
{

    //Ejemplo de Implementacion de dato...
    // Al cambiar Modelo se adapta todo el DAO a la clase que se indique.
    using Modelo = CentroMedico;

    public class CentroMedicoDAO : DAO<Modelo>, ICentroMedicoDAO
    {
        public Modelo ObtenerCC(int centroMedicoId)
        {

            // Ambas opciones sirven para seleccionar 1 registro (Usando una condicion)...            
            // var resultado = base.ObtenerPrimeroQue(registro => registro.CentroMedicoId == centroMedicoId );

            var resultado = base.ObtenerPrimeroQue(registro => registro.CentroMedicoId == centroMedicoId);
            if (resultado == null)
                throw Utilidades.Instancia.Fabrica.CrearExcepcion("No se encontro registro de Centro Medico");
            return resultado;
        }

        public List<Modelo> ObtenerTodosCC()
        {
            // Ambas opciones sirven para seleccionar varios registros (Todos o Filtrado...)...
            // var resultado = this.ObtenerTodos(centroMedicoId);
            // var resultado = this.ObtenerTodosLosQue(registro => registro.Nombre.Contains("a") );

            var consulta = base.ObtenerTodos();
            if (consulta == null)
                throw Utilidades.Instancia.Fabrica.CrearExcepcion("No se encontro registro de Centro Medico");

            var resultado = consulta.ToList();
            if (resultado.Count == 0)
                throw Utilidades.Instancia.Fabrica.CrearExcepcion("No se encontro registro de Centro Medico");

            return resultado;
        }

        public void CrearCC(Modelo centroMedico)
        {
            base.Crear(centroMedico);
        }

        public void ActualizarCC(Modelo centroMedico)
        {
            base.Actualizar(centroMedico, registro => registro.CentroMedicoId == centroMedico.CentroMedicoId);
        }

        public void BorrarCC(Modelo centroMedico)
        {
            base.Borrar(centroMedico);
        }

        public static void EjemploDeUso()
        {
            var daoPersonalizado = Utilidades.Instancia.Fabrica.CrearCentroMedicoDAO();
            // Esta instancia tiene las funcionalidades definidas en la interfaz,
            // las cuales tienes que implementar en la clase.
            // daoPersonalizado.ActualizarCC
            // daoPersonalizado.BorrarCC
            // daoPersonalizado.CrearCC
            // daoPersonalizado.ObtenerCC
            // daoPersonalizado.ObtenerTodosCC
            // Este metodo, EjemploDeUso no esta en la interfaz por tanto no se puede incovar a traves de daoPersonalizado.

            var daoGenerico = Utilidades.Instancia.Fabrica.CrearDAO<CentroMedico>();
            // Esta isntancia tiene los metodos genericos ya implementados en la clase DAO.
            // daoGenerico.Actualizar
            // daoGenerico.Borrar
            // daoGenerico.Crear
            // daoGenerico.ObtenerTodos
            // daoGenerico.ObtenerTodosLosQue
        }

        // Por ultimo, si quieres o necesitas hacer un CustomDAO, lo recomendable es que crees una clase y que herede de 
        // la clase DAO, asi como se hace con CentroMedicoDAO.
        // Si solo necesitas las primitivas, te puedes ahorrar tiempo usando Utilidades.Instancia.Fabrica.CrearDAO<ClaseModelo>(), lo unico
        // que tienes que cuidar es que la clase modelo debe tener un DbSet en ContextoDB, si no, dara una excepcion...
    }
}