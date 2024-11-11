using backend.Context;
using backend.DTO;
using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        /*public static List<Pessoa> Pessoas = new List<Pessoa>()
        {
            new Pessoa(Guid.NewGuid(), "Henrique", DateTime.Now),
            new Pessoa(Guid.NewGuid(), "Julia", DateTime.Now),
            new Pessoa(Guid.NewGuid(), "Johnson", DateTime.Now)
        };*/

        [HttpGet]
        [Route("pessoas")]
        public async Task<IActionResult> RetornarPessoa()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            return Ok(pessoas);
        }
        
        [HttpGet]
        [Route("pessoas/{nome}")]
        public IActionResult RetornarNome(string nome)
        {
            var result = _context.Pessoas.FirstOrDefault(x => x.Nome.StartsWith(nome));

            if (string.IsNullOrEmpty(nome) || result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("pessoas")]
        public async Task<IActionResult> AdicionarPessoa([FromBody] PessoaDTO pessoaDto)
        {
            Pessoa pessoa = new Pessoa
            (
                Guid.NewGuid(),
                pessoaDto.Nome,
                DateTime.Now
            );

            await _context.AddAsync(pessoa);
            await _context.SaveChangesAsync();

            return Ok(pessoa);
        }
        
        [HttpPut]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> AtualizarNome(Guid id, [FromBody] PessoaDTO pessoaDto)
        {
            Pessoa pessoa = await _context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa == null)
                return NotFound();

            pessoa.Nome = pessoaDto.Nome;
            _context.Pessoas.Update(pessoa);

            await _context.SaveChangesAsync();

            return Ok(pessoa);
        }
        
        [HttpDelete]
        [Route("pessoas/{id}")]
        public async Task<IActionResult> DeletarPessoa(Guid id)
        {
            Pessoa pessoa = await _context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa == null)
                return NotFound();

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return Ok(pessoa);
        }
    }
}
