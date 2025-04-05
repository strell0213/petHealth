using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.ServiceFolder
{
    interface IProfileService
    {
        string GetNameProfile();
    }
    class ProfileService : IProfileService
    {
        public string GetNameProfile()
        {
            var prof = App.AC.Employees.Where(x => x.EmployeeID == GlobalSettings.IDUser).FirstOrDefault();
            return prof.login;
        }
    }
}
