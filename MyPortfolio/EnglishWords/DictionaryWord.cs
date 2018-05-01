using System.Collections.Generic;

namespace MyPortfolio.EnglishWords
{
    class DictionaryWord : Files
    {
        public Dictionary<string, Word> Words { get; set; }
        public DictionaryWord()
        {
            this.Words = new Dictionary<string, Word>();
        }

        //поиск слова
        public string FindWord(string word)
        {
            if (word != "")
            {
                foreach (var item in Words)
                {
                    if (item.Key == word || item.Value.Translate == word)
                        return word = item.Key + " [" + item.Value.Transcription + "] " + item.Value.Translate;
                }
            }
            return word = "The word is missing";
        }
        //добавить слово
        public bool AddWord(string word, Word translate)
        {
            if (!Words.ContainsKey(word))
            {
                this.Words.Add(word, translate);
                return true;
            }
            return false;
        }
        //удалить слово
        public void DeleteWord(string word) { this.Words.Remove(word); }
        //редактировать слово
        public void EditeWord(string word, Word translate)
        {
            foreach (var item in Words)
            {
                if (word.Equals(item.Key))
                {
                    item.Value.Transcription = translate.Transcription;
                    item.Value.Translate = translate.Translate;
                }
            }
        }
    }
}
