import axiosInstance from './axiosInstance';

// Fetch all job applications
export const getJobApplications = async () => {
  try {
    const response = await axiosInstance.get('/JobApplication');
    return response.data;
  } catch (error) {
    console.error('Error fetching job applications:', error);
    throw error;
  }
};

// Add a new job application
export const addJobApplication = async (jobData) => {
  try {
    const response = await axiosInstance.post('/JobApplication', jobData);
    return response.data;
  } catch (error) {
    console.error('Error adding job application:', error);
    throw error;
  }
};

// Update a job application
export const updateJobApplication = async (id, jobData) => {
  try {
    const response = await axiosInstance.put(`/JobApplication/${id}`, jobData);
    return response.data;
  } catch (error) {
    console.error('Error updating job application:', error);
    throw error;
  }
};

// Delete a job application
export const deleteJobApplication = async (id) => {
  try {
    const response = await axiosInstance.delete(`/JobApplication/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error deleting job application:', error);
    throw error;
  }
};
