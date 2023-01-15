# C Sharp Sudoku Solver 
## This is a project that contains 2 different algorithms to solve a sudoku puzzle.
 
## This Project is made by @Eldar Aslanbeily (username on GitHub: eldarush)

# Importent note: (not related to the algorithm):
## I accidentally merged all the branches into the main branch using "squash and merge", and they don't show on the site, but if you look under the "adding the working project to the main branch" commit to the "main" branch, you will see all the previous commits in the description that contain the work with brances in the project!
## here are some of the branches that are still saved locally on my computer:
[brances](https://user-images.githubusercontent.com/89807526/212556124-d46d42cb-af26-487a-b6ae-3233d68b6b9d.JPG)


## The main algorithm used to solve is called the "Dancing Links" algorithm, and this is how this algorithm works:

### Dancing Links (DLX) is a technique for adding and deleting a node from a circular doubly linked list. It is particularly useful for efficiently implementing backtracking algorithms, such as Knuth's Algorithm X for the exact cover problem.

### Algorithm X is a recursive, nondeterministic, depth-first, backtracking algorithm that finds all solutions to the exact cover problem.

### Firstly, the algorithm gets a board and converts it to an "Exact Cover" matrix which is a huge matrix filled with ones and zeroes. 

## Here is an example of how the matrix looks for an empty 4 by 4 board:

![WhatsApp Image 2023-01-06 at 17 33 00](https://user-images.githubusercontent.com/89807526/212379488-65528b11-aff0-4f28-9bfc-e9bd7b8ae9c5.jpeg)

### In this example, every 1 represents a possible value and every 0 represents a not valid value for the constraint and the location that they represent.

### In this example, the order of constraints is: Cell, Col, Row, and Value.

### After this, the algorithm transforms this board into a matrix of doubly linked nodes that will be used to find the answer. This matrix will look like this: (The following matrix has no connection to the 4 by 4 board shown above)

![nodesmatrix2](https://user-images.githubusercontent.com/89807526/212380602-727924da-c7a0-46b0-bfb3-101ef2520753.png)

### After this is done, the algorithm will solve the matrix as if it was an exact cover problem, meaning that it will remove and restore columns until it finds the combination of columns for which each row is only appearing once, and that will be the solution of the board. 

## The secondary algorithm used to solve the sudoku puzzle is called a backtracking algorithm, this is how the algorithm works:
## (this is an example with a 9 by 9 board but the same logic is used for a board of any size):

### 1. Start with the top left square of the Sudoku grid, and try filling it with the numbers 1 through 9.

### 2. If the number you have chosen is valid (it does not violate any of the rules of Sudoku, such as being present
### in the same row, column, or 3x3 subgrid), move on to the next square.
### If the number is not valid, backtrack and try a different number for the current square.

### 3. Repeat this process for each square until the Sudoku puzzle is solved.

## Here is a visual representation of how the algorithm works:

![afttou4aho4ok6s4ftt4](https://user-images.githubusercontent.com/89807526/208711758-31dc2480-be6e-496f-8f79-d69eda7d5b0e.gif)

## Note that this implementation of this algorithm is done using bitwise manipulation and will choose the next cell with the lowest amount of possible candidates, s the representation is not a 1-to-1 replica of how the algorithm is running.

# How to run the project:

## The project is written in C# language in the environment 'Visual Studio 2022':
## the file that runs the project is called 'handler' and it includes the 'main' function that 
## executes the project:

![image](https://user-images.githubusercontent.com/89807526/208712610-1a57a310-37e9-448f-8df0-646d016a8d0f.png)

### When the project is executed, the user will get an option to either insert a file path that contains the board string or insert the board string by hand through the console:

![image](https://user-images.githubusercontent.com/89807526/208713098-dcdb11b2-fea8-4321-a3cf-e75e08a8a1ce.png)

### after the board string wan inputted, the program will run a bunch of validation checks to make sure that the board is valid,
### if it is not, the program will display an error message. Otherwise, if the board is valid, the program will ask the user using what algorithm he want to solve the given board with:

![choice](https://user-images.githubusercontent.com/89807526/212382359-bc782b1d-b697-4165-b37a-a6c0e3886455.png)

### that will go over each cell in the board trying to fill it with the correct value using the backtracking algorithm.
### If the function could not find a solution to the board, it will print an error message but if it managed to find at least one 
### solution, it will display it and the solved board string:

![solved](https://user-images.githubusercontent.com/89807526/212382617-62cf7c41-a5e9-41f2-bd4b-426b2705f025.png)

## The program can be run recursively until the user either chooses to exit it via entering a different char to 'c' or 'f' at the board inputting menu or presses Ctrl+c at any given time
