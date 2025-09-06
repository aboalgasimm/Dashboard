import axiosClient from "./axiosClient";

const pageviewsApi = {
  getAll: (params) => axiosClient.get("/pageviews", { params }),
};

export default pageviewsApi;
