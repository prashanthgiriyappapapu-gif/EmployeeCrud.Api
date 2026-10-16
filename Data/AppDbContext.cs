using EmployeeCrud.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
}
