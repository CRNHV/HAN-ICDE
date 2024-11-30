﻿using ICDE.Lib.Dto.Lessen;

namespace ICDE.Lib.Services.Interfaces;
public interface ILesService
{
    Task<LesDto> CreateLesson(string naam, string beschrijving);
    Task<List<LesDto>> GetAllUniqueLessons();
    Task<LesMetEerdereVersies> GetLessonWithPreviousVersions(Guid groupId);
    Task KoppelLukAanLes(Guid lesGroupId, Guid lukGroupId);
    Task OntkoppelLukAanLes(Guid lesGroupId, Guid lukGroupId);
}