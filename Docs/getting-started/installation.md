# Installation

> **Guide d'installation détaillé**

---

## Prérequis

### .NET 9.0 SDK

**Vérifier l'installation :**
```bash
dotnet --version
```

**Installer :**  
Télécharger depuis [https://dotnet.microsoft.com/download/dotnet/9.0](https://dotnet.microsoft.com/download/dotnet/9.0)

### IDE Recommandé

=== "JetBrains Rider"
    **Recommandé pour ce projet**
    
    - [Télécharger Rider](https://www.jetbrains.com/rider/)
    - Excellent support .NET et C#
    - Refactoring puissant
    
=== "Visual Studio 2022"
    - [Télécharger VS 2022](https://visualstudio.microsoft.com/)
    - Version Community gratuite
    - Workload ".NET Desktop Development" requis

=== "Visual Studio Code"
    - [Télécharger VS Code](https://code.visualstudio.com/)
    - Installer l'extension C# DevKit
    - Léger mais moins complet

### Git

```bash
git --version
```

[Télécharger Git](https://git-scm.com/downloads)

---

## Installation du Projet

### 1. Cloner le Repository

```bash
git clone https://github.com/Gauthier-Damien/AdvancedDevSample.git
cd AdvancedDevSample
```

### 2. Vérifier la Structure

```
AdvancedDevSample/
├── AdvancedDevSample.sln          # Solution principale
├── AdvancedDevSample.API/         # API REST
├── AdvancedDevSample.Application/ # Services
├── AdvancedDevSample.Domain/      # Cœur métier
├── AdvancedDevSample.Infrastructure/ # Données
└── AdvancedDevSample.Test/        # Tests
```

### 3. Restaurer les Packages NuGet

```bash
dotnet restore
```

### 4. Compiler la Solution

```bash
dotnet build
```

**Résultat attendu :**
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

---

## Configuration

### appsettings.json

Le fichier se trouve dans `AdvancedDevSample.API/appsettings.json`.

**Configuration JWT par défaut :**
```json
{
  "JwtSettings": {
    "SecretKey": "VotreCleSuperSecreteICIPourLesTokensJWT2024!",
    "Issuer": "AdvancedDevSample",
    "Audience": "AdvancedDevSampleUsers",
    "ExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

!!! warning "Secret JWT"
    En production, ne JAMAIS commiter le secret JWT dans Git. Utiliser :
    - Azure Key Vault
    - Variables d'environnement
    - User Secrets (.NET)

### User Secrets (Développement)

```bash
cd AdvancedDevSample.API
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecretKey" "VotreSecretLocal"
```

---

## Vérification de l'Installation

### 1. Lancer les Tests

```bash
dotnet test
```

**Résultat attendu :**
```
Passed!  - Failed:     0, Passed:   137, Skipped:     0, Total:   137
```

### 2. Lancer l'Application

```bash
dotnet run --project AdvancedDevSample.API
```

**Résultat attendu :**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5181
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shutdown.

Comptes de demo crees automatiquement:
   Student: demo / demo123
   Admin: admin / admin123
```

### 3. Tester Swagger

Ouvrir [https://localhost:5181/swagger](https://localhost:5181/swagger)

---

## IDE Spécifique

### Rider

1. Ouvrir `AdvancedDevSample.sln`
2. Attendre l'indexation
3. Run → Run 'AdvancedDevSample.API'
4. Swagger s'ouvre automatiquement

### Visual Studio 2022

1. Ouvrir `AdvancedDevSample.sln`
2. Définir `AdvancedDevSample.API` comme projet de démarrage
3. F5 pour lancer
4. Swagger s'ouvre automatiquement

### Visual Studio Code

1. Ouvrir le dossier
2. Terminal → `dotnet run --project AdvancedDevSample.API`
3. Ouvrir manuellement Swagger

---

## Packages NuGet

### Installation Automatique

Les packages sont automatiquement installés lors du `dotnet restore`.

### Packages Principaux

```xml
<!-- API -->
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />

<!-- Application -->
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />

<!-- Tests -->
<PackageReference Include="xunit" Version="2.6.5" />
<PackageReference Include="Moq" Version="4.20.70" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

### Installer Manuellement un Package

```bash
cd AdvancedDevSample.API
dotnet add package NomDuPackage
```

---

## Troubleshooting

### Erreur : Framework .NET 9.0 non trouvé

**Solution :**  
Installer le SDK .NET 9.0 : [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

### Erreur : Packages NuGet non restaurés

```bash
dotnet clean
dotnet restore --force
dotnet build
```

### Erreur : Port 5181 occupé

Modifier `AdvancedDevSample.API/Properties/launchSettings.json` :

```json
"applicationUrl": "https://localhost:5182;http://localhost:5002"
```

### Erreur : Certificat SSL

```bash
dotnet dev-certs https --trust
```

---

## Prochaines Étapes

✅ Installation terminée !

Continuez avec :

- [Démarrage Rapide](quick-start.md)
- [Tests avec Swagger](swagger-testing.md)

---

*Dernière mise à jour : 10 février 2026*
