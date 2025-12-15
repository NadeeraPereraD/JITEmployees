namespace JITEmployees.API.Models.Departments
{
    public class DepartmentsDto
    {
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
