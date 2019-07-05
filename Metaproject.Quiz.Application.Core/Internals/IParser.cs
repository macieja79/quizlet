using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metaproject.Quiz.Application.Core.Internals
{
    public interface IParser<T>
    {
        bool TryParse(string str, out T result);

    }
}
