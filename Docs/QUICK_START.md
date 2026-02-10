# Guide de démarrage rapide

> 🚀 Commencez avec AdvancedDevSample en quelques minutes !

## ⚡ Démarrage en 5 minutes

### Prérequis

- ✅ [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installé
- ✅ Un IDE : [Visual Studio 2022](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), ou [VS Code](https://code.visualstudio.com/)
- ✅ Git installé

### 1️⃣ Cloner le projet

```powershell
git clone https://github.com/votre-username/AdvancedDevSample.git
cd AdvancedDevSample
```

### 2️⃣ Restaurer les dépendances

```powershell
dotnet restore
```

### 3️⃣ Compiler le projet

```powershell
dotnet build
```

### 4️⃣ Lancer l'application

```powershell
dotnet run --project AdvancedDevSample.API
```

### 5️⃣ Accéder à Swagger

Ouvrez votre navigateur à l'adresse :
```
https://localhost:7001/swagger
```

ou

```
http://localhost:5000/swagger
```

## 🔐 Tester l'authentification

### Comptes de démonstration

#### Compte Étudiant
- **Username:** `demo`
- **Password:** `demo123`
- **Rôle:** Student

#### Compte Administrateur
- **Username:** `admin`
- **Password:** `admin123`
- **Rôle:** Admin

### Procédure de test avec Swagger

#### Étape 1 : Se connecter

1. Dans Swagger, déroulez l'endpoint **POST /api/auth/login**
2. Cliquez sur **"Try it out"**
3. Entrez les identifiants :
```json
{
  "username": "demo",
  "password": "demo123"
}
```
4. Cliquez sur **"Execute"**
5. **Copiez le token** retourné dans la réponse

#### Étape 2 : Autoriser les requêtes

1. Cliquez sur le bouton **🔒 Authorize** en haut à droite de Swagger
2. Dans le champ "Value", entrez :
```
Bearer VOTRE_TOKEN_ICI
```
3. Cliquez sur **"Authorize"**
4. Fermez la fenêtre

#### Étape 3 : Tester les endpoints

Vous pouvez maintenant tester tous les endpoints protégés :
- GET /api/products
- GET /api/suppliers
- GET /api/users
- etc.

## 📚 Premiers pas

### Consulter les produits

```http
GET https://localhost:7001/api/products
Authorization: Bearer {votre_token}
```

### Créer un produit

```http
POST https://localhost:7001/api/products
Authorization: Bearer {votre_token}
Content-Type: application/json

{
  "name": "Nouveau Produit",
  "description": "Description du produit",
  "price": 29.99,
  "stock": 100,
  "supplierId": 1
}
```

### Consulter un produit

```http
GET https://localhost:7001/api/products/1
Authorization: Bearer {votre_token}
```

### Mettre à jour un produit

```http
PUT https://localhost:7001/api/products/1
Authorization: Bearer {votre_token}
Content-Type: application/json

{
  "name": "Produit Modifié",
  "description": "Nouvelle description",
  "price": 39.99,
  "stock": 150,
  "supplierId": 1
}
```

### Supprimer un produit

```http
DELETE https://localhost:7001/api/products/1
Authorization: Bearer {votre_token}
```

## 🧪 Lancer les tests

```powershell
dotnet test
```

Pour plus de détails :
```powershell
dotnet test --logger "console;verbosity=detailed"
```

## 🔧 Configuration

### Modifier la clé secrète JWT

Éditez le fichier `appsettings.json` :

```json
{
  "Jwt": {
    "SecretKey": "VOTRE_NOUVELLE_CLE_SECRETE_TRES_LONGUE",
    "Issuer": "AdvancedDevSample",
    "Audience": "AdvancedDevSampleUsers",
    "ExpirationMinutes": 60
  }
}
```

⚠️ **Important :** La clé secrète doit faire au moins 32 caractères.

### Modifier le port

Éditez `Properties/launchSettings.json` :

```json
{
  "applicationUrl": "https://localhost:VOTRE_PORT;http://localhost:VOTRE_PORT"
}
```

## 📖 Documentation

### Ressources disponibles

| Document | Description |
|----------|-------------|
| [README.md](../README.md) | Documentation principale |
| [ARCHITECTURE.md](./ARCHITECTURE.md) | Architecture détaillée |
| [API_DOCUMENTATION.md](./API_DOCUMENTATION.md) | Documentation des endpoints |
| [GUIDE_TEST_JWT.md](./GUIDE_TEST_JWT.md) | Guide complet de test JWT |
| [CONTRIBUTING.md](./CONTRIBUTING.md) | Guide de contribution |

## 🐛 Dépannage

### Problème : Port déjà utilisé

**Erreur :**
```
Failed to bind to address https://localhost:7001
```

**Solution :**
1. Arrêter l'application qui utilise le port
2. Ou modifier le port dans `launchSettings.json`

### Problème : Token expiré

**Erreur :**
```
401 Unauthorized
```

**Solution :**
1. Reconnecter via `/api/auth/login`
2. Obtenir un nouveau token
3. Mettre à jour l'autorisation dans Swagger

### Problème : Erreur de compilation

**Solution :**
```powershell
dotnet clean
dotnet restore
dotnet build
```

### Problème : Le fichier .exe est verrouillé

**Erreur :**
```
MSB3027: Impossible de copier [...] Le fichier est verrouillé
```

**Solution :**
1. Arrêter tous les processus de l'application
2. Fermer Rider/Visual Studio
3. Relancer

Ou en PowerShell :
```powershell
Stop-Process -Name "AdvancedDevSample.API" -Force
```

## 🎯 Étapes suivantes

1. ✅ **Explorer l'API** - Testez tous les endpoints avec Swagger
2. ✅ **Lire l'architecture** - Comprenez la structure du projet
3. ✅ **Consulter le code** - Examinez les contrôleurs et services
4. ✅ **Écrire des tests** - Ajoutez vos propres tests
5. ✅ **Contribuer** - Proposez des améliorations

## 📞 Besoin d'aide ?

- 📚 Consultez la [documentation complète](./README.md)
- 🐛 Signalez un bug via les Issues GitHub
- 💡 Proposez une amélioration via une Pull Request

## 🎓 Ressources d'apprentissage

### Concepts clés à maîtriser

- **Clean Architecture** - Séparation des responsabilités
- **Repository Pattern** - Abstraction de l'accès aux données
- **Dependency Injection** - Inversion de contrôle
- **JWT Authentication** - Authentification par token
- **RESTful API** - Principes REST

### Tutoriels recommandés

- [Clean Architecture avec .NET](https://docs.microsoft.com/fr-fr/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [ASP.NET Core - Vue d'ensemble](https://docs.microsoft.com/fr-fr/aspnet/core/)
- [JWT Authentication](https://jwt.io/introduction)

## ✅ Checklist de démarrage

- [ ] .NET 9.0 SDK installé
- [ ] Projet cloné
- [ ] Dépendances restaurées
- [ ] Compilation réussie
- [ ] Application lancée
- [ ] Swagger accessible
- [ ] Connexion réussie avec compte démo
- [ ] Token JWT obtenu
- [ ] Endpoint testé avec succès
- [ ] Documentation consultée

---

**Félicitations ! Vous êtes prêt à travailler avec AdvancedDevSample ! 🎉**

*Dernière mise à jour : 2026-02-10*
