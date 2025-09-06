import { useEffect, useState } from "react";
import pageviewsApi from "../../api/pageviewsApi";

export default function PageviewsTable({ from, to }) {
  const [pageviews, setPageviews] = useState([]);

  useEffect(() => {
    async function fetchPageviews() {
      const res = await pageviewsApi.getAll({ from, to });
      setPageviews(res.data);
    }
    fetchPageviews();
  }, [from, to]);

  return (
    <div className="bg-white shadow-md rounded-xl p-4">
      <h3 className="text-lg font-semibold mb-2">Recent Pageviews</h3>
      <table className="w-full text-sm text-left">
        <thead className="text-gray-600 border-b">
          <tr>
            <th className="py-2 px-3">Date</th>
            <th className="py-2 px-3">Article</th>
            <th className="py-2 px-3">Duration (s)</th>
            <th className="py-2 px-3">Bounce</th>
          </tr>
        </thead>
        <tbody>
          {pageviews.map((pv) => (
            <tr key={pv.id} className="border-b last:border-none">
              <td className="py-2 px-3">{new Date(pv.viewed_at).toLocaleString()}</td>
              <td className="py-2 px-3">{pv.articleTitle}</td>
              <td className="py-2 px-3">{pv.duration_seconds}</td>
              <td className="py-2 px-3">{pv.is_bounce ? "Yes" : "No"}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
