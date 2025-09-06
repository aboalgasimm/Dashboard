import { useEffect, useState } from "react";
import analyticsApi from "../api/analyticsApi";
import KpiCard from "../components/Dashboard/KpiCard";
import ChartDailyViews from "../components/Dashboard/ChartDailyViews";

export default function DashboardPage() {
  const [kpis, setKpis] = useState({});
  const [dailyViews, setDailyViews] = useState([]);

  useEffect(() => {
    async function fetchData() {
      const kpiRes = await analyticsApi.getKpis({ from: "2025-08-01", to: "2025-09-01" });
      const dailyRes = await analyticsApi.getDailyViews({ from: "2025-08-01", to: "2025-09-01" });

      setKpis(kpiRes.data);
      setDailyViews(dailyRes.data);
    }
    fetchData();
  }, []);

  return (
    <div className="p-6 space-y-6">
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <KpiCard title="Total Views" value={kpis.totalViews || 0} />
        <KpiCard title="Avg Time on Page" value={`${kpis.avgTime || 0} sec`} />
        <KpiCard title="Bounce Rate" value={`${kpis.bounceRate || 0}%`} />
      </div>
      <ChartDailyViews data={dailyViews} />
    </div>
  );
}
