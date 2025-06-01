namespace PetSaver.Models.ViewModel
{
    public class UserPetCaseViewModel
    {
        public bool IsLost { get; set; } // true = perdido, false = encontrado
        public string Description { get; set; }
        public int? ApproximateAge { get; set; }
        public string AgeUnit { get; set; }
        public string Gender { get; set; }
        public DateTime Date { get; set; }
    }
}