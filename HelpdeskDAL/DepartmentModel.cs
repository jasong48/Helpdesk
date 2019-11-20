using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class DepartmentModel
    {
        private IRepository<Departments> repo;

        public DepartmentModel()
        {
            repo = new HelpdeskRepository<Departments>();
        }

        public List<Departments> GetAll()
        {
            List<Departments> allDepartments = new List<Departments>();

            try
            {
                allDepartments = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allDepartments;
        }
    }
}
