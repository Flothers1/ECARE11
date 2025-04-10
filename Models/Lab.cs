namespace ECARE.Models
{
    public class Lab
    {
        public int Id { get; set; }
        public string LabName { get; set; }

        public ICollection<LabBranch>? LabBranch { get; set; }

    }
}
