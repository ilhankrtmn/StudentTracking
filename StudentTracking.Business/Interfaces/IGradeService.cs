using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Business.Interfaces
{
    public interface IGradeService
    {
        Task<List<Grade>> GetGradeListAsync(GetGradeListRequestDto requestDto);
        Task SaveGrade(GradeforListPage requestDto);
    }
}