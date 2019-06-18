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
        public void CheatsheetTable()
        {
            var wordDocRepo = new WordFilesRepository();

            var rootPath = IntegrationTestsHelper.GetFullResourcePath("integration-test-cheatsheet-table.docx");

            var docs = wordDocRepo.GetDocument(rootPath);

            

        }

        [Test]
        public void QuestionTable()
        {
            var wordDocRepo = new WordFilesRepository();

            var rootPath = IntegrationTestsHelper.GetFullResourcePath("integration-test-question-table.docx");

            var docs = wordDocRepo.GetDocument(rootPath);

            //var tables = docs.SelectMany(d => d.Tables).ToList();
        }





       
    }

}