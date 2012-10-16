using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TickSell.Model;


namespace TickSell
{
   
    public static class GlobalVars
    {
        static TicksModelContainer tkContainer;
        public static List<string> TicketTypeList
        {
            get
            {
                if (tkContainer == null)
                {
                    tkContainer = new TicksModelContainer();
                }
                
                if (tkContainer.SeatTypes.Count() == 0)
                {
                    throw new Exception("请先添加票的类型信息");
                }

                return tkContainer.TickTypes.Select(c => c.Name).ToList();
            }   
        }

        //private static List<string> seatTypeList;   

        public static List<string> SeatTypeList
        {
            get
            {
                if (tkContainer == null)
                {
                    tkContainer = new TicksModelContainer();
                }
                if (tkContainer.SeatTypes.Count() == 0)
                {
                    throw new Exception("请先添加座位类型信息");
                }
                return tkContainer.SeatTypes.Select(c=>c.Name).ToList();
            }
        }

        public static int DefaultPrice=20;
    }
}
