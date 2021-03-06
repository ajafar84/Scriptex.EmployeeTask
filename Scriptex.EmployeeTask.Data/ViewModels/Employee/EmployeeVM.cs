﻿using Scriptex.EmployeeTask.Data.ViewModels.Job;
using System;

namespace Scriptex.EmployeeTask.Data.ViewModels.Employee
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JobId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string NationalId { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }

        public JobVM Job { get; set; }
    }
}
