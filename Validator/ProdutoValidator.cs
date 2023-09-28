using FluentValidation;
using LojaDeGames.Model;

namespace LojaDeGames.Validator
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator() {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(255);
            RuleFor(p => p.Decricao)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(255);
            RuleFor(p => p.Console)
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(255);
            RuleFor(p => p.Foto)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(255);

            RuleFor(p => p.Preco)
                .ScalePrecision(2,6);

           
        }

    }
}
