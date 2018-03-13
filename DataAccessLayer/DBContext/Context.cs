using DataAccessLayer.Models;
using System.Data.Entity;


namespace DataAccessLayer.DBContext
{
    public class Context : DbContext
    {
        public Context() : base(nameOrConnectionString: "myConnectionString")
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("public");

            //One to many relation mapping between Employee and Department entity
            modelBuilder.Entity<Employee>()
                .HasRequired<Department>(d => d.Department)
                .WithMany(e => e.Employee)
                .HasForeignKey<int>(e => e.DepartmentId);

            //Set primary key for Login entity
            modelBuilder.Entity<Login>()
                .HasKey<int>(l => l.Id);

            //Set unique constraint for Password property of Login entity
            modelBuilder.Entity<Login>()
                .HasIndex(p => p.Password)
                .IsUnique();

            //Set unique constraint for Username property of Login entity
            modelBuilder.Entity<Login>()
                .HasIndex(p => p.Username)
                .IsUnique();

            //Set required constraint to properties of Employee entity 
            modelBuilder.Entity < Employee>()
            .Property(e => e.Name)
            .IsRequired();

            modelBuilder.Entity<Employee>()
            .Property(e => e.Photo)
            .IsRequired();

            modelBuilder.Entity<Employee>()
            .Property(e => e.MobileNumber)
            .IsRequired();

            modelBuilder.Entity<Employee>()
            .Property(e => e.DateOfBirth)
            .IsRequired();

            modelBuilder.Entity<Employee>()
            .Property(e => e.DateOfJoining)
            .IsRequired();

            modelBuilder.Entity<Employee>()
            .Property(e => e.Address)
            .IsRequired();

           

            //Map entity to table
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Login>().ToTable("Login");

            base.OnModelCreating(modelBuilder);
        }
    }
}
