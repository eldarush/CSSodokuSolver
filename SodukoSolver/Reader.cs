﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    abstract class Reader
    {
        // an instance of a class that implements the IReadable interface
        protected IReadable reader;

        // constructor
        public Reader(IReadable reader)
        {
            this.reader = reader;
        }

        // read function that will return a board string
        public string Read()
        {
            return reader.Read();
        }
    }
}
