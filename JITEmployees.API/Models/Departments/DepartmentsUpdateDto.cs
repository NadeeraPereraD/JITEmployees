namespace JITEmployees.API.Models.Departments
{
    public class DepartmentsUpdateDto
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
    }
}
