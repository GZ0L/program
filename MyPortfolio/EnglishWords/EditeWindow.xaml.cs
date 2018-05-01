using System.Windows;

namespace MyPortfolio.EnglishWords
{
    public partial class EditeWindow : Window
    {
        DictionaryWord words = new DictionaryWord();

        public EditeWindow(string path, string word)
        {
            InitializeComponent();
            this.words.path = path;
            Lbl_Word.Content = word;
            words.ReadFile(this.words.Words, this.words.path);
            Txt_Transcription.Focus();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            words.EditeWord(Lbl_Word.Content.ToString(), new Word(Txt_Transcription.Text, Txt_Translate.Text));
            words.WriteToFile(this.words.Words, this.words.path);
            this.Close();
        }
    }
}
