﻿using Microsoft.Build.Framework;

namespace FinalWebProject.ViewModel.Admin.WarehouseManagement
{
    public class WarehouseViewModelAdd
    {
        [Required]
        public string WarehouseName { get; set; }
        [Required]
        public string WarehouseLocation { get; set; }
    }
}
