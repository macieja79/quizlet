using FluentAssertions;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Application.Internals;
using Metaproject.Quiz.Inf.LearningService;
using NUnit.Framework;

namespace Metaproject.Quiz.Test.UnitTests
{
    public class LearningServiceTests
    {
        [Test]
        public void LearningStatus_Should_Show_Results()
        {
            var numberOfQuestions = 10;
            var questions = MockFactory.CreateEmptyTestQuestions(numberOfQuestions);
            var learningService = new LearningService();

            var result = learningService.SetupAndGetFirstQuestion(questions);
            var currentQuestion = result.NextQuestion;

            result.Status.New.Should().Be(10);
            result.Status.Learning.Should().Be(0);
            result.Status.Memorized.Should().Be(0);
            result.Status.IsAnythingToLearn.Should().BeTrue();

            // answering 'Again' and 'Memorized' alternately
            for (var i = 1; i <= 10; i++)
            {
                var validationResult = i.IsOdd() ? QuestionResult.Again : QuestionResult.Memorized;
                result = learningService.ProcessResultAndGetNextQuestion(currentQuestion, validationResult);
            }

            result.Status.New.Should().Be(0);
            result.Status.Learning.Should().Be(5);
            result.Status.Memorized.Should().Be(5);
            result.Status.IsAnythingToLearn.Should().BeTrue();

            // answering 'Memorized'
            for (var i = 1; i <= 5; i++)
            {
                result = learningService.ProcessResultAndGetNextQuestion(currentQuestion, QuestionResult.Memorized);
            }

            result.Status.New.Should().Be(0);
            result.Status.Learning.Should().Be(0);
            result.Status.Memorized.Should().Be(10);
            result.Status.IsAnythingToLearn.Should().BeFalse();

        }
    }
}
