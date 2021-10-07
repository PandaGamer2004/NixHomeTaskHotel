using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.models
{
    [Serializable]
    enum RoomOccupation
    {
        Booked,
        Busy
    }
    [Serializable]
    class RoomOccupationAndInfoItem
    {
        RoomOccupation roomOccupation;
        Int32 userId;
        Int32 roomId;
        DateTime startOfOccupation;
        DateTime endOfOccupation;

        public RoomOccupationAndInfoItem(RoomOccupation roomOccupation, int userId, int roomId, DateTime startOfOccupation, DateTime endOfOccupation)
        {
            this.roomOccupation = roomOccupation;
            this.userId = userId;
            this.roomId = roomId;
            this.startOfOccupation = startOfOccupation;
            this.endOfOccupation = endOfOccupation;
        }


        public int UserId { get => userId;}
        public int RoomId { get => roomId; }
        public RoomOccupation RoomOccupation { get => roomOccupation; set => roomOccupation = value; }
        public DateTime StartOfOccupation { get => startOfOccupation; set => startOfOccupation = value; }
        public DateTime EndOfOccupation { get => endOfOccupation; set => endOfOccupation = value; }

        public bool isBookedOnDate(DateTime from, DateTime to)
        {
            if(from > to)
            {
                (from, to) = (to, from);
            }
            var res = (from >= startOfOccupation && from <= endOfOccupation) || (to >= startOfOccupation && to <= endOfOccupation);
            return res;
        }
    }
}
