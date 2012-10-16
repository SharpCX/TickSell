//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TickSell.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Cells = new HashSet<Cell>();
            this.Seats = new HashSet<Seat>();
            this.Users1 = new HashSet<User>();
            this.TimeCellSeats = new HashSet<TimeCellSeat>();
        }
    
        public System.Guid ID { get; set; }
        public string UserName { get; set; }
        public string CreatDate { get; set; }
        public string UserLevel { get; set; }
        public Nullable<System.Guid> CreaterId { get; set; }
        public string UserPassword { get; set; }
    
        public virtual ICollection<Cell> Cells { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<User> Users1 { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<TimeCellSeat> TimeCellSeats { get; set; }
    }
}
