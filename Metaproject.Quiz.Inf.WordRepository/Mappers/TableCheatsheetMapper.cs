using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Metaproject.Quiz.Domain.Entities;
using Metaproject.Quiz.Inf.WordRepository.Mappers;

namespace Metaproject.Quiz.Inf.WordRepository
{
    public class TableCheatsheetMapper : TableMapperBase, ITableMapper
    {
        private readonly IQuestionIdParser _questionIdParser;

        public TableCheatsheetMapper(IQuestionIdParser questionIdParser)
        {
            _questionIdParser = questionIdParser;
        }

        public bool TryGetQuestionTable(Table wordTable, out List<QuestionTable> questions)
        {
            questions = new List<QuestionTable>();

            var rows = wordTable.ChildElements.OfType<TableRow>().ToList();

            if (rows.Count < 2) return false;

            var innerText = rows[0].InnerText;
            if (string.IsNullOrEmpty(innerText)) return false;

            int questionCharUnicode = innerText[0];
            if (questionCharUnicode != 10000) return false;
            
            var questionRows = rows.Skip(1).Take(rows.Count - 2);
            var tagsRow = rows.Last();


            var tagRowsCells = tagsRow.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.TableCell>().ToList();
            var tagsCell = tagRowsCells[0];
            var optionsCell = tagRowsCells[1];
            var tags = GetChildParagraphs(tagsCell).Split('|').Select(s => s.ToLower()).ToList();
            var options = GetChildParagraphs(optionsCell).Split('|').Select(s => s.ToLower()).ToList();
            bool isSwitchable = options.Contains("switch");

            bool hasIds = tagRowsCells.Count >= 3;


            foreach (var questionRow in questionRows)
            {
                var questionRowCells = questionRow.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.TableCell>().ToList();


                var questionCell = questionRowCells[0];
                var answerCell = questionRowCells[1];
                
                var question = GetChildParagraphs(questionCell);
                var answers = GetChildParagraphsForAnswer(answerCell);

                var table = new QuestionTable
                {
                    Question = question,
                    Answers = answers,
                    Tags = tags,
                    IsSwitchable = isSwitchable
                };

                if (hasIds)
                {
                    var idCell = questionRowCells[2];
                    var id = GetChildParagraphs(idCell);
                    if (_questionIdParser.TryParse(id, out DateTime result))
                    {
                        table.Id = result;
                    }
                }

                questions.Add(table);
            }

            return true;
            
        }
        
    }
}
