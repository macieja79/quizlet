﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Domain.Entities;
using Metaproject.Quiz.Inf.WordRepository;
using Metaproject.Quiz.Inf.WordRepository.Mappers;
using Path = System.IO.Path;
using Word = DocumentFormat.OpenXml.Wordprocessing;


namespace Metaproject.Quiz.Inf.WordDocsRepository
{
    public class WordFilesRepository : IWordFilesRepository
    {

        TableCheatsheetMapper _cheatsheetMapper = new TableCheatsheetMapper(new QuestionIdParser());
        TableQuestionMapper _questionMapper = new TableQuestionMapper(new QuestionIdParser());


        public List<WordDocument> GetAllDocuments(string rootPath)
        {
            var pattern = "*.docx";

            var docPaths = Directory.GetFiles(rootPath, pattern, SearchOption.TopDirectoryOnly);

            List<WordDocument> documents = new List<WordDocument>();

            foreach (var docPath in docPaths)
            {
                var fileName = Path.GetFileName(docPath);
                if (fileName.StartsWith("~")) continue;

                var doc = GetDocument(docPath);
                if (null == doc) continue;
                documents.Add(doc);
            }

            return documents;
        }

        public WordDocument GetDocument(string path)
        {

            try
            {
                using (WordprocessingDocument wordDocument =
                    WordprocessingDocument.Open(path, false))
                {

                    var body = wordDocument.MainDocumentPart.Document.Body;

                    var tables = body.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.Table>().ToList();


                    var quizTables = new List<QuestionTable>();
                    foreach (var table in tables)
                    {
                        if (TryGetQuestionTable(wordDocument, table, out List<QuestionTable> domainTables))
                        {
                            quizTables.AddRange(domainTables);
                        }
                    }

                    var doc = new WordDocument
                    {
                        Tables = quizTables,
                        Path = path,
                        Name = Path.GetFileName(path)
                    };

                    return doc;
                }
            }
            catch
            {
                return null;
            }
        }


        bool TryGetQuestionTable(WordprocessingDocument wordDocument, Word.Table wordTable, out List<QuestionTable> domainTables)
        {

            if (_questionMapper.TryGetQuestionTable(wordDocument, wordTable, out domainTables))
                return true;

            return _cheatsheetMapper.TryGetQuestionTable(wordDocument, wordTable, out domainTables);


        }

        string GetChildParagraphs(OpenXmlCompositeElement element)
        {
            return string.Join(Environment.NewLine, element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.Paragraph>().Select(p => p.InnerText));
        }

        List<string> GetChildParagraphsForAnswer(OpenXmlCompositeElement element)
        {
            return element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.Paragraph>().Select(p => p.InnerText).ToList();
        }







    }
}
