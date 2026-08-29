# dotnet-template-willock

Template backend em .NET com Clean Architecture.

## Uso

```bash
dotnet new install .
dotnet new dotnet-template-willock -n NomeDoProjeto
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
