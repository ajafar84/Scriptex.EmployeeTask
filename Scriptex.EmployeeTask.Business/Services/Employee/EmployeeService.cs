using Scriptex.EmployeeTask.Business.Services;
using System;
using Scriptex.EmployeeTask.Data.ViewModels.Employee;
using Scriptex.EmployeeTask.Common.Interfaces;
using Scriptex.EmployeeTask.Common.Enums;
using Scriptex.EmployeeTask.Data.ViewModels.Employee.Page;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using Scriptex.EmployeeTask.Data.ViewModels.Job;
using System.Linq;

namespace Scriptex.EmployeeTask.Business.Services.Employee
{
    public class EmployeeService : BaseService<Data.Models.Employee, EmployeePostVM>
    {
        public override IResponse Create(EmployeePostVM postVM)
        {
            var employee = _mapper.Map<EmployeePostVM, Data.Models.Employee>(postVM);
            employee.CreationDate = DateTime.Now;

            _unitOfWork.Employees.Add(employee);
            _unitOfWork.Complete();

            return GetResponse(true, employee.Id);
        }

        public override IResponse Edit(EmployeePostVM postVM)
        {
            var employee = _unitOfWork.Employees.Find(postVM.Id);

            #region Validations
            if (employee == null)
                GetResponse(false, FailReason.InvalidId);
            #endregion

            _unitOfWork.Employees.Update(employee, postVM);
            _unitOfWork.Complete();

            return GetResponse(true, employee.Id);
        }

        public override IResponse GetCreate()
        {
            var employeeCreatePageVM = new EmployeeCreatePageVM
            {
                JobsVM = _unitOfWork.Jobs.Where(j => j.IsActive).ProjectTo<JobVM>(_mapConfig).ToList()
            };

            return GetResponse(true, employeeCreatePageVM);
        }

        public override IResponse GetEdit(int id)
        {
            var employee = _unitOfWork.Employees
                .Where(e => e.Id == id)
                .ProjectTo<EmployeeVM>(_mapConfig)
                .FirstOrDefault();

            #region Validations
            if (employee == null)
                GetResponse(false, FailReason.InvalidId);
            #endregion

            var employeeEditPageVM = new EmployeeEditPageVM
            {
                EmployeeVM = employee,
                JobsVM = _unitOfWork.Jobs.Where(j => j.IsActive).ProjectTo<JobVM>(_mapConfig).ToList()
            };

            return GetResponse(true, employeeEditPageVM);
        }

        public override IResponse Delete(int id)
        {
            var employee = _unitOfWork.Employees.Find(id);

            #region Validations
            if (employee == null)
                GetResponse(false, FailReason.InvalidId);
            #endregion

            _unitOfWork.Employees.Remove(employee);
            _unitOfWork.Complete();

            return GetResponse(true, id);
        }

        public IResponse ChangeStatus(int id, bool status)
        {
            var employee = _unitOfWork.Employees.Find(id);

            #region Validations
            if (employee == null)
                GetResponse(false, FailReason.InvalidId);
            #endregion

            employee.IsActive = status;
            _unitOfWork.Complete();

            return GetResponse(true, id);
        }
    }
}
