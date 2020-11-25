import React, { useEffect, useState } from 'react';
import DataTable from 'react-data-table-component';
import {
    // BrowserRouter as Router,
    // Switch,
    // Route,
    Link,
    useParams
} from "react-router-dom";
import api from "../../../services/api";

function NovoProduto() {
    const [salvando, setSalvando] = useState(false);
    const [novoNome, setNome] = useState("");
    const [novoValor, setValor] = useState("");

    async function salvar(e) {
        try {
            e.preventDefault();

            if (!novoNome || !novoValor) {
                return alert("Preencha todos campos");
            }

            if(Number.isNaN(parseFloat(novoValor))){
                return alert("Confira que se o valor está correto");
            }

            setSalvando(true);

            const result = await api.post("/Produto", {
                nome: novoNome, valor: parseFloat(novoValor)
            });

            if (result.success) {
                alert("Produto salvo com sucesso");
                window.location.pathname = "/admin/produtos";
            } else {
                alert(result.message || "Não foi possivel criar o Produto");
            }
        } catch (error) {
            console.log(error);
            console.log("Erro Inesperado.");
        } finally {
            setSalvando(false);
        }
    }

    return (
        <>
            <h1 className="mt-4">Novo Produto</h1>
            <hr />
            <div className="row">
                <div className="col-xl-12 col-md-12">
                    <div className="col-md-3">
                        <form onSubmit={salvar}>
                            <div className="form-group">
                                <label htmlFor="nome"><strong>Nome</strong></label>
                                <input type="text" id="nome" className="form-control" placeholder="Nome" required autofocusautofocus="true" value={novoNome} onChange={(e) => setNome(e.target.value)} />
                            </div>

                            <div className="form-group">
                                <label htmlFor="valor"><strong>Valor</strong></label>
                                <input type="text" id="text" className="form-control" placeholder="9.99 (Use ponto para decimal)" required value={novoValor} onChange={(e) => setValor(e.target.value)} />
                            </div>

                            <button disabled={salvando} type="submit" className="btn btn-sm btn-success mb-5 mr-3"><span>Salvar</span></button>
                            <Link className="btn btn-sm btn-danger mb-5" to={`/admin/produtos/`}> Cancelar </Link>
                        </form>
                    </div>
                </div>
            </div >
        </>
    );
}

export default NovoProduto;
