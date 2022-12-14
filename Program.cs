// Задание 1
// Создайте приложение «Крестики-Нолики». Пользователь играет с компьютером. При старте игры случайным
// образом выбирается, кто ходит первым. Игроки ходят по
// очереди. Игра может закончиться победой одного из игроков
// или ничьей. Используйте механизмы пространств имён.
// Задание 2
// Добавьте к первому заданию возможность игры с
// другим пользователем.
using static System.Console;

namespace TicTacToe
{
    // Класс лобби(меню) игры.
    class TicTacToeLobby
    {
        static void Main()
        {
            WriteLine("Добро пожаловать в игру \"Крестики-нолики\"!");
            bool IsOk = false;
            string? Value;
            string? StrChoice;

            do {
                WriteLine("Как Вы будете играть? 1 - с компьютером; 2 - с другом: ");                
                Value = Console.ReadLine();
                if (Value.Length != 0 && (Value == "1" || Value == "2"))
                    IsOk = true;
                if (!IsOk)
                    WriteLine("Введите ещё раз!");
            } while (!IsOk);

            do
            {
                TicTacToeGame tTTGame = new TicTacToeGame();
                tTTGame.Game(int.Parse(Value));

                do
                {
                    WriteLine("Хотите снова начать игру? 1 - повторить; 2 - выйти из игры:");
                    StrChoice = Console.ReadLine();
                } while (StrChoice.Length == 0 && (StrChoice != "1" || StrChoice != "2"));
            } while (StrChoice != "2");
            WriteLine("Выход из игры.");
        }
    }

    // Класс игры.
    class TicTacToeGame
    {
        TTTField GameField;

        public TicTacToeGame()
        {
            GameField = new TTTField();
        }

        // Проверка выигрышных комбинаций; если комбинаций нет, то возращает значение "false".
        private bool CheckForWinCombination(int Sign)
        {
            if (GameField.ReturnSign(0, 0) == Sign && GameField.ReturnSign(0, 1) == Sign && GameField.ReturnSign(0, 2) == Sign)
                return true;
            else if (GameField.ReturnSign(0, 0) == Sign && GameField.ReturnSign(1, 1) == Sign && GameField.ReturnSign(2, 2) == Sign)
                return true;
            else if (GameField.ReturnSign(0, 0) == Sign && GameField.ReturnSign(1, 0) == Sign && GameField.ReturnSign(2, 0) == Sign)
                return true;
            else if (GameField.ReturnSign(0, 1) == Sign && GameField.ReturnSign(1, 1) == Sign && GameField.ReturnSign(2, 1) == Sign)
                return true;
            else if (GameField.ReturnSign(0, 2) == Sign && GameField.ReturnSign(1, 2) == Sign && GameField.ReturnSign(2, 2) == Sign)
                return true;
            else if (GameField.ReturnSign(1, 0) == Sign && GameField.ReturnSign(1, 1) == Sign && GameField.ReturnSign(1, 2) == Sign)
                return true;
            else if (GameField.ReturnSign(2, 0) == Sign && GameField.ReturnSign(2, 1) == Sign && GameField.ReturnSign(2, 2) == Sign)
                return true;
            else if (GameField.ReturnSign(0, 2) == Sign && GameField.ReturnSign(1, 1) == Sign && GameField.ReturnSign(2, 0) == Sign)
                return true;
            else
                return false;
        }

        // Ход игрока.
        private void PlayerMove(int Sign)
        {
            // Целочисленные переменные для передачи в методы класса "Поле".
            int IntRow, IntCol;
            // Переменные типа string для получения значений.
            string? StrRow, StrCol;
            // Флаг для проверки правильности введённых данных.
            bool flag = false;

            do
            {
                do
                {
                    WriteLine("\nВведите координаты по \"x\":");
                    StrCol = ReadLine();
                    IntCol = int.Parse(StrCol) - 1; 
                    if (IntCol < 0 || IntCol > 2)
                        WriteLine("Повторите ввод!");
                } while (IntCol < 0 || IntCol > 2);
            
                do
                {
                    WriteLine("Введите координаты по \"y\":");
                    StrRow = ReadLine();
                    IntRow = int.Parse(StrRow) - 1;
                    if (IntRow < 0 || IntRow > 2)
                        WriteLine("Повторите ввод!");
                } while (IntRow < 0 || IntRow > 2);

                if (GameField.ReturnSign(IntRow, IntCol) == 0)
                    flag = true;
                if (!flag)
                    WriteLine("Неправильно введены координаты! Повторите ввод!");
            } while (!flag);

            GameField.SetSign(IntRow, IntCol, Sign);
        }

