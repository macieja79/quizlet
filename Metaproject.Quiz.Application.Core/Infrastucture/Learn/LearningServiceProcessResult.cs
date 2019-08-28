using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Application.Core
{
    public class LearningServiceProcessResult
    {
        public QuestionTable NextQuestion { get; set; }
        public LearningStatus Status { get; set; }
    }
}