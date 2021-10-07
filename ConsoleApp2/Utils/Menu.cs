using System;
using System.Collections.Generic;

using ConsoleApp2.models;

namespace ConsoleApp2.Utils
{
    class Menu
    {

        private readonly Dictionary<String, Delegate> menuLogicCallbacks = new Dictionary<String, Delegate>();
        

        private readonly String checkInDateString = "Введите дату заселения: ";
        private readonly String checkOutDateString = "Введите дату выселения: ";
        private readonly String dateParseErrorString = "Введите дату в правильном формате. Пример: 04.04.2003";

        
        public Menu(Func<User> createUserCallBack, Func<Room> addRoomCallback, Action bookRoomCallback,
            Action makeBusyRoomCallback, Action logOutCallBack,
            Action endWorkCallback
            )
        {
            menuLogicCallbacks.Add("создать пользователя", createUserCallBack);
            menuLogicCallbacks.Add("добавить комнату", addRoomCallback);
            menuLogicCallbacks.Add("забронировать комнату на дату", bookRoomCallback);
            menuLogicCallbacks.Add("заселится в комнату", makeBusyRoomCallback);
            menuLogicCallbacks.Add("выйти", logOutCallBack);
            menuLogicCallbacks.Add("завершить работу", endWorkCallback);
        }
        
        public void PrintToConsoleNoSuchUser()
        {
            Console.WriteLine("Такого пользователя не существует");
        }
        public void PrintNumOfTryLeft(Int32 tryCount)
        {
            Console.WriteLine($"Осталось попыток: {tryCount}");
        }

        public void ClearConsole() => Console.Clear();

        public Int32 GetUserPasportNumberFromConsole()
        {
             var number = User.GetPassportNumbeFromConsole();
             return number;
        }

       

        public String GetInputLoginStringFromConsole()
        {
            Console.WriteLine("Войти|Создать пользователя");
            var strFromInput = Console.ReadLine().Trim().ToLower();
            return strFromInput;
        }


        public Int32 GetSelectedRoomNumberFromConsole()
        {
            Int32 selectedRoomNumber;
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите номер комнаты которую вы хотите забронировать: ");
                    selectedRoomNumber = Int32.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите числом номер комнаты!");
                }
            }
            return selectedRoomNumber;
        }
        public void printRoomsToConsole(List<Room> rooms){
            if (rooms.Count == 0)
            {
                Console.WriteLine("Свободных комнат на данную дату нет");
               
            }

            rooms.ForEach(room => Console.WriteLine(room));
        }
        public void GetDateFromConsole(out DateTime dateFrom, out DateTime dateTo, RoomOccupation op)
        {
            while (true)
            {
                try
                {
                    if (op == RoomOccupation.Busy)
                    {
                        dateFrom = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine(checkInDateString);
                        dateFrom = DateTime.Parse(Console.ReadLine());
                    }
                    Console.WriteLine(checkOutDateString);

                    dateTo = DateTime.Parse(Console.ReadLine());
                    break;

                }
                catch (FormatException)
                {
                    Console.Write(dateParseErrorString);
                }

            }
        }

       
        public void MenuLogic()
        {
            Console.WriteLine($"|Добавить комнату|Забронировать комнату на дату|Заселится в комнату|Выйти|Завершить работу");

            var inputString = Console.ReadLine().Trim().ToLower();

            
            if (menuLogicCallbacks.ContainsKey(inputString)) menuLogicCallbacks[inputString].DynamicInvoke();

           }
        
    }
}
