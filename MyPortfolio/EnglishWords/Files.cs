using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyPortfolio.EnglishWords
{
    class Files
    {

        public string path { get; set; }
        public Files()
        {
            this.path = @"words\Favorite.txt";
        }
        //создаем папку по умолчанию
        public void CreatDirectori()
        {
            if (!Directory.Exists("words"))
                Directory.CreateDirectory("words");
        }
        //создает фаил 
        public void CreatFile()
        {
            if (!File.Exists(this.path))
                using (File.Create(this.path)) { }
        }
        //удалить фаил
        public void DeleteFile(string path)
        {
            if (File.Exists(path))
                using (null) { File.Delete(path); }
        }
        //запись в файл
        public void WriteToFile(Dictionary<string, Word> words, string path)
        {
            File.Delete(path);
            using (null)
            {
                if (words.Count > 0)
                {
                    foreach (var item in words)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append(item.Key.ToLower().Trim());
                        stringBuilder.Append("[");
                        stringBuilder.Append(item.Value.Transcription);
                        stringBuilder.Append("]");
                        stringBuilder.Append(item.Value.Translate.ToLower().Trim());
                        stringBuilder.Append("\r\n");
                        
                        File.AppendAllText(path, stringBuilder.ToString(), Encoding.UTF8);
                    }
                }
            }
        }
        //чтение файла
        public void ReadFile(Dictionary<string, Word> words, string path)
        {
            using (null)
            {
                words.Clear();
                if (File.Exists(path))
                {
                    string[] strLines = File.ReadAllLines(path);
                    foreach (var item in strLines)
                    {
                        if (item != "" && item != null)
                        {
                            string[] word = item.Split('[', ']');
                            words.Add(word[0], new Word(word[1], word[2]));
                        }
                    }
                }
            }
        }
    }
}
