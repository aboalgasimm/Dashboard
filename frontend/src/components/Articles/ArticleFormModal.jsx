import { useState, useEffect } from "react";

export default function ArticleFormModal({ open, onClose, onSave, initialData }) {
  const [form, setForm] = useState({
    title: "",
    category: "",
    published_at: "",
    details: { summary: "", hero_image_url: "", reading_time_seconds: 0 },
  });

  useEffect(() => {
    if (initialData) setForm(initialData);
  }, [initialData]);

  if (!open) return null;

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name.startsWith("details.")) {
      const key = name.split(".")[1];
      setForm((prev) => ({
        ...prev,
        details: { ...prev.details, [key]: value },
      }));
    } else {
      setForm((prev) => ({ ...prev, [name]: value }));
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave(form);
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-40">
      <div className="bg-white p-6 rounded-xl shadow-lg w-full max-w-lg">
        <h2 className="text-xl font-bold mb-4">
          {form.id ? "Edit Article" : "New Article"}
        </h2>

        <form onSubmit={handleSubmit} className="space-y-4">
          <input
            type="text"
            name="title"
            value={form.title}
            onChange={handleChange}
            placeholder="Title"
            className="w-full p-2 border rounded"
            required
          />
          <input
            type="text"
            name="category"
            value={form.category}
            onChange={handleChange}
            placeholder="Category"
            className="w-full p-2 border rounded"
            required
          />
          <input
            type="date"
            name="published_at"
            value={form.published_at?.split("T")[0] || ""}
            onChange={handleChange}
            className="w-full p-2 border rounded"
            required
          />
          <textarea
            name="details.summary"
            value={form.details.summary}
            onChange={handleChange}
            placeholder="Summary"
            className="w-full p-2 border rounded"
          />
          <input
            type="text"
            name="details.hero_image_url"
            value={form.details.hero_image_url}
            onChange={handleChange}
            placeholder="Hero Image URL"
            className="w-full p-2 border rounded"
          />
          <input
            type="number"
            name="details.reading_time_seconds"
            value={form.details.reading_time_seconds}
            onChange={handleChange}
            placeholder="Reading Time (seconds)"
            className="w-full p-2 border rounded"
          />

          <div className="flex justify-end space-x-2">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-2 bg-gray-200 rounded"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="px-4 py-2 bg-blue-600 text-white rounded"
            >
              Save
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
