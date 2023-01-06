﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SodukoSolver.Interfaces;
namespace SodukoSolver
{
    public class FileReader : IReadable, IWritable
    {
        // the name of the file that will contain the soduko puzzle
        private readonly string filepath;

        /// <summary>
        /// constructor that will set the filepath
        /// </summary>
        /// <param name="filepath">fiven file path</param>
        public FileReader(string filepath)
        {
            this.filepath = filepath;
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
                if (!File.Exists(filepath))
                {
                    throw new FileNotFoundException("file path does not exist, cannot read file\n"+
                        $"entered file path is: {filepath}\n");
                }
                // read the file and return the Board string
                string text = File.ReadAllText(filepath);
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


        /// <summary>
        /// write function that will write a Board string to the file
        /// </summary>
        /// <param name="boardstring"></param>
        /// <returns>if the writing sucseeced or not</returns>
        public bool Write(string boardstring)
        {
            // get the file name from the file path
            string filename = Path.GetFileName(filepath);
            // change the name to the original file name + 'SOLVED'
            string solvedfilename = filename.Replace(".txt", "SOLVED.txt");
            // get the directory path from the file path
            string directorypath = Path.GetDirectoryName(filepath);
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
                // print the message
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

    }
}
