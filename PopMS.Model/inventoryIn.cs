using System;
using System.Collections.Generic;
using System.Text;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Attributes;

namespace PopMS.Model
{
    [MiddleTable]
    public class inventoryIn:BasePoco
    {
        public order_pop OrderPop { get; set; }
        public int OrderPopID { get; set; }
        public inventory Inv { get; set; }
        public Guid InvID { get; set; }
        public int InQty { get; set; }
    }
}
