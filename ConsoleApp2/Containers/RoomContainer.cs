using System;
using System.Collections.Generic;
using System.Linq;


using ConsoleApp2.models;
namespace ConsoleApp2.Containers
{
    [Serializable]
    class RoomContainer
    {
        private List<Room> rooms;
       
        public RoomContainer(List<Room> rooms)
        {
            this.rooms = rooms;
        }

        
        public RoomContainer() {
            rooms = new List<Room>();
        }

        public void AddRoom(Room rm)
        {
            if (rm != null) rooms.Add(rm);
        }

        public List<Room> GetRoomsExceptedById(List<Int32> roomsId)
        {
            var setOfIds = new HashSet<Int32>(roomsId);


            return rooms.Where(user => !setOfIds.Contains(user.Id)).ToList();

        }
    }
}
