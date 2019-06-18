using Metaproject.Quiz.Application.Core.Infrastucture.Quiz;
using Metaproject.Quiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metaproject.Quiz.Inf.QuizletService
{
    public class QuizletServiceImpl : IQuestionGenerator
    {
        public string GenerateQuizQuestions(List<QuestionTable> questions)
        {
            var builder = new StringBuilder();

            foreach (var question in questions)
            {
                var line = $"{question.Question}|{question.Answer};";
                builder.Append(line);
            }

            return builder.ToString();
        }
    }
}
