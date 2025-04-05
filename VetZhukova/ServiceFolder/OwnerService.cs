using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    class OwnerService
    {
        public OwnerService() { }

        public List<Owner> GetOwners()
        {
            return App.AC.Owners.ToList();
        }

        public int AddOwner(string fio, string phone, string login, string pass)
        {
            Owner owner = new Owner()
            {
                fio = fio,
                phone = phone,
                login = login,
                password = pass
            };
            App.AC.Owners.Add(owner);
            App.AC.SaveChanges();

            return owner.OwnerID;
        }
    }
}
