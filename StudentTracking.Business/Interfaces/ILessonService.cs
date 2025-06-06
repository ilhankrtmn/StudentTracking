﻿using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;
using System.Web.Mvc;

namespace StudentTracking.Business.Interfaces
{
    public interface ILessonService
    {
        Task<List<Lesson>> GetLessonListAsync(GetLessonListRequestDto requestDto);
        Task<Lesson> GetLessonAsync(int lessonId);
        Task SaveorUpdateLesson(Lesson lesson);
        Task<bool> DeleteLesson(int lessonId);
        Task<List<SelectListItem>> GetLessonDataSelectListAsync(GetLessonListRequestDto requestDto);
    }
}