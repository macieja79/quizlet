using System;
using FluentAssertions;
using Metaproject.Quiz.Inf.WordRepository.Mappers;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void QuestionIdParser_Should_Parse_Date()
        {
            var sut = "2019-07-05 14:21:59";

            var parser = new QuestionIdParser();

            bool isParsed = parser.TryParse(sut, out DateTime result);

            isParsed.Should().BeTrue();
            result.Year.Should().Be(2019);
            result.Month.Should().Be(7);
            result.Day.Should().Be(5);
            result.Hour.Should().Be(14);
            result.Minute.Should().Be(21);
            result.Second.Should().Be(59);

        }
    }
}