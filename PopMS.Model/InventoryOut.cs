using System;
using System.Collections.Generic;
using System.Text;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Attributes;

namespace PopMS.Model
{
    [MiddleTable]
    public class inventoryOut:BasePoco
    {
        public inventory Inv { get; set; }
        public Guid InvID { get; set; }
        public ship_pop sp { get; set; }
        public Guid spID { get; set; }
        public int OutQty { get; set; }
    }
}
