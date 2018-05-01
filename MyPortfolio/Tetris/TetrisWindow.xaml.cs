using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyPortfolio.Tetris
{
    class Tetromino
    {
        // позиция фигуры
        private Point currentPosition;
        public Point CurrentPosition
        {
            get { return currentPosition; }
            set { currentPosition = value; }
        }
        //фигура
        private Point[] currentShape;
        public Point[] CurrentShape
        {
            get { return currentShape; }
            set { currentShape = value; }
        }
        //цвет фигуры
        private Brush currentColor;
        public Brush CurrentColor
        {
            get { return currentColor; }
            set { currentColor = value; }
        }
        // поворот
        private bool rotate;

        public Tetromino()
        {
            currentPosition = new Point(0, 0);
            currentColor = Brushes.Transparent;
            currentShape = setRandomShape();
        }

        //смешение фигуры
        public void moveLeft()
        {
            currentPosition.X -= 1;
        }
        public void moveRight()
        {
            currentPosition.X += 1;
        }
        public void moveDown()
        {
            currentPosition.Y += 1;
        }
        public void moveRotate()
        {
            if (rotate)
            {
                for (int i = 0; i < currentShape.Length; i++)
                {
                    double temp = currentShape[i].X;
                    currentShape[i].X = currentShape[i].Y * -1;
                    currentShape[i].Y = temp;
                }
            }
        }
        //случайная фигура
        private Point[] setRandomShape()
        {
            Random Rnd = new Random();

            switch (Rnd.Next() % 7)
            {
                case 0: //I
                    rotate = true;
                    currentColor = Brushes.Yellow;
                    return new Point[]{
                        new Point(1,0),
                        new Point(1,1),
                        new Point(1,2),
                        new Point(1,3)
                    };
                case 1: //J
                    rotate = true;
                    currentColor = Brushes.DarkBlue;
                    return new Point[]{
                        new Point(1,0),
                        new Point(1,1),
                        new Point(1,2),
                        new Point(0,2)
                    };
                case 2: //L
                    rotate = true;
                    currentColor = Brushes.Blue;
                    return new Point[]{
                        new Point(1,0),
                        new Point(1,1),
                        new Point(1,2),
                        new Point(2,2),
                    };
                case 3: //O
                    rotate = false;
                    currentColor = Brushes.Orange;
                    return new Point[]{
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,0),
                        new Point(1,1)
                    };
                case 4: //T
                    rotate = true;
                    currentColor = Brushes.Green;
                    return new Point[]{
                        new Point(0,0),
                        new Point(1,0),
                        new Point(2,0),
                        new Point(1,1)
                    };
                case 5: //S
                    rotate = true;
                    currentColor = Brushes.Purple;
                    return new Point[]{
                        new Point(1,0),
                        new Point(2,0),
                        new Point(0,1),
                        new Point(1,1)
                    };
                case 6: //Z
                    rotate = true;
                    currentColor = Brushes.Red;
                    return new Point[]{
                        new Point(0,0),
                        new Point(1,0),
                        new Point(1,1),
                        new Point(2,1)
                    };
                default:
                    return null;
            }
        }
    }

    class Board
    {
        public int rows { get; set; } // строки
        public int collumns { get; set; } // колонки

        public int score { get; set; } //скорость
        public int linesFilled { get; set; } //кол уничтоженых линий

        private Tetromino currentTetramino; // фигура
        private Label[,] blockControls; //массив блоков игрового поля

        static private Brush noBrush = Brushes.Transparent; // безцветная заливка
        static private Brush silverBrush = Brushes.Gray;

        public Board(Grid TetrisGrid)
        {
            rows = TetrisGrid.RowDefinitions.Count;
            collumns = TetrisGrid.ColumnDefinitions.Count;

            score = 0;
            linesFilled = 0;

            currentTetramino = new Tetromino();
            BoardDraw(TetrisGrid);
            currentTetraminoDraw();
        }
        //рисуем игровое поле
        private void BoardDraw(Grid TetrisGrid)
        {
            blockControls = new Label[collumns, rows];
            for (int x = 0; x < collumns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    blockControls[x, y] = new Label();
                    blockControls[x, y].Background = noBrush;
                    blockControls[x, y].BorderBrush = silverBrush;
                    blockControls[x, y].BorderThickness = new Thickness(1);
                    Grid.SetRow(blockControls[x, y], y);
                    Grid.SetColumn(blockControls[x, y], x);
                    TetrisGrid.Children.Add(blockControls[x, y]);
                }
            }
        }
        //рисуем фигуру на поле
        private void currentTetraminoDraw()
        {
            Point currentPosition = currentTetramino.CurrentPosition; // текушие кординаты фигуры 
            Point[] Shape = currentTetramino.CurrentShape; // кардинаты фигуры по умолчанию
            Brush Color = currentTetramino.CurrentColor;

            foreach (Point position in Shape)
            {
                blockControls[(int)(position.X + currentPosition.X) + ((collumns / 3) + 1), (int)(position.Y + currentPosition.Y)].Background = Color;
                //blockControls[(int)(position.X + currentPosition.X) + ((collumns / 3) + 1), (int)(position.Y + currentPosition.Y)].FontSize = 20;
                //blockControls[(int)(position.X + currentPosition.X) + ((collumns / 3) + 1), (int)(position.Y + currentPosition.Y)].Content = ((int)(position.X + currentPosition.X) + ((collumns / 3)+1)) + " " + (int)(position.Y + currentPosition.Y);
            }
        }
        //удаляем фигуру
        private void currentTetraminoErase()
        {
            Point currentPosition = currentTetramino.CurrentPosition;
            Point[] Shape = currentTetramino.CurrentShape;
            Brush Color = currentTetramino.CurrentColor;
            foreach (Point position in Shape)
            {
                blockControls[(int)(position.X + currentPosition.X) + ((collumns / 3) + 1), (int)(position.Y + currentPosition.Y)].Background = noBrush;
            }
        }

        //проверяем заполненость сроки
        private void CheckRows()
        {
            bool full;
            for (int y = rows - 1; y > 0; y--)
            {
                full = true;
                for (int x = 0; x < collumns; x++)
                {
                    if (blockControls[x, y].Background == noBrush) //если безцветный фон у блока, вернем - ложь
                        full = false;
                }
                if (full)
                {
                    RemoveRow(y);
                    score += 100;
                    linesFilled += 1;
                }
            }
        }
        //удалим строку если она заполнeна
        private void RemoveRow(int row)
        {
            for (int y = row; y > 2; y--)
            {
                for (int x = 0; x < collumns; x++)
                {
                    blockControls[x, y].Background = blockControls[x, y - 1].Background;
                }
            }
        }
        //конец игры
        //public void TheEndGame()
        //{
        //    bool full;
        //    for (int x = collumns - 1; x > 0; x--)
        //    {
        //        full = true;
        //        for (int y = 0; y < rows; y++)
        //        {
        //            if (blockControls[x, y].Background == noBrush)
        //                return false;
        //        }
        //        if (full)
        //            ;
        //    }
        //}

        //Движение вниз
        public void CurrentTetraminoMoveDown()
        {
            Point currentPosition = currentTetramino.CurrentPosition;
            Point[] Shape = currentTetramino.CurrentShape;
            bool move = true;

            currentTetraminoErase();

            foreach (Point position in Shape)
            {
                if (((int)(position.Y + currentPosition.Y) + 1) >= rows)
                    move = false;

                else if (blockControls[((int)(position.X + currentPosition.X) + ((collumns / 3) + 1)), (int)(position.Y + currentPosition.Y) + 1].Background != noBrush)
                    move = false;
            }
            if (move)
            {
                currentTetramino.moveDown();
                currentTetraminoDraw();
            }
            else
            {
                currentTetraminoDraw();
                CheckRows();
                currentTetramino = new Tetromino();
            }
        }
        //движение влево
        public void CurrentTetraminoMoveLeft()
        {
            Point currentPosition = currentTetramino.CurrentPosition;
            Point[] Shape = currentTetramino.CurrentShape;
            bool move = true;

            currentTetraminoErase();

            foreach (Point position in Shape)
            {
                if ((int)(position.X + currentPosition.X) + (collumns / 3) + 1 <= 0)
                    move = false;
                else if (blockControls[(int)(position.X + currentPosition.X) + (collumns / 3), (int)(position.Y + currentPosition.Y)].Background != noBrush)
                    move = false;
            }
            if (move)
            {
                currentTetramino.moveLeft();
                currentTetraminoDraw();
            }
            else
                currentTetraminoDraw();
        }
        //Движение вправо
        public void CurrentTetraminoMoveRight()
        {
            Point currentPosition = currentTetramino.CurrentPosition;
            Point[] Shape = currentTetramino.CurrentShape;
            bool move = true;

            currentTetraminoErase();

            foreach (Point position in Shape)
            {
                if ((int)(position.X + currentPosition.X) + ((collumns / 3) + 1) >= collumns - 1)
                    move = false;
                else if (blockControls[(int)(position.X + currentPosition.X) + ((collumns / 3) + 2), (int)(position.Y + currentPosition.Y)].Background != noBrush)
                    move = false;
            }
            if (move)
            {
                currentTetramino.moveRight();
                currentTetraminoDraw();
            }
            else
                currentTetraminoDraw();
        }
        //Поворот
        public void CurrentTetraminoMoveRotate()
        {
            Point currentPosition = currentTetramino.CurrentPosition;
            Point[] Shape = currentTetramino.CurrentShape;
            bool move = true;

            Point[] new_Shape = new Point[4];
            Shape.CopyTo(new_Shape, 0);

            currentTetraminoErase();
            for (int i = 0; i < new_Shape.Length; i++)
            {
                double temp = new_Shape[i].X;
                new_Shape[i].X = new_Shape[i].Y * -1;
                new_Shape[i].Y = temp;

                if ((int)(new_Shape[i].Y + currentPosition.Y) >= rows || (int)(new_Shape[i].Y + currentPosition.Y) < 0)
                    move = false;
                else if ((int)(new_Shape[i].X + currentPosition.X) + ((collumns / 3) + 1) >= collumns || (int)(new_Shape[i].X + currentPosition.X) + ((collumns / 3) + 1) < 0)
                    move = false;
                else if (blockControls[(int)(new_Shape[i].X + currentPosition.X) + ((collumns / 3) + 1), (int)(new_Shape[i].Y + currentPosition.Y)].Background != noBrush)
                    move = false;
            }
            if (move)
            {
                currentTetramino.moveRotate();
                currentTetraminoDraw();
            }
            else
                currentTetraminoDraw();
        }
    }

    public partial class TetrisWindow : Window
    {
        Board myBoard;
        DispatcherTimer gameTimer;

        DispatcherTimer speedUpTimer;
        public int speedBlock { get; set; }


        public TetrisWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            speedUpTimer = new DispatcherTimer();

            speedUpTimer.Tick += new EventHandler(speedUpTimer_Tick);
            speedUpTimer.Interval = new TimeSpan(0, 0, 0, 1);

            gameTimer = new DispatcherTimer();

            gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);

            StartGame();
        }

        private void speedUpTimer_Tick(object sender, EventArgs e)
        {
            if (speedUp.Maximum != speedUp.Value)
                speedUp.Value++;
            else
            {
                speedUp.Value = 0;
                speedBlock = (speedBlock / 2) + 30;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            Lbl_Score.Content = myBoard.score.ToString("Score: 000 000");
            Lbl_Lines.Content = myBoard.linesFilled.ToString("Lines: 000");
            myBoard.CurrentTetraminoMoveDown();
        }

        private void StartGame()
        {
            MainGrid.Children.Clear();
            myBoard = new Board(MainGrid);

            speedUp.Value = 0;
            speedBlock = 400;

            speedUpTimer.Start();
            gameTimer.Start();
        }

        private void PauseGame()
        {
            if (gameTimer.IsEnabled)
            {
                gameTimer.IsEnabled = false;
            }
            else
            {
                gameTimer.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case (Key.Left):
                    myBoard.CurrentTetraminoMoveLeft();

                    break;
                case (Key.Right):
                    myBoard.CurrentTetraminoMoveRight();
                    break;
                case (Key.Down):
                    myBoard.CurrentTetraminoMoveDown();
                    break;
                case (Key.Up):
                    myBoard.CurrentTetraminoMoveRotate();
                    break;
                case (Key.Escape):
                    PauseGame();
                    break;
                case (Key.Space):
                    StartGame();
                    break;
                default:
                    break;
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
