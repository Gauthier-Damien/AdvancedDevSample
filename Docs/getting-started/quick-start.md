# 🚀 Démarrage Rapide

> **Lancez l'application en 5 minutes**

---

## Prérequis

Avant de commencer, assurez-vous d'avoir :

- [x] **.NET 9.0 SDK** installé
- [x] Un IDE : [Rider](https://www.jetbrains.com/rider/), [Visual Studio 2022](https://visualstudio.microsoft.com/), ou [VS Code](https://code.visualstudio.com/)
- [x] Git installé

!!! info "Vérifier .NET"
    ```bash
    dotnet --version
    # Devrait afficher 9.0.x
    ```

---

## Installation

### Étape 1 : Cloner le Repository

```bash
git clone https://github.com/Gauthier-Damien/AdvancedDevSample.git
cd AdvancedDevSample
```

### Étape 2 : Restaurer les Dépendances

```bash
dotnet restore
```

### Étape 3 : Compiler le Projet

```bash
dotnet build
```

### Étape 4 : Lancer les Tests

```bash
dotnet test
```

**Résultat attendu :**
```
✅ Passed: 137
❌ Failed: 0
Total: 137
```

### Étape 5 : Lancer l'Application

```bash
dotnet run --project AdvancedDevSample.API
```

---

## 🌐 Accès à l'Application

Une fois l'application lancée :

| Service | URL | Description |
|---------|-----|-------------|
| **Swagger UI** | [https://localhost:5181/swagger](https://localhost:5181/swagger) | Interface de test de l'API |
| **API** | [https://localhost:5181/api](https://localhost:5181/api) | API REST |

---

## 👤 Comptes de Démonstration

Deux comptes sont préconfigurés :

| Username | Password | Rôle | Description |
|----------|----------|------|-------------|
| `demo` | `demo123` | **Student** | Compte étudiant |
| `admin` | `admin123` | **Admin** | Compte administrateur |

!!! tip "Comptes créés automatiquement"
    Ces comptes sont créés au démarrage de l'application (voir `Program.cs`)

---

## 🧪 Test Rapide avec Swagger

### 1. Ouvrir Swagger

Ouvrir [https://localhost:5181/swagger](https://localhost:5181/swagger) dans votre navigateur.

### 2. S'authentifier

1. Cliquer sur `POST /api/auth/login`
2. Cliquer sur **Try it out**
3. Entrer :
   ```json
   {
     "username": "demo",
     "password": "demo123"
   }
   ```
4. Cliquer sur **Execute**
5. Copier le `accessToken` dans la réponse

### 3. Autoriser les Requêtes

1. Cliquer sur le bouton 🔒 **Authorize** (en haut à droite)
2. Entrer : `Bearer VOTRE_TOKEN`
3. Cliquer sur **Authorize**
4. Fermer la popup

### 4. Tester un Endpoint Protégé

1. Cliquer sur `GET /api/auth/me`
2. Cliquer sur **Try it out**
3. Cliquer sur **Execute**
4. Voir vos informations utilisateur !

---

## 📋 Commandes Utiles

### Développement

```bash
# Lancer en mode watch (redémarrage automatique)
dotnet watch run --project AdvancedDevSample.API

# Lancer les tests en continu
dotnet watch test
```

### Tests

```bash
# Tous les tests
dotnet test

# Tests avec détails
dotnet test --logger "console;verbosity=detailed"

# Tests avec couverture
dotnet test /p:CollectCoverage=true
```

### Build

```bash
# Build Debug
dotnet build

# Build Release
dotnet build --configuration Release

# Clean + Build
dotnet clean && dotnet build
```

---

## 🔧 Configuration

### appsettings.json

Le fichier principal de configuration se trouve dans `AdvancedDevSample.API/appsettings.json` :

```json
{
  "JwtSettings": {
    "SecretKey": "VotreCleSuperSecretePourJWT...",
    "Issuer": "AdvancedDevSample",
    "Audience": "AdvancedDevSampleUsers",
    "ExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }
}
```

!!! warning "Production"
    En production, utilisez **Azure Key Vault** ou **variables d'environnement** pour les secrets.

---

## ❓ Dépannage

### Erreur : SDK .NET non trouvé

```bash
# Installer .NET 9.0 SDK
# https://dotnet.microsoft.com/download/dotnet/9.0
```

### Erreur : Port 5181 déjà utilisé

Modifier le port dans `AdvancedDevSample.API/Properties/launchSettings.json`.

### Erreur de compilation

```bash
# Nettoyer et reconstruire
dotnet clean
dotnet restore
dotnet build
```

---

## 🎯 Prochaines Étapes

- [x] Application lancée
- [ ] [Explorer l'Architecture](../architecture/overview.md)
- [ ] [Tester les endpoints API](swagger-testing.md)
- [ ] [Comprendre l'authentification JWT](../api/authentication.md)
- [ ] [Lancer les tests](../guides/unit-testing.md)

---

!!! success "Bravo !"
    Votre environnement est prêt ! 🎉

---

*Dernière mise à jour : 10 février 2026*
