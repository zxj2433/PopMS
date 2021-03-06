﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.ViewModel.INV.inventoryVMs;

namespace PopMS.Controllers
{
    [Area("INV")]
    [ActionDescription("库存报告")]
    public partial class inventorySumController : BaseController
    {
        #region Search
        [ActionDescription("Search")]
        public ActionResult Index()
        {
            var vm = CreateVM<inventoryListVM_Sum>();
            return PartialView(vm);
        }

        [ActionDescription("Search")]
        [HttpPost]
        public string Search(inventoryListVM_Sum vm)
        {
            if (ModelState.IsValid)
            {
                return vm.GetJson(false);
            }
            else
            {
                return vm.GetError();
            }
        }

        #endregion

        [ActionDescription("Export")]
        [HttpPost]
        public IActionResult ExportExcel(inventoryListVM_Sum vm)
        {
            return vm.GetExportData();
        }

    }
}
