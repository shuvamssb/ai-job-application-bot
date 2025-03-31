import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5248/api', // Change the port if needed
  headers: {
    'Content-Type': 'application/json',
  },
});

export default axiosInstance;
