import api from "./services/api";
export const TOKEN_KEY = "hw_auth";
export const logout = () => localStorage.removeItem(TOKEN_KEY);
export const getToken = () => localStorage.getItem(TOKEN_KEY);

export const isAuthenticated = () => {
    return localStorage.getItem(TOKEN_KEY) !== null
};

export const login = token => {
    localStorage.setItem(TOKEN_KEY, token);
};

export const verificarToken = async () => {
    try {
        const token = getToken();

        if (!token) {
            return false
        }
        
        const result = await api.post("/Autenticacao/Verificartoken", {
            token
        });

        if (!result.success) {
            console.log(result.message || "Erro ao validar token");
        }

        return result.data;
    } catch (error) {
        console.log(error);
        return false;
    }
}


