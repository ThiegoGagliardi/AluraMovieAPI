# Projeto: API RESTful para Cadastro de Cinemas, Filmes e Sessões de Cinema

## Descrição

Esta API RESTful foi desenvolvida para gerenciar o cadastro de cinemas, filmes e sessões de cinema. A aplicação foi construída utilizando C#, MySQL, Entity Framework (EF), AutoMapper e Migrations. Ela fornece endpoints para realizar operações CRUD (Create, Read, Update, Delete) para cinemas, endereços de cinemas, filmes e sessões.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal.
- **MySQL**: Banco de dados relacional utilizado para armazenar as informações.
- **Entity Framework (EF)**: ORM (Object-Relational Mapper) utilizado para interagir com o banco de dados.
- **AutoMapper**: Biblioteca para mapeamento de objetos.
- **Migrations**: Ferramenta do Entity Framework para gerenciar alterações no esquema do banco de dados.

## Estrutura do Projeto

- **Controllers**: Contém os controladores da API, responsáveis por gerenciar as requisições HTTP.
- **Models**: Contém os modelos de dados, que representam as entidades do banco de dados.
- **DTOs**: Contém os Data Transfer Objects, utilizados para transferência de dados entre camadas.
- **Mappings**: Contém as configurações do AutoMapper.
- **Data**: Contém o contexto do banco de dados e as configurações do Entity Framework.
- **Migrations**: Contém as migrações do Entity Framework para gerenciar o esquema do banco de dados.

## Configuração do Projeto

### Pré-requisitos

- .NET Core SDK
- MySQL Server

### Configuração do Banco de Dados

1. Crie um banco de dados no MySQL para a aplicação.
2. Atualize a string de conexão no arquivo `appsettings.json` com as informações do seu banco de dados.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=nome_do_banco;User=root;Password=sua_senha;"
  }
}
```

### Executando as Migrações

1. Abra o terminal na raiz do projeto.
2. Execute o comando abaixo para aplicar as migrações e criar o esquema do banco de dados:

```bash
dotnet ef database update
```

### Executando a Aplicação

1. Abra o terminal na raiz do projeto.
2. Execute o comando abaixo para iniciar a aplicação:

```bash
dotnet run
```

A aplicação estará disponível em `http://localhost:5000`.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.


**Autor**: Thiego Gagliardi