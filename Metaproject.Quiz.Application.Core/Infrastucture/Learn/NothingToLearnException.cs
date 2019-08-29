using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metaproject.Quiz.Application.Core.Infrastucture.Learn
{
    public class NothingToLearnException : ApplicationException
    {
        public override string Message => "All questions have been already learned.";
    }
}
