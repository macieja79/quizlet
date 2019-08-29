using System.Collections.Generic;
using System.Linq;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Application.Internals;
using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Inf.LearningService
{
    public class LearningService : ILearningService
    {
        private Queue<QuestionTable> _newQuestions = new Queue<QuestionTable>();
        private Queue<QuestionTable> _learning = new Queue<QuestionTable>();
        private Stack<QuestionTable> _memorized = new Stack<QuestionTable>();
      

        public LearningServiceProcessResult SetupAndGetFirstQuestion(List<QuestionTable> questions)
        {
            ClearData();

            var shuffled = questions.Shuffle().ToList();
            
            shuffled.ForEach(q => _newQuestions.Enqueue(q));

            var currentQuestion = _newQuestions.Peek();
            var status = GetStatus();

            return new LearningServiceProcessResult
            {
                NextQuestion = currentQuestion,
                Status = status
            };
        }

        
        public LearningServiceProcessResult ProcessResultAndGetNextQuestion(QuestionTable currentQuestion, QuestionResult result)
        {

            // process

            QuestionTable processed = null;

            if (_newQuestions.Any())
                processed = _newQuestions.Dequeue();
            else
                processed = _learning.Dequeue();

            if (result == QuestionResult.Again)
                _learning.Enqueue(processed);
            else
               _memorized.Push(processed);

            // get next question

            QuestionTable toLearn;

            if (_newQuestions.Any())
            {
                toLearn = _newQuestions.Peek();
            }
            else if (_learning.Any())
            {
                toLearn = _learning.Peek();
            }
            else
            {
                toLearn = null;
            }
            
            var status = GetStatus();

            return new LearningServiceProcessResult
            {
                NextQuestion = toLearn,
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

        void ClearData()
        {
            _newQuestions.Clear();
            _learning.Clear();
            _memorized.Clear();
        }
    }
}