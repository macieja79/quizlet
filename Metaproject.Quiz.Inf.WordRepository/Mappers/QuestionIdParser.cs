using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metaproject.Quiz.Inf.WordRepository.Mappers
{
    public class QuestionIdParser : IQuestionIdParser
    {
        private string regex =
            @"^20(?<year>\d\d)-(?<month>\d\d)-(?<day>\d\d) (?<hour>\d\d):(?<minute>\d\d):(?<second>\d\d$)";

        public bool TryParse(string str, out DateTime result)
        {
            return DateTime.TryParse(str, out result);
        }
    }
}
