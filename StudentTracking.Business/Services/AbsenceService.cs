using StudentTracking.Business.Interfaces;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using StudentTracking.Data.Models.PageModel;
using StudentTracking.Data.Models;

namespace StudentTracking.Business.Services
{
    public class AbsenceService : IAbsenceService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAbsenceRepository _absenceRepository;

        public AbsenceService(IUnitOfWork unitOfWork, IAbsenceRepository absenceRepository)
        {
            _unitOfWork = unitOfWork;
            _absenceRepository = absenceRepository;
        }

        public async Task<List<Absence>> GetAbsenceListAsync(GetAbsenceListRequestDto requestDto)
        {
            return await _absenceRepository.GetAbsenceAsync(requestDto);
        }

        public async Task SaveAbsence(AbsenceforListPage requestDto)
        {
            foreach (var item in requestDto.Absences)
            {
                var existingGrade = await _absenceRepository.FindAsync(p => p.Id == item.Id);
                if (existingGrade != null)
                {
                    existingGrade.Count = item.Count;
                }
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
