using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Application.Core
{
    public interface IWordFilesRepository
    {
        List<WordDocument> GetAllDocuments(string docPath);
        WordDocument GetDocument(string path);
    }
}