        // Основной метод игры.
        public void Game(int Choice)
        {
            Clear();
            // "Второй игрок ходит"; "Первый игрок выиграл"; "Второй игрок выиграл".
            bool SecondPlayerIsMoving, FirstPlayerIsWinner, SecondPlayerIsWinner;
            int Random0To1 = new Random().Next(1);
            bool IsOk = false;
            string? Value;
            int FirstPlayerSign, SecondPlayerSign;

            // Определение: кто совершит первый ход.
            if (Random0To1 == 1)
                SecondPlayerIsMoving = true;
            else
                SecondPlayerIsMoving = false;            
            
            // Выбор знака для первого игрока.
            do
            {
                if (Choice == 1)
                    WriteLine("За кого Вы будете играть? 1 - крестик; 2 - нолик: ");
                else
                    WriteLine("За кого будет играть первый игрок? 1 - крестик; 2 - нолик: ");
                Value = Console.ReadLine();
                if (Value.Length != 0 && (Value == "1" || Value == "2"))
                    IsOk = true;
                if (!IsOk)
                    WriteLine("Введите ещё раз!");
            } while (!IsOk);

            // Присаивание знаков.
            if (Value == "1")
            {
                FirstPlayerSign = 1;
                SecondPlayerSign = 2;
            }
            else
            {
                FirstPlayerSign = 2;
                SecondPlayerSign = 1;
            }

            do
            {
                if (Choice == 2 || !SecondPlayerIsMoving)
                {
                    Clear();
                    GameField.PrintField();
                }

                if (SecondPlayerIsMoving)
                {
                    // Ход комьютера.
                    if (Choice == 1)
                    {
                        // WriteLine("Ходит ии!");                        
                        int Row, Col;
                        do
                        {
                            Row = new Random().Next() % 3;
                            Col = new Random().Next() % 3;
                        } while (GameField.ReturnSign(Row, Col) != 0);
                        GameField.SetSign(Row, Col, SecondPlayerSign);
                    }
                    else
                    {
                        // Ход второго игрока.
                        WriteLine("Ходит второй игрок!");
                        PlayerMove(SecondPlayerSign);
                    }
                    SecondPlayerIsMoving = false;
                }
                else
                {
                    // Ход первого игрока.
                    if (Choice == 1)
                    {
                        WriteLine("Вы ходите!");
                    }
                    else
                    {
                        WriteLine("Ходит первый игрок!");
                    }
                    PlayerMove(FirstPlayerSign);
                    SecondPlayerIsMoving = true;
                }

                // Проверка на выигрышные комбинации.
                FirstPlayerIsWinner = CheckForWinCombination(FirstPlayerSign);
                SecondPlayerIsWinner = CheckForWinCombination(SecondPlayerSign);
                                
            } while (!FirstPlayerIsWinner && !SecondPlayerIsWinner && !GameField.IsFull());

            if (FirstPlayerIsWinner)
            {
                if (Choice == 1)
                    WriteLine("Победил игрок!");
                else
                    WriteLine("Победил первый игрок!");
            }
            else if (SecondPlayerIsWinner)
            {
                if (Choice == 1)
                    WriteLine("Победил компьютер!");
                else
                    WriteLine("Победил второй игрок!");
            }
            else
                WriteLine("Ничья!");
        }

    }

    class TTTField
    {
        int[,] Field;
        const int Size = 3;
        public TTTField()
        {
            Field = new int[Size, Size];
        }

        // Возращение знака.
        public int ReturnSign(int Row, int Column)
        { 
            return Field[Row, Column]; 
        }

        // Установка знака.
        public void SetSign(int Row, int Column, int Sign)
        {
            Field[Row, Column] = Sign;                
        }

        // Проверка на заполненость.
        public bool IsFull()
        {
            bool IsFull = true;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                    if (Field[i, j] == 0)
                        IsFull = false;
            }
            return IsFull;
        }

        // Печать поля.
        public void PrintField() 
        {
            for (int i = 0; i < Size; i++)
            {
                if (i == 0)
                    WriteLine(" 1 2 3");

                if (i == 1 || i == 2)
                    WriteLine(" -+-+-");

                for (int j = 0; j < Size; j++)
                {   
                    if (j == 0)
                        Write(i + 1);
                    if (Field[i, j] == 0)
                        Write(" ");
                    
                    if (Field[i, j] == 1)
                        Write("X");
                    
                    if (Field[i, j] == 2)
                        Write("O");

                    

                    if (j == 0 || j == 1)
                        Write("|");

                    if (j == 2)
                        WriteLine();
                }
            }
        }
       
    }
}
