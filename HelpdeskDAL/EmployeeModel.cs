using HelpdeskDAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HelpdeskDAL
{
   public class EmployeeModel
   {
        public Employees GetByEmail(string email)
        {
            Employees selectedEmployee = null;

            try
            {
                helpdeskContext _db = new helpdeskContext();
                selectedEmployee = _db.Employees.FirstOrDefault(emp => emp.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedEmployee;
        }

        public Employees GetByLastname(string name)
        {

            List<Employees> selectedEmployee = null;

            try
            {
                selectedEmployee = repository.GetByExpression(emp => emp.LastName == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedEmployee.FirstOrDefault();
        }
        public Employees GetById(int id)
        {
            List<Employees> selectedEmployees = null;

            try
            {
                selectedEmployees = repository.GetByExpression(emp => emp.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedEmployees.FirstOrDefault();
        }

        public List<Employees> GetAll()
        {
            List<Employees> allEmployees = new List<Employees>();

            try
            {
                allEmployees = repository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allEmployees;
        }

        public int Add(Employees newEmployee)
        {
            try
            {
                newEmployee = repository.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newEmployee.Id;
        }

        public UpdateStatus Update(Employees updatedEmployee)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                operationStatus = repository.Update(updatedEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in" + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return operationStatus;
        }

        public int Delete(int id)
        {
            int EmployeesDeleted = -1;

            try
            {
                EmployeesDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return EmployeesDeleted;
        }

        IRepository<Employees> repository;
        public EmployeeModel()
        {
            repository = new HelpdeskRepository<Employees>();
        }


    }
}
