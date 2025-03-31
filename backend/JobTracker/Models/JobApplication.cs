using System;

namespace JobTracker.Models
{
    public class JobApplication
{
    public int Id { get; set; }
    public string? JobTitle { get; set; }
    public string? Company { get; set; }
    public string? Location { get; set; }
    public string? Status { get; set; }
    public DateTime AppliedDate { get; set; }
     public string? Portal { get; set; }  // New field for job portal name
    public string? JobLink { get; set; }  // New field for job link
}

}
