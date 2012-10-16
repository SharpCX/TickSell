using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TickSell.Model
{
    public static class Translaters
    {
        public static TimeCellSeat TranseSeatToTimeCellSeat(Seat seat)
        {
            return new TimeCellSeat
                    {
                        ColIndex = seat.ColIndex,
                        CreatDate = DateTime.Now.ToString(),
                        ID = Guid.NewGuid(),
                        IsSold = seat.IsSold,
                        IsUsing = seat.IsUsing,
                        RowIndex = seat.RowIndex,
                        SeatName = seat.SeatName,
                        SeatIndex = seat.SeatIndex,
                        SeatType = seat.SeatType,
                        TicketPrice=seat.TicketPrice,
                        TicketType=seat.TicketType,
                        SoldUser=null
                    };
        }
    }
}
