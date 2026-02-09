# Installation et Configuration

## Prérequis

### Logiciels requis

| Logiciel | Version minimale | Lien |
|----------|------------------|------|
| .NET SDK | 9.0 | [Télécharger](https://dotnet.microsoft.com/download) |
| IDE | - | Rider, Visual Studio 2022, ou VS Code |
| Git | 2.0+ | [Télécharger](https://git-scm.com/) |

### Optionnel

- **Docker** : Pour containerisation future
- **Postman** : Alternative à Swagger pour tester l'API

## Installation

### 1. Cloner le repository

```bash
git clone https://github.com/yourusername/AdvancedDevSample.git
cd AdvancedDevSample
```

### 2. Restaurer les dépendances

```bash
dotnet restore
```

### 3. Compiler la solution

```bash
dotnet build
```

### 4. Exécuter les tests

```bash
dotnet test
```

## Configuration de l'environnement

### Variables d'environnement

Créer un fichier `appsettings.Development.json` dans `AdvancedDevSample.API` :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Configuration de l'IDE

#### JetBrains Rider

1. Ouvrir `AdvancedDevSample.sln`
2. Définir `AdvancedDevSample.API` comme projet de démarrage
3. Configurer le profil de démarrage : `https` ou `http`

#### Visual Studio

1. Ouvrir `AdvancedDevSample.sln`
2. Clic droit sur `AdvancedDevSample.API` → "Définir comme projet de démarrage"
3. Appuyer sur F5 pour lancer en mode debug

#### VS Code

1. Installer l'extension **C# Dev Kit**
2. Ouvrir le dossier du projet
3. Créer `.vscode/launch.json` :

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/AdvancedDevSample.API/bin/Debug/net9.0/AdvancedDevSample.API.dll",
      "args": [],
      "cwd": "${workspaceFolder}/AdvancedDevSample.API",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  ]
}
```

## Lancer l'application

### En ligne de commande

```bash
cd AdvancedDevSample.API
dotnet run
```

### Accès à l'application

Une fois lancée, l'application est accessible via :

- **HTTPS** : `https://localhost:5181`
- **HTTP** : `http://localhost:5180`
- **Swagger UI** : `https://localhost:5181/swagger`

## Configuration des commentaires XML (Swagger)

Les commentaires XML sont automatiquement générés. Vérifier dans chaque `.csproj` :

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

## Commandes utiles

### Build

```bash
# Build en mode Debug
dotnet build

# Build en mode Release
dotnet build -c Release

# Build avec verbosité
dotnet build -v detailed
```

### Tests

```bash
# Lancer tous les tests
dotnet test

# Lancer les tests avec couverture
dotnet test --collect:"XPlat Code Coverage"

# Lancer un projet de test spécifique
dotnet test AdvancedDevSample.Test/AdvancedDevSample.Test.csproj
```

### Nettoyage

```bash
# Nettoyer les artifacts de build
dotnet clean

# Nettoyer et rebuild
dotnet clean && dotnet build
```

## Résolution de problèmes

### Port déjà utilisé

Si le port 5181 est déjà utilisé :

1. Modifier `Properties/launchSettings.json` dans `AdvancedDevSample.API`
2. Changer les ports dans `applicationUrl`

```json
"applicationUrl": "https://localhost:7001;http://localhost:5001"
```

### Erreurs de restauration NuGet

```bash
# Nettoyer le cache NuGet
dotnet nuget locals all --clear

# Restaurer à nouveau
dotnet restore
```

### Erreurs de compilation

```bash
# Vérifier la version du SDK
dotnet --version

# Lister les SDKs installés
dotnet --list-sdks
```

## Documentation de l'API

### Générer la documentation Swagger statique

```bash
# Installer Swashbuckle CLI
dotnet tool install -g Swashbuckle.AspNetCore.Cli

# Générer le fichier swagger.json
swagger tofile --output swagger.json AdvancedDevSample.API/bin/Debug/net9.0/AdvancedDevSample.API.dll v1
```

## Prochaines étapes

- ✅ [Configuration avancée](configuration.md)
- ✅ [Bonnes pratiques de développement](best-practices.md)
- ✅ [Guide de contribution](../development/best-practices.md)

---

*Guide mis à jour pour .NET 9.0 - Février 2026*
