import { useState } from 'react';
import { AppBar, Toolbar, Typography, IconButton, Menu, MenuItem } from '@mui/material';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

function Nav({ isLoggedIn, onLoginClick, onLogout }) {
    const [anchorEl, setAnchorEl] = useState(null);

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
        <AppBar position="static">
            <Toolbar>
                <div style={{ display: 'flex', flexGrow: 1 }}>
                    <Typography variant="h6" style={{ flexGrow: 1 }}>
                        Kashmir Captain
                    </Typography>
                    <Typography variant="button" style={{ margin: '0 10px' }}>
                        <Link to="/" style={{ color: '#fff', textDecoration: 'none' }}>Home</Link>
                    </Typography>
                    <Typography variant="button" style={{ margin: '0 10px' }}>
                        <Link to="/About" style={{ color: '#fff', textDecoration: 'none' }}>About</Link>
                    </Typography>
                    <Typography variant="button" style={{ margin: '0 10px' }}>
                        <Link to="/Privacy" style={{ color: '#fff', textDecoration: 'none' }}>Privacy</Link>
                    </Typography>
                </div>
                <IconButton edge="end" color="inherit" onClick={(e) => setAnchorEl(e.currentTarget)}>
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
                            <MenuItem onClick={handleChangePassword}>Change Password</MenuItem>
                            <MenuItem onClick={onLogout}>Logout</MenuItem>
                        </>
                    ) : (
                        <MenuItem onClick={onLoginClick}>Login</MenuItem>
                    )}
                </Menu>
            </Toolbar>
        </AppBar>
    );
}

Nav.propTypes = {
    isLoggedIn: PropTypes.bool.isRequired,
    onLoginClick: PropTypes.func.isRequired,
    onLogout: PropTypes.func.isRequired,
};

export default Nav;
