
using Azure_api.Data;
using Azure_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Azure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public AmigosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<AmigosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friends>>> GetAll()
        {

            var amigos = new List<Friends>();


            amigos = _context.Friends.FromSqlRaw("EXECUTE dbo.ConsultarAmigos '%%'").ToList();

            if (amigos == null)
            {
                return NoContent();
            }

            return amigos;
        }

        // GET api/<AmigosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Friends>>> Get(int id)
        {
            var amigoList = new List<Friends>();
            var amigoId = new SqlParameter("@AmigosId", id);
            amigoList = _context.Friends.FromSqlRaw("EXECUTE dbo.GetAmigoId @AmigosId", amigoId).ToList();

            if (amigoList == null)
            {
                return NoContent();
            }


            return amigoList;
        }

        // POST api/<AmigosController>
        [HttpPost]
        public async Task<ActionResult<Friends>> Post(Friends amigo)
        {
            var nome = new SqlParameter("@Nome", amigo.Nome);
            var sobrenome = new SqlParameter("@Sobrenome", amigo.Sobrenome);
            var telefone = new SqlParameter("@Telefone", amigo.Telefone);
            var email = new SqlParameter("@Email", amigo.Email);
            var aniversario = new SqlParameter("@Aniversario", amigo.Aniversario);
            var affected = _context.Database.ExecuteSqlRaw("EXECUTE dbo.CadastrarAmigos @Nome, @Sobrenome, @Telefone, @Email, @Aniversario",
                nome, sobrenome, telefone, email, aniversario );

            if (affected > 0)
            {
                return Created("Amigo criado", amigo);
            }
            else
            {
                throw new Exception();
            }

        }

        // PUT api/<AmigosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Friends>> UpdateAmigo(int id, [FromBody] Friends amigo)
        {
            if (id != amigo.AmigosId)
            {
                return BadRequest();
            }

            var amigoId = new SqlParameter("@AmigosId", amigo.AmigosId);
            var nome = new SqlParameter("@Nome", amigo.Nome);
            var sobrenome = new SqlParameter("@Sobrenome", amigo.Sobrenome);
            var telefone = new SqlParameter("@Telefone", amigo.Telefone);
            var email = new SqlParameter("@Email", amigo.Email);
            var aniversario = new SqlParameter("@Aniversario", amigo.Aniversario);

            var affected = _context.Database.ExecuteSqlRaw("EXECUTE dbo.UpdateAmigo @AmigosId, @Nome, @Sobrenome, @Telefone, @Email, @Aniversario",
                amigoId, nome, sobrenome, telefone, email, aniversario);

            if (affected > 0)
            {
                return Ok();
            }
            else
            {
                throw new Exception();
            }
        }

        // DELETE api/<AmigosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friends>> Delete(int id)
        {

            var delete = new SqlParameter("@AmigosId", id);
            _context.Database.ExecuteSqlRaw("EXECUTE dbo.DeleteAmigo1 @AmigosId", delete);

            return NoContent();
        }
    }
}
