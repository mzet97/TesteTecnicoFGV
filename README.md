# Guia para Uso da API - DesafioFGV

Este documento detalha como utilizar a API fornecida no Swagger e executar operações como registro, login e manipulação de pedidos e usuários.

## 📌 Tecnologias Utilizadas

O projeto foi desenvolvido seguindo as melhores práticas de engenharia de software, incluindo:

- **Arquitetura Limpa (Clean Architecture)** para garantir modularidade e separação de responsabilidades.
- **CQRS (Command Query Responsibility Segregation)** para melhor organização das operações de leitura e escrita.
- **Elasticsearch** para armazenamento e indexação eficiente de logs.
- **Kibana** para visualização e análise de logs em tempo real.
- **.NET 9** como framework principal para desenvolvimento da API.
- **Identity Core 9** para gerenciamento de autenticação e controle de usuários.
- **Entity Framework 9** para manipulação de banco de dados de forma eficiente.
- **MediatR** para implementação do **Mediator Pattern**, facilitando a comunicação entre componentes.
- **Swagger** para documentação interativa da API.
- **FluentValidation** para validação robusta de dados de entrada.
- **Serilog** para logging estruturado e análise de eventos.
- **xUnit** para testes unitários.
- **Redis** para caching de requisições e otimização de performance.

## 📌 Pré-requisitos

Antes de iniciar, certifique-se de que possui os seguintes itens instalados:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

Preferencialmente, utilize Linux ou WSL para rodar a aplicação.

## 🔧 Configuração Inicial

1. **Acesse a pasta do projeto**

   ```sh
   cd DesafioFGV
   ```

2. **Dê permissão de execução ao script de inicialização**

   ```sh
   sudo chmod 777 run.sh && ./run.sh
   ```

3. **Criação da Rede Docker**

   ```sh
   docker network create desafio-network
   ```

4. **Configuração das Variáveis de Ambiente**

   Crie um arquivo `.env` na raiz do projeto com as seguintes variáveis:

   ```env
   REDIS_PASSWORD=suasenha_redis
   ELASTIC_PASSWORD=suasenha_elastic
   KIBANA_PASSWORD=suasenha_kibana
   MSSQL_SA_PASSWORD=suasenha_sqlserver
   ```

5. **Criação de Diretórios Persistentes**

   ```sh
   mkdir -p /dados/redis /dados/redis/logs
   mkdir -p /dados/elasticsearch
   mkdir -p /dados/logstash/logs /dados/logstash/data
   mkdir -p /dados/sqlserver/data /dados/sqlserver/log
   ```

## 🚀 Executando a API

1. **Subir os Contêineres**

   ```sh
   docker compose up --build -d
   ```

2. **Verificar os Contêineres em Execução**

   ```sh
   docker ps
   ```

3. **Acompanhar Logs de um Serviço**

   ```sh
   docker logs -f nome_do_servico
   ```

## 🔌 Serviços Disponíveis

- **Redis**: Porta `6379`
- **Elasticsearch**: Porta `9200`
- **Kibana**: Porta `5601`
- **Logstash**: Portas `5044-5046`
- **SQL Server**: Porta `1433`
- **API**: Porta `8080` e `8081`

## ⛔ Parando e Removendo os Contêineres

Para parar os contêineres:

```sh
   docker compose down
```

Para remover os volumes associados:

```sh
   docker compose down -v
```

---

# 🔍 Explorando a API via Swagger

A interface Swagger pode ser acessada em:

🔗 [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

O Swagger fornece uma interface gráfica para interação com os endpoints da API, permitindo o teste de funcionalidades e validação dos recursos disponíveis.

## 🔑 Autenticação e Registro de Usuário

### ➤ Criar um Novo Usuário

Faça uma requisição **POST** para:

```
http://localhost:8080/auth/register
```

Com o corpo da requisição:

```json
{
  "email": "user@example.com",
  "name": "string",
  "password": "string"
}
```

### ➤ Login e Geração de Token

Faça uma requisição **POST** para:

```
http://localhost:8080/auth/login
```

Com o corpo da requisição:

```json
{
  "email": "user@example.com",
  "password": "string"
}
```

A resposta conterá um **token JWT**, que deverá ser utilizado em chamadas autenticadas, enviando-o no header:

```
Authorization: Bearer {seu_token_aqui}
```

## 📦 Manipulação de Pedidos

### ➤ Criar um Pedido

Faça uma requisição **POST** para:

```
http://localhost:8080/pedidos
```

Com o corpo da requisição:

```json
{
  "description": "Compra de produtos",
  "value": 99.90,
  "dateOrder": "2025-03-01T12:00:00Z",
  "idUser": "c4d9b1f6-4a3d-4a8d-9dfb-d0c1a7b78e2a"
}
```

### ➤ Buscar Pedidos

Faça uma requisição **GET** para:

```
http://localhost:8080/pedidos
```

Com os seguintes parâmetros opcionais:

- `Value`: Valor do pedido
- `Description`: Descrição
- `UserName`: Nome do usuário
- `Email`: E-mail do usuário
- `Id`: Identificador do pedido (UUID)
- `PageIndex`: Número da página
- `PageSize`: Tamanho da página

### ➤ Buscar Pedido por ID

Faça uma requisição **GET** para:

```
http://localhost:8080/pedidos/{id}
```

Substituindo `{id}` pelo UUID do pedido.

### ➤ Atualizar Pedido

Faça uma requisição **PUT** para:

```
http://localhost:8080/pedidos/{id}
```

Com o corpo da requisição:

```json
{
  "description": "Novo pedido atualizado",
  "value": 120.50,
  "dateOrder": "2025-03-01T15:30:00Z",
  "idUser": "c4d9b1f6-4a3d-4a8d-9dfb-d0c1a7b78e2a",
  "id": "3d1f70e2-3c2b-4f98-95e4-85b08c6b8a9f"
}
```

### ➤ Deletar Pedido

Faça uma requisição **DELETE** para:

```
http://localhost:8080/pedidos/{id}
```

Substituindo `{id}` pelo UUID do pedido.

## 👤 Gerenciamento de Usuários

### ➤ Buscar Usuários

Faça uma requisição **GET** para:

```
http://localhost:8080/usuarios
```

Com os seguintes parâmetros opcionais:

- `Name`: Nome do usuário
- `Email`: E-mail do usuário
- `Id`: Identificador do usuário (UUID)

### ➤ Atualizar Usuário

Faça uma requisição **PUT** para:

```
http://localhost:8080/usuarios/{id}
```

Com o corpo da requisição:

```json
{
  "id": "c4d9b1f6-4a3d-4a8d-9dfb-d0c1a7b78e2a",
  "email": "new_email@example.com",
  "name": "Novo Nome"
}
```

### ➤ Alteração de Senha

Faça uma requisição **PATCH** para:

```
http://localhost:8080/usuarios/{id}
```

Com o corpo da requisição:

```json
{
  "id": "c4d9b1f6-4a3d-4a8d-9dfb-d0c1a7b78e2a",
  "newPassword": "novaSenha123",
  "confirmPassword": "novaSenha123"
}
```

---

📌 **Agora você está pronto para utilizar a API do DesafioFGV!** 🚀
