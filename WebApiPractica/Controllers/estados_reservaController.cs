using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class estados_reservaController : ControllerBase
    {
        
        public readonly equiposContext _estados_reservacontext;

        public estados_reservaController(equiposContext estados_)
        {
            _estados_reservacontext = estados_;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<estados_reserva> reservas=(from e in _estados_reservacontext.estados_reservas
                                            select e).ToList();

            if (reservas==null)
            {
                return NotFound();
            }
            return Ok(reservas);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult add(estados_reserva estado_)
        {
            try
            {
                _estados_reservacontext.estados_reservas.Add(estado_);
                _estados_reservacontext.SaveChanges();
                return Ok(estado_);

            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route ("update")]

        public IActionResult update(int id, estados_reserva estadosupdate)
        {
            estados_reserva? estados_ = (from e in _estados_reservacontext.estados_reservas
                                         where e.estado_res_id == id
                                         select e).FirstOrDefault();

            if (estados_==null)
            {
                return NotFound();
            }

            estados_.estado = estadosupdate.estado;
            _estados_reservacontext.Entry(estados_).State= EntityState.Modified;
            _estados_reservacontext.SaveChanges();

            return Ok(estadosupdate);
        }


        [HttpDelete]
        [Remote ("Desactivate")]

        public IActionResult Desactivate (int id)
        {
            estados_reserva? estados = (from e in _estados_reservacontext.estados_reservas
                                        where e.estado_res_id == id
                                        select e).FirstOrDefault();

            if (estados==null)
            {
                return NotFound();
            }

            estados.estado = "I";
            _estados_reservacontext.Entry(estados).State= EntityState.Modified;
            _estados_reservacontext.SaveChanges();
            return Ok(estados);
        }

    }
}
