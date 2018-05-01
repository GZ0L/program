using System.Windows;

namespace MyPortfolio.EnglishWords
{
    public partial class AddWordWindow : Window
    {
        DictionaryWord words = new DictionaryWord();

        public AddWordWindow(string path)
        {
            InitializeComponent();
            this.words.path = path;
            words.ReadFile(this.words.Words, this.words.path);
            Txt_Word.Focus();
        }


        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {

            if (Txt_Word.Text.Length > 0 && Txt_Transcription.Text.Length > 0 && Txt_Translate.Text.Length > 0)
            {
                if (Txt_Word.Text != "error" && Txt_Transcription.Text != "error" && Txt_Translate.Text != "error")
                {
                    if (words.AddWord(Txt_Word.Text, new Word(Txt_Transcription.Text, Txt_Translate.Text)))
                    {
                        words.WriteToFile(this.words.Words, this.words.path);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Such a word already exists in the current dictionary", "Error");
                }
            }
            if (Txt_Word.Text.Length == 0)
            {
                Txt_Word.Text = "error";
            }
            if (Txt_Transcription.Text.Length == 0)
            {
                Txt_Transcription.Text = "error";
            }
            if (Txt_Translate.Text.Length == 0)
            {
                Txt_Translate.Text = "error";
            }
        }
    }
}
