using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Metaproject.Quiz.Domain.Entities;
using Metaproject.Quiz.Inf.WordRepository.Mappers;

namespace Metaproject.Quiz.Inf.WordRepository
{
    public class TableQuestionMapper : TableMapperBase, ITableMapper
    {
        public bool TryGetQuestionTable(Table wordTable, out List<QuestionTable> questions)
        {
            questions = new List<QuestionTable>();

            var rows = wordTable.ChildElements.OfType<TableRow>().ToList();

            if (rows.Count < 2) return false;

            var innerText = rows[0].InnerText;
            if (string.IsNullOrEmpty(innerText)) return false;

            int questionCharUnicode = innerText[0];
            if (questionCharUnicode != 10068) return false;

            var questionRowIndex = 0;
            var tagRowIndex = 1;

            var questionRow = rows[questionRowIndex];
            var tagsRow = rows[tagRowIndex];

            var questionRowCells = questionRow.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.TableCell>().ToList();
            var tagRowsCells = tagsRow.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.TableCell>().ToList();

            var questionCell = questionRowCells[1];
            var answerCell = questionRowCells[2];
            var tagsCell = tagRowsCells[1];
            var optionsCell = tagRowsCells[2];

            var question = GetChildParagraphs(questionCell);
            var answers = GetChildParagraphsForAnswer(answerCell);

            var tags = GetChildParagraphs(tagsCell).Split('|').Select(s => s.ToLower()).ToList();
            var options = GetChildParagraphs(optionsCell).Split('|').Select(s => s.ToLower()).ToList();

            bool isSwitchable = options.Contains("switch");

            var table = new Domain.Entities.QuestionTable
            {
                Question = question,
                Answers = answers,
                Tags = tags,
                IsSwitchable = isSwitchable
            };

            questions.Add(table);

            return true;
        }

    }
}
