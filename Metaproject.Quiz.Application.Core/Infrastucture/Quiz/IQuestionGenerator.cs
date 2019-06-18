using Metaproject.Quiz.Domain.Entities;
using System.Collections.Generic;

namespace Metaproject.Quiz.Application.Core.Infrastucture.Quiz
{
    public interface IQuestionGenerator
    {
        string GenerateQuizQuestions(List<QuestionTable> questions);
    }
}
