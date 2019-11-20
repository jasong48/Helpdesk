using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class DepartmentViewModel
    {
        private DepartmentModel _model;

        public int Id { get; set; }
        public string Name { get; set; }
        public String Timer { get; set; }

        //constructor 
        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                List<Departments> allDepartments = _model.GetAll();
                foreach (Departments div in allDepartments)
                {
                    DepartmentViewModel divVm = new DepartmentViewModel();
                    divVm.Name = div.DepartmentName;
                    divVm.Id = div.Id;
                    divVm.Timer = Convert.ToBase64String(div.Timer);
                    allVms.Add(divVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);
            }
            return allVms;
        }

    }
}
