using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace Domain.Entities
    {
        public class Permission
        {
           [Key]
           public string Value { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }

            // ✅ Constructor đầy đủ
            public Permission(string value, string description, bool isActive)
            {
                Value = value;
                Description = description;
                IsActive = isActive;
            }

            // ✅ Constructor mặc định để EF Core hoạt động
            public Permission() { }
        }
    }


