
namespace ExoticPlantsAPI.Models
{
    public class ExoticPlant
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Countries { get; set; }
        public string? Image { get; set; } // Base64 encoded image
    }
}