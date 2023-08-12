<h1 align="center">DM106 - Blog</h1>

---

## ❓ Sobre o projeto

Este projeto da disciplina DM106 (Desenvolvimento de Web Services com segurança sob plataforma .NET) tem como objetivo criar uma API REST para gerenciar um Blog.

Para isso, foi utilizado o framework .NET Core 7.0, Azure SQL e a Azure Web App.

---

## 💻 Tecnologias

As seguintes tecnologias foram utilizadas na construção do projeto:

 - **[.NET Core 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)**
 - **[Azure SQL](https://azure.microsoft.com/pt-br/services/sql-database/)**
 - **[Azure Web App](https://azure.microsoft.com/pt-br/services/app-service/web/)**
 - **[Swagger](https://swagger.io/)**

> Para mais detalhes, veja o arquivo  **[Tasks.csproj](Tasks.csproj)**

### Utilitários
- Editor:  **[Visual Studio Code](https://code.visualstudio.com/)**

---
## ⚙️ Como executar o projeto

### 💡 Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
**[Git](https://git-scm.com)** e **[.NET](https://dotnet.microsoft.com/download/dotnet/7.0)**; 

```bash

# Clone este repositório
$ git clone https://github.com/Brendhon/dm106-tasks.git

# Acesse a pasta do projeto

# Limpe o projeto
$ dotnet clean

# Build da aplicação
$ dotnet build

```

É necessário também ter uma conta na **[Azure](https://azure.microsoft.com/pt-br/)**, criar um banco de dados SQL, criar uma Web App e criar um arquivo **appsettings.json** na raiz do projeto com a seguinte estrutura:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTION": "<connection-string-do-banco-de-dados>"
  }
}

```

### ⚽ Rodando no modo desenvolvedor

É necessário a criação de um arquivo **appsettings.Development.json** na raiz do projeto com a mesma estrutura do arquivo **appsettings.json**.

Após isso, execute o seguinte comando:

```bash

# Execute a aplicação
$ dotnet run

# Iniciará na porta:500

```

---

## Comandos úteis

```bash

# Gerando o arquivo de migração
$ dotnet ef migrations add <nome da migration> --context <nome do contexto>

# Após fazer a migração no comando anterior, vamos agora aplicar essas migrações no banco
$ dotnet ef database update

# Aplicar as migrações em um banco de dados específico
$ dotnet ef database update --connection "<your-azure-sql-connection-string>" --context TaskContext

# Para remover uma migração e que inclusive já foi enviada para o banco, use
$ dotnet ef migrations remove
$ dotnet ef database update

# Para gerar o script SQL do banco, use o comando
$ dotnet ef migrations script -o ./script.sql

```

---

## Observações

- Lembrando que é necessário habilitar o acesso ao banco de dados do Azure para o IP da sua máquina, caso contrário, não será possível acessar o banco de dados.
- Após executar a aplicação, acesse a documentação da API em: **[http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)**
- Lembre-se, sempre que alterar o banco de dados, é necessário criar uma nova migration e atualizar o banco de dados, para isso, utilize os comando acima.
- Caso use o Visual Studio Code, recomendo que instale as seguintes extensões para facilitar o desenvolvimento:
  - **[Azure App Service](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureappservice)**
  - **[C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)**
  - **[Azure Account](https://marketplace.visualstudio.com/items?itemName=ms-vscode.azure-account)**

---

## 👥 Autor
<h4 align="left">
<img style="border-radius: 5%; margin-right: 30px" src="https://avatars.githubusercontent.com/Brendhon" width="120px;" alt="Avatar"/><br>
Brendhon Moreira
</h4>


[![Linkedin Badge](https://img.shields.io/badge/-Brendhon-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/brendhon-moreira)](https://www.linkedin.com/in/brendhon-moreira)
[![Gmail Badge](https://img.shields.io/badge/-brendhon.e.c.m@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:brendhon.e.c.m@gmail.com)](mailto:brendhon.e.c.m@gmail.com)

---
## 📝 License
[![License](https://img.shields.io/github/license/Brendhon/Pokedex?style=plastic)](http://badges.mit-license.org)

- **[MIT license](https://choosealicense.com/licenses/mit/)**
