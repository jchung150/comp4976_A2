using Microsoft.EntityFrameworkCore;

namespace ExoticPlantsAPI.Models
{
    public class ExoticPlantsContext : DbContext
    {
        public ExoticPlantsContext(DbContextOptions<ExoticPlantsContext> options)
            : base(options)
        {
        }

        public DbSet<ExoticPlant> ExoticPlants { get; set; }
    }
}