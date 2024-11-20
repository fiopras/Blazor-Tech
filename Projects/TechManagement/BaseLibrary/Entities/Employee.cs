using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CivilId { get; set; }
        public string? FileNumber { get; set; }
        public string? Fullname { get; set; }
        public string? JobName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public string? Other { get; set; }


        // Relationship One to many
        public GeneralDepartement? GeneralDepartements { get; set; }
        public int GeneralDepartementId { get; set; }

        public Departement? Departement { get; set; }
        public int DepartementId { get; set; }

        public Branch? Branch { get; set; }
        public int BranchId { get; set; }

        public Town? Town { get; set; }
        public int TownId { get; set; }
    }
}
