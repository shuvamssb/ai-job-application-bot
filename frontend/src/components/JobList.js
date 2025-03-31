import React, { useState, useEffect } from 'react';
import { getJobApplications } from '../api/jobApplicationService';

const JobList = () => {
  const [jobs, setJobs] = useState([]);
  const [loading, setLoading] = useState(true);

  // Fetch job applications when the component loads
  useEffect(() => {
    const fetchJobs = async () => {
      try {
        debugger;
        const data = await getJobApplications();
        setJobs(data);
        setLoading(false);
      } catch (error) {
        console.error('Error loading jobs:', error);
        setLoading(false);
      }
    };

    fetchJobs();
  }, []);

  return (
    <div>
      <h2>Job Applications</h2>
      {loading ? (
        <p>Loading...</p>
      ) : jobs.length > 0 ? (
        <ul>
          {jobs.map((job) => (
            <li key={job.id}>
              <strong>{job.jobTitle}</strong> at {job.company} - {job.location} ({job.status})<br />
              <strong>Portal:</strong> {job.portal} <br />
              <strong>Job Link:</strong> <a href={job.jobLink} target="_blank" rel="noopener noreferrer">View Job</a>
            </li>
          ))}
        </ul>
      ) : (
        <p>No job applications found.</p>
      )}
    </div>
  );
};

export default JobList;
