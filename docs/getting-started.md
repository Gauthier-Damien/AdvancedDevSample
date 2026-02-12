# Guide de démarrage rapide

## 🚀 Installation et démarrage

### Prérequis

- **.NET 9.0 SDK** : [Télécharger ici](https://dotnet.microsoft.com/download/dotnet/9.0)
- **IDE** : Visual Studio 2022 ou JetBrains Rider
- **Git** : Pour cloner le repository

### Cloner le projet

```bash
git clone https://github.com/Gauthier-Damien/AdvancedDevSample.git
cd AdvancedDevSample
```

### Restaurer les dépendances

```bash
dotnet restore
```

### Lancer l'application

```bash
cd AdvancedDevSample.API
dotnet run
```

L'API sera accessible sur : **https://localhost:7000**

### Accéder à Swagger

Une fois l'application lancée, ouvrez votre navigateur :

```
https://localhost:7000/swagger
```

---

## 🔐 Comptes de démonstration

Deux utilisateurs sont créés automatiquement au démarrage :

### Étudiant
- **Username** : `demo`
- **Password** : `demo123`
- **Rôle** : Student

### Administrateur
- **Username** : `admin`
- **Password** : `admin123`
- **Rôle** : Admin

---

## 📝 Premier appel API

### 1. S'authentifier

**Endpoint :** `POST /api/Auth/login`

**Request :**
```json
{
  "username": "demo",
  "password": "demo123"
}
```

**Response :**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "expiresAt": "2026-02-12T15:30:00Z",
  "username": "demo",
  "role": "Student"
}
```

### 2. Utiliser le token

Dans Swagger :
1. Cliquez sur le bouton **Authorize** (cadenas)
2. Entrez : `Bearer {accessToken}`
3. Cliquez sur **Authorize**

Vous pouvez maintenant appeler tous les endpoints protégés.

### 3. Créer un produit

**Endpoint :** `POST /api/Product`

**Request :**
```json
{
  "name": "Laptop Dell XPS 15",
  "description": "Laptop haute performance",
  "price": 1499.99,
  "vatRate": 20
}
```

**Response 201 Created :**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Laptop Dell XPS 15",
  "description": "Laptop haute performance",
  "price": 1499.99,
  "vatRate": 20,
  "isActive": true
}
```

---

## 🧪 Exécuter les tests

### Tous les tests

```bash
dotnet test
```

**Résultat attendu :**
```
Passed! - Failed: 0, Passed: 137, Skipped: 0, Total: 137
```

### Tests avec couverture

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Tests d'un projet spécifique

```bash
cd AdvancedDevSample.Test
dotnet test
```

---

## 📁 Structure du projet

```
AdvancedDevSample/
├── AdvancedDevSample.API/          # Couche API (Controllers)
├── AdvancedDevSample.Application/  # Couche Application (Services, DTOs)
├── AdvancedDevSample.Domain/       # Couche Domain (Entités, Interfaces)
├── AdvancedDevSample.Infrastructure/ # Couche Infrastructure (Repositories)
├── AdvancedDevSample.Test/         # Tests unitaires
└── docs/                           # Documentation MkDocs
```

---

## 🔧 Configuration

### appsettings.json

Fichier de configuration principal dans `AdvancedDevSample.API/appsettings.json` :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "JwtSettings": {
    "Secret": "your-super-secret-key-min-32-characters-long",
    "Issuer": "AdvancedDevSample",
    "Audience": "AdvancedDevSample-Users",
    "ExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }
}
```

**Important :** Le `Secret` doit faire au minimum 32 caractères.

### appsettings.Development.json

Configuration spécifique à l'environnement de développement :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

---

## 🐛 Débogage

### Dans Visual Studio

1. Ouvrir `AdvancedDevSample.sln`
2. Définir `AdvancedDevSample.API` comme projet de démarrage
3. Appuyer sur `F5` pour démarrer en mode debug

### Dans Rider

1. Ouvrir `AdvancedDevSample.sln`
2. Sélectionner la configuration `AdvancedDevSample.API`
3. Appuyer sur `Shift+F9` pour démarrer en mode debug

### Logs

Les logs sont affichés dans la console. Niveau par défaut : `Information`

---

## 📚 Prochaines étapes

1. **Explorer la documentation complète** : [Architecture](architecture/index.md)
2. **Comprendre le Domain** : [Entités et règles métier](domain/index.md)
3. **Découvrir les Services** : [Application layer](application/index.md)
4. **Consulter les endpoints** : [API REST](api/index.md)
5. **Voir la persistance** : [Infrastructure](infrastructure/index.md)

---

## 🆘 Besoin d'aide ?

- **Documentation complète** : [Index](index.md)
- **Repository GitHub** : [GitHub](https://github.com/Gauthier-Damien/AdvancedDevSample)
- **Issues** : [Signaler un problème](https://github.com/Gauthier-Damien/AdvancedDevSample/issues)

---

## ✅ Checklist de démarrage

- [ ] .NET 9.0 SDK installé
- [ ] Projet cloné
- [ ] Dépendances restaurées (`dotnet restore`)
- [ ] Application lancée (`dotnet run`)
- [ ] Swagger accessible (https://localhost:7000/swagger)
- [ ] Authentification testée avec compte demo
- [ ] Premier produit créé
- [ ] Tests exécutés avec succès
