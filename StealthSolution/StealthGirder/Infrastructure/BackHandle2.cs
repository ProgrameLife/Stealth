﻿using StealthQuartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StealthGirder.Infrastructure
{
    public class BackHandle2 : IBackHandle
    {
        public BackHandle2(string name)
        {
            Console.WriteLine($"BackHandle2：{name}");
        }
        public bool Handle(params object[] parmeters)
        {
            Console.WriteLine("BackHandle2");
            return true;
        }
    }
}