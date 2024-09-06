/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from 'react';

import apiClient from './../Client/apiClient';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Typography,
    CircularProgress,
    Alert,
    IconButton,
    Button,
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

function UsersList() {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const pageNumber = 1;
    const pageSize = 10;

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await apiClient.get('/Users', {
                    params: { pageNumber : 1, pageSize : 10},
                });
                setUsers(response.data);
            } catch (error) {
                console.error('Failed to fetch users:', error);
                setError('Failed to fetch users');
            } finally {
                setLoading(false);
            }
        };

        fetchUsers();
    }, []);

    const handleEdit = async (userId) => {
        // Handle the Edit action (implement the logic to navigate to the edit page)
    };

    const handleDelete = async (userId) => {
        try {
            console.log(`Deleting user with ID: ${userId}`);
            await apiClient.delete(`/Users/${userId}`);
            setUsers((prevUsers) => ({
                ...prevUsers,
                items: prevUsers.items.filter(user => user.id !== userId),
            }));
        } catch (error) {
            console.error('Failed to delete user:', error);
            setError('Failed to delete user');
        }
    };

    const handleCreateUser = () => {
        history.push('/create-user');
    };

    if (loading)
        return (
            <div style={{ display: 'flex', justifyContent: 'center', marginTop: '20px' }}>
                <CircularProgress />
            </div>
        );

    if (error) return <Alert severity="error">{error}</Alert>;

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                onClick={handleCreateUser}
                style={{ margin: '20px 0' }}
            >
                Create New User
            </Button>
            <TableContainer component={Paper} style={{ marginTop: '20px' }}>
                <Typography variant="h6" component="div" style={{ padding: '16px' }}>
                    Users List
                </Typography>
                {users.items && users.items.length > 0 ? (
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell><strong>First Name</strong></TableCell>
                                <TableCell><strong>Last Name</strong></TableCell>
                                <TableCell><strong>Mobile Number</strong></TableCell>
                                <TableCell><strong>Email</strong></TableCell>
                                <TableCell><strong>Role</strong></TableCell>
                                <TableCell><strong>Actions</strong></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {users.items.map((user) => (
                                <TableRow key={user.id}>
                                    <TableCell>{user.firstName}</TableCell>
                                    <TableCell>{user.lastName}</TableCell>
                                    <TableCell>{user.mobileNumber}</TableCell>
                                    <TableCell>{user.email}</TableCell>
                                    <TableCell>{user.role}</TableCell>
                                    <TableCell>
                                        <IconButton color="primary" onClick={() => handleEdit(user.id)}>
                                            <EditIcon />
                                        </IconButton>
                                        <IconButton color="secondary" onClick={() => handleDelete(user.id)}>
                                            <DeleteIcon />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                ) : (
                    <Typography variant="body1" style={{ padding: '16px' }}>
                        No users found
                    </Typography>
                )}
            </TableContainer>
        </>
    );
}

export default UsersList;
