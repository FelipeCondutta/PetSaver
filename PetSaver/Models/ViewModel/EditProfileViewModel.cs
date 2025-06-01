using System.ComponentModel.DataAnnotations;

namespace PetSaver.Models.ViewModel
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite sua senha para confirmar as alterações.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
    }
}
