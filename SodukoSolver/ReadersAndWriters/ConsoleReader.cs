﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolver.Interfaces;
# pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
# pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace SodukoSolver
{
    public class ConsoleReader : IReadable, IWritable
    {
        // read Function that will return a Board string
        public string Read()
        {
            try
            {
                // read the Board string from the console
                string text = Console.ReadLine();
                // keep just the characters that represent the values
                text = text.Replace(" ", "");
                text = text.Replace("\t", "");
                text = text.Replace("\n", "");
                text = text.Replace("\r", "");
                return text;
            }
            catch (Exception e)
            {
                // print the error message
                Console.WriteLine(e.Message);
                return "";
            }
        }

        // write function that will write a Board string
        public bool Write(string boardstring)
        {
            Console.WriteLine(boardstring);
            return true;
        }
    }
}
