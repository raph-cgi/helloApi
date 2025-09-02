namespace HelloApi.Models.V2
{
    public class TPerson
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateBorn { get; set; }
        public DateTime? DateDead { get; set; }
        public string Nationalite { get; set; } // Exemple de différence
    }
}