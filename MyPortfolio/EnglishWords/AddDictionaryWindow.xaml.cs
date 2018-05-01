using System.Windows;
using System.Windows.Controls;

namespace MyPortfolio.EnglishWords
{
    public partial class AddDictionaryWindow : Window
    {
        DictionaryWord words = new DictionaryWord();

        public AddDictionaryWindow()
        {
            InitializeComponent();
            Txt_Box.Focus();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            using (null)
            {
                if (Txt_Box.Text.Length > 0 && Txt_Box.Text != " ")
                {
                    words.path = "words\\" + Txt_Box.Text + ".txt";
                    words.CreatFile();
                    this.Close();
                }
            }
        }
    }
}
