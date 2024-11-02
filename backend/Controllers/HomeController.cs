using backend.DTO;
using backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public static List<Pessoa> Pessoas = new List<Pessoa>()
        {
            new Pessoa(Guid.NewGuid(), "Henrique", DateTime.Now),
            new Pessoa(Guid.NewGuid(), "Julia", DateTime.Now),
            new Pessoa(Guid.NewGuid(), "Johnson", DateTime.Now)
        };

        [HttpGet]
        [Route("pessoas")]
        public IActionResult RetornarPessoa()
        {
            return Ok(Pessoas);
        }

        [HttpGet]
        [Route("pessoas/{nome}")]
        public IActionResult RetornarNome(string nome)
        {
            var result = Pessoas.FirstOrDefault(x => x.Nome.StartsWith(nome, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(nome) || result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("pessoas")]
        public IResult AdicionarPessoa([FromBody] PessoaDTO pessoaDto)
        {
            Pessoa pessoa = new Pessoa
            (
                Guid.NewGuid(),
                pessoaDto.Nome,
                DateTime.Now
            );

            Pessoas.Add(pessoa);
            return Results.Ok(pessoa);
        }

        [HttpPut]
        [Route("pessoas/{id}")]
        public IActionResult AtualizarNome(Guid id, [FromBody] PessoaDTO pessoaDto)
        {
            Pessoa pessoa = Pessoas.FirstOrDefault(x => x.Id == id);

            if (pessoa == null)
                return NotFound();

            pessoa.Nome = pessoaDto.Nome;

            return Ok(pessoa);
        }

        [HttpDelete]
        [Route("pessoa/{id}")]
        public IActionResult DeletarPessoa(Guid id)
        {
            Pessoa pessoa = Pessoas.FirstOrDefault(x => x.Id == id);

            if (pessoa == null)
                return NotFound();

            Pessoas.Remove(pessoa);

            return Ok(pessoa);
        }
    }
}
