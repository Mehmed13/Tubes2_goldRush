using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;
using System.Windows.Threading;
using lib;
using System.Collections;
using System.Globalization;
using System.Data.Common;

namespace GoldRush
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private int[] startIdx;
        private char[,] maze;
        private string filename;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void handleChooseFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".txt"; // set default file extension
            dialog.Filter = "Text files (*.txt)|*.txt"; // set file filter
            Nullable<bool> result = dialog.ShowDialog(); // open file dialog
            if (result == true) // if user selected a file
            {
                string filename = dialog.FileName; // get file name
                                                   // set text block text to file name
                selectedFileLabel.Text = filename;
                this.filename = filename;
                try
                {
                    this.maze = Utility.readFromFile(filename);
                }
                catch (Exception exception)
                {
                    selectedFileLabel.Text = "Invalid txt file!"; // validation fails
                }
                fillMatrix(); // fill to dataGrid
            }
            else
            {
                selectedFileLabel.Text = "No file chosen";
            }
        }
        private void handleVisualize(object sender, RoutedEventArgs e)
        {
            if (selectedFileLabel.Text.Equals("No file chosen") || selectedFileLabel.Text.Equals("Invalid txt file!"))
            {
                // Open error pop up
                errorPopup.IsOpen = true;
                popUpText.Text = selectedFileLabel.Text.ToString();
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += (s, args) =>
                {
                    errorPopup.IsOpen = false;
                    timer.Stop();
                };
                timer.Start();
            }
            else
            {
                // Hide the input grid
                inputGrid.Visibility = Visibility.Collapsed;

                // Show the visualization grid
                visualizeGrid.Visibility = Visibility.Visible;
            }
        }

        private void showInput(object sender, RoutedEventArgs e)
        {
            // Show the input grid
            inputGrid.Visibility = Visibility.Visible;

            // Show the visualization grid
            visualizeGrid.Visibility = Visibility.Collapsed;

            // reset the matrix
            fillMatrix();

            // hide solution
            solutionPanel.Visibility = Visibility.Collapsed;
        }

        // function to fill the matrix DataGrid
        private void fillMatrix()
        {
            try
            {
                int numOfNodes = 0;
                DataTable dt = new DataTable();
                int baseWidth;
                if (this.maze.GetLength(1) > this.maze.GetLength(0)) // pick the larger between row size and col size as baseWidth
                {
                    baseWidth = this.maze.GetLength(1);
                }
                else
                {
                    baseWidth = this.maze.GetLength(0);
                }
                double cellWidth = (300.0 / baseWidth);
                mazeGrid.Width = this.maze.GetLength(1) * cellWidth;
                // Get the existing Style resource
                Style cellStyle = (Style)FindResource("MyCellStyle");

                // Create a new Style object based on the existing Style resource
                Style newCellStyle = new Style(typeof(DataGridCell));
                newCellStyle.BasedOn = cellStyle;

                // Modify the new Style object
                newCellStyle.Setters.Add(new Setter(DataGridCell.WidthProperty, cellWidth));
                newCellStyle.Setters.Add(new Setter(DataGridCell.HeightProperty, cellWidth));
                newCellStyle.Setters.Add(new Setter(DataGridCell.MarginProperty, new Thickness(0)));

                // Set the modified Style object to the DataGrid's CellStyle property
                mazeGrid.CellStyle = newCellStyle;
                for (int i = 0; i < this.maze.GetLength(1); i++)
                {
                    dt.Columns.Add(new DataColumn(i.ToString(), typeof(int)));
                }
                for (int i = 0; i < this.maze.GetLength(0); i++)
                {
                    DataRow row = dt.NewRow();
                    for (int j = 0; j < this.maze.GetLength(1); j++)
                    {
                        // if starting point (Krusty Krab), save as 6
                        if (this.maze[i, j] == 'K')
                        {
                            numOfNodes++;
                            startIdx = new int[] { i, j };
                            row[j] = 6;
                        }
                        // if treasure, save as 5
                        else if (this.maze[i, j] == 'T')
                        {
                            numOfNodes++;
                            row[j] = 5;
                        }
                        // if road, save as 0
                        else if (this.maze[i, j] == 'R')
                        {
                            numOfNodes++;
                            row[j] = 0;
                        }
                        // if dirt, save as -1
                        else
                        {
                            row[j] = -1;
                        }
                    }
                    dt.Rows.Add(row);
                }
                mazeGrid.DataContext = dt;
            }
            catch (Exception e)
            {
                selectedFileLabel.Text = "Invalid txt file!";
            }
        }

        // function to showMaze and hides solution
        private void showMaze(object sender, RoutedEventArgs e)
        {
            fillMatrix();
            solutionPanel.Visibility = Visibility.Collapsed;
            routeLabel.Text = "-";
            stepsLabel.Content = "-";
            executionTimeLabel.Content = "-";
            numOfNodesLabel.Content = "-";
        }

        // Bonus : animate steps
        private async void showSteps(object sender, RoutedEventArgs e)
        {
            // lock other buttons that may cause RCE
            showSolutionToggle.IsEnabled = false;
            showSolutionToggle.Cursor = Cursors.No;
            stepButton.IsEnabled = false;
            stepButton.Cursor = Cursors.No;
            backButton.IsEnabled = false;
            backButton.Cursor = Cursors.No;
            fillMatrix();

            // convert from matrix to graph
            ArrayList graphData = Utility.getGraphData(Utility.readFromFile(filename));
            List<GraphNode> graph = (List<GraphNode>)graphData[0];
            int numOfTreasures = (int)graphData[1];

            List<GraphNode> steps;
            if (rbDFS.IsChecked == true)
            {
                DFS searchingDFS = new DFS(numOfTreasures, graph);
                searchingDFS.runDFSAlgorithm();
                steps = searchingDFS.getVisitedNodeSequence();
                if (TSPCheck.IsChecked == true)
                {
                    TSP searchingTSPDFS = new TSP(searchingDFS);
                    searchingTSPDFS.runTSPDFSAlgorithm();
                    steps = searchingTSPDFS.getVisitedNodeSequence();
                }
            }
            else
            {
                BFS searchingBFS = new BFS(numOfTreasures, graph[0]);
                if (TSPCheck.IsChecked == true)
                {
                    searchingBFS.runTSPBFSAlgorithm(this.maze.GetLength(0), this.maze.GetLength(1));
                }
                else
                {
                    searchingBFS.runBFSAlgorithm(this.maze.GetLength(0), this.maze.GetLength(1));
                }
                steps = searchingBFS.getVisitedNodeSequence();
            }

            // animate
            int valuePrev;
            for (int i = 0; i < steps.Count; i++)
            {
                if (i != 0)
                {
                    // set the previously visited to yellow
                    valuePrev = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[steps[i - 1].getCoordinate().x][steps[i - 1].getCoordinate().y]);
                    if (valuePrev == 5)
                    {
                        valuePrev = 0;
                    }
                    ((DataTable)mazeGrid.DataContext).Rows[steps[i - 1].getCoordinate().x][steps[i - 1].getCoordinate().y] = valuePrev % 6 + 7;
                }
                // set the currently checked to blue
                int valueCurr = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[steps[i].getCoordinate().x][steps[i].getCoordinate().y]);
                ((DataTable)mazeGrid.DataContext).Rows[steps[i].getCoordinate().x][steps[i].getCoordinate().y] = 6; // set to blue
                await Task.Delay(Convert.ToInt32(animationSlider.Value));
                ((DataTable)mazeGrid.DataContext).Rows[steps[i].getCoordinate().x][steps[i].getCoordinate().y] = valueCurr; // reset the value
            }
            // set last to yellow
            valuePrev = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[steps[steps.Count - 1].getCoordinate().x][steps[steps.Count - 1].getCoordinate().y]);
            ((DataTable)mazeGrid.DataContext).Rows[steps[steps.Count - 1].getCoordinate().x][steps[steps.Count - 1].getCoordinate().y] = valuePrev % 6 + 7;
           
            // after animation is done, show the solution (w/o animation)
            displaySolution();

            // unlock buttons
            showSolutionToggle.IsEnabled = true;
            showSolutionToggle.Cursor = Cursors.Hand;
            stepButton.IsEnabled = true;
            stepButton.Cursor = Cursors.Hand;
            backButton.IsEnabled = true;
            backButton.Cursor = Cursors.Hand;
        }


        // shows the solution without animation
        private void displaySolution()
        {
            // reset the DataGrid
            fillMatrix();

            ArrayList graphData = Utility.getGraphData(Utility.readFromFile(filename));
            List<GraphNode> graph = (List<GraphNode>)graphData[0];
            int numOfTreasures = (int)graphData[1];
            int nodesCount = 0;
            int stepsCount = 0;
            double executionTime = 0;
            List<char> steps;
            if (rbDFS.IsChecked == true)
            {
                DFS searchingDFS = new DFS(numOfTreasures, graph);
                searchingDFS.runDFSAlgorithm();
                steps = searchingDFS.getPath();
                stepsCount = searchingDFS.getNumOfNodesVisited();
                executionTime = searchingDFS.getExecutionTime();
                nodesCount = searchingDFS.getNumOfNodesVisited();
                if (TSPCheck.IsChecked == true)
                {
                    TSP searchingTSPDFS = new TSP(searchingDFS);
                    searchingTSPDFS.runTSPDFSAlgorithm();
                    steps = searchingTSPDFS.getPath();
                    stepsCount = searchingTSPDFS.getNumOfNodesVisited();
                    executionTime = searchingTSPDFS.getExecutionTime();
                    nodesCount = searchingTSPDFS.getNumOfNodesVisited();
                }
            }
            else
            {
                BFS searchingBFS = new BFS(numOfTreasures, graph[0]);
                if (TSPCheck.IsChecked == true)
                {
                    searchingBFS.runTSPBFSAlgorithm(maze.GetLength(0), maze.GetLength(1));
                }
                else
                {
                    searchingBFS.runBFSAlgorithm(maze.GetLength(0), maze.GetLength(1));
                }
                steps = searchingBFS.getFinalPath();
                stepsCount = searchingBFS.getFinalPath().Count;
                executionTime = searchingBFS.getExecutionTime();
                nodesCount = searchingBFS.getNumOfNodesVisited();
            }
            int[] startIdxTemp = new int[] { startIdx[0], startIdx[1] };
            string route = "";
            int value;
            ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = 1;
            for (int i = 0; i < steps.Count; i++)
            {
                switch (steps[i])
                {
                    case 'U':
                        startIdxTemp[0] -= 1;
                        break;
                    case 'D':
                        startIdxTemp[0] += 1;
                        break;
                    case 'R':
                        startIdxTemp[1] += 1;
                        break;
                    case 'L':
                        startIdxTemp[1] -= 1;
                        break;
                }
                route += steps[i];
                if (i != steps.Count - 1)
                {
                    route += "-";
                }
                value = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]]);
                ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = (value + 1) % 5;

            }
            numOfNodesLabel.Content = nodesCount;
            stepsLabel.Content = stepsCount;
            routeLabel.Text = route;
            executionTimeLabel.Content = executionTime + " ms";
            solutionPanel.Visibility = Visibility.Visible;
        }

        // shows the solution with animation
        private async void showSolution(object sender, RoutedEventArgs e)
        {
            backButton.IsEnabled = false;
            backButton.Cursor = Cursors.No;
            showSolutionToggle.IsEnabled = false;
            showSolutionToggle.Cursor = Cursors.No;
            stepButton.IsEnabled = false;
            stepButton.Cursor = Cursors.No;
            fillMatrix();
            ArrayList graphData = Utility.getGraphData(Utility.readFromFile(filename));
            List<GraphNode> graph = (List<GraphNode>)graphData[0];
            int numOfTreasures = (int)graphData[1];
            int nodesCount = 0;
            int stepsCount = 0;
            double executionTime = 0;
            List<char> steps;
            if (rbDFS.IsChecked == true)
            {
                DFS searchingDFS = new DFS(numOfTreasures, graph);
                searchingDFS.runDFSAlgorithm();
                steps = searchingDFS.getPath();
                stepsCount = searchingDFS.getNumOfNodesVisited();
                executionTime = searchingDFS.getExecutionTime();
                nodesCount = searchingDFS.getNumOfNodesVisited();
                if (TSPCheck.IsChecked == true)
                {
                    TSP searchingTSPDFS = new TSP(searchingDFS);
                    searchingTSPDFS.runTSPDFSAlgorithm();
                    steps = searchingTSPDFS.getPath();
                    stepsCount = searchingTSPDFS.getNumOfNodesVisited();
                    executionTime = searchingTSPDFS.getExecutionTime();
                    nodesCount = searchingTSPDFS.getNumOfNodesVisited();
                }
            }
            else
            {
                BFS searchingBFS = new BFS(numOfTreasures, graph[0]);
                if (TSPCheck.IsChecked == true)
                {
                    searchingBFS.runTSPBFSAlgorithm(maze.GetLength(0), maze.GetLength(1));
                }
                else
                {
                    searchingBFS.runBFSAlgorithm(maze.GetLength(0), maze.GetLength(1));
                }
                steps = searchingBFS.getFinalPath();
                stepsCount = searchingBFS.getFinalPath().Count;
                executionTime = searchingBFS.getExecutionTime();
                nodesCount = searchingBFS.getNumOfNodesVisited();
            }
            solutionPanel.Visibility = Visibility.Visible;
            int[] startIdxTemp = new int[] { startIdx[0], startIdx[1] };
            int[] prevIdx = new int[2] { startIdx[0], startIdx[1] };
            string route = "";
            int value = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]]);
            ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = 1;
            await Task.Delay(Convert.ToInt32(animationSlider.Value));
            for (int i = 0; i < steps.Count; i++)
            {
                if (i != 0)
                { // set the prev to green
                    value = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[prevIdx[0]][prevIdx[1]]);
                    ((DataTable)mazeGrid.DataContext).Rows[prevIdx[0]][prevIdx[1]] = (value + 1) % 5;
                }
                switch (steps[i])
                {
                    case 'U':
                        startIdxTemp[0] -= 1;
                        break;
                    case 'D':
                        startIdxTemp[0] += 1;
                        break;
                    case 'R':
                        startIdxTemp[1] += 1;
                        break;
                    case 'L':
                        startIdxTemp[1] -= 1;
                        break;
                }
                route += steps[i];
                if (i != steps.Count - 1)
                {
                    route += "-";
                }
                // set the curr to blue
                value = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]]);
                ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = 6;

                // save the prev
                prevIdx[0] = startIdxTemp[0];
                prevIdx[1] = startIdxTemp[1];
                routeLabel.Text = route;
                await Task.Delay(Convert.ToInt32(animationSlider.Value));
                // reset the value of the curr
                ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = value;
            }
            // set last to green
            value = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]]);
            ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = (value + 1) % 5;
            numOfNodesLabel.Content = nodesCount;
            stepsLabel.Content = stepsCount;
            executionTimeLabel.Content = executionTime + " ms";
            showSolutionToggle.IsEnabled = true;
            showSolutionToggle.Cursor = Cursors.Hand;
            stepButton.IsEnabled = true;
            stepButton.Cursor = Cursors.Hand;
            backButton.IsEnabled = true;
            backButton.Cursor = Cursors.Hand;
        }
    }

}
