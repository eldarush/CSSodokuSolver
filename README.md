# C# Sudoku Solver 
## This is a project that contains 2 different algorithms to solve a sudoku puzzle.
 
This Project is made by @Eldar Aslanbeily (username on GitHub: eldarush)

### Important note: (not related to the algorithm):
I accidentally merged all the branches into the main branch using "squash and merge",
and they don't show on the site, but if you look under the "adding the working project to the main branch" commit to the "main" branch, you will see all the previous commits in the description that contain the work with branches in the project!
here are some of the branches that are still saved locally on my computer:


![brances](https://user-images.githubusercontent.com/89807526/212556124-d46d42cb-af26-487a-b6ae-3233d68b6b9d.JPG)


### The main algorithm used to solve is called the "Dancing Links" algorithm, and this is how this algorithm works:

This is an implementation of this algorithm based on ["Solving Sudoku efficiently with Dancing Links"](https://www.kth.se/social/files/58861771f276547fe1dbf8d1/HLaestanderMHarrysson_dkand14.pdf),  a paper done on this algorithm by Mattias Harryson and
Hjalmar Laestander, that insipred me to search for the most efficiant algorithm to solve this [NP-Complete problem](https://en.wikipedia.org/wiki/NP-completeness).

Dancing Links (DLX) is a technique for adding and deleting a node from a circular doubly linked list. It is particularly useful for efficiently implementing backtracking algorithms, such as Knuth's Algorithm X for the exact cover problem.

Algorithm X is a recursive, nondeterministic, depth-first, backtracking algorithm that finds all solutions to the exact cover problem.

Firstly, the algorithm gets a board and converts it to an "Exact Cover" matrix which is a huge matrix filled with ones and zeroes. 

Here is an example of how the matrix looks for an empty 4 by 4 board:

![WhatsApp Image 2023-01-06 at 17 33 00](https://user-images.githubusercontent.com/89807526/212379488-65528b11-aff0-4f28-9bfc-e9bd7b8ae9c5.jpeg)

In this example, every 1 represents a possible value and every 0 represents a not valid value for the constraint and the location that they represent.

In this example, the order of constraints is: Cell, Col, Row, and Value.

After this, the algorithm transforms this board into a matrix of doubly linked nodes that will be used to find the answer. This matrix will look like this: (The following matrix has no connection to the 4 by 4 board shown above)

![mat2](https://user-images.githubusercontent.com/89807526/212732537-aba3c1b2-5027-4789-aad7-22b064294882.png)

After this is done, the algorithm will solve the matrix as if it was an exact cover problem, meaning that it will remove and restore columns until it finds the combination of columns for which each row is only appearing once, and that will be the solution of the board. 

## The secondary algorithm used to solve the sudoku puzzle is called a backtracking algorithm, this is how the algorithm works:
### (this is an example with a 9 by 9 board but the same logic is used for a board of any size):

 1. Start with the top left square of the Sudoku grid, and try filling it with the numbers 1 through 9.

 2. If the number you have chosen is valid (it does not violate any of the rules of Sudoku, such as being present  in the same row, column, or 3x3 subgrid), move on to the next square. If the number is not valid, backtrack and try a different number for the current square.

 3. Repeat this process for each square until the Sudoku puzzle is solved.

## Here is a visual representation of how the algorithm works:

![example](https://user-images.githubusercontent.com/89807526/212731405-f0258c5c-f449-46b2-9cb1-e2c946e9c468.gif)

## Note that this implementation of this algorithm is done using bitwise manipulation and will choose the next cell with the lowest amount of possible candidates, s the representation is not a 1-to-1 replica of how the algorithm is running.

# Requirements

The project is written in C# language in the environment 'Visual Studio 2022' And required a 
.Net 6.0 or newer to run

# How to run the project:


The file that runs the project is called 'handler' and it includes the 'main' function that executes the project:

```
 public class Handler
    {
        /// <summary>
        /// defined entry point for the project
        /// </summary>
        static void Main()
        {
            // start the program that gets soduko Board from the user 
            // and runs the algorithm
            UserInterface.Run();
        }
    }
```

The input of the board is done by entering a string of characters that represents the board,
Where ascii the chars represent the ascii character of the number they are representing,
where a string like this:

```
0000000000000000
```

represents a board like this:

```
0 0 0 0
0 0 0 0
0 0 0 0
0 0 0 0
```

If the board has a solution, the solution will be returned both as a visual matrix,
and in a string format (same as input).

## Input


When the project is executed, the user will get an option to either insert a file path that contains the board string or insert the board string by hand through the console:

```
Welcome to the Sudoku Solver!
This is a program written in c# by @Eldar Aslanbeily
For more information about the program, please visit:
https://github.com/eldarush/CSSodokuSolver.git
This is a program that will solve any sudoku Board
The compatible sizes are (1x1 4x4 9x9 16x16 25x25)


Please choose the form of input for the sudoku:
         C: Use the console to input the board string by hand
         F: Input a file that will contain the board string
         Press any other key to exit the program

Please enter your choice:
```

After the board string wan inputted, the program will run a bunch of validation checks to make sure that the board is valid, 
If it is not, the program will display an error message. Otherwise, if the board is valid, the program will ask the user using what algorithm he want to solve the given board with:



For example, an error message for an invalid length will look like this:

```
ERROR: The Board string cannot be used to create a Board with the given dimensions,
The current Board string's Size is 39 and the square root of 39 is 6.244997998398398,
and for a board to be valid, the square root of the total number of chars has to be a square number.
```

If there were no problems with the validation, the program will for the 
solving method of choice:

```
Please choose the way you want to solve the Board:
         D: using the Dancing Links algorithm (Highly Recommended)
         B: using the Backtracking algorithm

         Dancing Links works better for bigger and more complicated Sudoku Board but takes up more memory.
         Backtracking works better with simpler and smaller boards and takes up less memory,
         But may struggle with more complicated boards.

Please enter your choice:
```

That will go over each cell in the board trying to fill it with the correct value using the chosen algorithm.
If the function could not find a solution to the board, it will print an error message but if it managed to find at least one 
solution, it will display it and the solved board string:

![solved](https://user-images.githubusercontent.com/89807526/212382617-62cf7c41-a5e9-41f2-bd4b-426b2705f025.png)

For an unsolvable board, the next error message will be printed:

```
Board Cannot Be Solved :( please try again with a different Board
```

The program can be run recursively until the user either chooses to exit it via entering a different char to 'c' or 'f' at the board inputting menu or presses Ctrl+c at any given time.
