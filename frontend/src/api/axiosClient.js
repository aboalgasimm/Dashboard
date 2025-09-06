import axios from "axios";

const axiosClient = axios.create({
  baseURL: "http://localhost:5000/api", // Backend ASP.NET Core API
  headers: {
    "Content-Type": "application/json",
  },
});

export default axiosClient;
