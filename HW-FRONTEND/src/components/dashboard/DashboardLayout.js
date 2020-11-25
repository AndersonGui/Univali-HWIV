import React from 'react';
import { Route, Switch, Link, Redirect } from "react-router-dom"

import "./DashboardLayout.css";

import Home from "./Home/Home";
import Produtos from "./Produtos/Produtos";
import ProdutoEditar from "./Produtos/ProdutoEditar";
import NovoProduto from "./Produtos/NovoProduto";
import Pedidos from "./Pedidos/Pedidos";
import NovoPedido from "./Pedidos/NovoPedido";
import { logout } from "../../auth"

function DashboardLayout() {

    function siderbar(e) {
        e.preventDefault();
        document.getElementsByTagName("body")[0].classList.toggle("sb-sidenav-toggled");
    };


    function handleLogOut() {
        logout();
        window.location.pathname = "/"
    }

    return (
        <>
            <nav className="sb-topnav navbar navbar-expand navbar-dark bg-dark">
                <Link to="/" className="navbar-brand" href="index.html">Lanchonete</Link>
                <button onClick={siderbar} className="btn btn-link btn-sm order-1 order-lg-0" id="sidebarToggle">
                    <i className="fas fa-bars"></i>
                </button>
                <ul className="navbar-nav ml-auto">
                    <li className="nav-item dropdown">
                        <a className="nav-link dropdown-toggle" id="userDropdown" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i className="fas fa-user fa-fw"></i></a>
                        <div className="dropdown-menu dropdown-menu-right" aria-labelledby="userDropdown">
                            <Link className="dropdown-item" to="/" onClick={handleLogOut}> Logout </Link>
                        </div>
                    </li>
                </ul>
            </nav>
            <div id="layoutSidenav">
                <div id="layoutSidenav_nav">
                    <nav className="sb-sidenav accordion sb-sidenav-dark" id="sidenavAccordion">
                        <div className="sb-sidenav-menu">
                            <div className="nav">
                                {/* <div className="sb-sidenav-menu-heading">Interface</div> */}
                                <a className="nav-link collapsed" href="#" data-toggle="collapse" data-target="#operacao" aria-expanded="false" aria-controls="operacao">
                                    <div className="sb-nav-link-icon"><i className="fas fa-columns"></i></div>
                                    Operação
                                    <div className="sb-sidenav-collapse-arrow"><i className="fas fa-angle-down"></i></div>
                                </a>
                                <div className="collapse" id="operacao" aria-labelledby="headingOne" data-parent="#sidenavAccordion">
                                    <nav className="sb-sidenav-menu-nested nav">
                                        <Link className="nav-link" to="/admin/pedidos"> Pedidos </Link>
                                    </nav>
                                </div>

                                <a className="nav-link collapsed" href="#" data-toggle="collapse" data-target="#manutencao" aria-expanded="false" aria-controls="manutencao">
                                    <div className="sb-nav-link-icon"><i className="fas fa-columns"></i></div>
                                    Manutenção
                                    <div className="sb-sidenav-collapse-arrow"><i className="fas fa-angle-down"></i></div>
                                </a>
                                <div className="collapse" id="manutencao" aria-labelledby="headingOne" data-parent="#sidenavAccordion">
                                    <nav className="sb-sidenav-menu-nested nav">
                                        <Link className="nav-link" to="/admin/produtos"> Produtos </Link>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </nav>
                </div>
                <div id="layoutSidenav_content">
                    <main>
                        <div className="container-fluid">
                            <Switch>
                                <Route path="/admin/" exact component={Home} />
                                <Route path="/admin/produtos" exact component={Produtos} />
                                <Route path="/admin/produtos/criar" exact component={NovoProduto} />
                                <Route path="/admin/produtos/:idProduto" exact component={ProdutoEditar} />
                                <Route path="/admin/pedidos" exact component={Pedidos} />
                                <Route path="/admin/pedidos/criar" exact component={NovoPedido} />
                                <Redirect to="/admin" />
                            </Switch>
                        </div>
                    </main>
                </div>
            </div>
        </>
    );
}

export default DashboardLayout;
