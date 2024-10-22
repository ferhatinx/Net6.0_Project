using Humanizer;
using JwtAppBack.Persistance.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace JwtAppBack.Persistance.Context;

public class JwtAppContext : DbContext
{
    //* Hepsi aynı farklı gösterimleri
    public DbSet<Product> Products { get{ return this.Products;} }
    public DbSet<Category> Categories => this.Categories;
    public DbSet<AppUser> AppUsers { get {return this.Set<AppUser>();} }
    public DbSet<AppRole> AppRoles => this.Set<AppRole>();
    //*
    public JwtAppContext(DbContextOptions<JwtAppContext> options): base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //* Relation
        modelBuilder.Entity<Product>().HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);
        modelBuilder.Entity<AppRole>().HasMany(x=>x.AppUsers).WithOne(x=>x.AppRole).HasForeignKey(x=>x.AppRoleId);
        base.OnModelCreating(modelBuilder);

        //* ADD-ROLE
        modelBuilder.Entity<AppRole>().HasData(new AppRole[]{
            new AppRole(1,"Admin"),
            new AppRole(2,"Member"),

        });
    }
}
