using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetSaver.Models
{
    [Table("Users")] // nome da tabela no banco
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        // Relacionamento com FoundPet
        public ICollection<FoundPet> FoundPets { get; set; } = new List<FoundPet>();
    }
}
