﻿using MCS.Entities;

namespace MCS.Models
{
    public class ManageEmployeesViewModel
    {
        public string SearchId { get; set; }
        public string department {  get; set; }
        public List<DeptStaff> Employees { get; set; }
    }
}
