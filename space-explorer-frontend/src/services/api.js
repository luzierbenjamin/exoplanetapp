import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5000/api',
})

export const login = async (email, password) => {
    const response = await api.post('/auth/login', { email, password});
    return response.data.token;
}

export const getAuthHeaders = () => ({
    Authorization: `Bearer ${localStorage.getItem('token')}`
})

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export const fetchPlanets = async () => {
    const response = await api.get('/planets')
    return response.data
}

export const fetchEnrichedPlanets = async () => {
  const response = await api.get('/planets/enriched')
  return response.data
}

export default api