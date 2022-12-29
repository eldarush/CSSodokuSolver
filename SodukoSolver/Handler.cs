using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
