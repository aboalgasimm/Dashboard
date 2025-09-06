const analyticsApi = {
  getKpis: (params) => axiosClient.get("/analytics/kpis", { params }),
  getDailyViews: (params) => axiosClient.get("/analytics/daily-views", { params }),
  getTopArticles: (params) => axiosClient.get("/analytics/top-articles", { params }),
};
