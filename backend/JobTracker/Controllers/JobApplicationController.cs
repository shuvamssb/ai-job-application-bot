using Microsoft.AspNetCore.Mvc;
using JobTracker.Data;
using JobTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
/*
GET
{
  "jobTitle": "Software Engineer",
  "company": "Google",
  "location": "Bangalore",
  "status": "Searching",
  "appliedDate": "2025-03-30T00:00:00"
    "portal": "Test Linkedin",
  "jobLink": "Test Linkedin Link"
}
PUT
GET
{
    id: "1",
  "jobTitle": "Software Engineer",
  "company": "Google",
  "location": "Bangalore",
  "status": "Searching",
  "appliedDate": "2025-03-30T00:00:00"
    "portal": "Test Linkedin",
  "jobLink": "Test Linkedin Link"
}

*/

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

        // GET: api/JobApplication/{id}
        [HttpGet("{id}")]

        public async Task<ActionResult<JobApplication>> GetJobApplication(int id)
        {
            // Find the job application by ID
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound($"Job application with ID = {id} not found.");
            }

            return Ok(jobApplication);
        }


        // POST: api/JobApplication
        [HttpPost]
        public async Task<ActionResult<JobApplication>> AddJobApplication(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobApplications), new { id = jobApplication.Id }, jobApplication);
        }

        // PUT: api/JobApplication/{id}
        [HttpPut("{id}")] // Maps the endpoint to: api/JobApplication/1 
        public async Task<ActionResult> UpdateJobApplication(int id, JobApplication updatedJob)
        {
            // Step 1: Check if the ID from the route matches the ID from the request body
            if (id != updatedJob.Id)
            {
                return BadRequest("Job ID mismatch."); // If IDs don't match, return a bad request error
            }

            // Step 2: Find the existing job application by ID
            var existingJob = await _context.JobApplications.FindAsync(id);
            if (existingJob == null) // If no such job exists, return not found
            {
                return NotFound($"No job application found with ID = {id}");
            }

            // Step 3: Update the job details with new values from the request
            existingJob.JobTitle = updatedJob.JobTitle;
            existingJob.Company = updatedJob.Company;
            existingJob.Location = updatedJob.Location;
            existingJob.Status = updatedJob.Status;
            existingJob.AppliedDate = updatedJob.AppliedDate;
             existingJob.Portal = updatedJob.Portal;      // New field
    existingJob.JobLink = updatedJob.JobLink;    // New field

            try
            {
                // Step 4: Save the updated data to the database
                await _context.SaveChangesAsync();
                return NoContent(); // Return 204 No Content, indicating success
            }
            catch (DbUpdateConcurrencyException) // If something goes wrong during update
            {
                return StatusCode(500, "Error updating the job application.");
            }
        }
        // DELETE: api/JobApplication/{id}
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            // Step 1: Find the existing job application by ID
            var existingJob = await _context.JobApplications.FindAsync(id);
            if (existingJob == null) // If no such job exists, return not found
            {
                return NotFound($"No job application found with ID = {id}");
            }

            // Step 2: Remove the job application from the database
            _context.JobApplications.Remove(existingJob);
            await _context.SaveChangesAsync();

            // Step 3: Return 204 No Content to indicate successful deletion
            return NoContent();
        }

        // GET: api/JobApplication/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<JobApplication>>> SearchJobApplications(
            [FromQuery] string? jobTitle,
            [FromQuery] string? company,
            [FromQuery] string? location,
            [FromQuery] string? status,
             [FromQuery] string? portal,
              [FromQuery] string? jobLink,
              [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            // Start with the complete list of job applications
            var query = _context.JobApplications.AsQueryable();

            // Apply filters dynamically based on query parameters
            if (!string.IsNullOrEmpty(jobTitle))
                query = query.Where(j => j.JobTitle != null && j.JobTitle.Contains(jobTitle));

            if (!string.IsNullOrEmpty(company))
                query = query.Where(j => j.Company != null && j.Company.Contains(company));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(j => j.Location != null && j.Location.Contains(location));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(j => j.Status != null && j.Status.Contains(status));

            if (!string.IsNullOrEmpty(portal))
    query = query.Where(j => j.Portal != null && j.Portal.Contains(portal));

if (!string.IsNullOrEmpty(jobLink))
    query = query.Where(j => j.JobLink != null && j.JobLink.Contains(jobLink));


            // Execute the query and return results
            var result = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
            return Ok(result);
        }




    }
}
