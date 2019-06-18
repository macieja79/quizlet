using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Metaproject.Quiz.Domain.Entities
{
    public class QuestionTable
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public List<string> Tags { get; set; }
        public bool IsSwitchable { get; set; }
        public string Answer => string.Join(Environment.NewLine, Answers);

        public override string ToString()
        {
            return $"{Question} | { Answer} | { string.Join(";", Tags)}";
        }
    }
}
