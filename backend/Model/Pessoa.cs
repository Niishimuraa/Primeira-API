namespace backend.Model
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataDeCadastro { get; set; }

        public Pessoa(Guid id, string nome, DateTime dataDeCadastro)
        {
            Id = id;
            Nome = nome;
            DataDeCadastro = dataDeCadastro;
        }
    }
}
