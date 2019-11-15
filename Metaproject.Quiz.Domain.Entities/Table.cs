using System;
using System.Collections.Generic;

namespace Metaproject.Quiz.Domain.Entities
{
    public class QuestionTable
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public List<string> Tags { get; set; }
        public bool IsSwitchable { get; set; }
        public string Answer => string.Join(Environment.NewLine, Answers);

        public AnswerTypeEnum AnswerType => IsImage ? AnswerTypeEnum.Image : AnswerTypeEnum.Text;

        public byte[] AnswerAsImage { get; set; }
        public bool IsImage => AnswerAsImage != null && AnswerAsImage.Length > 0;

        public DateTime? Id { get; set; }

        public override string ToString()
        {
            return $"{Question} | { Answer} | { string.Join(";", Tags)}";
        }
    }
}
