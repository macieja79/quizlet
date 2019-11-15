using System.Runtime.InteropServices.ComTypes;
using FluentAssertions;
using Metaproject.Quiz.Inf.WordDocsRepository;
using Metaproject.Quiz.Test.IntegrationTests.Helpers;
using NUnit.Framework;

namespace Tests
{
    public class WordTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WordFilesRepository_Should_Extract_Cheatsheet_Table_From_Docx()
        {
            var wordDocRepo = new WordFilesRepository();

            var rootPath = IntegrationTestsHelper.GetFullResourcePath("integration-test-cheatsheet-table.docx");

            var doc = wordDocRepo.GetDocument(rootPath);

            doc.Should().NotBeNull();
            doc.Tables.Should().HaveCount(3);
            doc.Tables[0].Question.Should().Be(@"Test question 1/3");
            doc.Tables[0].Answer.Should().Be(@"cmd answer –demo1");
            doc.Tables[1].Question.Should().Be(@"Test question 2/3");
            doc.Tables[1].Answer.Should().Be(@"cmd answer –demo2");
            doc.Tables[2].Question.Should().Be(@"Test question 3/3");
            doc.Tables[2].Answer.Should().Be(@"cmd answer –demo3");
        }

        [Test]
        public void WordFilesRepository_Should_Extract_Question_Table_From_Docx()
        {
            var wordDocRepo = new WordFilesRepository();

            var rootPath = IntegrationTestsHelper.GetFullResourcePath("integration-test-question-table.docx");

            var doc = wordDocRepo.GetDocument(rootPath);

            doc.Should().NotBeNull();
            doc.Tables.Should().HaveCount(1);
            doc.Tables[0].Question.Should().Be("Test question");
            doc.Tables[0].Answers[0].Should().Be("Test answer 1");
            doc.Tables[0].Answers[1].Should().Be("Test answer 2");
            doc.Tables[0].Answers[2].Should().Be("Test answer 3");
        }

        [Test]
        public void WordFilesRepository_Should_Store_Image_As_Answer()
        {
            var wordDocRepo = new WordFilesRepository();

            var rootPath = IntegrationTestsHelper.GetFullResourcePath("integration-test-question-image.docx");

            var doc = wordDocRepo.GetDocument(rootPath);
            
            doc.Tables.Should().HaveCount(1);
            


        }



    }
}