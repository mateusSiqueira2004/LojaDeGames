using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaDeGames.Model
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Nome { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Decricao { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Console { get; set;} = string.Empty;

        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        public DateOnly? DataLancamento { get; set; }

        [Column(TypeName = "DECIMAL(6,2)")]
        public decimal Preco { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Foto { get; set; } = string.Empty;

        public virtual Categoria? Categoria { get; set; }
        public virtual User? Usuario { get; set; }

    }
}
