using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tareas.Server.Models;
using TareasServer.Clases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


namespace Tareas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasSQLController : ControllerBase
    {
        private readonly TareasContext _dbContext;

        public TareasSQLController(TareasContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Consulta/{titulo}/{estado}")]
        public async Task<IActionResult> Consulta(String titulo, String estado)
        {
            var responseApi = new ResponseAPI<List<TareasDA>>();
            List<TareaOD> olpendiente = new List<TareaOD>();
            try
            {
                System.Data.SqlClient.SqlConnection oconexion = (System.Data.SqlClient.SqlConnection)_dbContext.Database.GetDbConnection();
                System.Data.SqlClient.SqlCommand comando = oconexion.CreateCommand();
                oconexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_dr_select_all_tareas_pendientes";
                comando.Parameters.Add("@Titulo", System.Data.SqlDbType.VarChar, 20).Value = titulo;
                comando.Parameters.Add("@estado", System.Data.SqlDbType.VarChar, 1).Value = estado;
                System.Data.SqlClient.SqlDataReader tarearead = comando.ExecuteReader();

                while (tarearead.Read())
                {
                    TareaOD opendiente = new TareaOD();
                    opendiente.Id = (int)tarearead["Id"];
                    opendiente.Titulo = (string)tarearead["Titulo"];
                    opendiente.Descripcion = (string)tarearead["Descripcion"];
                    opendiente.FechaCreacion = (DateTime)tarearead["Fecha_Creacion"];
                    opendiente.FechaVencimiento = (DateTime)tarearead["Titulo"];
                    opendiente.Completada = (bool)tarearead["Completada"];
                    opendiente.Estado = (string)tarearead["Titulo"];

                    olpendiente.Add(opendiente);
                }

                oconexion.Close();
                responseApi.EsCorrecto = true;
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(olpendiente);
        }

        [HttpGet]
        [Route("BuscarTitulo/{titulo}")]
        public async Task<IActionResult> BuscarTitulo(String titulo)
        {
            var responseApi = new ResponseAPI<TareasDA>();
            var TareasDA = new TareasDA();

            try
            {
                var dbTareas = await _dbContext.TareasPendientes.FirstOrDefaultAsync(x => x.Titulo == titulo);

                if (dbTareas != null)
                {
                    TareasDA.Id = dbTareas.Id;
                    TareasDA.Titulo = dbTareas.Titulo;
                    TareasDA.Descripcion = dbTareas.Descripcion;
                    TareasDA.Fecha_Creacion = dbTareas.FechaCreacion;
                    TareasDA.Fecha_Vencimiento = dbTareas.FechaVencimiento;
                    TareasDA.Completada = dbTareas.Completada;
                    TareasDA.Estado = dbTareas.Estado;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TareasDA;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<TareasDA>();
            var TareasDA = new TareasDA();

            try
            {
                var dbTareas = await _dbContext.TareasPendientes.FirstOrDefaultAsync(x => x.Id == id);

                if (dbTareas != null)
                {
                    TareasDA.Id = dbTareas.Id;
                    TareasDA.Titulo = dbTareas.Titulo;
                    TareasDA.Descripcion = dbTareas.Descripcion;
                    TareasDA.Fecha_Creacion = dbTareas.FechaCreacion;
                    TareasDA.Fecha_Vencimiento = dbTareas.FechaVencimiento;
                    TareasDA.Completada = dbTareas.Completada;
                    TareasDA.Estado = dbTareas.Estado;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TareasDA;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Grabar")]
        public async Task<IActionResult> Guardar(TareasDA tarea)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTareas = new TareasPendiente
                {
                    Titulo = tarea.Titulo,
                    Descripcion = tarea.Descripcion,
                    FechaCreacion = tarea.Fecha_Creacion,
                    FechaVencimiento = tarea.Fecha_Vencimiento,
                    Completada = tarea.Completada,
                    Estado = tarea.Estado
                };

                _dbContext.TareasPendientes.Add(dbTareas);
                await _dbContext.SaveChangesAsync();

                if (dbTareas.Id != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTareas.Id;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(TareasDA tarea, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbTarea = await _dbContext.TareasPendientes.FirstOrDefaultAsync(e => e.Id == id);

                if (dbTarea != null)
                {

                    dbTarea.Titulo = tarea.Titulo;
                    dbTarea.Descripcion = tarea.Descripcion;
                    dbTarea.FechaCreacion = tarea.Fecha_Creacion;
                    dbTarea.FechaVencimiento = tarea.Fecha_Vencimiento;
                    dbTarea.Completada = tarea.Completada;
                    dbTarea.Estado = tarea.Estado;


                    _dbContext.TareasPendientes.Update(dbTarea);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTarea.Id;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbTarea = await _dbContext.TareasPendientes.FirstOrDefaultAsync(e => e.Id == id);

                if (dbTarea != null)
                {
                    _dbContext.TareasPendientes.Remove(dbTarea);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
