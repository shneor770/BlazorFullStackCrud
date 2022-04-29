namespace BlazorFullStackCrud.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comic>().HasData(
                new Comic { Id = 1, Name = "Shneor" },
                new Comic { Id = 2, Name = "Mushka" }
            );

            modelBuilder.Entity<SuperHero>().HasData(
                new SuperHero
                {
                    Id = 1,
                    FirsName = "Super",
                    LastName = "Man",
                    HeroName = "SuperMan",
                    ComicId = 1
                },
               new SuperHero
               {
                   Id = 2,
                   FirsName = "Piter",
                   LastName = "Pen",
                   HeroName = "PiterPen",
                   ComicId = 2
               }
           );
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Comic>  Comics { get; set; }
    }
}
