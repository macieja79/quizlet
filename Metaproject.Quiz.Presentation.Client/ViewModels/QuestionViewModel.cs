using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Metaproject.Quiz.Domain.Entities;

namespace Metaproject.Quiz.Presentation.Client.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionTable Table { get; }

        public QuestionViewModel(QuestionTable table)
        {
            Table = table;
        }

        public string Question => Table.Question;

        public string Answer => Table.Answer;

        public string Id => Table.Id?.ToString(CultureInfo.InvariantCulture) ?? "";

        public string Tag1 => Table.Tags.Count > 0 ? Table.Tags[0] : "";

        public string Tag2 => Table.Tags.Count > 1 ? Table.Tags[1] : "";

        public string Tag3 => Table.Tags.Count > 2 ? Table.Tags[2] : "";

        public string Tag4 => Table.Tags.Count > 3 ? Table.Tags[3] : "";
    }
}
