import axios from "axios";
import { getToken } from "../auth";

const api = axios.create({
    // baseURL: "http://10.240.10.23:8081/api"
    baseURL: "https://localhost:44359/api"
});

api.interceptors.request.use(async config => {
    const token = getToken();
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

api.interceptors.response.use(result => {
    return { data: result.data, success: result.status == 200 ? true : false, message: result.data.message };
}, error => {
    if (error.response?.status === 500) {
        return Promise.reject(error);
    }

    return { data: error.response?.data?.data, success: error.response?.data?.success, message: error.response?.data?.message, error: error.response?.data?.error };
});

export default api;