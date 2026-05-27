DROP DATABASE IF EXISTS supermercado_db;
CREATE DATABASE IF NOT EXISTS supermercado_db;
USE supermercado_db;

CREATE USER IF NOT EXISTS 'admin_supermercado'@'localhost' IDENTIFIED WITH mysql_native_password BY '123456'; 
GRANT ALL PRIVILEGES ON supermercado_db.* TO 'admin_supermercado'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;

CREATE TABLE categorias (
    id_categoria INT AUTO_INCREMENT PRIMARY KEY,
    nome_categoria VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE produtos (
    id_produto INT AUTO_INCREMENT PRIMARY KEY,
    codigo VARCHAR(50) UNIQUE NOT NULL,
    nome VARCHAR(100) NOT NULL,
    id_categoria INT NOT NULL, -- Relacionamento com a tabela de categorias
    quantidade INT NOT NULL DEFAULT 0,
    preco DECIMAL(10, 2) NOT NULL,
    data_cadastro DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_categoria) REFERENCES categorias(id_categoria) ON DELETE RESTRICT
);

CREATE TABLE vendas (
    id_venda INT AUTO_INCREMENT PRIMARY KEY,
    data_venda DATETIME DEFAULT CURRENT_TIMESTAMP,
    subtotal DECIMAL(10, 2) NOT NULL,
    desconto DECIMAL(10, 2) DEFAULT 0.00,
    total DECIMAL(10, 2) NOT NULL
);

CREATE TABLE itens_venda (
    id_item INT AUTO_INCREMENT PRIMARY KEY,
    id_venda INT NOT NULL,
    id_produto INT NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (id_venda) REFERENCES vendas(id_venda) ON DELETE CASCADE,
    FOREIGN KEY (id_produto) REFERENCES produtos(id_produto)
);

INSERT INTO categorias (id_categoria, nome_categoria) VALUES (1, 'Geral');

INSERT INTO categorias (nome_categoria) VALUES 
('Alimentos'), 
('Bebidas'), 
('Limpeza'), 
('Higiene'), 
('Hortifrúti');

SELECT * FROM categorias;
SELECT * FROM produtos;
SELECT * FROM vendas;
SELECT * FROM itens_venda;