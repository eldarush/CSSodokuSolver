# C Sharp Sudoku Solver
## This is a project that contains a algoritm to solve a soduku puzzle 
## This Project is made by @Eldar Aslanbeily (username on GitHub: eldarush)

## The algoritm used to solve the sudoku puzzle is called a backtracking algoritm, this is how the algoritm works:
## (this is an example with a 9 by 9 board but the same logic is used for a board of ant size):

### 1. Start with the top left square of the Sudoku grid, and try filling it with the numbers 1 through 9.

### 2. If the number you have chosen is valid (it does not violate any of the rules of Sudoku, such as being present
### in the same row, column, or 3x3 subgrid), move on to the next square.
### If the number is not valid, backtrack and try a different number for the current square.

### 3. Repeat this process for each square until the Sudoku puzzle is solved.

## Here is a visual representation fo how the algoritm wroks:

![afttou4aho4ok6s4ftt4](https://user-images.githubusercontent.com/89807526/208711758-31dc2480-be6e-496f-8f79-d69eda7d5b0e.gif)

# How to run the project:

## The project is written in c# language in the environment 'visual studio 2022':
## the file that runs the project is called 'handler' and it includes the 'main' function that 
## executes the project:

![image](https://user-images.githubusercontent.com/89807526/208712610-1a57a310-37e9-448f-8df0-646d016a8d0f.png)

### When the projected is exceuted, the user will get an option to either use a file stored to locally import a 
### sudoku board, or to input the board string himself:

![image](https://user-images.githubusercontent.com/89807526/208713098-dcdb11b2-fea8-4321-a3cf-e75e08a8a1ce.png)

### after the user inputted a soduko board, the program will run a bunch of validation checks to make sure that the board is valid,
### if it is not, the program will display an error message. Otherwise, if the board is valid, the program will run the "Solve" function
### that will go over each cell in the board trying to fill it with the correct value using the backtracking algorithm.
### If the function could not find a solution to the board, it will print an error message but if it managed to find at least one 
### solution, it will display it as following:

![image](https://user-images.githubusercontent.com/89807526/208713938-4a42f389-bb48-4500-8b04-5ef5fdbba5a7.png)

### Also the program will display how long it took the algoritm to come to a result on the given board (solved/unsolved)
### in ms and in seconds:

![image](https://user-images.githubusercontent.com/89807526/208714130-4581206f-3f04-4e7e-be2c-e1e90993bcd8.png)

