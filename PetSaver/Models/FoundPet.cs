using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetSaver.Models
{
    [Table("FoundedPets")] // Nome da tabela no banco
    public class FoundPet
    {
        [Key]
        public int Id { get; set; } // Chave primária

        [Range(0, 100, ErrorMessage = "A idade aproximada deve estar entre 0 e 100.")]
        public int? ApproximateAge { get; set; } // Idade aproximada

        [Required(ErrorMessage = "A unidade de idade é obrigatória.")]
        [StringLength(10, ErrorMessage = "A unidade de idade deve ter no máximo 10 caracteres.")]
        public string AgeUnit { get; set; } // Unidade da idade (anos/meses)

        [Required(ErrorMessage = "O sexo do pet é obrigatório.")]
        [StringLength(10, ErrorMessage = "O sexo deve ter no máximo 10 caracteres.")]
        public string Gender { get; set; } // Sexo 

        [StringLength(50, ErrorMessage = "O código do chip deve ter no máximo 50 caracteres.")]
        public string? ChipCode { get; set; } // Código do chip identificador

        [StringLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres.")]
        public string Description { get; set; } // Descrição do pet

        [Required(ErrorMessage = "É necessário informar se o telefone será compartilhado.")]
        public bool SharePhone { get; set; } // Se o telefone será compartilhado

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string PhoneNumber { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? ImageUrl { get; set; } // Caminho da imagem no wwwroot
        [NotMapped]
        public IFormFile? ImageFile { get; set; } // Para o upload temporário

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; } 
    }
}
