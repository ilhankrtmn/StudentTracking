using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Business.Interfaces
{
    public interface IAbsenceService
    {
        Task<List<Absence>> GetAbsenceListAsync(GetAbsenceListRequestDto requestDto);
        Task SaveAbsence(AbsenceforListPage requestDto);
    }
}