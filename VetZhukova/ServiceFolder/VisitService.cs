using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova
{
    public class VisitDTO
    {
        public string VisitDate { get; set; }
        public string ServiceName { get; set; }
        public string PatientName { get; set; }
        public string EmployeeName { get; set; }
    }
    class VisitService
    {
        public VisitService() { }

        public List<VisitDTO> GetLastVisit()
        {
            var listVisit = App.AC.Visits
                .AsEnumerable()
                .Where(v => DateTime.TryParse(v.visitDate, out DateTime visitDate) &&
                            visitDate >= DateTime.Now &&
                            visitDate < DateTime.Now.AddDays(7))
                .Join(App.AC.Services, v => v.ServiceID, s => s.ServiceID, (v, s) => new { v, s })
                .Join(App.AC.Patients, vs => vs.v.PatientID, p => p.PatientID, (vs, p) => new { vs, p })
                .Join(App.AC.Employees, vsp => vsp.vs.v.EmployeeID, e => e.EmployeeID, (vsp, e) => new
                {
                    VisitDate = vsp.vs.v.visitDate,
                    ServiceName = vsp.vs.s.serviceName,
                    PatientName = vsp.p.name,
                    EmployeeName = e.fullName
                })
                .ToList()
                .Select(v => new VisitDTO  // Преобразуем в нужный формат после выполнения запроса
                {
                    VisitDate = v.VisitDate,
                    ServiceName = v.ServiceName,
                    PatientName = v.PatientName,
                    EmployeeName = v.EmployeeName
                })
                .ToList();

            return listVisit;
        }

        public List<VisitDTO> GetAllVisit()
        {
            var listVisit = App.AC.Visits
                .Join(App.AC.Services, v => v.ServiceID, s => s.ServiceID, (v, s) => new { v, s })
                .Join(App.AC.Patients, vs => vs.v.PatientID, p => p.PatientID, (vs, p) => new { vs, p })
                .Join(App.AC.Employees, vsp => vsp.vs.v.EmployeeID, e => e.EmployeeID, (vsp, e) => new
                {
                    VisitDate = vsp.vs.v.visitDate,
                    ServiceName = vsp.vs.s.serviceName,
                    PatientName = vsp.p.name,
                    EmployeeName = e.fullName
                })
                .ToList()
                .Select(v => new VisitDTO  // Преобразуем в нужный формат после выполнения запроса
                {
                    VisitDate = v.VisitDate,
                    ServiceName = v.ServiceName,
                    PatientName = v.PatientName,
                    EmployeeName = v.EmployeeName
                })
                .ToList(); 

            return listVisit;
        }

        public bool AddVisit(int employeeID, int patientID, int serviceID, string date, string notes)
        {
            Visit visit = new Visit()
            {
                EmployeeID = employeeID,
                PatientID = patientID,
                ServiceID = serviceID,
                visitDate = date,
                notes = notes
            };
            App.AC.Visits.Add(visit);
            App.AC.SaveChanges();

            return true;
        }
    }
}
