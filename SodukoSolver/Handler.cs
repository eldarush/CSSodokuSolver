using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static SodukoSolver.HelperFunctions;
using static SodukoSolver.SolvingFunctions;
using static SodukoSolver.ValidatingFunctions;

namespace SodukoSolver
{
    public class Handler
    {
        static void Main(string[] args)
        {
            // start the program that gets soduko board from the user 
            // and runs the algorithm
            UserInterface.Run();

            //char test = (char)(11+ '0');
            //Console.WriteLine(test);

            // create a new file with given file path
            //string filepath = "C:\\Users\\eldar\\source\\repos\\eldarush\\CSSodokuSolver\\SodukoSolver\\testboard.txt\r\n";
            ////try
            ////{
            ////    // create new file with the given file path
            ////    using (StreamWriter sw = File.CreateText(filepath))
            ////    {
            ////        // write the board string to the file
            ////        sw.WriteLine("000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    // print the error message
            ////    Console.WriteLine(ex.Message);
            ////}

            //string filedirectory = Path.GetDirectoryName(filepath);
            //string filename = Path.GetFileName(filepath);
            //Console.WriteLine(filename);
            //Console.WriteLine(filedirectory);
            ////// change the name to the original file name + 'SOLVED'
            //string solvedfilename = filename.Replace(".txt", "SOLVED.txt");

            ////// check if a path in the given directory with the solved file name already exists
            //string solvedfilepath = Path.Combine(filedirectory, solvedfilename);
            //Console.WriteLine(solvedfilepath);
            //if (File.Exists(solvedfilepath)){
            //    Console.WriteLine("File already exists");
            //}
            //else
            //{
            //    // print the directory path and the file name
            //    Console.WriteLine(solvedfilepath);
            //    Console.WriteLine("file doesnt exist");
            //}
            //Console.WriteLine(solvedfilename);


            //int size = 9;
            //Node[][] mat = ConvertStringBoard("800000070006010053040600000000080400003000700020005038000000800004050061900002000", size);
            //PrintBoardNode(mat, size);

            // print the right value and left value for each node
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        Console.WriteLine($"Node: {mat[i][j].Value} Left: {mat[i][j].Left.Value} Right: {mat[i][j].Right.Value}");


            //    }
            //}
            //SolvingFunctions solver = new SolvingFunctions(size, ConvertNodeBoardToCellBoard(mat, size));
            //solver.DancingLinks(solver.board);
            //PrintBoard(solver.board, solver.size);
            //Console.ReadLine();
        }
    }
}
