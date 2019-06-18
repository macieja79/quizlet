using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Inf.WordRepository.Mappers
{
    public interface ITableMapper
    {
        bool TryGetQuestionTable(Table wordTable, out List<QuestionTable> questions);
    }
}