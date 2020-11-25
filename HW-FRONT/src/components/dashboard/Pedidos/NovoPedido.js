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

function NovoPedido() {
    const [salvando, setSalvando] = useState(false);
    const [pesquisando, setpesquisando] = useState(false);
    const [produtos, setProdutos] = useState([]);
    const [identificador, setIdentificador] = useState("");
    const [observacoes, setObservacoes] = useState("");

    async function abrirPedido(e) {
        try {
            e.preventDefault();

            if (!identificador) {
                return alert("Preencha todos campos");
            }

            if(produtos.some(p => p.quantidade > 0) == false){
                return alert("Você deve selecionar pelo menos um produto");
            }

            let resposta = window.confirm(`Você deseja abrir o Pedido: ${identificador}`);
            if (resposta == false) {
                return;
            }

            setSalvando(true);

            let produtosSelecionados = produtos.filter(p => p.quantidade > 0);

            const result = await api.post("/Pedido/RealizarPedido", {
                observacoes, identificador, produtos: produtosSelecionados
            });

            if (result.success) {
                alert("Pedido aberto com sucesso");
                window.location.pathname = "/admin/pedidos";
            } else {
                alert(result.message || "Não foi possivel criar o Produto");
            }
        } catch (error) {
            console.log(error);
            console.log("Erro Inesperado.");
            alert("Erro Inesperado.");
        } finally {
            setSalvando(false);
        }
    }

    async function pesquisarProdutos() {
        try {
            setpesquisando(true);

            const result = await api.get("/produto");

            if (result.success) {

                if (result.data.length == 0) {
                    alert("Nenhum produto localizado, não será possivel abrir um pedido!");
                    return window.location.pathname = "/admin/pedidos";
                }

                await setProdutos(result.data.map(c => { return { nome: c.nome, valor: c.valor, quantidade: 0, idProduto: c.id } }));
            } else {
                alert(result.message || "Não foi possivel pesquisar os Produtos");
            }
        } catch (error) {
            console.log(error);
            console.log("Erro Inesperado.");
        } finally {
            setpesquisando(false);
        }
    }

    async function aumentarQuantidade(produto) {
        setProdutos(produtos.map(p => {
            if (p.idProduto == produto.idProduto) {
                p.quantidade += 1;
            };

            return p;
        }))
    }

    async function diminuirQuantidade(produto) {
        setProdutos(produtos.map(p => {
            if (p.idProduto == produto.idProduto && p.quantidade > 0) {
                p.quantidade -= 1;
            };

            return p;
        }))
    }

    useEffect(() => {
        pesquisarProdutos();
    }, [])

    return (
        <>
            <h1 className="mt-4">Novo Pedido</h1>
            <hr />
            <div className="row">
                <div className="col-xl-12 col-md-12">
                    {!pesquisando ? (
                        <>
                            <div className="col-md-6">
                                <form onSubmit={abrirPedido}>
                                    <div className="form-group">
                                        <label htmlFor="nome"><strong>Identificador do Pedido</strong></label>
                                        <input type="text" id="nome" className="col-md-5 form-control" placeholder="Identificador" required autofocusautofocus="true" value={identificador} onChange={(e) => setIdentificador(e.target.value)} />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="nome"><strong>Observações</strong></label>
                                        <textarea type="text" id="nome" className="col-md-5 form-control" placeholder="Observações" value={observacoes} onChange={(e) => setObservacoes(e.target.value)} />
                                    </div>

                                    <div>
                                        {produtos.map(produto => {
                                            return (
                                                <div className="row" key={produto.idProduto}>
                                                    <div className="col-md-12">
                                                        <p>{produto.nome} - R$ {produto.valor}
                                                            <button type="button" onClick={() => diminuirQuantidade(produto)} className="btn btn-sm btn-primary ml-3"><span>-</span></button> {produto.quantidade}
                                                            <button type="button" onClick={() => aumentarQuantidade(produto)} className="btn btn-sm btn-primary ml-2"><span>+</span></button>
                                                        </p>
                                                    </div>
                                                </div>
                                            );
                                        })}

                                        {!pesquisando && produtos.length == 0 &&
                                            <>
                                                <p className="mt-5">Nenhum Produto encontrado</p>
                                            </>
                                        }
                                    </div>

                                    <button disabled={salvando} type="submit" className="btn btn-sm btn-success mb-5 mr-3"><span>Abrir Pedido</span></button>
                                    <Link className="btn btn-sm btn-danger mb-5" to={`/admin/pedidos/`}> Cancelar </Link>
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

export default NovoPedido;
