namespace PetSaver.Models.ViewModel
{
    public class AdoptionRequestViewModel
    {
        public int Id { get; set; }
        public string PetNome { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsApproved { get; set; }
        public string ReasonForAdoption { get; set; }
    }
}
