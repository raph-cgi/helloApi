using HelloApi.Entities;
using HelloApi.RecycleBin;

namespace HelloApi.Models.V1
{
    public class TPerson
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateOnly DateBorn { get; set; }
        public DateOnly? DateDead { get; set; }

        // Conversion implicite vers TPersonEntity
        public static implicit operator TPersonEntity(TPerson person)
        {
            if (person == null) return null;
            return new TPersonEntity
            {
                Id = person.Id,
                Nom = person.Nom,
                Prenom = person.Prenom,
                DateBorn = person.DateBorn,
                DateDead = person.DateDead
            };
        }
    }
}