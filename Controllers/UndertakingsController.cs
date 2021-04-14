using System.Collections.Generic;
using afternoon.Models;
using afternoon.Services;
using Microsoft.AspNetCore.Mvc;

namespace afternoon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UndertakingsController : ControllerBase
    {
        private readonly UndertakingsService _uservice;

        public UndertakingsController(UndertakingsService uservice)
        {
            _uservice = uservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Undertaking>> Get()
        {
            try
            {
                return Ok(_uservice.Get());
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Undertaking> Get(int id)
        {
            try
            {
                return Ok(_uservice.GetById(id));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        public ActionResult<Undertaking> Create([FromBody] Undertaking newUnder)
        {
            try
            {
                return Ok(_uservice.Create(newUnder));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Undertaking> Edit([FromBody] Undertaking updated, int id)
        {
            try
            {
                updated.Id = id;
                return Ok(_uservice.Edit(updated));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);

            }
        }
        [HttpDelete("{id}")]
        public ActionResult<Undertaking> Delete(int id, string userId)
        {
            try
            {
                return Ok(_uservice.Delete(id, userId));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}