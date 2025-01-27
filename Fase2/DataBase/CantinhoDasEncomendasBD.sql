CREATE DATABASE CantinhoDasEncomendas;
--DROP DATABASE CantinhoDasEncomendas;

USE CantinhoDasEncomendas;

CREATE TABLE Utilizador(
	email VARCHAR(45) NOT NULL,
	senha VARCHAR(45) NOT NULL,
	tipo VARCHAR(45) NOT NULL,
	morada VARCHAR(255),
	telemovel VARCHAR(10),
	PRIMARY KEY (email)
);

ALTER TABLE Utilizador
ADD CONSTRAINT tipo CHECK(tipo IN('Funcionario','Cliente'));


CREATE TABLE Avaliacao(
	cliente VARCHAR(45) NOT NULL,
	rating INT NOT NULL,
	comentario VARCHAR(500),
	PRIMARY KEY (cliente),
	CONSTRAINT FK_AvaliacaoCliente FOREIGN KEY (cliente)
	REFERENCES Utilizador(email)
);


CREATE TABLE CarrinhoDeCompras(
	id INT NOT NULL IDENTITY(1,1),
	proprietario VARCHAR(45) NOT NULL,
	PRIMARY KEY (id),
	CONSTRAINT FK_CarrinhoCliente FOREIGN KEY (proprietario)
	REFERENCES Utilizador(email)
);


CREATE TABLE Encomenda(
	id INT NOT NULL IDENTITY(1,1),
	estado VARCHAR(45) NOT NULL,
	relatorio VARCHAR(500),
	realizada_por VARCHAR(45) NOT NULL,
	idCarrinho INT NOT NULL,
	PRIMARY KEY (id),
	CONSTRAINT FK_EncomendaCliente FOREIGN KEY (realizada_por)
	REFERENCES Utilizador(email),
	CONSTRAINT FK_EncomendaCarrinho FOREIGN KEY (idCarrinho)
	REFERENCES CarrinhoDeCompras(id)
);

ALTER TABLE Encomenda
ADD CONSTRAINT estado CHECK(estado IN('Em espera','Interrompida','Em preparação','Enviada'));


CREATE TABLE Pagamento(
	id INT NOT NULL IDENTITY(1,1),
	valor FLOAT NOT NULL,
	dataPagamento DATETIME NOT NULL,
	idEncomenda INT NOT NULL,
	PRIMARY KEY (id),
	CONSTRAINT FK_EncomendaPagamento FOREIGN KEY (id)
	REFERENCES Encomenda(id)
);

--###########################################
CREATE TABLE Tarte(
	id INT NOT NULL IDENTITY(1,1),
	nome VARCHAR(45) NOT NULL,
	descricao VARCHAR(255) NOT NULL,
	preco FLOAT NOT NULL,
	PRIMARY KEY (id)
);


CREATE TABLE Cliente_Favoritos(
	idTarte INT NOT NULL,
	cliente VARCHAR(45) NOT NULL,
	PRIMARY KEY (idTarte, cliente),
	CONSTRAINT FK_TarteFavoritos FOREIGN KEY (idTarte)
	REFERENCES Tarte(id),
	CONSTRAINT FK_ClienteFavoritos FOREIGN KEY (cliente)
	REFERENCES Utilizador(email)
);


CREATE TABLE Carrinho_Tarte(
	idCarrinho INT NOT NULL,
	idTarte INT NOT NULL,
	quantidade INT NOT NULL,
	PRIMARY KEY (idCarrinho, idTarte),
	CONSTRAINT FK_CTCarrinho FOREIGN KEY (idCarrinho)
	REFERENCES CarrinhoDeCompras(id),
	CONSTRAINT FK_CTTarte FOREIGN KEY (idTarte)
	REFERENCES Tarte(id)
);


CREATE TABLE Montagem(
	idTarte INT NOT NULL,
	idFase INT NOT NULL,
	nomeFase VARCHAR(45) NOT NULL,
	descricaoFase VARCHAR(255) NOT NULL,
	imagemFase VARCHAR(255) NOT NULL,
	proximaFase INT,
	PRIMARY KEY (idTarte, idFase),
	CONSTRAINT FK_MontagemTarte FOREIGN KEY (idTarte)
	REFERENCES Tarte(id),
	CONSTRAINT FK_MontagemMontagem FOREIGN KEY (idTarte, proximaFase)
	REFERENCES Montagem(idTarte, idFase)
);


CREATE TABLE Material(
	id INT NOT NULL IDENTITY(1,1),
	nome VARCHAR(45) NOT NULL,
	quantidade FLOAT NOT NULL,
	capacidade FLOAT NOT NULL,
	PRIMARY KEY (id)
);


CREATE TABLE Tarte_Material(
	idTarte INT NOT NULL,
	idMaterial INT NOT NULL,
	quantidade FLOAT NOT NULL,
	PRIMARY KEY (idTarte, idMaterial),
	CONSTRAINT FK_TMTarte FOREIGN KEY (idTarte)
	REFERENCES Tarte(id),
	CONSTRAINT FK_TMMaterial FOREIGN KEY (idMaterial)
	REFERENCES Material(id)
);
