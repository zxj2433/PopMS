using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using NPOI.SS.Formula.Atp;

namespace PopMS.ViewModel.INV.inv_recordVMs
{
    public partial class inv_recordBatchVM : BaseBatchVM<inv_record, inv_record_BatchEdit>
    {
        public inv_recordBatchVM()
        {
            ListVM = new inv_recordListVM();
            LinkedVM = new inv_record_BatchEdit();
        }
        public override bool DoBatchEdit()
        {
            List<inventory> Invs = DC.Set<inventory>().Where(r => Ids.Select(x => Guid.Parse(x)).Contains(r.ID)).ToList();
            var records = Invs.Select(r => new inv_record
            {
                InvID = r.ID,
                NewInvID=r.ID,
                FromLocID=r.LocationID,
                ToLocID = LinkedVM.ToLoc.Value,
                Type = RecordType.TSF,
                Qty = r.Stock,
                UserName = LoginUserInfo.ITCode + " | " + LoginUserInfo.Name,
                UpdateTime = DateTime.Now
            }).ToList();
            foreach (var item in Invs)
            {
                item.LocationID = LinkedVM.ToLoc.Value;
            }
            DC.Set<inv_record>().AddRange(records);
            DC.Set<inventory>().UpdateRange(Invs);
            return DC.SaveChanges() > 0 ? true : false;
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class inv_record_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
            AllLocs = DC.Set<area_location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Location);
        }
        public List<ComboSelectListItem> AllLocs { get; set; }
        [Display(Name ="至货位")]
        public Guid? ToLoc { get; set; }
    }

}
