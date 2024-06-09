using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;

namespace StudentTracking.Data.EntityFramework.Repositories.Interfaces
{
    public interface IGradeRepository : IRepositoryBase<Grade>
    {
        Task<List<Grade>> GetGradesAsync(GetGradeListRequestDto requestDto);
        Task<List<Grade>> GetAllGradesAsync();
    }
}
