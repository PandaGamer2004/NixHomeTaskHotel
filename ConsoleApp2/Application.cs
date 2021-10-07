using System;
using System.Collections.Generic;
using System.Text;


using ConsoleApp2.Utils;
using ConsoleApp2.Containers;
using ConsoleApp2.models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp2
{
    [Serializable]
    class Application
    {
        private static Application appInstance = new Application();
        private RoomOccupationAndInfoContainer occupatedRoomsConatiner = new RoomOccupationAndInfoContainer();
        private UserContainer usersContainer = new UserContainer();
        private RoomContainer roomsContainer = new RoomContainer();
        private User loggedInUser;

        private Menu menu;

        private readonly Int32 triesToGetPassportNumber = 3;
        private bool continueRead = true;
        private Application() {
            menu = new Menu(CreateAndAddUser, CreateAndAddRoom,
                BookRoomCallback, MakeRoomBusyCallback, LogOut,EndWork);
                  
        }
       

        protected User GetUserByPassportNumber(Int32 number)
        {
            return usersContainer.GetUserByPassportNumber(number);
        }
        protected void SetLogedInUser(User u)
        {
            this.loggedInUser = u;
        }
        protected void EndWork()
        {
            continueRead = false;
            LogOut();
            SerializeApplication();
        }
        protected void LogOut()
        {
            loggedInUser = null;
        }

        protected void LogIn()
        {
            var strFromInput = menu.GetInputLoginStringFromConsole();
            if (strFromInput == "войти")
            {

                for (var currentTry = 0; currentTry < triesToGetPassportNumber; currentTry++)
                {
                    menu.PrintNumOfTryLeft(triesToGetPassportNumber - currentTry);
                    var userPasporNumber = menu.GetUserPasportNumberFromConsole();
                    var user = usersContainer.GetUserByPassportNumber(userPasporNumber);
                    if (user == null)
                    {
                        menu.PrintToConsoleNoSuchUser();
                    }
                    else
                    {
                        SetLogedInUser(user);
                        break;
                    }
                }
            }
            else
            {
                var user = CreateAndAddUser();
                SetLogedInUser(user);
            }

            menu.ClearConsole();
        }

        protected void SerializeApplication()
        {

            using (FileStream fs = new FileStream("application.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(fs, this);

            };
        }
        public static Application getSingeltonApplication()
        {
            return appInstance;
        }

        protected User CreateAndAddUser()
        {
            var user = new User();
            user.GetUserFromConsole();
            usersContainer.AddUser(user);
            return user;
        }

        protected Room CreateAndAddRoom()
        {
            var room = new Room();
            room.GetRoomFromConsole();
            roomsContainer.AddRoom(room);
            return room;
        }

        protected List<Room> GetFreeRoomsOnDate(DateTime from, DateTime to)
        {
            var occupatedRoomsId = occupatedRoomsConatiner.getAllOccupatedRoomsId(from, to);

            var freeRooms = roomsContainer.GetRoomsExceptedById(occupatedRoomsId);

            return freeRooms;
        }


        protected Room GetSelectedRoom(List<Room> freeRooms, Int32 selectedRoomNumber)
        {
            return freeRooms.Find(room => room.RoomNumber == selectedRoomNumber);
        }
        protected void BookRoomOnDate(Room selectedRoom, DateTime from, DateTime to)
        {
            
            CreateAndAddRoomOccupationAndInfoItem(RoomOccupation.Booked, selectedRoom.Id, from, to);
        }

        protected void MakeRoomBusyOnDate(DateTime from, DateTime to)
        {
            var occup = occupatedRoomsConatiner.getRoomOccupationAndInfoItem(loggedInUser.Id, from, to);
            if (occup != null)
            {
                occup.RoomOccupation = RoomOccupation.Busy;
            }
        }


        protected void MakeRoomBusyCallback()
        {

            DateTime dateFrom;
            DateTime dateTo;

            menu.GetDateFromConsole(out dateFrom, out dateTo, RoomOccupation.Busy);


            MakeRoomBusyOnDate(dateFrom, dateTo);
        }
        protected void BookRoomCallback()
        {
            DateTime dateFrom;
            DateTime dateTo;
            menu.GetDateFromConsole(out dateFrom, out dateTo, RoomOccupation.Booked);
            var freeRooms = GetFreeRoomsOnDate(dateFrom, dateTo);
            menu.printRoomsToConsole(freeRooms);
            if (freeRooms.Count == 0) return;

            var selectedRoomNumber = menu.GetSelectedRoomNumberFromConsole();

            var selectedRoom = GetSelectedRoom(freeRooms, selectedRoomNumber);
            BookRoomOnDate(selectedRoom, dateFrom, dateTo);
        }

      
        protected void CreateAndAddRoomOccupationAndInfoItem(RoomOccupation oc,Int32 roomId, DateTime from, DateTime to)
        {
            var item = new RoomOccupationAndInfoItem(oc, loggedInUser.Id,roomId, from, to);
            occupatedRoomsConatiner.addOccupatedRoom(item);
        }
       

        

        public void RunApplication()
        {
            
         
            while (continueRead)
            {
                Console.Clear();
                if(loggedInUser == null) LogIn();

                if (loggedInUser != null) menu.MenuLogic();
                


                
            }
        }
    }
}
