using StudentTracking.Business.Interfaces;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Business.Services
{
    public class GradeService : IGradeService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IUnitOfWork unitOfWork, IGradeRepository gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _gradeRepository = gradeRepository;
        }

        public async Task<List<Grade>> GetGradeListAsync(GetGradeListRequestDto requestDto)
        {
            return await _gradeRepository.GetGradesAsync(requestDto);
        }

        public async Task SaveGrade(GradeforListPage requestDto)
        {
            foreach (var item in requestDto.Grades)
            {
                var existingGrade = await _gradeRepository.FindAsync(p => p.Id == item.Id);
                if (existingGrade != null)
                {
                    existingGrade.MidtermGrade = item.MidtermGrade;
                    existingGrade.FinalGrade = item.FinalGrade;
                }
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}