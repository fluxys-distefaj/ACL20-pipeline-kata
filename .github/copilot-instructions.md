# Copilot instructions — ACL20 pipeline kata

## Stack
- .NET 10 · Blazor WASM (client) · ASP.NET Core (gateway, backend)
- Monorepo: src/{shared/contracts,client,gateway,backend}

## Conventions
- Clean Architecture + CQRS in the backend
- Keep a change scoped to one component unless the shared contract changes

## Definition of done (before marking the PR ready for review)
- dotnet build -c Release with no warnings
- dotnet test green
- touch version.json only if you intend a version bump