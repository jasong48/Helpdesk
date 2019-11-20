using HelpdeskViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HelpdeskTests
{
    public class EmployeeViewModelTests
    {
        [Fact]
        public void ViewModelGetbyLastNameTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Goldenberg";
            vm.GetByLastname();
            Assert.NotNull(vm.Lastname);
        }

        [Fact]
        public void ViewModelGetbyIdTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Id = 1;
            vm.GetById();
            Assert.True(vm.Id > 0);

        }

        [Fact]
        public void ViewModelGetAll()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            List<EmployeeViewModel> vms = vm.GetAll();
            Assert.True(vms.Count > 0);
        }

        [Fact]
        public void ViewModelAdd()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Title = "Mr.";
            vm.Firstname = "Jason";
            vm.Lastname = "Goldenberg";
            vm.Email = "ts@abc.com";
            vm.Phoneno = "(555)555-5551";
            vm.DepartmentId = 100;
            vm.DepartmentName = "myDepartment";
            vm.IsTech = true;
            vm.Add();
            Assert.True(vm.Id > 0);
        }

        [Fact]
        public void ViewModelUpdate()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Goldenberg";
            vm.GetByLastname();
            vm.Phoneno = vm.Phoneno == "(555)555-1111" ? "(555)555-2222" : "(555)555-3333";
            int employeesUpdated = vm.Update();
            Assert.True(employeesUpdated > 0);
        }
        [Fact]
        public void ViewModelDelete()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Goldenberg";
            vm.GetByLastname();
            int employeesDeleted = vm.Delete();
            Assert.True(employeesDeleted == 1);
        }

    }
}
