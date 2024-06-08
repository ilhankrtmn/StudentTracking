using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;

namespace StudentTracking.Data.EntityFramework.Repositories.Interfaces
{
    public interface IAbsenceRepository : IRepositoryBase<Absence>
    {
        Task<List<Absence>> GetAbsenceAsync(GetAbsenceListRequestDto requestDto);
    }
}