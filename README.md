# Este é meu esboço inicial do projeto webapi
### Foi usado arquitetura MVC (Controllers, Dtos, Models, Services), funcões específicas foram separadas em blocos menores para Manter clareza no código como: ProdutoExists em ProdutoController, DataBaseMigrationService em Services.

## Requerimentos
### Instalações iniciais do projeto: dotnet ef tools + webapi + EntityFrameworkCore
```shell
dotnet tool install --global dotnet-ef
dotnet new webapi -o api
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

```
# Para executar o webapi localmente
## 1) Banco de dados
### Instale o Sqlserver e crie a tabela webapi, foi configurado um serviço para executar a migration automaticamente em Services/DatabaseMigrationService.cs a função é executada em Program.cs

## Se necessário, execute as migrations manualmente
```shell
dotnet ef migrations add Init
dotnet ef database update
```

## 2) Webapi
### Execute o webapi manualmente
```shell
dotnet watch run
```

# Configurações Docker em progresso
### Subir docker containers
```shell
docker-compose up --build
```

### Subir sqlserver docker container
```shell
docker-compose up -d sqlserver
```

## 2) Database
### Entre no sqlserver container
```shell
docker-compose exec sqlserver /bin/bash
```

### Liste all databases
```sql
SHOW DATABASES;
```

### Crie *webapi* database
```sql
CREATE DATABASE webapi;
```

# :) Thanks!
