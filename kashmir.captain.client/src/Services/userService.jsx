// userService.js
import apiClient from './../Client/apiClient';

export const getUsers = async () => {
    try {
        const response = await apiClient.get('/Users');
        return response.data;
    } catch (error) {
        console.error('Error fetching users:', error);
        throw error;
    }
};

export const getUserById = async (id) => {
    try {
        const response = await apiClient.get(`/Users/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching user:', error);
        throw error;
    }
};

export const createUser = async (user) => {
    try {
        const response = await apiClient.post('/Users', user);
        return response.data;
    } catch (error) {
        console.error('Error creating user:', error);
        throw error;
    }
};

export const updateUser = async (id, user) => {
    try {
        const response = await apiClient.put(`/Users/${id}`, user);
        return response.data;
    } catch (error) {
        console.error('Error updating user:', error);
        throw error;
    }
};

export const deleteUser = async (id) => {
    try {
        await apiClient.delete(`/Users/${id}`);
    } catch (error) {
        console.error('Error deleting user:', error);
        throw error;
    }
};
