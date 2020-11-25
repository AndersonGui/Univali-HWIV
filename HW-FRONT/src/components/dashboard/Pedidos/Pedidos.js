import React, { useEffect, useState } from 'react';
import api from "../../../services/api";
import { Link } from "react-router-dom";

function Pedidos() {
    const [pesquisando, setpesquisando] = useState(false);
    const [pesquisaRealizada, setPesquisaRealizada] = useState(false);
    const [pedidos, setPedidos] = useState([]);


    async function pesquisarPedidos() {
        try {
            setpesquisando(true);

            const result = await api.get("/pedido");

            if (result.success) {
                await setPedidos(result.data);
            } else {
                console.log(result.message || "Não foi possivel pesquisar os Produtos");
            }
        } catch (error) {
            console.log(error);
            console.log("Erro Inesperado.");
        } finally {
            setPesquisaRealizada(true);
            setpesquisando(false);
        }
    }

    async function finalizarPedido(pedido) {
        try {
            let resposta = window.confirm(`Você finalizar o pedido: ${pedido.identificador}`);
            if (resposta == false) {
                return;
            }

            let response = await api.post("/Pedido/FinalizarPedido", {
                id: pedido.id
            });

            if (response.success) {
                pesquisarPedidos();
                alert("Pedido finalizado com sucesso!");
            } else {
                console.log(response.message || "Não foi possivel deletar o produto");
            }
        } catch (error) {
            console.log(error);
            console.log("Erro Inesperado.");
        } finally {

        }
    }

    useEffect(() => {
        pesquisarPedidos();
    }, [])

    return (
        <>
            <h1 className="mt-4">Pedidos</h1>
            <hr />

            <Link className="btn btn-success mr-2 mb-3" to="/admin/pedidos/criar"> Novo Pedido </Link>
            <div className="row">
                <div className="col-xl-12 col-md-12">
                    {pedidos.length && !pesquisando ? (
                        <>
                            <div className="row">
                                {pedidos.map((pedido, index) => {
                                    return (<div className="col-sm-6 col-md-3" key={pedido.id}>
                                        <div className="card">
                                            <div className="card-body">
                                                <h5 className="card-title text-uppercase">{pedido.identificador}
                                                </h5>
                                                {pedido.produtos.map(produto => {
                                                    return (
                                                        <p key={`${index}-${produto.id}`} className="card-text">{produto.quantidade}x {produto.produto.nome} - R$ {produto.valor}</p>
                                                    );
                                                })}

                                                {/* <button onClick={finalizarPedido} className="btn btn-sm btn-success mb-1">
                                                    Adicionar produto ao pedido
                                                </button> */}

                                                <p><strong>Observações</strong> <br />
                                                    <spam className="card-text">{pedido.observacoes}</spam>
                                                </p>


                                                <p><strong>Total: R$ {pedido.valorFinal}</strong></p>

                                                <button onClick={() => finalizarPedido(pedido)} className="btn btn-sm btn-primary">
                                                    FINALIZAR PEDIDO
                                            </button>
                                            </div>
                                        </div>
                                    </div>)
                                })}
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

                    {!pesquisando && pedidos.length == 0 && pesquisaRealizada &&
                        <>
                            <p className="mt-5">Nenhum item encontrado</p>
                        </>
                    }
                </div>
            </div >
        </>
    );
}

export default Pedidos;
