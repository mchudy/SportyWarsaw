using System.Collections.Generic;

namespace SportyWarsaw.Domain.Entities
{
    public class SportsFacility
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string District { get; set; }
        public string AdministrativeUnit { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public IList<string> Emails { get; set; }
    }
}
