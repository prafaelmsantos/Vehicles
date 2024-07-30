namespace Vehicles.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<VehicleImage> VehicleImages { get; set; } = null!;
        public DbSet<Mark> Marks { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleMap());
            modelBuilder.ApplyConfiguration(new VehicleImageMap());
            modelBuilder.ApplyConfiguration(new MarkMap());
            modelBuilder.ApplyConfiguration(new ModelMap());

            modelBuilder.AddInitialSeed();
        }
    }
}
