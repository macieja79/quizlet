using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Metaproject.Quiz.Domain.Entities;
using Metaproject.Quiz.Inf.WordRepository.Mappers;

namespace Metaproject.Quiz.Inf.WordRepository
{
    public class TableQuestionMapper : TableMapperBase, ITableMapper
    {
        private readonly IQuestionIdParser _questionIdParser;

        public TableQuestionMapper(IQuestionIdParser questionIdParser)
        {
            _questionIdParser = questionIdParser;
        }

        public bool TryGetQuestionTable(WordprocessingDocument document, Table wordTable,
            out List<QuestionTable> questions)
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

            var imgAsBytes = GetImageAsBytes(document, answerCell);

            var question = GetChildParagraphs(questionCell);
            var answers = GetChildParagraphsForAnswer(answerCell);

            var tags = GetChildParagraphs(tagsCell).Split('|').Select(s => s.ToLower()).ToList();
            var options = GetChildParagraphs(optionsCell).Split('|').Select(s => s.ToLower()).ToList();

            bool isSwitchable = options.Contains("switch");

            var id = GetIdFromOptions(options, _questionIdParser);

            var table = new Domain.Entities.QuestionTable
            {
                Question = question,
                Answers = answers,
                AnswerAsImage = imgAsBytes,
                Tags = tags,
                IsSwitchable = isSwitchable,
                Id = id
            };

            questions.Add(table);
            return true;
        }


        byte[] GetImageAsBytes(WordprocessingDocument document, TableCell par)
        {
            // this code is copy-pasted
            var imageParts = from graphic in par.Descendants<DocumentFormat.OpenXml.Drawing.Graphic>()
                let graphicData = graphic.Descendants<DocumentFormat.OpenXml.Drawing.GraphicData>().FirstOrDefault()
                let pic = graphicData.ElementAt(0)
                let nvPicPrt = pic.ElementAt(0).FirstOrDefault()
                let blip = pic.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().FirstOrDefault()
                select new
                {
                    blip
                };

            if (!imageParts.Any())
            {
                return null;
            }

            var imagePart2 = imageParts?.FirstOrDefault();
            var id = imagePart2?.blip?.Embed?.Value;

            var imagePart = (ImagePart) document.MainDocumentPart.GetPartById(id);
            var stream = imagePart.GetStream();
            
            byte[] imageAsBytes = null;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imageAsBytes = ms.ToArray();
                return imageAsBytes;
            }
        }
    }
}
