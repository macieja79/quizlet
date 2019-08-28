using System.Collections.Generic;
using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Application.Core
{
    public interface ILearningService
    {

        LearningServiceProcessResult SetupAndGetFirstQuestion(List<QuestionTable> questions);

        LearningServiceProcessResult ProcessResultAndGetNextQuestion(QuestionTable currentQuestion,
            QuestionResult result);
    }
}
