using DoctorMvc.Models;

namespace DoctorMvc.DtoViewModel
{
    public class EditViewModel
    {
        public IEnumerable<Department> Department { get; set; }
        public Doctor Doctor { get; set; }
    }
}
