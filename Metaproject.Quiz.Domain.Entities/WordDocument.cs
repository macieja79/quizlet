using System;
using System.Collections.Generic;
using System.Text;

namespace Metaproject.Quiz.Domain.Entities
{
    public class WordDocument
    {
        public List<QuestionTable> Tables { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
