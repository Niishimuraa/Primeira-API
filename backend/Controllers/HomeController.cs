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
            var result = Pessoas.Find(x => x.Nome.StartsWith(nome));
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
    }
}
