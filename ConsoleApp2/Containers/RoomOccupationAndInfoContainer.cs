using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ConsoleApp2.models;
namespace ConsoleApp2.Containers
{

    [Serializable]
    class RoomOccupationAndInfoContainer
    {
        List<RoomOccupationAndInfoItem> occupatedRooms;

        public RoomOccupationAndInfoContainer(List<RoomOccupationAndInfoItem> occupatedRooms)
        {
            this.occupatedRooms = occupatedRooms;
        }

        public RoomOccupationAndInfoContainer()
        {
            occupatedRooms = new List<RoomOccupationAndInfoItem>();
        }
        public void addOccupatedRoom(RoomOccupationAndInfoItem ci)
        {
            if (ci != null) occupatedRooms.Add(ci);
        }

        public RoomOccupationAndInfoItem getRoomOccupationAndInfoItem(Int32 id, DateTime from, DateTime to)
            => occupatedRooms.Find(occupatedRoom => occupatedRoom.UserId == id && occupatedRoom.StartOfOccupation == from && occupatedRoom.EndOfOccupation == to);
        
        public List<Int32> getAllOccupatedRoomsId(DateTime dateFrom, DateTime dateTo) =>
            occupatedRooms.Where(roomOccupation => roomOccupation.isBookedOnDate(dateFrom, dateTo))
                .Select(roomOccupation => roomOccupation.RoomId).ToList();
        
       
    }
}
