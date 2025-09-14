using HelloApi.Entities;

namespace HelloApi.Models.V2
{
    public class TPerson
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateBorn { get; set; }
        public DateTime? DateDead { get; set; }
        public string? Nationalite { get; set; } // Exemple de différence


        // Conversion implicite vers TPersonneEntity
        public static implicit operator TPersonEntity(TPerson person)
        {
            if (person == null) return null;
            return new TPersonneEntity
            {
                Id = person.Id,
                Nom = person.Nom,
                Prenom = person.Prenom,
                DateBorn = person.DateBorn,
                DateDead = person.DateDead,
                Nationalite = person.Nationalite
            };
        }
    }
}