namespace MyPortfolio.EnglishWords
{
    class Word
    {
        public string Transcription { get; set; }
        public string Translate { get; set; }

        public Word(string transcription, string translate)
        {
            this.Transcription = transcription;
            this.Translate = translate;
        }
    }
}
