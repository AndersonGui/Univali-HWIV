import React, { useEffect, useState } from 'react';
import DataTable from 'react-data-table-component';
import api from "../../../services/api";
import { Link } from "react-router-dom";

function Produtos() {
    const [pesquisando, setpesquisando] = useState(false);
    const [pesquisaRealizada, setPesquisaRealizada] = useState(false);
    const [produtos, setProdutos] = useState([]);


    async function pesquisarProdutos() {
        try {
            setpesquisando(true);

            const result = await api.get("/produto");

            if (result.success) {
                await setProdutos(result.data);
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

    async function deletarProduto(produto) {
        try {
            let resposta = window.confirm(`Você deseja excluir o produto: ${produto.nome}`);
            if (resposta == false) {
                return;
            }

            let response = await api.delete("/produto", {
                params: {
                    idProduto: produto.id
                }
            });

            if (response.success) {
                pesquisarProdutos();
                alert("Produto Excluido com sucesso!");
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
        pesquisarProdutos();
    }, [])

    return (
        <>
            <h1 className="mt-4">Produtos</h1>
            <hr />

            <Link className="btn btn-success mr-2" to="/admin/produtos/criar"> Novo Produto </Link>
            <div className="row">
                <div className="col-xl-12 col-md-12">
                    {produtos.length && !pesquisando ? (
                        <>
                            <DataTable
                                data={produtos}
                                pagination={true}
                                columns={[
                                    { name: "Id", selector: "id", sortable: true },
                                    { name: "Nome", selector: "nome", sortable: true },
                                    { name: "Valor", selector: "valor", sortable: true },
                                    {
                                        name: 'Ações',
                                        sortable: true,
                                        cell: row => (
                                            <div data-tag="allowRowEvents">

                                                <Link className="btn btn-primary mr-2" to={`/admin/produtos/${row.id}`}> Editar </Link>

                                                <button onClick={() => deletarProduto(row)} type="button" className="btn btn-danger">
                                                    Excluir
                                                </button>
                                            </div>
                                        ),
                                    },
                                ]}
                                keyField={"id"} />
                        </>) : pesquisando && (
                            <>
                                <div className="d-flex justify-content-center">
                                    <div className="spinner-border" role="status">
                                        <span className="sr-only">Carregando...</span>
                                    </div>
                                </div>
                            </>
                        )}

                    {!pesquisando && produtos.length == 0 && pesquisaRealizada &&
                        <>
                            <p className="mt-5">Nenhum item encontrado</p>
                        </>
                    }
                </div>
            </div >
        </>
    );
}

export default Produtos;
