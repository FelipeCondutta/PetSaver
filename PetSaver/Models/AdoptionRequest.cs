using System.ComponentModel.DataAnnotations;

namespace PetSaver.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }

        [Required]
        public int PetId { get; set; }

        [Required]
        [StringLength(100)]
        public string ApplicantName { get; set; }

        [Required]
        [EmailAddress]
        public string ApplicantEmail { get; set; }

        [Phone]
        public string ApplicantPhone { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(500)]
        public string ReasonForAdoption { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
