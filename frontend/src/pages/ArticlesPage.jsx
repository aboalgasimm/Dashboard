import { useEffect, useState } from "react";
import articlesApi from "../api/articlesApi";
import ArticlesTable from "../components/Articles/ArticlesTable";
import ArticleFormModal from "../components/Articles/ArticleFormModal";

export default function ArticlesPage() {
  const [articles, setArticles] = useState([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingArticle, setEditingArticle] = useState(null);
  const [search, setSearch] = useState("");

  useEffect(() => {
    fetchArticles();
  }, []);

  async function fetchArticles() {
    const res = await articlesApi.getAll({ search });
    setArticles(res.data);
  }

  const handleSave = async (article) => {
    if (article.id) {
      await articlesApi.update(article.id, article);
    } else {
      await articlesApi.create(article);
    }
    setModalOpen(false);
    setEditingArticle(null);
    fetchArticles();
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this article?")) {
      await articlesApi.delete(id);
      fetchArticles();
    }
  };

  return (
    <div className="p-6 space-y-6">
      <div className="flex justify-between items-center">
        <input
          type="text"
          placeholder="Search articles..."
          className="p-2 border rounded"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <button
          className="px-4 py-2 bg-blue-600 text-white rounded"
          onClick={() => {
            setEditingArticle(null);
            setModalOpen(true);
          }}
        >
          + New Article
        </button>
      </div>

      <ArticlesTable
        articles={articles}
        onEdit={(article) => {
          setEditingArticle(article);
          setModalOpen(true);
        }}
        onDelete={handleDelete}
      />

      <ArticleFormModal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        onSave={handleSave}
        initialData={editingArticle}
      />
    </div>
  );
}
