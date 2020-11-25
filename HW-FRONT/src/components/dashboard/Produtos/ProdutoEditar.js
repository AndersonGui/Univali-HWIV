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

function ProdutoEditar() {
    const [pesquisando, setpesquisando] = useState(false);
    const [salvando, setSalvando] = useState(false);
    const [produto, setProduto] = useState(null);
    const [nome, setNome] = useState("");
    const [valor, setValor] = useState("");
    let { idProduto } = useParams();


    async function pesquisarProduto() {
        try {
            setpesquisando(true);

            const result = await api.get(`/produto/${idProduto}`);

            if (result.success) {
                setNome(result.data.nome);
                setValor(result.data.valor);
                await setProduto(result.data);
            } else {
                console.log(result.message || "Não foi possivel pesquisar os Produtos");
            }
        } catch (error) {
            console.log(error);
            console.log("Erro Inesperado.");
        } finally {
            setpesquisando(false);
        }
    }

    async function salvar(e) {
        try {
            e.preventDefault();

            if (!nome || !valor) {
                return alert("Preencha todos campos");
            }

            if(Number.isNaN(parseFloat(valor))){
                return alert("Confira que se o valor está correto");
            }

            setSalvando(true);

            let produtoAtualizado = {
                id: produto.id,
                nome: nome,
                valor: parseFloat(valor)
            }

            const result = await api.put(`/produto`, produtoAtualizado);

            if (result.success) {
                alert("Produto salvo com sucesso");
                window.location.pathname = "/admin/produtos"
            } else {
                alert(result.message || "Não foi possivel atualizar o Produto");
            }
        } catch (error) {
            console.log(error);
            console.log("Erro Inesperado.");
        } finally {
            setSalvando(false);
        }
    }

    useEffect(() => {
        pesquisarProduto();
    }, [])

    return (
        <>
            <h1 className="mt-4">{produto != null ? produto.nome : ''}</h1>
            <hr />
            <div className="row">
                <div className="col-xl-12 col-md-12">

                    {produto != null && !pesquisando ? (
                        <>
                            <div className="col-md-3">
                                <form onSubmit={salvar}>
                                    <div className="form-group">
                                        <label htmlFor="nome"><strong>Nome</strong></label>
                                        <input type="text" id="nome" className="form-control" placeholder="Nome" required autofocusautofocus="true" value={nome} onChange={(e) => setNome(e.target.value)} />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="valor"><strong>Valor</strong></label>
                                        <input type="text" id="text" className="form-control" placeholder="9.99 (Use ponto para decimal)" required value={valor} onChange={(e) => setValor(e.target.value)} />
                                    </div>

                                    <button disabled={salvando} type="submit" className="btn btn-sm btn-success mb-5 mr-3"><span>Salvar</span></button>
                                    <Link className="btn btn-sm btn-danger mb-5" to={`/admin/produtos/`}> Cancelar </Link>
                                </form>
                            </div>
                        </>) : pesquisando && (
                            <>
                                <div className="d-flex justify-content-center">
                                    <div className="spinner-border" role="status">
                                        <span className="sr-only">Carregando...</span>
                                    </div>
                                </div>
                            </>
                        )}
                </div>
            </div >
        </>
    );
}

export default ProdutoEditar;
