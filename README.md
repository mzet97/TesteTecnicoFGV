
 # Guia para Rodar o Docker Compose

Este guia descreve como rodar o ambiente descrito no arquivo `docker-compose.yml`. Preferencialmente use linux ou wsl.

## Pré-requisitos

Antes de iniciar, certifique-se de que possui os seguintes itens instalados:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Configuração
1. Acesse a pasta DesafioFGV

    Dentro da pasta execute o script
    ```sh
    sudo chmod 777 run.sh && ./run.sh
    ```

2. **Criar a Rede do Docker**
   
   O `docker-compose.yml` utiliza uma rede chamada `desafio-network`, que precisa ser criada antes de iniciar os contêineres:
   ```sh
   docker network create desafio-network
   ```

3. **Configurar Variáveis de Ambiente**

   Crie um arquivo `.env` na raiz do projeto com as seguintes variáveis:
   ```env
   REDIS_PASSWORD=suasenha_redis
   ELASTIC_PASSWORD=suasenha_elastic
   KIBANA_PASSWORD=suasenha_kibana
   MSSQL_SA_PASSWORD=suasenha_sqlserver
   ```

4. **Criar Diretórios Persistentes**

   Para garantir a persistência dos dados, crie os diretórios necessários:
   ```sh
   mkdir -p /dados/redis /dados/redis/logs
   mkdir -p /dados/elasticsearch
   mkdir -p /dados/logstash/logs /dados/logstash/data
   mkdir -p /dados/sqlserver/data /dados/sqlserver/log
   ```

## Executando os Contêineres

1. **Rodar o Docker Compose**
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

## Serviços Disponíveis

- **Redis**: Porta `6379`
- **Elasticsearch**: Porta `9200`
- **Kibana**: Porta `5601`
- **Logstash**: Portas `5044-5046`
- **SQL Server**: Porta `1433`
- **API**: Porta `8080` e `8081`

## Parando e Removendo os Contêineres

Para parar os contêineres:
```sh
   docker compose down
```

Para remover os volumes associados:
```sh
   docker compose down -v
```

## Execuando a aplicação
#### Acessar a Interface do Swagger

A interface Swagger está disponível em: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html) O Swagger fornece uma interface gráfica para interação com os endpoints da API, permitindo o teste de funcionalidades e a validação dos recursos disponíveis. Esta interface é essencial para garantir que os desenvolvedores possam explorar e compreender o comportamento da API de maneira intuitiva.

#### Registro na Aplicação

Faça uma requisição POST para o seguinte endpoint:

```
http://localhost:8080/Auth/register
```

Com o corpo da requisição:

```json
{
  "email": "user@example.com",
  "name": "string",
  "password": "string"
}
```

Este endpoint permite o registro de novos usuários na aplicação. O processo de registro é uma etapa fundamental para garantir o acesso seguro aos recursos disponibilizados pela API, utilizando um sistema de autenticação robusto.

#### Login e Geração de Token

Faça uma requisição POST para o seguinte endpoint:

```
http://localhost:8080/Auth/login
```

Com o corpo da requisição:

```json
{
  "email": "user@example.com",
  "password": "string"
}
```

Após o registro, o usuário deve realizar o login para obter um token de acesso. Este token será utilizado para autenticar as requisições subsequentes, garantindo que apenas usuários autorizados possam acessar os recursos protegidos da API.