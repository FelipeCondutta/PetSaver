namespace PetSaver.Models.ViewModel
{
    public class PetViewModel
    {

        public string Description { get; set; }
        public int ApproximateAge { get; set; }
        public string AgeUnit { get; set; }
        public string Gender { get; set; }
        public string ChipCode { get; set; }
        public bool SharePhone { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public string PetType { get; set; } // "Found" ou "Lost"
        public int UserId { get; set; }

    }
}
