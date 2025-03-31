using Microsoft.AspNetCore.Mvc;
using JobTracker.Data;
using JobTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace JobTracker.Controllers //groups all controllers under JobTracker.Controllers for better organization.
{
    [Route("api/[controller]")]
    [ApiController] //It enables automatic model validation and JSON request handling, reducing manual error handling.
    public class JobApplicationController : ControllerBase //inherits from ControllerBase, which provides built-in API functionalities like Ok(), NotFound(), etc.
    {
        //Dependency Injection of DbContext
        private readonly JobTrackerDbContext _context;

        public JobApplicationController(JobTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/JobApplication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplication>>> GetJobApplications()
        {
            return await _context.JobApplications.ToListAsync();
        }

        // POST: api/JobApplication
        [HttpPost]
        public async Task<ActionResult<JobApplication>> AddJobApplication(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobApplications), new { id = jobApplication.Id }, jobApplication);
        }
    }
}
