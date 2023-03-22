# Gold Rush - Maze Treasure Hunt Solver

![image](https://user-images.githubusercontent.com/91037907/226859463-2dc0dd67-240d-4994-ac7a-b9604ec155b6.png)

## Overview

This project is a C# .NET project that implements BFS and DFS algorithm to solve Maze Treasure Hunt problem. 

### Problem Description

Mr. Krabs discovered a distortion labyrinth located directly under the Krusty Krab called El Doremi, which he believes contains a number of treasures inside, and of course he wants to take the treasure. Due to the labyrinth's potential for distortion, Mr. Krabs must constantly measure the size of the labyrinth. Therefore, Mr. Krabs spends a lot of energy doing this, so he needs to figure out how he can navigate this labyrinth and obtain all the treasures easily.
Help Mr.Krab to find the path so that he can get all the treasure(s). 
Mr.Krab can move RIGHT, DOWN, LEFT, UP one block at a time.

![image](https://user-images.githubusercontent.com/91037907/226874542-a2d3001b-2516-4140-a9c8-274a7f636b88.png)

The implemented BFS and DFS algorithm have RIGHT -> DOWN -> LEFT -> UP movement priority in solving the problem. 

This project is built to meet the following [guidelines](https://docs.google.com/document/d/1lAUaI6PsZK089rcWfaTYRgMwNwe3wvDbGdIdJrLatEU/edit).

## Prerequisites

- `.NET CORE SDK Version 6.0` installed
- Operating System `Windows 7.0` or up

## How To Build
1. Clone this repository.

```
 $ git clone https://github.com/Mehmed13/Tubes2_goldRush
```

2. Open terminal and navigate to the src directory of this repository.

```
 $ cd src
```

3. Run `dotnet build -o ../bin` in terminal. 

```
 $ dotnet build -o ../bin
```

4. The resulting executable file will be located at the `bin` directory.

## How To Run

1. After [building the program](#how-to-building), navigate to the bin directory in terminal.


```
 $ cd bin
```

2. Enter the following command to run the program.

```
 $ dotnet run
```

or

```
 $ ./GoldRush.exe
```

3. To load the maze configurations, create `.txt` files in `/test/` and choose the file in the program. Configuration file syntax can be referred from the txt files in `/test/`.
The configuration should have:
- Exactly one "K" which represents the starting point.
- At least one "T" which represents the treasure.
- At least one "R" which represents the road.
The "X" represents the dirt(forbidden path). The treasures must be accessible from the road.
```
X T X X
X R R T
K R X T
X R X R
X R R R
```


4. Choose the algorithm that will be used to solve the problem (BFS/DFS). You can also enable TSP to show the steps needed for Mr. Krabs to return to Krusty Krab. Then, use the animation time slider to choose the interval between the steps shown.

5. Click the arrow button in the bottom to visualize the maze.
![image](https://user-images.githubusercontent.com/91037907/226874641-c697b472-8c3e-4c93-b50c-fa403c71c7bf.png)

6. Click `Show Solution` toggle once to show the solution. Click it again to hide the solution if you want to. 
![image](https://user-images.githubusercontent.com/91037907/226874714-3f271476-c748-4d78-8ecb-f6ef56541cae.png)

7. Click `Show Steps` to animate the steps taken by the algorithm to solve the problem. The yellow grid in the maze represents the checked nodes.
![image](https://user-images.githubusercontent.com/91037907/226874756-b7b02cfd-f7f6-4ba2-8eae-ec53f9c0048d.png)


## Authors

| Name                  | GitHub                                            | NIM                  |
| --------------------- | ------------------------------------------------- | --------------------- |
| Rachel Gabriela Chen  | [chaerla](https://github.com/chaerla)             | 13521044 |
| Melvin Kent Jonathan  | [melvinkj](https://github.com/melvinkj)           | 13521052 |
| Muhammad Fadhil Amri  | [Mehmed13](https://github.com/mehmed13)           | 13521066 |
