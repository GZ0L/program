using System;

namespace MyPortfolio._2048
{
    class game2048
    {
        //массив 
        public int[,] arr { get; private set; }
        public int score { get; set; }


        Random Rnd = new Random();

        public game2048()
        {
            this.arr = new int[4, 4];
            this.score = 0;
        }

        //очистка массива
        public void ClearArr()
        {
            Array.Clear(arr, 0, arr.Length);
        }
        //начало игры
        public void StartGame()
        {
            score = 0;
            ClearArr();
            arr[Rnd.Next(0, 4), Rnd.Next(0, 4)] = 2;
            arr[Rnd.Next(0, 4), Rnd.Next(0, 4)] = 4;
            arr[Rnd.Next(0, 4), Rnd.Next(0, 4)] = 2;
        }
        //новое число
        public void NewNumber()
        {
            int x, y;
            int[] z = new int[] { 2, 2, 2, 4 };
            bool check = true;

            for (int i = 0; check; i++)
            {
                x = Rnd.Next(0, 4);
                y = Rnd.Next(0, 4);
                if (arr[x, y] == 0)
                {
                    arr[x, y] = z[Rnd.Next(z.Length)];
                    check = false;
                }
            }

        }

        //при нажатии вправо
        public void KeyDownRight()
        {
            bool check = false;
            bool count = true;
            bool countSecond = true;

            for (; count;)
            {
                count = false;
                //смешение вправо
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (arr[i, j] != 0 && j + 1 < 4 && arr[i, j + 1] == 0)
                        {
                            arr[i, j + 1] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            count = true;
                        }
                    }
                }
            }
            //сложение 2х чисел
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j > -1; j--)
                {
                    if (arr[i, j] != 0 && j - 1 > -1 && arr[i, j] == arr[i, j - 1])
                    {
                        score += arr[i, j] + arr[i, j - 1];
                        arr[i, j] += arr[i, j - 1];
                        arr[i, j - 1] = 0;

                        check = true;
                        //break;
                    }
                }
            }
            // // //
            for (; countSecond;)
            {
                countSecond = false;
                //смешение вправо
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (arr[i, j] != 0 && j + 1 < 4 && arr[i, j + 1] == 0)
                        {
                            arr[i, j + 1] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            countSecond = true;
                        }
                    }
                }
            }
            if (check == true)
                NewNumber();
        }
        //при нажатии клавиши влево
        public void KeyDownLeft()
        {
            bool check = false;
            bool count = true;
            bool countSecond = true;

            for (; count;)
            {
                count = false;
                //смешение влево
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 3; j > -1; j--)
                    {
                        if (arr[i, j] != 0 && j - 1 > -1 && arr[i, j - 1] == 0)
                        {
                            arr[i, j - 1] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            count = true;
                        }
                    }
                }
            }
            //сложение 
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] != 0 && j + 1 < 4 && arr[i, j] == arr[i, j + 1])
                    {
                        score += arr[i, j] + arr[i, j + 1];
                        arr[i, j] += arr[i, j + 1];
                        arr[i, j + 1] = 0;

                        check = true;
                        //break;
                    }
                }
            }
            // // //
            for (; countSecond;)
            {
                countSecond = false;
                //смешение влево
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 3; j > -1; j--)
                    {
                        if (arr[i, j] != 0 && j - 1 > -1 && arr[i, j - 1] == 0)
                        {
                            arr[i, j - 1] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            countSecond = true;
                        }
                    }
                }
            }
            if (check == true)
                NewNumber();
        }
        //при нажатии вниз
        public void KeyDownDown()
        {
            bool check = false;
            bool count = true;
            bool countSecond = true;

            for (; count;)
            {
                count = false;
                //смешение вниз
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (arr[i, j] != 0 && i + 1 < 4 && arr[i + 1, j] == 0)
                        {
                            arr[i + 1, j] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            count = true;
                        }
                    }
                }
            }
            //сложение
            for (int i = 3; i > -1; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] != 0 && i - 1 > -1 && arr[i - 1, j] == arr[i, j])
                    {
                        score += arr[i, j] + arr[i - 1, j];
                        arr[i, j] += arr[i - 1, j];
                        arr[i - 1, j] = 0;

                        check = true;
                        //break;
                    }
                }
            }
            // // //
            for (; countSecond;)
            {
                countSecond = false;
                //смешение вниз
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (arr[i, j] != 0 && i + 1 < 4 && arr[i + 1, j] == 0)
                        {
                            arr[i + 1, j] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            countSecond = true;
                        }
                    }
                }
            }
            if (check == true)
                NewNumber();
        }
        //нажатие клавиши вверх
        public void KeyDownUp()
        {
            bool check = false;
            bool count = true;
            bool countSecond = true;

            for (; count;)
            {
                count = false;
                //смешение вверх
                for (int i = 3; i > -1; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (arr[i, j] != 0 && i - 1 > -1 && arr[i - 1, j] == 0)
                        {
                            arr[i - 1, j] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            count = true;
                        }
                    }
                }
            }
            //сложение
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] != 0 && i + 1 < 4 && arr[i + 1, j] == arr[i, j])
                    {
                        score += arr[i, j] + arr[i + 1, j];
                        arr[i, j] += arr[i + 1, j];
                        arr[i + 1, j] = 0;

                        check = true;
                        //break;
                    }
                }
            }
            // // // 
            for (; countSecond;)
            {
                countSecond = false;
                //смешение вверх
                for (int i = 3; i > -1; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (arr[i, j] != 0 && i - 1 > -1 && arr[i - 1, j] == 0)
                        {
                            arr[i - 1, j] = arr[i, j];
                            arr[i, j] = 0;

                            check = true;
                            countSecond = true;
                        }
                    }
                }
            }
            if (check == true)
                NewNumber();
        }


        //конец игры 
        public bool TheEndGame()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] == 0)
                        return false;
                    if (j + 1 < 4 && arr[i, j] == arr[i, j + 1])
                        return false;
                    if (i + 1 < 4 && arr[i, j] == arr[i + 1, j])
                        return false;
                }
            }
            return true;
        }

    }
}
