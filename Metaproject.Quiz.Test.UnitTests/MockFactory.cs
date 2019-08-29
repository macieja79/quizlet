using System;
using System.Collections.Generic;
using System.Text;
using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Test.UnitTests
{
    public class MockFactory
    {

        public static List<QuestionTable> CreateEmptyTestQuestions(int numberOfQuestions)
        {
            var list = new List<QuestionTable>();

            for (var i = 0; i < numberOfQuestions; i++)
                list.Add(new QuestionTable {Question = $"Question {i}"});

            return list;
        }

    }
}
