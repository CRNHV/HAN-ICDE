using ICDE.Data.Entities;
using ICDE.Data.Repositories.Base;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ICDE.Data.Repositories;
internal class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int?> ZoekStudentNummerVoorUserId(int userId)
    {
        var student = await _context.Studenten.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        if (student is null)
        {
            return null;
        }

        return student.StudentNummer;
    }
}
