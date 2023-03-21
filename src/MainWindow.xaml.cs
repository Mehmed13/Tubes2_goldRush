﻿using System;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".txt"; // set default file extension
            dialog.Filter = "Text files (*.txt)|*.txt"; // set file filter
            Nullable<bool> result = dialog.ShowDialog(); // open file dialog
            if (result == true) // if user selected a file
            {
                string filename = dialog.FileName; // get file name
                                                   // set text block text to file name
                selectedFileLabel.Content = filename;
                this.filename = filename;
                fillMatrix();
            }
            else
            {
                selectedFileLabel.Content = "No file chosen";
            }
        }

        private void Visualize(object sender, RoutedEventArgs e)
        {
            if (selectedFileLabel.Content.Equals("No file chosen") || selectedFileLabel.Content.Equals("Invalid txt file!"))
            {
                errorPopup.IsOpen = true;
                popUpText.Text = selectedFileLabel.Content.ToString();
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
                // Hide the first grid
                inputGrid.Visibility = Visibility.Collapsed;

                // Show the second grid
                visualizeGrid.Visibility = Visibility.Visible;
            }
        }

        private void showInput(object sender, RoutedEventArgs e)
        {
            // Hide the first grid
            inputGrid.Visibility = Visibility.Visible;

            // Show the second grid
            visualizeGrid.Visibility = Visibility.Collapsed;

            fillMatrix();
        }

        private void fillMatrix()
        {
            try
            {
                int numOfNodes = 0;
                DataTable dt = new DataTable();
                this.maze = Utility.readFromFile(filename);
                char[,] temp = Utility.readFromFile(filename);
                Debug.WriteLine(temp.GetLength(0));
                Debug.WriteLine(temp.GetLength(1));
                double cellWidth = Math.Floor(300.0 / temp.GetLength(1));
                mazeGrid.Width = temp.GetLength(1) * cellWidth;
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
                for (int i = 0; i < temp.GetLength(1); i++)
                {
                    dt.Columns.Add(new DataColumn(i.ToString(), typeof(int)));
                }
                for (int i = 0; i < temp.GetLength(0); i++)
                {
                    DataRow row = dt.NewRow();
                    for (int j = 0; j < temp.GetLength(1); j++)
                    {
                        if (temp[i, j] == 'K')
                        {
                            numOfNodes++;
                            startIdx = new int[] { i, j };
                            row[j] = 0;
                            Debug.WriteLine("BBBBB");
                        }
                        else if (temp[i, j] == 'T')
                        {
                            numOfNodes++;
                            row[j] = 5;
                        }
                        else if (temp[i, j] == 'R')
                        {
                            numOfNodes++;
                            row[j] = 0;
                        }
                        else
                        {
                            row[j] = -1;
                        }
                    }
                    dt.Rows.Add(row);
                }
                numOfNodesLabel.Content = numOfNodes;
                mazeGrid.DataContext = dt;
            }
            catch (Exception e)
            {
                selectedFileLabel.Content = "Invalid txt file!";
            }
        }

        private void showMaze(object sender, RoutedEventArgs e)
        {
            fillMatrix();
        }

        private async void showSteps(object sender, RoutedEventArgs e)
        {
            fillMatrix();
            ArrayList graphData = Utility.getGraphData(Utility.readFromFile(filename));
            List<GraphNode> graph = (List<GraphNode>)graphData[0];
            int numOfTreasures = (int)graphData[1];
            int stepsCount = 0;
            double executionTime = 0;
            List<GraphNode> steps;
            if (rbDFS.IsChecked == true)
            {
                DFS searchingDFS = new DFS(numOfTreasures, graph);
                searchingDFS.runDFSAlgorithm();
                steps = searchingDFS.getVisitedNodeSequence();
                Debug.WriteLine(steps.Count);
                int valuePrev;
                for (int i = 0; i < steps.Count; i++)
                {
                    if (i != 0)
                    {
                        valuePrev = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[steps[i - 1].getCoordinate().x][steps[i - 1].getCoordinate().y]);
                        ((DataTable)mazeGrid.DataContext).Rows[steps[i - 1].getCoordinate().x][steps[i - 1].getCoordinate().y] = valuePrev % 6 + 7;
                    }
                    int valueCurr = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[steps[i].getCoordinate().x][steps[i].getCoordinate().y]);
                    ((DataTable)mazeGrid.DataContext).Rows[steps[i].getCoordinate().x][steps[i].getCoordinate().y] = 6; // set to blue
                    await Task.Delay(Convert.ToInt32(animationSlider.Value));
                    ((DataTable)mazeGrid.DataContext).Rows[steps[i].getCoordinate().x][steps[i].getCoordinate().y] = valueCurr; // reset the value
                }
                // set last to yellow
                valuePrev = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[steps[steps.Count - 1].getCoordinate().x][steps[steps.Count - 1].getCoordinate().y]);
                ((DataTable)mazeGrid.DataContext).Rows[steps[steps.Count - 1].getCoordinate().x][steps[steps.Count - 1].getCoordinate().y] = valuePrev % 6 + 7;
            }
            displaySolution();
        }

        private void displaySolution() {
            fillMatrix();
            ArrayList graphData = Utility.getGraphData(Utility.readFromFile(filename));
            List<GraphNode> graph = (List<GraphNode>)graphData[0];
            int numOfTreasures = (int)graphData[1];
            int stepsCount = 0;
            double executionTime = 0;
            List<char> steps;
            if (rbDFS.IsChecked == true)
            {
                DFS searchingDFS = new DFS(numOfTreasures, graph);
                var watch = Stopwatch.StartNew();
                searchingDFS.runDFSAlgorithm();
                watch.Stop();
                steps = (searchingDFS.getPath());
                Debug.WriteLine(searchingDFS.getPath().Count);
                for (int i = 0; i < searchingDFS.getPath().Count; i++)
                {
                    Debug.WriteLine(searchingDFS.getPath()[i]);
                }
                stepsCount = searchingDFS.getPath().Count;
                executionTime = watch.ElapsedMilliseconds;
                Debug.WriteLine(executionTime);
            }
            else
            {
                Debug.WriteLine("HEHE");
                steps = new List<char> { 'U', 'R', 'R', 'D', 'D', 'R' };
            }
            int[] startIdxTemp = new int[] { startIdx[0], startIdx[1] };
            string route = "";
            int value = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]]);
            ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = (value + 1) % 5;
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
            stepsLabel.Content = stepsCount;
            routeLabel.Content = route;
            executionTimeLabel.Content = executionTime + " ms";
            solutionPanel.Visibility = Visibility.Visible;
        }

        private async void showSolution(object sender, RoutedEventArgs e)
        {
            fillMatrix();
            ArrayList graphData = Utility.getGraphData(Utility.readFromFile(filename));
            List<GraphNode> graph = (List<GraphNode>)graphData[0];
            int numOfTreasures = (int)graphData[1];
            int stepsCount = 0;
            double executionTime = 0;
            List<char> steps;
            if (rbDFS.IsChecked == true)
            {
                DFS searchingDFS = new DFS(numOfTreasures, graph);
                var watch = Stopwatch.StartNew();
                searchingDFS.runDFSAlgorithm();
                watch.Stop();
                steps = (searchingDFS.getPath());
                Debug.WriteLine(searchingDFS.getPath().Count);
                for (int i = 0; i < searchingDFS.getPath().Count; i++)
                {
                    Debug.WriteLine(searchingDFS.getPath()[i]);
                }
                stepsCount = searchingDFS.getPath().Count;
                executionTime = watch.ElapsedMilliseconds;
                Debug.WriteLine(executionTime);
            }
            else
            {
                Debug.WriteLine("HEHE");
                steps = new List<char> { 'U', 'R', 'R', 'D', 'D', 'R' };
            }
            solutionPanel.Visibility = Visibility.Visible;
            int[] startIdxTemp = new int[] { startIdx[0], startIdx[1] };
            string route = "";
            int value = Convert.ToInt32(((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]]);
            ((DataTable)mazeGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = (value + 1) % 5;
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
                routeLabel.Content = route;
                await Task.Delay(Convert.ToInt32(animationSlider.Value));
            }
            stepsLabel.Content = stepsCount;
            executionTimeLabel.Content = executionTime + " ms";
        }



    }

}
