using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Consultas
{
    public class ArticulosRepositorio
    {
        /// <summary>
        /// Este metodo se encarga de consultar todos los registros de la tabla articulos
        /// </summary>
        /// <returns></returns>
        public List<tblArticulos> ConsultaTodosArticulos()
        {
            //Se estable conexion con la base de datos
            using (GalletasDBEntities db = new GalletasDBEntities())
            {
                //Se retorna la lista de articulos consultados en la Base de datos
                return db.tblArticulos.ToList();
            }


        }

        /// <summary>
        /// Este metodo se encarga de crear un nuevo registro en la tabla articulos y guardarlo en la base de datos
        /// </summary>
        /// <param name="_nomArticulo"></param>
        /// <param name="_descripcion"></param>
        /// <param name="_categoria"></param>
        /// <param name="_precio"></param>
        /// <returns></returns>
        public bool AgregarArticulo(string _nomArticulo, string _descripcion, string _categoria, int _precio)
        {
            //Se crea objeto del molde tblArticulos y los cargamos con toda la informacion que llega
            tblArticulos nuevoArticulo = new tblArticulos();
            //Se asigna los valores de
            nuevoArticulo.IdArticulo = Guid.NewGuid().ToString();
            nuevoArticulo.NomArticulo = _nomArticulo;
            nuevoArticulo.Descripcion = _descripcion;
            nuevoArticulo.Categoria = _categoria;
            nuevoArticulo.Precio = _precio;
            nuevoArticulo.Eliminado = false;

            //Se crea un objeto de tipo contex de la base de datos para accederla
            using (GalletasDBEntities db = new GalletasDBEntities())
            {
                //Se agrega el nuevo registro a la tabla tblArticulos de la base de datos
                db.tblArticulos.Add(nuevoArticulo);
                //Se guarda el resultado de los registros insertados en la base de datos
                int numeroRegistro = db.SaveChanges();
                //Se valida el resultado de insertar el nuevo registro y retornamos el resultado
                if (numeroRegistro > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Este metodo se encarga de consultar un registro y de modificarlo
        /// </summary>
        /// <param name="_idArticulo"></param>
        /// <param name="_nomArticulo"></param>
        /// <param name="_descripcion"></param>
        /// <param name="_categoria"></param>
        /// <param name="_precio"></param>
        /// <returns></returns>
        public bool ActualizarArticulo(string _idArticulo, string _nomArticulo, string _descripcion, string _categoria, int _precio, bool _eliminado)
        {
            //Se crea un objeto de tipo contex de la base de datos para accederla
            using (GalletasDBEntities db = new GalletasDBEntities())
            {
                //var dato = db.tblArticulos.Find(_idArticulo);
                //Se consulta el articulo en la base de datos
                var registro = (from a in db.tblArticulos
                                where a.IdArticulo == _idArticulo
                                select a).FirstOrDefault();
                //Se valida si se tiene un registro para modificar
                if (registro == null)
                    return false;

                //Se realiza las modificaciones necesarias
                registro.NomArticulo = _nomArticulo;
                registro.Descripcion = _descripcion;
                registro.Categoria = _categoria;
                registro.Precio = _precio;
                registro.Eliminado = _eliminado;

                //Se guarda el numero de registros modificados
                int regitrosModificados = db.SaveChanges();
                //Validamos si se realizaron los cambios
                if (regitrosModificados == 0)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Este metodo se utiliza para actividad o desactivar un articulo
        /// </summary>
        /// <param name="_idArticulo"></param>
        /// <param name="_eliminado"></param>
        /// <returns></returns>
        public bool ActivarDesActivarArticulo(string _idArticulo, bool _eliminado)
        {
            //Se crea un objeto de tipo contex de la base de datos para accederla
            using (GalletasDBEntities db = new GalletasDBEntities())
            {
                //var dato = db.tblArticulos.Find(_idArticulo);
                //Se consulta el articulo en la base de datos
                var registro = (from a in db.tblArticulos
                                where a.IdArticulo == _idArticulo
                                select a).FirstOrDefault();
                //Se valida si se tiene un registro para modificar
                if (registro == null)
                    return false;

                //Se realiza las modificaciones necesarias           
                registro.Eliminado = _eliminado;

                //Se guarda el numero de registros modificados
                int regitrosModificados = db.SaveChanges();
                //Validamos si se realizaron los cambios
                if (regitrosModificados == 0)
                {
                    return false;
                }
                return true;
            }           
        }
    }

}
