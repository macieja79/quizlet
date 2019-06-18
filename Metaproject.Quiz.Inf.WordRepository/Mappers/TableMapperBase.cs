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
    }
}
