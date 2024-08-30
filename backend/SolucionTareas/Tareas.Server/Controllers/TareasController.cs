using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Tareas.Server.Models;
using TareasServer.Clases;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tareas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly TareasContext _dbContext;

        public TareasController(TareasContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Consulta")]
        public async Task<IActionResult> Consulta()
        {
            var responseApi = new ResponseAPI<List<TareasDA>>();
            var listaTareaDA = new List<TareasDA>();

            try
            {
                var datos = await _dbContext.TareasPendientes.ToListAsync();

                foreach (var item in await _dbContext.TareasPendientes.ToListAsync())
                {
                    listaTareaDA.Add(new TareasDA
                    {
                        Id = item.Id,
                        Titulo = item.Titulo,
                        Descripcion = item.Descripcion,
                        Fecha_Creacion = item.FechaCreacion,
                        Fecha_Vencimiento = item.FechaVencimiento,
                        Completada = item.Completada,
                        Estado = item.Estado
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaTareaDA;
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
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

