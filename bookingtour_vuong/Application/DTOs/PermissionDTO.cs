﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PermissionDTO
    {
        public string Value { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
