import React, { useState } from "react";
import api from "../services/api";

import "./Login.css"

const Login = () => {
    const [login, setLogin] = useState("");
    const [senha, setSenha] = useState("");

    const logar = async (e) => {
        e.preventDefault();

        if (!login || !senha) {
            return alert("Preencha todos campos");
        }

        let result = await api.post("/Autenticacao", {
            login,
            senha
        });

        if (!result.success) {
            return alert("Não foi possivel realizar login!");
        }

        localStorage.setItem("hw_auth", result.data.token);
        window.location.pathname = "/admin"
    }

    return (
        <div className="col-md-12 login-page">
            <form onSubmit={logar} className="form-signin">
                <div className="form-group">
                    <label htmlFor="login"><strong>Login</strong></label>
                    <input type="text" id="login" className="form-control" placeholder="Email address" required autofocusautofocus="true" value={login} onChange={(e) => setLogin(e.target.value)} />
                </div>

                <div className="form-group">
                    <label htmlFor="senha"><strong>Senha</strong></label>
                    <input type="password" id="password" className="form-control" placeholder="Password" required value={senha} onChange={(e) => setSenha(e.target.value)} />
                </div>

                <button className="btn btn-lg btn-primary btn-block" type="submit">Acessar</button>
                <p className="mt-5 mb-3 text-muted text-center">&copy; 2020</p>
            </form>
        </ div>
    )
}

export default Login;