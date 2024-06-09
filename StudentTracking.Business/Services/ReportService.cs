using StudentTracking.Business.Interfaces;
using StudentTracking.Core;
using StudentTracking.Core.Enums;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Business.Services
{
    public class ReportService : IReportService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IGradeRepository _gradeRepository;

        public ReportService(IUnitOfWork unitOfWork, IAbsenceRepository absenceRepository, IGradeRepository gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _absenceRepository = absenceRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<ReportforPage> ReportDataAsync(ReportforPage requestDto)
        {
            if (requestDto.ReportTypeId == (int)ReportTypes.Grade)
            {
                var grades = requestDto.LessonId.HasValue
                    ? await _gradeRepository.GetGradesAsync(new GetGradeListRequestDto
                    {
                        UserId = null,
                        LessonId = requestDto.LessonId.Value
                    })
                    : await _gradeRepository.GetAllGradesAsync();

                requestDto.Grades = FilterGrades(grades, requestDto.SituationTypeId, requestDto.StatusTypeId, requestDto.SearchParameter);
                requestDto.Absences = new List<Absence>();
            }
            else
            {
                var absences = requestDto.LessonId.HasValue
                    ? await _absenceRepository.GetAbsenceAsync(new GetAbsenceListRequestDto
                    {
                        UserId = null,
                        LessonId = requestDto.LessonId.Value
                    })
                    : await _absenceRepository.GetAllAbsenceAsync();

                requestDto.Absences = FilterAbsences(absences, requestDto.SituationTypeId, requestDto.StatusTypeId, requestDto.SearchParameter);
                requestDto.Grades = new List<Grade>();
            }

            return requestDto;
        }

        private List<Grade> FilterGrades(List<Grade> grades, int? situationTypeId, int? statusTypeId, int? searchParameter)
        {
            switch (situationTypeId)
            {
                case 1:
                    return grades.Where(p => p.MidtermGrade < 50 && p.FinalGrade < 50).ToList();
                case 2:
                    return grades.Where(p => p.MidtermGrade >= 50 && p.FinalGrade >= 50).ToList();
                default:
                    return statusTypeId == 1
                        ? grades.Where(p => p.MidtermGrade >= searchParameter && p.FinalGrade >= searchParameter).ToList()
                        : grades.Where(p => p.MidtermGrade < searchParameter && p.FinalGrade < searchParameter).ToList();
            }
        }

        private List<Absence> FilterAbsences(List<Absence> absences, int? situationTypeId, int? statusTypeId, int? searchParameter)
        {
            switch (situationTypeId)
            {
                case 1:
                    return absences.Where(p => p.Count >= 10).ToList();
                case 2:
                    return absences.Where(p => p.Count < 10).ToList();
                default:
                    return statusTypeId == 1
                        ? absences.Where(p => p.Count >= searchParameter).ToList()
                        : absences.Where(p => p.Count < searchParameter).ToList();
            }
        }
    }
}