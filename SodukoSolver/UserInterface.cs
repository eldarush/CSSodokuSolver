using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    // main class that will run the program
    class UserInterface
    {
        // create a soduko board object
        static SodokuBoard board;
        
        // function that will run the program
        public static void Run()
        {
            // print welcome message
            PrintWelcomeMessage();
        }

        // function that will print the welcome message
        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Soduko Solver! \n" +
                "This is a program that will solve any soduko board \n" +
                "Please choose the form of input for the soduko: \n" +
                "\t s: input a board string that will represent a board \n" +
                "\t f: input a file that will conntain the board \n" +
                "\t press any other key to exit the program");

            // get the char from the input
            char input = Console.ReadLine()[0];
            switch (input)
            {
                case 's':
                    // create a soduko board from the string
                    board = new SodokuBoard();
                    break;
                case 'f':
                    // implement getting the board from a file
                    //board = new SodokuBoardF();
                    break;
                default:
                    Console.WriteLine("Exiting the program");
                    break;
            }
        }
    }
}
