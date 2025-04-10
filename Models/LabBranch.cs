namespace ECARE.Models
{
    public class LabBranch
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public string LabBranchName { get; set; }

        public ICollection<ApplicationUser>? ApplicationUser { get; set; }
        public Lab? Lab { get; set; }
    }
}
