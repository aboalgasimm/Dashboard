export default function ArticlesTable({ articles, onEdit, onDelete }) {
  return (
    <div className="bg-white shadow-md rounded-xl p-4">
      <h3 className="text-lg font-semibold mb-2">Articles</h3>
      <table className="w-full text-sm text-left">
        <thead className="text-gray-600 border-b">
          <tr>
            <th className="py-2 px-3">Title</th>
            <th className="py-2 px-3">Category</th>
            <th className="py-2 px-3">Published</th>
            <th className="py-2 px-3">Actions</th>
          </tr>
        </thead>
        <tbody>
          {articles.map((article) => (
            <tr key={article.id} className="border-b last:border-none">
              <td className="py-2 px-3">{article.title}</td>
              <td className="py-2 px-3">{article.category}</td>
              <td className="py-2 px-3">
                {new Date(article.published_at).toLocaleDateString()}
              </td>
              <td className="py-2 px-3 space-x-2">
                <button
                  className="text-blue-600 hover:underline"
                  onClick={() => onEdit(article)}
                >
                  Edit
                </button>
                <button
                  className="text-red-600 hover:underline"
                  onClick={() => onDelete(article.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
