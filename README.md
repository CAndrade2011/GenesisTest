# CDB Calculadora - WebAPI e Frontend Angular

Este projeto fornece uma aplicação completa para cálculo de investimentos em CDB (Certificado de Depósito Bancário), usando uma **WebAPI em ASP.NET Core 8** e um **frontend Angular 18+**.

## 🧮 Funcionalidade

Calcula o **valor bruto** e **valor líquido** de um investimento em CDB, com base em:
- Valor inicial investido
- Prazo (em meses)
- Alíquota regressiva do IR, conforme regra de mercado

---

## 🧩 Tecnologias Utilizadas

### Backend (API)
- .NET 8
- ASP.NET Core Web API
- SOLID Principles
- Testes unitários com xUnit, FluentAssertions e Moq
- Cobertura de testes com `coverlet` e `ReportGenerator`
- Docker

### Frontend
- Angular 18
- Angular CLI
- SCSS
- Docker

---

## 🚀 Como Executar Localmente

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://www.docker.com/)

---

#### Executar com Docker

```bash
docker-compose up --build
```
O frontend estará em: `http://localhost:4200`
O backend estará em: `http://localhost:5143/swagger`



### 🔧 Backend (API)

#### Executar localmente (sem Docker)

```bash
cd API
dotnet run
```

A API estará acessível em: `https://localhost:5143`

---

### 💻 Frontend (Angular)

#### Instalar dependências

```bash
cd Angular/cdb-frontend
npm install
```

#### Executar localmente (sem Docker)

```bash
ng serve
```

A aplicação estará em: `http://localhost:4200`

---

## ✅ Testes

### Backend
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:ExcludeByAttribute=System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage
```

### Frontend
```bash
ng test  (Não foi implementado)
```

---

## 🔒 Autenticação via API Key

A API exige um cabeçalho com a chave:

```http
X-API-Key: Genesis-API-Key-2024
```

Exemplo com `curl`:

```bash
curl -X POST "https://localhost:5143/calculadora/calcular" \
  -H "Content-Type: application/json" \
  -H "X-API-Key: Genesis-API-Key-2024" \
  -d '{"valorInicial": 1000, "prazoMeses": 12}'
```

---

## 🧪 Cobertura de Testes

- API: > 90% (requerido)
- Validação de lógica, entrada e exceções

Para ver o relatório de cobertura
```bash
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
coveragereport\index.html
```

Caso o comando reportgenerator não exista, pode ser instalado com o comando abaixo:
```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```

---

## 📁 Estrutura de Pastas

```
Genesis/
├── API/                   # WebAPI .NET
├── API.Tests/             # Testes unitários da API
├── Application/           # Camada de lógica de negócio (Application Layer)
├── Application.Tests/     # Testes unitários da Application
├── Domain/                # Testes diretos das entidades e objetos de valor
├── Domain.Tests/          # Testes unitários do domínio
├── Domain.Services/       # Serviços de domínio: lógica de negócio complexa
├── Domain.Services.Tests/ # Testes unitários dos serviços de domínio
├── Angular/cdb-frontend/  # Aplicação Angular
```

---
