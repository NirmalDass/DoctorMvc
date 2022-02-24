using System.ComponentModel;

namespace DoctorMvc.Models
{
    public class DepartmentViewModel
    {
        public IEnumerable<Department> Department { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Qualification { get; set; }
        [DisplayName("Department")]
        public string Dept_Id { get; set; }
        public double MobileNo { get; set; }
        public string? AvailableDays { get; set; }
    }
}
