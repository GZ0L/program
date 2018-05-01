using System.Windows;
using System.Windows.Input;
using MyPortfolio._2048;
using MyPortfolio.EnglishWords;
using MyPortfolio.Tetris;

namespace MyPortfolio
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_2048_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window_2048 game2048 = new Window_2048();
            game2048.ShowDialog();
        }

        private void Btn_EnglishWord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowEnglishWord englsihWord = new WindowEnglishWord();
            englsihWord.ShowDialog();
        }

        private void Btn_Tetris_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TetrisWindow tetris = new TetrisWindow();
            tetris.ShowDialog();
        }
    }
}
