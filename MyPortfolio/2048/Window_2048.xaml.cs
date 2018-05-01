using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyPortfolio._2048
{
    public partial class Window_2048 : Window
    {
        game2048 game = new game2048();

        public Window_2048()
        {
            InitializeComponent();
            EndGame.Visibility = Visibility.Hidden;

        }
        //Начать игру
        private void Btn_NewGame_Click(object sender, RoutedEventArgs e)
        {
            EndGame.Visibility = Visibility.Hidden;
            game.StartGame();
            ShowGame();
        }
        //нажатие клавишь
        private void Window_2048_KeyDown(object sender, KeyEventArgs e)
        {
            if (game.TheEndGame() == false)
            {
                if (e.Key == Key.Up)
                {
                    game.KeyDownUp();
                    ShowGame();
                }
                if (e.Key == Key.Down)
                {
                    game.KeyDownDown();
                    ShowGame();
                    //Lbl_1.Style = (Style)Lbl_1.FindResource("Lbl_2");
                }
                if (e.Key == Key.Left)
                {
                    game.KeyDownLeft();
                    ShowGame();
                }
                if (e.Key == Key.Right)
                {
                    game.KeyDownRight();
                    ShowGame();
                }
            }
            else
                EndGame.Visibility = Visibility.Visible;
        }
        //свернуть
        private void Btn_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //выход
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // // // // // // // // // // // // // // // // // // // //

        public void ShowGame()
        {
            //массив лейблов
            Label[,] arrLabel = {
                                 {Lbl_1, Lbl_2, Lbl_3, Lbl_4},
                                 {Lbl_5, Lbl_6, Lbl_7, Lbl_8},
                                 {Lbl_9, Lbl_10, Lbl_11, Lbl_12},
                                 {Lbl_13, Lbl_14, Lbl_15, Lbl_16}
                                 };

            //значения из массива в лейблы
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (game.arr[i, j] != 0)
                    {
                        arrLabel[i, j].Style = (Style)arrLabel[i, j].FindResource("Lbl_Base");
                        arrLabel[i, j].Content = game.arr[i, j].ToString();

                        if (game.arr[i, j] == 2048)
                            arrLabel[i, j].Style = (Style)arrLabel[i, j].FindResource("Lbl_2048");
                    }
                    else
                    {
                        arrLabel[i, j].Content = null;
                        arrLabel[i, j].Style = (Style)arrLabel[i, j].FindResource("Lbl_Base");
                    }
                }
            }
            //счет
            Lbl_Score.Content = game.score;
        }
    }
}
