import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost5000/api',
});

export const login = async (email, password) => {
    const response = await api.post('/auth/login', { email, password});
    return response.data.token;
};

export const getAuthHeaders = () => ({
    Authorization: `Bearer ${localStorage.getItem('token')}`
})

export default api;