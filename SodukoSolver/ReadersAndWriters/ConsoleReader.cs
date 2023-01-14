using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolver.Interfaces;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace SodukoSolver
{
    /// <summary>
    /// class that will be respoinsible to read the soduko board from the user
    /// and write back the solution in string format
    /// </summary>
    public class ConsoleReader : IReadable, IWritable
    {
        /// <summary>
        /// read function that will read the input from the console and will return it
        /// </summary>
        /// <returns>the input in string format</returns>
        public string Read()
        {
            try
            {
                // ask the user for the Board string
                Console.WriteLine("Please enter the Board string:");
                
                // read the Board string from the console
                string text = Console.ReadLine();
                // keep just the characters that represent the values
                text = text.Replace(" ", "");
                text = text.Replace("\t", "");
                text = text.Replace("\n", "");
                text = text.Replace("\r", "");
                return text;
            }
            // catch any exception that might happen and print it, returning empty string
            catch (Exception e)
            {
                // print the error message
                Console.WriteLine(e.Message);
                return "";
            }
        }

        /// <summary>
        /// write function that will write a Board string into the console
        /// </summary>
        /// <param name="boardstring"></param>
        /// <returns></returns>
        public bool Write(string boardstring)
        {
            Console.WriteLine(boardstring);
            return true;
        }
    }
}
