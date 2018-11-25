using StealthBackHandle;
using StealthQuartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthGirder.Infrastructure
{
    public class BackHandle1 : IBackHandle
    {
        public BackHandle1(string name)
        {
            Console.WriteLine($"BackHandle1：{name}");
        }
        public bool Handle()
        {
            throw new NotImplementedException();
        }
        public bool Handle(string content, Encoding encoding, params object[] parmeters)
        {
            Console.WriteLine("BackHandle1");
            return true;
        }
    }
}
