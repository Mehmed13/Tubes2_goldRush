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

namespace GoldRush
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private int[] startIdx;
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
            if (selectedFileLabel.Content.Equals("No file chosen"))
            {
                myPopup.IsOpen = true;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += (s, args) =>
                {
                    myPopup.IsOpen = false;
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

        private void ShowInput(object sender, RoutedEventArgs e)
        {
            // Hide the first grid
            inputGrid.Visibility = Visibility.Visible;

            // Show the second grid
            visualizeGrid.Visibility = Visibility.Collapsed;
        }

        private void fillMatrix()
        {
            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(filename);
            Debug.WriteLine(lines.Length);
            if (lines.Length > 0)
            {
                string[] columns = lines[0].Split(' ');
                double cellWidth = 300 / columns.Length;
                myDataGrid.Width = columns.Length * cellWidth;
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
                myDataGrid.CellStyle = newCellStyle;
                for (int i = 0; i < columns.Length; i++)
                {
                    dt.Columns.Add(new DataColumn("Column"+i, typeof(int)));
                }
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] values = lines[i].Trim().Split(' ');
                    DataRow row = dt.NewRow();
                    Debug.WriteLine(values.Length);
                    for (int j = 0; j < values.Length; j++)
                    {
                        if (values[j][0] == 'K')
                        {
                            startIdx = new int[] { i, j };
                            row[j] = 1;
                        }
                        else if (values[j][0] == 'T')
                        {
                            row[j] = 5;
                        }
                        else if (values[j][0] == 'R')
                        {
                            row[j] = 0;
                        }
                        else
                        {
                            row[j] = -1;
                        }
                    }
                    dt.Rows.Add(row);
                }
            }
            myDataGrid.DataContext = dt;
        }

        private void showMaze(object sender, RoutedEventArgs e)
        {
            fillMatrix();
        }

        private async void showSolution(object sender, RoutedEventArgs e)
        {
            char[] stepsTemp = new char[] { 'U', 'R', 'R', 'D', 'D', 'R' };
            int[] startIdxTemp = new int[] { startIdx[0], startIdx[1] };
            Stack<char> steps = new Stack<char>(stepsTemp);
            while(steps.Count > 0)
            {
                switch (steps.Peek())
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
                int value = Convert.ToInt32(((DataTable)myDataGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]]);
                ((DataTable)myDataGrid.DataContext).Rows[startIdxTemp[0]][startIdxTemp[1]] = (value + 1)%5;
                steps.Pop();
                await Task.Delay(500);
            }
        }
    }

}
