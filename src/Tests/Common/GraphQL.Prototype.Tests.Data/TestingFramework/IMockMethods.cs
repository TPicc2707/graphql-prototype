using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Prototype.Tests.Data.TestingFramework
{
    public interface IMockMethods
    {
        TValue GetObject<TValue>();
    }
}
