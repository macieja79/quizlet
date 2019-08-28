using System.Collections.Generic;
using System.Linq;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Inf.LearningService
{
    public class LearningService : ILearningService
    {
        private Queue<QuestionTable> _newQuestions = new Queue<QuestionTable>();
        private Queue<QuestionTable> _learning = new Queue<QuestionTable>();
        private Stack<QuestionTable> _memorized = new Stack<QuestionTable>();
        private QuestionTable _currentQuestion;

        public LearningServiceProcessResult SetupAndGetFirstQuestion(List<QuestionTable> questions)
        {
            questions.ForEach(q => _newQuestions.Enqueue(q));

            _currentQuestion = _newQuestions.Dequeue();
            var status = GetStatus();

            return new LearningServiceProcessResult
            {
                NextQuestion = _currentQuestion,
                Status = status
            };
        }

        public LearningServiceProcessResult ProcessResultAndGetNextQuestion(QuestionTable currentQuestion, QuestionResult result)
        {
            if (result == QuestionResult.Again)
                _learning.Enqueue(_currentQuestion);
            else
            {
               _memorized.Push(_currentQuestion);
            }

            _currentQuestion = _newQuestions.Any() ? _newQuestions.Dequeue() : _learning.Dequeue();

            var status = GetStatus();

            return new LearningServiceProcessResult
            {
                NextQuestion = _currentQuestion,
                Status = status
            };
        }

        LearningStatus GetStatus()
        {
            return new LearningStatus
            {
                New = _newQuestions.Count,
                Learning = _learning.Count,
                Memorized = _memorized.Count
            };
        }
    }
}