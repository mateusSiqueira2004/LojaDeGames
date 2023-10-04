using LojaDeGames.Data;
using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;

namespace LojaDeGames.Service.Implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var BuscaCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscaCategoria is null)
                    return null;
            }

            produto.Categoria = produto.Categoria is not null ? _context.Categorias.FirstOrDefault(t => t.Id == produto.Categoria.Id) : null;

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        }
        public async Task<Produto?> GetById(long id)
        {
            try
            {
                var Produto = await _context.Produtos.Include(p => p.Categoria).FirstAsync(i => i.Id == id);
                return Produto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Produto>> GetByNome(string nome)
        {
            var Produto = await _context.Produtos.
                               Include(p => p.Categoria).
                               Where(p => p.Nome.Contains(nome)).
                               ToListAsync();

            return Produto;
        }

        public async Task<IEnumerable<Produto>> GetByNomeConsole(string nome,string console)
        {
            var Produto = await _context.Produtos.
                                Include(p => p.Categoria).
                                Where(p => p.Nome.Contains(nome) || p.Console.Contains(console)).
                                ToListAsync();
            return Produto;
        }

        public async Task<Produto?> Update(Produto produto)
        {
            var ProdutoUpdate = await _context.Produtos.FindAsync(produto.Id);
            if (ProdutoUpdate is null)
                return null;
            if (produto.Categoria is not null)
            {
                var BuscaCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscaCategoria is null)
                    return null;
            }

            produto.Categoria = produto.Categoria is not null ? _context.Categorias.FirstOrDefault(t => t.Id == produto.Categoria.Id) : null;
            
            _context.Entry(ProdutoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }
        public async Task<IEnumerable<Produto>> GetByPrecoEntre(decimal valor1, decimal valor2)
        {
             var Produto = await _context.Produtos
                .Where(p => p.Preco >= valor1 && p.Preco <= valor2)
                .Include(p => p.Categoria)
                .ToListAsync();
            return Produto;
        }

        public async Task<IEnumerable<Produto>> GetByPreco(decimal preco)
        {
            var Produto = await _context.Produtos
               .Where(p => p.Preco == preco)
               .Include(p => p.Categoria)
               .ToListAsync();
            return Produto;
        }
    }
}
