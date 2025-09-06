import { useEffect, useState } from "react";
import analyticsApi from "../api/analyticsApi";
import KpiCard from "../components/Dashboard/KpiCard";
import ChartDailyViews from "../components/Dashboard/ChartDailyViews";
import ChartTopArticles from "../components/Dashboard/ChartTopArticles";
import PageviewsTable from "../components/Dashboard/PageviewsTable";

export default function DashboardPage() {
  const [kpis, setKpis] = useState({});
  const [dailyViews, setDailyViews] = useState([]);
  const [topArticles, setTopArticles] = useState([]);
  const [dateRange] = useState({ from: "2025-08-01", to: "2025-09-01" });

  useEffect(() => {
    async function fetchData() {
      const kpiRes = await analyticsApi.getKpis(dateRange);
      const dailyRes = await analyticsApi.getDailyViews(dateRange);
      const topRes = await analyticsApi.getTopArticles({ ...dateRange, limit: 5 });

      setKpis(kpiRes.data);
      setDailyViews(dailyRes.data);
      setTopArticles(topRes.data);
    }
    fetchData();
  }, [dateRange]);

  return (
    <div className="p-6 space-y-6">
      {/* KPIs */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <KpiCard title="Total Views" value={kpis.totalViews || 0} />
        <KpiCard title="Avg Time on Page" value={`${kpis.avgTime || 0} sec`} />
        <KpiCard title="Bounce Rate" value={`${kpis.bounceRate || 0}%`} />
      </div>

      {/* Charts */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <ChartDailyViews data={dailyViews} />
        <ChartTopArticles data={topArticles} />
      </div>

      {/* Pageviews Table */}
      <PageviewsTable from={dateRange.from} to={dateRange.to} />
    </div>
  );
}
