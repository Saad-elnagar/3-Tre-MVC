using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.DAL.Entity
{
    public class ModelBase
    {
        public int Id { get; set; }
        public DateTime  CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
