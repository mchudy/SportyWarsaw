using System.ComponentModel.DataAnnotations.Schema;

namespace SportyWarsaw.Domain.Entities
{
    [ComplexType]
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}