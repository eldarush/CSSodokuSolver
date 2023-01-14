using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SodukoSolver.Interfaces;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace SodukoSolver
{
    /// <summary>
    /// class that reads the soduko board from a file
    /// and save the solution in a file
    /// </summary>
    public class FileReader : IReadable, IWritable
    {
        // the name of the file that will contain the soduko puzzle
        private readonly string _filepath;

        /// <summary>
        /// constructor that will set the _filepath
        /// </summary>
        /// <param name="filepath">fiven file path</param>
        public FileReader(string filepath)
        {
            _filepath = filepath;
        }

        /// <summary>
        /// read Function that will return a Board string
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            try
            {
                // first check if the file exists
                if (!File.Exists(_filepath))
                {
                    throw new FileNotFoundException("file path does not exist, cannot read file\n"+
                        $"entered file path is: {_filepath}\n");
                }
                // read the file and return the Board string
                string text = File.ReadAllText(_filepath);
                return text;
            }
            // catch any exception, print the message and return an empty string
            catch (Exception e)
            {
                // print the error message
                Console.WriteLine(e.Message);
                return "";
            }
        }


        /// <summary>
        /// write function that will write a Board string to the file
        /// </summary>
        /// <param name="boardstring"></param>
        /// <returns>if the writing sucseeced or not</returns>
        public bool Write(string boardstring)
        {
            // get the file name from the file path
            string filename = Path.GetFileName(_filepath);
            // change the name to the original file name + 'SOLVED'
            string solvedfilename = filename.Replace(".txt", "SOLVED.txt");
            // get the directory path from the file path
            string directorypath = Path.GetDirectoryName(_filepath);
            // create the new file path
            string solvedfilepath = Path.Combine(directorypath, solvedfilename);
            try
            {
                if (File.Exists(solvedfilepath))
                {
                    // replace the contents of the file with the Board string
                    File.WriteAllText(solvedfilepath, boardstring);
                }
                else
                {
                    Console.WriteLine("NOTICE - A new file was created during this process");
                    // create new file with this name and write the Board string to it
                    File.WriteAllText(solvedfilepath, boardstring);
                }
            }
            catch (Exception e)
            {
                // print the message and return false
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

    }
}
