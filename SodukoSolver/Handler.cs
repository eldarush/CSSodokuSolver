using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public class Handler
    {
        static void Main(string[] args)
        {
            // start the program that gets soduko board from the user 
            // and runs the algorithm
            UserInterface.Run();



            //    int[,] puzzle = new int[,]
            //    {
            //        { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
            //        { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
            //        { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
            //        { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
            //        { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
            //        { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
            //        { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
            //        { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
            //        { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
            //    };

            //    List<List<int>> sudoku = Solver.PencilInNumbers(puzzle);

            //    PrintSudoku(sudoku);
            //}

            //// Adding candidates instead of zeros
            //public static class Solver
            //{
            //    public static List<List<int>> PencilInNumbers(int[,] puzzle)
            //    {
            //        var sudoku = new List<List<int>>();
            //        for (int i = 0; i < 9; i++)
            //        {
            //            sudoku.Add(new List<int>());
            //            for (int j = 0; j < 9; j++)
            //            {
            //                if (puzzle[i, j] != 0)
            //                {
            //                    sudoku[i].Add(puzzle[i, j]);
            //                }
            //                else
            //                {
            //                    for (int k = 1; k <= 9; k++)
            //                    {
            //                        sudoku[i].Add(k);
            //                    }
            //                }
            //            }
            //        }
            //        return sudoku;
            //    }
            //}


            //// Prints a Sudoku puzzle with the values of cells that have only one candidate
            //// and 'c' for cells with multiple candidates.
            //void PrintSudoku(List<List<List<int>>> sudoku)
            //{
            //    for (int i = 0; i < 9; i++)
            //    {
            //        if (i % 3 == 0)
            //        {
            //            Console.WriteLine("+-------+-------+-------+");
            //        }

            //        for (int j = 0; j < 9; j++)
            //        {
            //            if (j % 3 == 0)
            //            {
            //                Console.Write("| ");
            //            }

            //            // Print the value of the cell if it has only one candidate,
            //            // or 'c' if it has multiple candidates.
            //            if (sudoku[i][j][0].Count == 1)
            //            {
            //                Console.Write(sudoku[i][j][0][0] + " ");
            //            }
            //            else
            //            {
            //                Console.Write("c ");
            //            }
            //        }

            //        Console.WriteLine("|");
            //    }

            //    Console.WriteLine("+-------+-------+-------+");
            //}


            //Cell cell = new Cell(9);
            //Console.WriteLine(string.Join(", ", cell.Candidates));
            //// check if 1 is a candidate
            //for (int i = 1; i < 9; i++)
            //{
            //    cell.Candidates.Remove(i);
            //}
            //Console.WriteLine(cell.isSolved());



            // STEPS TO SOLVE SUDOKU:
            // 1. change the 2d array of ints to a 2d array of cells, each cell has a list of candidates
            // 2. make the current bruteforce algorithm work with the new data structure
            // 3. implement 'simple elimination' algorithm before all previous algoritms are run
            // 4. implement 'hidden single' algorithm before all previous algoritms are run
            // 5. implement 'naked pair/triples/quads' algorithm before all previous algoritms are run
            // 6. implement 'pointing pairs/triples' algorithm before all previous algoritms are run  -------------> optional
            // 7, implement 'intersection' algorithm before all previous algoritms are run
            // 8. implement 'X-wing' algorithm before all previous algoritms are run
            // 9. implement 'X-cycles' algorithm before all previous algoritms are run -----------------> optional if i have time to do it
        }
    }
}
