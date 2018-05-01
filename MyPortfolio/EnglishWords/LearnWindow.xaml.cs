using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyPortfolio.EnglishWords
{
    public partial class LearnWindow : Window
    {
        DictionaryWord words = new DictionaryWord();
        LearnWords learnWords = new LearnWords();
        //статистика по словам
        struct StatisticWord
        {
            public int true_Answer { get; set; }
            public int wrong_Answer { get; set; }
            public StatisticWord(int true_Answer, int wrong_Answer)
            {
                this.true_Answer = true_Answer;
                this.wrong_Answer = wrong_Answer;
            }
        }
        Dictionary<string, StatisticWord> statisticWord;
        //общая статистика
        public int total_true_answer { get; set; }
        public int total_skip_words { get; set; }
        public int total_words { get; set; }
        //кол нажатий энтер в норм  моде
        internal static short enter_down_count = 0;        

        //конструктор класса
        public LearnWindow(string path)
        {
            InitializeComponent();
            this.words.path = path;
            words.ReadFile(this.words.Words, this.words.path);


            statisticWord = new Dictionary<string, StatisticWord>();
            WordsToStatistic();
            total_true_answer = 0;
            total_skip_words = 0;
            total_words = 0;

            NewWord();
            Txt_Get.Focus();
        }

        //статистика
        //слова для статистики
        public void WordsToStatistic()
        {
            foreach (var item in words.Words)
            {
                statisticWord.Add(item.Key, new StatisticWord(0, 0));
            }
        }
        //обновление статистики по словам
        public void StatisticUpdate()
        {
            this.lst_Statistics.ItemsSource = null;
            this.lst_Statistics.ItemsSource = statisticWord;
        }
        //ключ слова для записи в статистику
        public string EngWordKey()
        {
            string word = Lbl_Word.Content.ToString();
            if (words.Words.ContainsKey(word))
                return word;

            foreach (var item in words.Words)
            {
                if (item.Value.Translate == word)
                    return word = item.Key;
            }
            return null;
        }

        //
        //логика изучения
        private void Learn(string word, string translate)
        {
            // статистика
            StatisticWord sts = new StatisticWord();
            sts = statisticWord[EngWordKey()];

            if (translate.Length > 0 && learnWords.СompareWords(word, translate, this.words.Words) == true)
            {
                //статистика
                sts.true_Answer++;
                total_true_answer++;
                total_words++;
                //
                Lbl_Check.Foreground = Brushes.DarkGreen;
                Lbl_Check.Content = "True";
            }
            if (learnWords.СompareWords(word, translate, this.words.Words) == false)
            {
                //статистика
                sts.wrong_Answer++;
                total_words++;
                //
                Lbl_Check.Foreground = Brushes.Red;
                Lbl_Check.Content = "Wrong";
                Lbl_Correct.Content = learnWords.Correct(word, this.words.Words).ToString();
            }
            //статистика                    
            statisticWord[EngWordKey()] = sts;
            StatisticUpdate();
            Lbl_TrueAnswer.Content = total_true_answer;
            Lbl_TotalWords.Content = total_words;
        }

        //
        //норм мод
        private void Txt_Get_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                enter_down_count++;
                if (enter_down_count == 1)
                {
                    Learn(Lbl_Word.Content.ToString(), Txt_Get.Text);
                    Txt_Get.IsReadOnly = true;
                }
                if (enter_down_count == 2)
                {
                    NewWord();
                    Txt_Get.IsReadOnly = false;
                    enter_down_count = 0;
                }
            }
        }

        //
        //изи мод 
        private void Btn_Easy_Click(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();
            btn = e.Source as Button;
            Learn(Lbl_Word.Content.ToString(), btn.Content.ToString());
            Timer(1500);
        }
        //таймер след слова изи мод
        public void Timer(int milliseconds)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, milliseconds);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            NewWord();
            EasyModeUpdate();
            (sender as DispatcherTimer).Stop();
        }
        //изи мод - обновление слов
        public void EasyModeUpdate()
        {
            Random Rnd = new Random();
            Button[] arrBtn = new Button[] { Btn_Easy_1, Btn_Easy_2, Btn_Easy_3, Btn_Easy_4 };
            string[] arrWords = learnWords.Translate(Lbl_Word.Content.ToString(), words.Words);

            foreach (var item in arrBtn)
            {
                item.Content = null;
            }

            int x = Rnd.Next(4);
            for (int i = 0; i < arrWords.Length; i++)
            {
                arrBtn[i].Content = arrWords[(i + x) % arrWords.Length];
            }
        }

        //
        //обновить слово дляизучения
        public void NewWord()
        {
            Lbl_Word.Content = learnWords.Rnd_Word(words.Words);
            foreach (var item in this.words.Words)
            {
                if (Lbl_Word.Content.ToString().Equals(item.Key))
                    Lbl_Transcription.Content = "[" + item.Value.Transcription + "]";
                if (Lbl_Word.Content.ToString().Equals(item.Value.Translate))
                    Lbl_Transcription.Content = "-";
            }
            Lbl_Check.Content = "";
            Lbl_Correct.Content = "";
            Txt_Get.Text = "";

            Txt_Get.Focus();
        }

        //изи мод вкл/выкл
        private void Check_Checked(object sender, RoutedEventArgs e)
        {
            Txt_Get.Visibility = Visibility.Hidden;
            Lbl_Correct.Visibility = Visibility.Hidden;
            Easy_Mode.Visibility = Visibility.Visible;
            EasyModeUpdate();
        }
        private void Check_Unchecked(object sender, RoutedEventArgs e)
        {
            Easy_Mode.Visibility = Visibility.Hidden;
            Txt_Get.Visibility = Visibility.Visible;
            Lbl_Correct.Visibility = Visibility.Visible;
            Txt_Get.Focus();
        }

        //кнопка статистика
        private void Btn_StatisticVisible_Click(object sender, RoutedEventArgs e)
        {
            StatisticUpdate();
            Statistics.Visibility = Visibility.Visible;
            this.Txt_Get.Focus();
        }
        private void Btn_StatisticsHide_Click(object sender, RoutedEventArgs e)
        {
            Statistics.Visibility = Visibility.Hidden;
            Txt_Get.Focus();
        }
        //кнопка назад 
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //кнопка пропустить
        private void Btn_Skip_Click(object sender, RoutedEventArgs e)
        {
            NewWord();
            total_skip_words++;
            total_words++;
            Lbl_Skip.Content = total_skip_words;
            Lbl_TotalWords.Content = total_words;
            EasyModeUpdate();
        }
    }
}
