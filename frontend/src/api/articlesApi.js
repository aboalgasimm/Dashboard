import axiosClient from "./axiosClient";

const articlesApi = {
  getAll: (params) => axiosClient.get("/articles", { params }),
  getById: (id) => axiosClient.get(`/articles/${id}`),
  create: (data) => axiosClient.post("/articles", data),
  update: (id, data) => axiosClient.put(`/articles/${id}`, data),
  delete: (id) => axiosClient.delete(`/articles/${id}`),
};

export default articlesApi;
