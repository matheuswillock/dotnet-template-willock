# dotnet-template-willock

Template backend em .NET com Clean Architecture.

## O Que Vem Configurado

- .NET 10.
- Clean Architecture com `Domain`, `Application`, `Infrastructure`, `Api` e `Tests`.
- Referencias entre projetos ja configuradas.
- Entity Framework Core com PostgreSQL/Npgsql.
- Banco em memoria no ambiente `Development` para rodar direto pelo Rider sem PostgreSQL local.
- `Dockerfile` e `docker-compose.yml` com PostgreSQL.
- Padrao `IOutput<T>` e `Output<T>` para comunicacao entre Use Cases e Controllers/Endpoints.
- Padrao de `Input` e `Output` por Use Case.
- Pasta `Application/DTOs` para DTOs compartilhados.
- Middleware global de tratamento de exceptions.
- Base abstrata para clientes HTTP externos com `BaseUrl`, `BearerToken` e headers vindos do `appsettings.json`.
- Controller inicial `GET /api/ping`.
- Health check `GET /health`.
- Swagger UI em `/swagger`.
- Testes unitarios e testes de integracao com xUnit.

## Instalacao Local

Se voce esta na maquina onde o template foi criado, instale direto pela pasta local:

```bash
/home/matheuswillock/.dotnet/dotnet new install /home/matheuswillock/dotnet-templates/clean-arch-backend --force
```

Confirme que o template apareceu:

```bash
/home/matheuswillock/.dotnet/dotnet new list dotnet-template-willock
```

## Instalacao Pelo GitHub

O `dotnet new install` nao instala diretamente de uma URL do GitHub. Clone o repositorio primeiro:

```bash
git clone https://github.com/matheuswillock/dotnet-template-willock.git
```

Depois instale pela pasta clonada:

```bash
dotnet new install ./dotnet-template-willock --force
```

Confirme a instalacao:

```bash
dotnet new list dotnet-template-willock
```

## Criar Um Novo Projeto

Pelo terminal:

```bash
dotnet new dotnet-template-willock -n NomeDoProjeto
```

Exemplo:

```bash
cd /home/matheuswillock/develop
dotnet new dotnet-template-willock -n CorretorStudio
```

Abra a solution gerada no Rider:

```bash
rider /home/matheuswillock/develop/CorretorStudio/CorretorStudio.sln
```

## Usar No Rider

Depois de instalar o template com `dotnet new install`:

1. Reinicie o Rider.
2. Abra `New Solution` ou `New Project`.
3. Pesquise por `dotnet-template-willock`.
4. Informe o nome do projeto.
5. Clique em `Create`.

Se o template nao aparecer no wizard:

1. Abra `Manage Templates...`.
2. Recarregue os templates.
3. Reinicie o Rider.

Configuracao recomendada do Rider nesta maquina:

```text
.NET CLI executable path:
/home/matheuswillock/.dotnet/dotnet

MSBuild version:
Auto detected (18.9) - /home/matheuswillock/.dotnet/sdk/10.0.400/MSBuild.dll
```

## Rodar O Projeto

Entre na pasta do projeto gerado:

```bash
cd NomeDoProjeto
```

Restaure e compile:

```bash
dotnet restore
dotnet build
```

Rode a API:

```bash
dotnet run --project src/NomeDoProjeto.Api/NomeDoProjeto.Api.csproj
```

No perfil de desenvolvimento, o template usa `Database:Provider = InMemory`, entao voce consegue testar endpoints pelo Rider sem subir PostgreSQL.

Para usar PostgreSQL local, altere em `src/NomeDoProjeto.Api/appsettings.Development.json`:

```json
{
  "Database": {
    "Provider": "PostgreSql"
  }
}
```

Depois garanta que existe um PostgreSQL escutando em `localhost:5432`, ou rode com Docker.

Teste o ping:

```bash
curl http://localhost:5000/api/ping
```

ou, dependendo da porta exibida pelo `dotnet run`:

```bash
curl http://localhost:8080/api/ping
```

Health check:

```bash
curl http://localhost:5000/health
```

## Rodar Os Testes

```bash
dotnet test
```

O projeto de testes ja vem com:

- Teste unitario do `Output<T>`.
- Teste de integracao para `GET /api/ping`.
- Teste de integracao para `GET /health`.
- Teste de integracao para `GET /swagger/v1/swagger.json`.
- Teste de integracao para `POST /api/samples` sem depender de PostgreSQL local.

## Rodar Com Docker

Na raiz do projeto gerado:

```bash
docker compose up --build
```

A API fica disponivel em:

```text
http://localhost:8080
```

Endpoints iniciais:

```text
GET http://localhost:8080/api/ping
GET http://localhost:8080/health
GET http://localhost:8080/swagger
```

## Camadas

- `CleanArchTemplate.Domain`: entidades, value objects, enums e regras de negocio puras.
- `CleanArchTemplate.Application`: use cases, inputs, outputs, DTOs, portas/interfaces e contratos.
- `CleanArchTemplate.Infrastructure`: EF Core, banco, repositorios, clientes HTTP externos e implementacoes.
- `CleanArchTemplate.Api`: endpoints, middlewares, DI, Swagger/OpenAPI e configuracao da aplicacao.
- `CleanArchTemplate.Tests`: testes automatizados.

## Fluxo Padrao

Controllers/endpoints chamam use cases passando classes `Input`. Use cases retornam `IOutput<T>`.

DTOs de entrada e saida devem ficar em `Application/DTOs` quando forem compartilhados por mais de um caso de uso. Inputs e outputs especificos devem ficar dentro da pasta do proprio use case.

## Endpoints Iniciais

- `GET /api/ping`: controller simples com corpo `{ message, timestamp }` para validar que a API esta respondendo.
- `GET /health`: health check em JSON para uso em Docker, orquestradores e monitoramento.

## Testes

O projeto `CleanArchTemplate.Tests` ja vem com testes unitarios do `Output<T>` e testes de integracao para `/api/ping` e `/health` usando `Microsoft.AspNetCore.Mvc.Testing`.
