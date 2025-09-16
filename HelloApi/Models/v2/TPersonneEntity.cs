using HelloApi.Entities;

namespace HelloApi.Models.V2
{
    internal class TPersonneEntity : TPersonEntity
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public object DateBorn { get; set; }
        public object DateDead { get; set; }
    }
}