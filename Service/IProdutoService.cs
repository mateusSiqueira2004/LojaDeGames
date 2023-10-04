using LojaDeGames.Model;

namespace LojaDeGames.Service
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAll();

        Task<Produto?> GetById(long id);

        Task<IEnumerable<Produto>> GetByNome(string nome);

        Task<IEnumerable<Produto>> GetByNomeConsole(string nome, string console);

        Task<IEnumerable<Produto>> GetByPrecoEntre(decimal valor1, decimal valor2);

        Task<IEnumerable<Produto>> GetByPreco(decimal preco);

        Task<Produto?> Create(Produto produto);

        Task<Produto?> Update(Produto produto);

        Task Delete(Produto produto);
    }
}
