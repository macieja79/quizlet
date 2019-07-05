using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;

namespace Metaproject.Quiz.Inf.WordRepository.Mappers
{
    public abstract class TableMapperBase
    {

        protected string GetChildParagraphs(OpenXmlCompositeElement element)
        {
            return string.Join(Environment.NewLine, element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.Paragraph>().Select(p => p.InnerText));
        }

        protected List<string> GetChildParagraphsForAnswer(OpenXmlCompositeElement element)
        {
            return element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.Paragraph>().Select(p => p.InnerText).ToList();
        }

        protected DateTime? GetIdFromOptions(List<string> options, IQuestionIdParser parser)
        {
            foreach (var option in options)
            {
                if (parser.TryParse(option, out DateTime result))
                {
                    return result;
                }
            }

            return null;
        }
    }
}
