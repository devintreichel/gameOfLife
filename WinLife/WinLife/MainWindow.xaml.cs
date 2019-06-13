using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int GridSize = 80;
        const bool DEAD = false;
        const bool ALIVE = true;
        private Cell[,] cells = new Cell[GridSize, GridSize];
        private System.Timers.Timer timer = new System.Timers.Timer(100);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WinLifeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < GridSize; i++)
            {
                LifeGrid.ColumnDefinitions.Add(new ColumnDefinition());
                LifeGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    cells[i, j] = new Cell(DEAD);
                    Rectangle cellRect = new Rectangle();
                    Grid.SetColumn(cellRect, j);
                    Grid.SetRow(cellRect, i);
                    cellRect.Fill = Brushes.Black;
                    cellRect.DataContext = cells[i, j];
                    cellRect.SetBinding(OpacityProperty, "State");
                    LifeGrid.Children.Add(cellRect);
                }
            }
            timer.Elapsed += (x, y) => ComputeNextGeneration();
        }

        private void WinLifeWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Rectangle)
            {
                Rectangle rect = ((Rectangle)e.OriginalSource);
                Cell cell = (Cell)rect.DataContext;
                cell.State = !cell.State;
            }
        }

        private void ComputeNextGeneration()
        {
            int[,] n = new int[GridSize, GridSize];

            for (int i = 1; i < GridSize - 1; i++)
            {
                for (int j = 1; j < GridSize - 1; j++)
                {
                    n[i, j] = cells[i - 1, j] + cells[i + 1, j] + cells[i, j - 1] + cells[i, j + 1]
                        + cells[i - 1, j - 1] + cells[i + 1, j - 1] + cells[i + 1, j + 1] + cells[i - 1, j + 1];
                }
            }

            for (int i = 1; i < GridSize - 1; i++)
            {
                for (int j = 1; j < GridSize - 1; j++)
                {
                    if (n[i, j] == 3) cells[i, j].State = ALIVE;
                    if (n[i, j] >= 4) cells[i, j].State = DEAD;
                    if (n[i, j] <= 1) cells[i, j].State = DEAD;
                }
            }
        }

        private void RunStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (RunStopButton.Content.ToString() == "Run")
            {
                RunStopButton.Content = "Stop";
                timer.Start();
            }
            else
            {
                RunStopButton.Content = "Run";
                timer.Stop();
            }
        }

        private void FillButtom_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < GridSize - 1; i++)
            {
                for (int j = 1; j < GridSize - 1; j++)
                {
                    cells[i, j].State = ALIVE;
                }
            }
        }
    }
}
