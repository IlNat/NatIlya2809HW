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
    class TicTacToeLobby
    {
        static void Main()
        {
            WriteLine("Добро пожаловать в игру \"Крестики-нолики\"!");
            bool IsOk = false;
            string? Value;
            do {
                WriteLine("Как Вы будете играть? 1 - с компьютером; 2 - с другом: ");                
                Value = Console.ReadLine();
                if (Value.Length != 0 && (Value == "1" || Value == "2"))
                    IsOk = true;
                if (!IsOk)
                    WriteLine("Введите ещё раз!");
            } while (!IsOk);
            
            TicTacToeGame tTTGame = new TicTacToeGame();
            tTTGame.Game(int.Parse(Value));
        }
    }

    class TicTacToeGame
    {
        TTTField GameField;
        bool GameIsOver;

        public TicTacToeGame()
        {
            GameField = new TTTField();
            GameIsOver = false;
        }

        /*public bool IsGameOver(ref bool GameIsOver)
        {
            if (GameField.ReturnSign(0,0) == )
        }*/

        public void PlayerMove(int Sign)
        {
            int IntRow, IntCol;
            string? StrRow, StrCol;
            bool flag = false;
            do
            {
                do
                {
                    WriteLine("\nВведите координаты по \"x\":");
                    StrCol = ReadLine();
                    IntCol = int.Parse(StrCol) - 1; 
                    if (IntCol < -1 || IntCol > 2)
                        WriteLine("Повторите ввод!");
                } while (IntCol < -1 || IntCol > 2);
                do
                {
                    WriteLine("Введите координаты по \"y\":");
                    StrRow = ReadLine();
                    IntRow = int.Parse(StrRow) - 1;
                    if (IntRow < -1 || IntRow > 2)
                        WriteLine("Повторите ввод!");
                } while (IntRow < -1 || IntRow > 2);
                if (GameField.ReturnSign(IntRow, IntCol) == 0)
                    flag = true;
                if (!flag)
                    WriteLine("Неправильно введены координаты! Повторите ввод!");
            } while (!flag);
            GameField.SetSign(IntRow, IntCol, Sign);
        }

        public void Game(int Choice)
        {
            Clear();
            bool SecondPlayerIsMoving;
            int Random0To1 = new Random().Next(1);
            bool IsOk = false;
            string? Value;
            int FirstPlayerSign;
            int SecondPlayerSign;

            if (Random0To1 == 1)
                SecondPlayerIsMoving = true;
            else
                SecondPlayerIsMoving = false;            
            
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
                /*if (Choice == 2 || !SecondPlayerIsMoving)
                {
                    //Clear();
                    GameField.PrintField();
                }*/
                GameField.PrintField();
                if (SecondPlayerIsMoving)
                {
                    if (Choice == 1)
                    {
                        WriteLine("Ходит ии!");
                        int Row, Col;
                        do
                        {
                            Row = new Random().Next(2);
                            Col = new Random().Next(2);
                        } while (GameField.ReturnSign(Row, Col) != 0);
                        GameField.SetSign(Row, Col, SecondPlayerSign);
                    }
                    else
                    {
                        WriteLine("Ходит второй игрок!");
                        PlayerMove(SecondPlayerSign);
                    }
                    SecondPlayerIsMoving = false;
                }
                else
                {
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


            } while (!GameIsOver);
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

        public int ReturnSign(int Row, int Column)
        { 
            return Field[Row, Column]; 
        }

        public bool SetSign(int Row, int Column, int Sign)
        {
            if (Field[Row, Column] == 0)
            {
                Field[Row, Column] = Sign;
                return true;
            }
            else 
                return false;
        }

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

        public void PrintField() 
        {
            for (int i = 0; i < Size; i++)
            {
                if (i == 1 || i == 2)
                    WriteLine("-+-+-");

                for (int j = 0; j < Size; j++)
                {   
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
