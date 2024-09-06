/* eslint-disable react/prop-types */
// src/components/Layout.jsx
import { Link } from 'react-router-dom';
import './Layout.css'; // Importing CSS for styling

const Layout = ({ children }) => {
    return (
        <div className="layout">
            <header className="header">
                <nav className="navbar">
                    <ul>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/about">About</Link></li>
                        <li><Link to="/contact">Contact</Link></li>
                    </ul>
                </nav>
            </header>
            <main className="main-content">
                {children}
            </main>
            <footer className="footer">
                <p>&copy; 2024 My Application</p>
            </footer>
        </div>
    );
};

export default Layout;
