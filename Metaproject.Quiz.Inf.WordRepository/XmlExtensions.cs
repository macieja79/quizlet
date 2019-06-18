using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;

namespace Metaproject.Quiz.Inf.WordDocsRepository
{
    public static class XmlExtensions
    {

        public static List<OpenXmlElement> GetAllElements(this OpenXmlElement element)
        {
            var result = new List<OpenXmlElement>();
            foreach (var elem in element.Elements())
            {
                result.Add(elem);
                var children = elem.GetAllElements();
                result.AddRange(children);
            }

            return result;
        }

        public static List<T> ChildrenOfType<T>(this OpenXmlElement element) where T: OpenXmlElement
        {

            return element.ChildElements.OfType<T>().ToList();
        }

    }
}
