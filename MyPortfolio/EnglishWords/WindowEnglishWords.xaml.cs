using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyPortfolio.EnglishWords
{
    public partial class WindowEnglishWord : Window
    {
        DictionaryWord words = new DictionaryWord();
        private string pathDelete { get; set; } // доп переменная для удаления файла

        public WindowEnglishWord()
        {
            InitializeComponent();
            words.CreatDirectori();
            words.CreatFile();
            words.ReadFile(this.words.Words, this.words.path);
            MenuFileUpdate();
        }
        /*
         *Верхнее меню
         */
        private void Btn_Menu_AddDictionary_Click(object sender, RoutedEventArgs e)
        {
            AddDictionaryWindow addDictionaryWindow = new AddDictionaryWindow();
            addDictionaryWindow.ShowDialog();
            MenuFileUpdate();
        }
        private void Btn_Menu_Cont_Delete_Click(object sender, RoutedEventArgs e)
        {
            words.DeleteFile(pathDelete);
            MenuFileUpdate();
        }
        private void Btn_Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Btn_About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("2018");
        }
        /////////////////////////////////////////////////////////////////
        /*
         *Основные кнопки
         */
        private void Btn_Translate_Click(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            ListBox.Items.Add(this.words.FindWord(Txt_Box.Text));
        }
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddWordWindow addWordWindow = new AddWordWindow(this.words.path);
            addWordWindow.ShowDialog();
            ShowWords();
        }
        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            this.words.DeleteWord(Txt_Box.Text);
            this.words.WriteToFile(this.words.Words, this.words.path);
            Txt_Box.Clear();
            ShowWords();
        }
        private void Btn_Edite_Click(object sender, RoutedEventArgs e)
        {
            bool add = true;
            foreach (var item in this.words.Words)
            {
                if (item.Key.Equals(Txt_Box.Text))
                {
                    add = false;
                    EditeWindow editeWindow = new EditeWindow(this.words.path, Txt_Box.Text);
                    editeWindow.ShowDialog();
                }
            }
            ShowWords();
            if (add)
            {
                ListBox.Items.Clear();
                ListBox.Items.Add("The word is missing");
            }
        }
        /*
         *Показать слова
         */
        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            ShowWords();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string[] str = ListBox.SelectedItem.ToString().Split();
                Txt_Box.Text = str[0];
            }
            catch { }
        }
        public void ShowWords()
        {
            using (null)
            {
                ListBox.Items.Clear();
                words.ReadFile(this.words.Words, this.words.path);
                foreach (var item in this.words.Words)
                {
                    ListBox.Items.Add($"{item.Key} [{item.Value.Transcription}] {item.Value.Translate}");
                }
            }
        }
        /*
         *Учить слова
         */
        private void Btn_Learn_Click(object sender, RoutedEventArgs e)
        {
            LearnWindow learnWindow = new LearnWindow(this.words.path);
            //learnWindow.ShowDialog();
            learnWindow.Show();
        }
        /*
         *выбор словаря
         */
        private void menuItem_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            //MenuItem menuItem  = e.Source as MenuItem;
            //MenuItem menuItem = (MenuItem)sender;  ;

            words.path = menuItem.Tag.ToString();
            MessageBox.Show(menuItem.Header.ToString(), "Dictionary:");
            ShowWords();
        }
        /*
         * Удаление словаря
         */         
        public void menuItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuItem menuItem = new MenuItem();
            menuItem = e.Source as MenuItem;
            pathDelete = menuItem.Tag.ToString();
        }


        /*
         * Читает фaйлы и добавляет в меню
         */
        public void MenuFileUpdate()
        {
            using (null)
            {
                this.Btn_Menu_SelectDictionary.Items.Clear();

                foreach (var item in Directory.GetFiles("words"))
                {
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = item.Substring(6);
                    menuItem.Tag = item;
                    menuItem.Click += new RoutedEventHandler(menuItem_Checked);
                    //menuItem.MouseEnter += new MouseEventHandler(menuItem_MouseEnter);
                    menuItem.PreviewMouseRightButtonDown += new MouseButtonEventHandler(menuItem_PreviewMouseRightButtonDown);

                    this.Btn_Menu_SelectDictionary.Items.Add(menuItem);
                }
            }
        }
    }
}
