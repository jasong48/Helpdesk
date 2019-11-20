using System;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    public partial class Employees : HelpdeskEntity
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public bool? IsTech { get; set; }
        public byte[] StaffPicture { get; set; }

        public virtual Departments Department { get; set; }
    }
}
