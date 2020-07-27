using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using Microsoft.EntityFrameworkCore;

namespace PopMS.ViewModel.INV.inv_recordVMs
{
    public partial class inv_recordVM : BaseCRUDVM<inv_record>
    {
        public List<ComboSelectListItem> AllInvs { get; set; }
        public List<ComboSelectListItem> AllToLocs { get; set; }
        [Display(Name ="已发放数量")]
        public int UsedQty { get; set; }
        [Display(Name = "已分配数量")]
        public int AlcQty { get; set; }
        [Display(Name = "可用数量")]
        public int EnableQty { get; set; }
        public inv_recordVM()
        {
            SetInclude(x => x.Inv);
            SetInclude(x => x.NewInv);
            SetInclude(x => x.ToLoc);
        }

        protected override void InitVM()
        {
            AllToLocs = DC.Set<area_location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Location);
        }

        public override void DoAdd()
        {
            inventory inv = DC.Set<inventory>().Where(r =>r.ID == Entity.InvID).FirstOrDefault();
            List<inventoryIn> invIns = DC.Set<inventoryIn>().Where(r => r.InvID == Entity.InvID).ToList();
            List<inventoryOut> invOuts = DC.Set<inventoryOut>().Include("sp").Where(r => r.InvID == Entity.InvID).ToList();
            if((Entity.Qty+ invOuts.Sum(r=>r.sp.AlcQty)-inv.Stock)>0)
            {
                MSD.AddModelError("OverStock", "超出最大可用量限制");
                return;
            }
            Entity.UserName = LoginUserInfo.ITCode + " | " + LoginUserInfo.Name;
            Entity.UpdateTime = DateTime.Now;
            if (Entity.Type==RecordType.ADJ)
            {
                inv.Stock += Entity.Qty;
                Entity.FromLocID = inv.LocationID;
                Entity.ToLocID = inv.LocationID;
            }
            if(Entity.Type==RecordType.TSF)
            {
                if(Entity.Qty<=0)
                {
                    MSD.AddModelError("ErroQty", "转移数量不能小于0");
                    return;
                }
                Entity.FromLocID = inv.LocationID;
                if ((inv.Stock-invOuts.Sum(r=>r.sp.AlcQty)) == Entity.Qty)
                {
                    inv.LocationID = Entity.ToLocID.Value;
                    Entity.NewInvID = inv.ID;
                }
                else
                {
                    inv.Stock -= Entity.Qty;
                    inventory NewInv = new inventory
                    {
                        ID=Guid.NewGuid(),
                        LocationID = Entity.ToLocID.Value,
                        Stock = Entity.Qty,
                        PutUser = LoginUserInfo.ITCode + " | " + LoginUserInfo.Name,
                        PutTime = DateTime.Now
                    };
                    Entity.NewInvID = NewInv.ID;
                    int TsfQty = Entity.Qty;
                    List<inventoryIn> NewInInvs = new List<inventoryIn>();
                    foreach (var item in invIns)
                    {
                        if(TsfQty>0)
                        {
                            int CurTsfQty= item.InQty > TsfQty ? TsfQty : item.InQty;
                            //如果当前记录库存大于等于转移数量
                            if(item.InQty>=TsfQty)
                            {
                                inventoryIn invIn = new inventoryIn
                                {
                                    ID = Guid.NewGuid(),
                                    InvID = NewInv.ID,
                                    OrderPopID = item.OrderPopID,
                                    InQty =TsfQty
                                };
                                item.InQty -= TsfQty;
                                DC.Set<inventoryIn>().Update(item);
                                NewInInvs.Add(invIn);
                            }
                            //当前记录库存小于等于转移数量
                            else
                            {
                                inventoryIn invIn = new inventoryIn
                                {
                                    ID = Guid.NewGuid(),
                                    InvID = NewInv.ID,
                                    OrderPopID = item.OrderPopID,
                                    InQty = item.InQty
                                };
                                DC.Set<inventoryIn>().Remove(item);
                                NewInInvs.Add(invIn);
                            }
                            item.InQty-= CurTsfQty;
                            TsfQty -= CurTsfQty;
                        }
                    }                    
                    DC.Set<inventory>().Add(NewInv);
                    DC.Set<inventoryIn>().AddRange(NewInInvs);
                }
            }
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            if(Entity.Type==RecordType.ADJ)
            {
                Entity.Inv.Stock -= Entity.Qty;
                DC.Set<inventory>().Update(Entity.Inv);
            }
            if(Entity.Type==RecordType.TSF)
            {
                var InvIn = DC.Set<inventoryIn>().Where(r => r.InvID == Entity.NewInvID);
                foreach (var item in InvIn)
                {
                    item.InvID = Entity.InvID;
                }
                Entity.Inv.Stock += Entity.Qty;
                DC.Set<inventoryIn>().UpdateRange(InvIn);
                DC.Set<inventory>().Remove(Entity.NewInv);
                DC.Set<inventory>().Update(Entity.Inv);
            }
            base.DoDelete();
        }
    }
}
