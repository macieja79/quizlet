using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Metaproject.Quiz.Test.IntegrationTests.Helpers
{
    public class IntegrationTestsHelper
    {

        public static string GetFullResourcePath(string fileName)
        {
            var fullPath = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(fullPath);
            var resourcePath = Path.Combine(path, "Resources", fileName);
            return resourcePath;

        }
    }
}
