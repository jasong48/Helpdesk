using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HelpdeskTests
{
    public class ModelTests
    {
        [Fact]
        public void Employee_GetByEmailTest()
        {
            EmployeeModel model = new EmployeeModel();
            Employees selectedEmployee = model.GetByEmail("bs@abc.com");
            Assert.NotNull(selectedEmployee);
        }

        [Fact]
        public void Employee_Update()
        {
            EmployeeModel model = new EmployeeModel();
            Employees employeeForUpdate = model.GetByLastname("Goldenberg");

            if (employeeForUpdate != null)
            {
                string oldPhoneNo = employeeForUpdate.PhoneNo;
                string newPhoneNo = oldPhoneNo == "519-555-1235" ? "555-155-5555" : "513-555-1234";
                employeeForUpdate.PhoneNo = newPhoneNo;
            }
            Assert.True(model.Update(employeeForUpdate) == UpdateStatus.Ok);
        }

        [Fact]
        public void Employee_GetById()
        {
            EmployeeModel model = new EmployeeModel();
            Employees selectedEmployee = model.GetById(1);
            Assert.NotNull(selectedEmployee);
        }

        [Fact]
        public void Employee_GetAll()
        {
            EmployeeModel model = new EmployeeModel();
            List<Employees> allEmployees = model.GetAll();
            Assert.True(allEmployees.Count > 0);
        }

        [Fact]
        public void Employee_Add()
        {
            helpdeskContext _db = new helpdeskContext();
            Employees newEmployee = new Employees();
            newEmployee.FirstName = "Jason";
            newEmployee.LastName = "goldy";
            newEmployee.PhoneNo = "(555)555-1234";
            newEmployee.Title = "Mr.";
            newEmployee.DepartmentId = 300;
            newEmployee.Email = "ms@someschool.com";
            _db.Employees.Add(newEmployee);
            _db.SaveChanges();
            Assert.True(newEmployee.Id > 1);
        }


        [Fact]
        public void Employee_Delete()
        {
            helpdeskContext _db = new helpdeskContext();
            Employees selectedEmployee = _db.Employees.FirstOrDefault(emp => emp.FirstName == "Jason");
            if (selectedEmployee != null)
            {
                _db.Employees.Remove(selectedEmployee);
                Assert.True(_db.SaveChanges() == 1);
            }
            else
            {
                Assert.True(false);
            }
        }



        [Fact]
        public void Employee_ConcurrencyTest()
        {
            EmployeeModel model1 = new EmployeeModel();
            EmployeeModel model2 = new EmployeeModel();
            Employees employeeForUpdate1 = model1.GetByLastname("goldy");
            Employees employeeForUpdate2 = model2.GetByLastname("goldy");

            if (employeeForUpdate1 != null)
            {
                string oldPhoneNo = employeeForUpdate1.PhoneNo;
                string newPhoneNo = oldPhoneNo == "529-555-1234" ? "555-355-5555" : "519-555-1234";
                employeeForUpdate1.PhoneNo = newPhoneNo;
                if (model1.Update(employeeForUpdate1) == UpdateStatus.Ok)
                {
                    // need to change the phone # to something else
                    employeeForUpdate2.PhoneNo = "667-666-6666";
                    Assert.True(model2.Update(employeeForUpdate2) == UpdateStatus.Stale);
                }
                else
                    Assert.True(false);
            }
        }

        [Fact]
        public void Employee_LoadPicsTest()
        {
            DALUtil util = new DALUtil();
            Assert.True(util.AddEmployeePicsToDb());
        }
    }
}
