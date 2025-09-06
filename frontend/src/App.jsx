import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import DashboardPage from "./pages/DashboardPage";
import ArticlesPage from "./pages/ArticlesPage";

function App() {
  return (
    <Router>
      <div className="min-h-screen bg-gray-100">
        <nav className="bg-white shadow px-6 py-4 flex justify-between">
          <h1 className="font-bold text-xl text-blue-600">Analytics Dashboard</h1>
          <div className="space-x-4">
            <Link to="/" className="text-gray-700 hover:text-blue-600">Dashboard</Link>
            <Link to="/articles" className="text-gray-700 hover:text-blue-600">Articles</Link>
          </div>
        </nav>

        <main className="p-6">
          <Routes>
            <Route path="/" element={<DashboardPage />} />
            <Route path="/articles" element={<ArticlesPage />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
