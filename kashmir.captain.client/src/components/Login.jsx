import { useState } from 'react';
import { Modal, Box, Typography, FormControlLabel, Checkbox, Button, TextField, Container, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import apiClient from '../Client/apiClient';
import PropTypes from 'prop-types';

function LoginForm({ open, handleClose, onLoginSuccess }) {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
        rememberMe: false, // Add this line to handle "Remember me"
    });

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({
            ...formData,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await apiClient.post('/Account/Login', formData);
            const token = response.data.token;
            localStorage.setItem('authToken', token);
            onLoginSuccess();
        } catch (err) {
            setError(err.response?.data?.message || 'Login failed');
        } finally {
            setLoading(false);
        }
    };

    return (
        <Modal open={open} onClose={handleClose}>
            <Box
                sx={{
                    position: 'absolute',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    width: 400,
                    bgcolor: 'background.paper',
                    border: '2px solid #000',
                    boxShadow: 24,
                    p: 4,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center'
                }}
            >
                <IconButton
                    edge="end"
                    color="inherit"
                    onClick={handleClose}
                    style={{
                        position: 'absolute',
                        top: 8,
                        right: 20,
                    }}
                >
                    <CloseIcon />
                </IconButton>
                <Container maxWidth="xs">
                    <Typography variant="h4" gutterBottom>
                        Login
                    </Typography>
                    <form onSubmit={handleSubmit}>
                        <TextField
                            label="Email"
                            name="email"
                            type="email"
                            value={formData.email}
                            onChange={handleChange}
                            fullWidth
                            margin="normal"
                            required
                        />
                        <TextField
                            label="Password"
                            name="password"
                            type="password"
                            value={formData.password}
                            onChange={handleChange}
                            fullWidth
                            margin="normal"
                            required
                        />
                        <FormControlLabel
                            control={
                                <Checkbox
                                    name="rememberMe"
                                    checked={formData.rememberMe}
                                    onChange={handleChange}
                                />
                            }
                            label="Remember me"
                        />
                        {error && <Typography color="error">{error}</Typography>}
                        <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            fullWidth
                            disabled={loading}
                            style={{ marginTop: '1rem' }}
                        >
                            {loading ? 'Logging in...' : 'Login'}
                        </Button>
                    </form>
                    <br />
                    <Typography component="div">
                        Do not have an Account?{' '}
                        <Button
                            variant="text"
                            color="primary"
                            style={{
                                marginTop: '0',
                                padding: 0,
                                minWidth: 'auto',
                                textTransform: 'none',
                                fontSize: 'inherit',
                                lineHeight: 'inherit',
                                fontWeight: 'normal',
                                display: 'inline',
                            }}
                        >
                            Register
                        </Button>
                    </Typography>
                </Container>
            </Box>
        </Modal>
    );
}

// PropTypes validation
LoginForm.propTypes = {
    open: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    onLoginSuccess: PropTypes.func.isRequired
};

export default LoginForm;
