using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeModel _model;

        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Id { get; set; }
        public bool IsTech { get; set; }
        public string StaffPicture64 { get; set; }

        //constructor
        public EmployeeViewModel()
        {
            _model = new EmployeeModel();
        }

        public void GetByEmail()
        {
            try
            {
                Employees emp = _model.GetByEmail(Email);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //
        // find employee using lastname property
        //
        public void GetByLastname()
        {
            try
            {
                Employees emp = _model.GetByLastname(Lastname);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public void GetById()
        {
            try
            {
                Employees emp = _model.GetById(Id);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

        }

        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                List<Employees> allEmployees = _model.GetAll();
                foreach (Employees emp in allEmployees)
                {
                    EmployeeViewModel empVm = new EmployeeViewModel();
                    empVm.Title = emp.Title;
                    empVm.Firstname = emp.FirstName;
                    empVm.Lastname = emp.LastName;
                    empVm.Phoneno = emp.PhoneNo;
                    empVm.Email = emp.Email;
                    empVm.Id = emp.Id;
                    empVm.DepartmentId = emp.DepartmentId;
                    empVm.Timer = Convert.ToBase64String(emp.Timer);
                    if (emp.StaffPicture != null)
                    {
                        empVm.StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                    }
                    allVms.Add(empVm);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }

        public void Add()
        {
            Id = -1;
            try
            {
                Employees emp = new Employees();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.DepartmentId = DepartmentId;
                if (emp.StaffPicture != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                Id = _model.Add(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public int Update()
        {
            UpdateStatus employeesUpdated = UpdateStatus.Failed;
            try
            {
                Employees emp = new Employees();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.Id = Id;
                emp.DepartmentId = DepartmentId;
                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }

                emp.Timer = Convert.FromBase64String(Timer);
                employeesUpdated = _model.Update(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(employeesUpdated);
        }

        public int Delete()
        {
            int employeesDeleted = -1;

            try
            {
                employeesDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return employeesDeleted;
        }

    }
}
