using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Business.Interfaces
{
    public interface IReportService
    {
        Task<ReportforPage> ReportDataAsync(ReportforPage reportforPage);
    }
}