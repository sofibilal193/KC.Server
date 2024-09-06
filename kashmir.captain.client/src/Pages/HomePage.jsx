import { useState, useEffect } from 'react';
import { Container, Typography } from "@mui/material";
import { AppBar, Toolbar, IconButton, Menu, MenuItem } from "@mui/material";
import { Link } from "react-router-dom";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";

import apiClient from '../Client/apiClient';

import Top from "../components/Top";
import Foot from "../components/Foot";
import LoginForm from "../components/Login";
import Body from "../components/Body";

function HomePage() {
    const [isLoginFormOpen, setLoginFormOpen] = useState(false);
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [anchorEl, setAnchorEl] = useState(null);
    const [error, setError] = useState(null);

    const onLoad = () => {
        console.log("HomePage loaded.");
        const token = localStorage.getItem('authToken');
        if (token) {
            setIsLoggedIn(true);
        }
    };

    // Call onLoad when the component mounts
    useEffect(() => {
        onLoad();
    }, []); // Empty dependency array ensures this runs only on mount
    const handleLoginClick = () => {
        setLoginFormOpen(true);
    };

    const handleLogout = async  () => {
        try {
            const response = await apiClient.post('/Auth/logout');
            localStorage.removeItem('authToken');
            setIsLoggedIn(false);
            console.log(response);
        } catch (err) {
            setError(err.response?.data?.message || 'Login failed');
            console.log(error);
        } finally {
            setIsLoggedIn(false);
            setAnchorEl(null);
        }

    };

    const handleCloseLoginForm = () => {
        setLoginFormOpen(false);
    };

    const handleLoginSuccess = () => {
        setIsLoggedIn(true);
        setLoginFormOpen(false);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
    };

    const handleProfile = () => {
        handleMenuClose();
    };

    const handleChangePassword = () => {
        handleMenuClose();
    };

    return (
        <>
            <AppBar position="static">
                <Toolbar>
                    <div style={{ display: "flex", flexGrow: 1 }}>
                        <Typography variant="h6" style={{ flexGrow: 1 }}>
                            Kashmir Captain
                        </Typography>
                        <Typography variant="button" style={{ margin: "0 10px" }}>
                            <Link to="/" style={{ color: "#fff", textDecoration: "none" }}>
                                Home
                            </Link>
                        </Typography>
                        <Typography variant="button" style={{ margin: "0 10px" }}>
                            <Link
                                to="/About"
                                style={{ color: "#fff", textDecoration: "none" }}
                            >
                                About
                            </Link>
                        </Typography>
                        <Typography variant="button" style={{ margin: "0 10px" }}>
                            <Link
                                to="/Privacy"
                                style={{ color: "#fff", textDecoration: "none" }}
                            >
                                Privacy
                            </Link>
                        </Typography>
                    </div>
                    <IconButton
                        edge="end"
                        color="inherit"
                        onClick={(e) => setAnchorEl(e.currentTarget)}
                        sx={{
                            border: isLoggedIn ? '2px solid blue' : '2px solid red',
                        }}
                    >
                        <AccountCircleIcon />
                    </IconButton>
                    <Menu
                        anchorEl={anchorEl}
                        open={Boolean(anchorEl)}
                        onClose={handleMenuClose}
                    >
                        {isLoggedIn ? (
                            <>
                                <MenuItem onClick={handleProfile}>Profile</MenuItem>
                                <MenuItem onClick={handleChangePassword}>
                                    Change Password
                                </MenuItem>
                                <MenuItem onClick={handleLogout}>Logout</MenuItem>
                            </>
                        ) : (
                            <MenuItem onClick={handleLoginClick}>Login</MenuItem>
                        )}
                    </Menu>
                </Toolbar>
            </AppBar>
            <Top />
            <Container
                style={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    height: "100vh",
                }}
            >
                <Body></Body>
            </Container>
            <LoginForm
                open={isLoginFormOpen}
                handleClose={handleCloseLoginForm}
                onLoginSuccess={handleLoginSuccess}
            />
            <Foot />
        </>
    );
}

export default HomePage;
