using System;
using System.Collections.Generic;
using System.Windows;

namespace MyPortfolio.EnglishWords
{
    class LearnWords
    {
        Random Rnd = new Random();
        //изи мод - слова
        public string[] Translate(string word, Dictionary<string, Word> words)
        {
            string[] rusTranslate = new string[4];
            string[] engTranslate = new string[4];


            string allRusWords = "";
            string allEngWords = "";

            foreach (var item in words)
            {
                allRusWords += item.Value.Translate + " ";
                allEngWords += item.Key + " ";
            }
            string[] arrRusWords = allRusWords.Trim().Split();
            string[] arrEngWords = allEngWords.Trim().Split();



            if (words.ContainsKey(word))
            {
                rusTranslate[0] = words[word].Translate;

                rusTranslate[1] = arrRusWords[Rnd.Next(arrRusWords.Length)];
                rusTranslate[2] = arrRusWords[Rnd.Next(arrRusWords.Length)];
                rusTranslate[3] = arrRusWords[Rnd.Next(arrRusWords.Length)];
                //MixArr(rusTranslate);
                return rusTranslate;
            }
            else
            {
                foreach (var item in words)
                {
                    if (item.Value.Translate == word)
                        engTranslate[0] = item.Key;
                }
                engTranslate[1] = arrEngWords[Rnd.Next(arrEngWords.Length)];
                engTranslate[2] = arrEngWords[Rnd.Next(arrEngWords.Length)];
                engTranslate[3] = arrEngWords[Rnd.Next(arrEngWords.Length)];
                //MixArr(engTranslate);
                return engTranslate;
            }
        }


        //рандомное слово
        public string Rnd_Word(Dictionary<string, Word> words)
        {
            if (words.Count > -1)
            {
                string allWords = "";

                foreach (var item in words)
                {
                    allWords += item.Key + " " + item.Value.Translate + " ";
                }

                string[] arrWords = allWords.Trim().Split();

                //MessageBox.Show(string.Join("\r\n/", arrWords));
                string rnd_word = arrWords[Rnd.Next(arrWords.Length-1)];
                return rnd_word;
            }
            else
                return "no words";
        }

        //сравнение слов
        public bool СompareWords(string word, string translate, Dictionary<string, Word> words)
        {
            foreach (var item in words)
            {
                if (word.Equals(item.Key) && translate.Equals(item.Value.Translate) || translate.Equals(item.Key) && word.Equals(item.Value.Translate))
                    return true;
            }
            return false;
        }
        //правильный перевод
        public string Correct(string word, Dictionary<string, Word> words)
        {
            foreach (var item in words)
            {
                if (word.Equals(item.Key))
                    return item.Value.Translate;
                if (word.Equals(item.Value.Translate))
                    return item.Key;
            }
            return word;
        }
    }
}
