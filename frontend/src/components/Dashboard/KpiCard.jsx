export default function KpiCard({ title, value }) {
  return (
    <div className="bg-white shadow-md rounded-xl p-4 flex flex-col items-center">
      <h3 className="text-gray-500 text-sm">{title}</h3>
      <p className="text-2xl font-bold text-blue-600">{value}</p>
    </div>
  );
}
