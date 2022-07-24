using To_Do_List.Application.Common;
using To_Do_List.Application.DTOs.Tarea;
using To_Do_List.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace To_Do_List.API.Controllers
{
    /// <summary>
    /// Servicios para crear, modificar, eliminar o listar las Tareas.
    /// </summary>
    [Route("api/[Controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        #region Public Methods
        /// <summary>
        /// Permite Buscar Todos las Tareas
        /// </summary>
        /// <returns>
        /// <para>Los datos de Todas las Tareas ordenados por Id</para>
        /// </returns>
        /// 
        /// Ejemplo de solicitud:
        // GET: api/Tarea
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TareaResponse>>> GetAll()
        {
            return Ok(await _tareaService.GetAll());
        }
        /// <summary>
        /// Permite Buscar por el Id de una Tarea
        /// </summary>
        /// <returns>
        /// <para>Los datos de la Tarea que se buscan por Id</para>
        /// <para>Caso contrario StatusCode404 si no existe una Tarea con la misma Id y un mensaje de la misma</para>
        /// </returns>
        /// <param name="id">Id de la Tarea a Buscar</param>
        /// 
        /// Ejemplo de solicitud:
        // GET: api/Tarea?id=1
        [HttpGet("{id}")]
        public async Task<ActionResult<TareaResponse>> GetById([FromRoute] int id)
        {
            var result = await _tareaService.GetById(id);
            if (result == null)
            {
                return StatusCode(404, new { message = $"La Tarea no fue encontrada en el sistema" });
            }
            return Ok(result);
        }
        /// <summary>
        /// Permite Buscar por el Nombre de una Tarea
        /// </summary>
        /// <returns>
        /// <para>Los datos de la Tarea que se buscan por Nombre</para>
        /// <para>Caso contrario StatusCode404 si no existe una Tarea con el mismo Nombre y un mensaje de la mismo</para>
        /// </returns>
        /// <param name="nombre">Nombre de la Tarea a Buscar</param>
        /// 
        /// Ejemplo de solicitud:
        // GET: api/Tarea?nombre=AbrirPuerta
        [HttpGet("{nombre}")]
        public async Task<ActionResult<TareaResponse>> GetByName([FromRoute] string nombre)
        {
            var result = await _tareaService.GetByName(nombre);
            if (result == null)
            {
                return StatusCode(404, new { message = $"La Tarea no fue encontrada en el sistema" });
            }
            return Ok(result);
        }
        /// <summary>
        /// Permite Buscar por el Estado de una Tarea
        /// </summary>
        /// <returns>
        /// <para>Los datos de la Tarea que se buscan por Estado</para>
        /// <para>Caso contrario StatusCode404 si no existe una Tarea con el mismo Estado y un mensaje de la mismo</para>
        /// </returns>
        /// <param name="estado">Estado de la Tarea a Buscar</param>
        /// 
        /// Ejemplo de solicitud:
        // GET: api/Tarea?estado=True
        [HttpGet("{estado}")]
        public async Task<ActionResult<TareaResponse>> GetByState([FromRoute] bool estado)
        {
            var result = await _tareaService.GetByState(estado);
            if (result == null)
            {
                return StatusCode(404, new { message = $"La Tarea no fue encontrada en el sistema" });
            }
            return Ok(result);
        }
        /// <summary>
        /// Este metodo permite Agregar una nueva Tarea a la base de datos
        /// </summary>
        /// <remarks> Ejemplo:
        /// {
        /// "Nombre": "Abrir Puerta",
        /// "Estado": "True"
        /// }
        /// <para>El campo Nombre es obligatorio y debe contener al menos 5 caracteres</para>
        /// <para>El campo Estado puede ser null</para>
        /// </remarks>
        /// <returns>
        /// <para>Los datos de la Tarea agregada y un mensaje de que se Agrego correctamente la misma</para>
        /// <para>Caso contrario BadRequest si ya existe una Tarea con el mismo Nombre y un mensaje del mismo</para>
        /// </returns>
        /// <param name="request">DTO de una Tarea que almacena los datos de la misma</param>
        /// 
        /// Ejemplo de solicitud:
        // POST: api/Tarea/Insert
        [HttpPost]
        public async Task<ActionResult<Result>> Insert([FromBody] TareaRequest request)
        {
            var response = await _tareaService.Insert(request);

            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);
        }
        /// <summary>
        /// Este metodo permite Modificar una Tarea de la base de datos
        /// </summary>
        /// <remarks> Ejemplo:
        /// {
        /// "Nombre": "nombre a Modificar",
        /// "Estado": "estado a modificar"
        /// }
        /// <para>El campo Nombre es obligatorio y debe contener al menos 5 caracteres</para>
        /// <para>El campo Estado puede ser null</para>
        /// </remarks>
        /// <returns>
        /// <para>Los nuevos datos de la Tarea y un mensaje de que se Modifico correctamente la misma</para>
        /// <para>Caso contrario BadRequest si no existe una Tarea con la misma Id y un mensaje del mismo</para>
        /// </returns>
        /// <param name="request">DTO de una Tarea que almacena los datos de la misma</param>
        /// 
        /// Ejemplo de solicitud:
        // PUT: api/Tarea/Update?id=1
        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Update([FromRoute] int id, [FromBody] TareaRequest request)
        {
            var response = await _tareaService.Update(id, request);

            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);
        }
        /// <summary>
        /// Permite Borrado lógico de una Tarea
        /// </summary>
        /// <returns>
        /// <para>Los datos de la Tarea eliminados de baja logica y un mensaje de que se eliminio correctamente la misma</para>
        /// <para>Caso contrario BadRequest si no existe una Tarea con el mismo Id y un mensaje del mismo</para>
        /// </returns>
        /// <param name="id">Id de la Tarea a Borrar</param>
        /// 
        /// Ejemplo de solicitud:
        // DELETE: api/Tarea/Delete?id=1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete([FromRoute] int id)
        {
            var response = await _tareaService.Delete(id);

            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);
        }
        /// <summary>
        /// Permite Marcar como Activa o Completa una Tarea
        /// </summary>
        /// <returns>
        /// <para>Los datos de la Tarea se activaran o desactivaran y un mensaje de la misma</para>
        /// <para>Caso contrario BadRequest si no existe una Tarea con el mismo Id y un mensaje del mismo</para>
        /// </returns>
        /// <param name="estado">Estado de la Tarea</param>
        /// 
        /// Ejemplo de solicitud:
        // DELETE: api/Tarea/estado=true
        [HttpPut("{estado}")]
        public async Task<ActionResult<Result>> TareaEstado([FromBody] bool estado)
        {
            var response = await _tareaService.TareaActiva(estado);

            return response.HasErrors
                ? BadRequest(response.Messages)
                : Ok(response);
        }
        #endregion
    }
}
