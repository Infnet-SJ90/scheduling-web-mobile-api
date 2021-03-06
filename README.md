# Scheduling Web Mobile Api

## Começando

Este repositório tem como objetivo ser uma API para se conectar com os aplicativos mobile e web.

### Pré-requisitos

* Docker
* Postman
* Visual Studio 

### Rodando o projeto.

• Clonar o repositório

`git clone git@github.com:Infnet-SJ90/scheduling-web-mobile-api.git`

• Abrir no cainho do projetor

`cd scheduling-web-mobile-api`

• Buildar a image de docker

`docker build -t sj90image .`

• Rodar aplicação

`docker run -d -p 8080:80 -it sj90image --name sj90webapi`

**Endpoint:**: http://localhost:8080/

## Desdobramento, desenvolvimento

Yuri Souza foi o desenvolvedor do código e Pedro Bastos foi o responsável por documentar os endpoints

## Construido com

* [.NET](https://www.microsoft.com/net) - Usada para fazer a API

## Autores

* **Yuri Souza** - [yurisouza](https://github.com/yurisouza)

* **Pedro Bastos** - [pedroppbastos](https://github.com/pedroppbastos) 

#### O.B.S.: Caso desejar ver como uttilizar nossos endpoints segue o endereço da nossa wiki: https://github.com/Infnet-SJ90/scheduling-web-mobile-api/wiki/API
