# Guide de contribution

> 🚧 **En cours de rédaction**

Merci de votre intérêt pour contribuer à **AdvancedDevSample** !

## 🎯 Objectif du projet

Ce projet est un exemple pédagogique de développement avancé avec ASP.NET Core, illustrant les bonnes pratiques et l'architecture Clean.

## 🌳 Organisation des branches

### Branches principales

- **`main`** : Branche de production stable
- **`Codding`** : Branche de développement principal
- **`Docs`** : Branche dédiée à la documentation

### Workflow Git

```
main (production)
  │
  ├── Codding (développement)
  │     │
  │     └── feature/nom-feature (fonctionnalités)
  │
  └── Docs (documentation)
        │
        └── docs/nom-doc (documentation spécifique)
```

## 📝 Convention de commits

Nous suivons la convention [Conventional Commits](https://www.conventionalcommits.org/fr/).

### Format

```
<type>(<scope>): <description>

[corps optionnel]

[pied de page optionnel]
```

### Types

- **feat**: Nouvelle fonctionnalité
- **fix**: Correction de bug
- **docs**: Documentation uniquement
- **style**: Formatage, point-virgules manquants, etc.
- **refactor**: Refactoring du code
- **test**: Ajout ou modification de tests
- **chore**: Maintenance, configuration

### Exemples

```bash
feat(auth): ajouter l'authentification JWT
fix(product): corriger la validation du prix
docs(api): mettre à jour la documentation des endpoints
refactor(service): simplifier la logique métier
test(user): ajouter tests unitaires pour UserService
```

## 🏗️ Standards de code

### C# / .NET

- **Conventions de nommage** :
  - PascalCase pour les classes, méthodes, propriétés
  - camelCase pour les variables locales et paramètres
  - Interface préfixée par `I` (ex: `IProductRepository`)

- **Organisation des fichiers** :
  - Un fichier par classe
  - Nom du fichier = nom de la classe

- **Commentaires** :
  - Utiliser les commentaires XML pour la documentation publique
  - Commenter le "pourquoi", pas le "quoi"

### Architecture

- Respecter la séparation des couches
- Ne jamais référencer Infrastructure depuis Domain
- Les dépendances vont toujours vers le Domain
- Utiliser l'injection de dépendances

### Exemple de structure

```csharp
namespace AdvancedDevSample.Application.Services;

/// <summary>
/// Service de gestion des produits.
/// </summary>
public class ProductService
{
    private readonly IProductRepository _repository;
    
    /// <summary>
    /// Initialise une nouvelle instance de ProductService.
    /// </summary>
    /// <param name="repository">Repository des produits</param>
    public ProductService(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    // ... méthodes
}
```

## 🧪 Tests

### Principes

- **Couverture** : Viser au minimum 80% de couverture
- **AAA Pattern** : Arrange, Act, Assert
- **Nommage** : `MethodeName_Scenario_ExpectedBehavior`

### Exemple

```csharp
[Fact]
public void GetProductById_ValidId_ReturnsProduct()
{
    // Arrange
    var repository = new Mock<IProductRepository>();
    var service = new ProductService(repository.Object);
    
    // Act
    var result = service.GetById(1);
    
    // Assert
    Assert.NotNull(result);
}
```

## 📦 Processus de contribution

### 1. Fork et Clone

```bash
git clone https://github.com/votre-username/AdvancedDevSample.git
cd AdvancedDevSample
```

### 2. Créer une branche

```bash
# Pour une fonctionnalité
git checkout -b feature/nom-feature

# Pour une correction
git checkout -b fix/nom-bug

# Pour la documentation
git checkout Docs
git checkout -b docs/nom-doc
```

### 3. Développer

- Écrire le code
- Ajouter les tests
- Mettre à jour la documentation si nécessaire

### 4. Tester

```bash
dotnet build
dotnet test
```

### 5. Commiter

```bash
git add .
git commit -m "feat(scope): description"
```

### 6. Pousser

```bash
git push origin feature/nom-feature
```

### 7. Pull Request

- Créer une PR vers la branche `Codding`
- Décrire les changements
- Lier les issues concernées

## ✅ Checklist avant PR

- [ ] Le code compile sans erreurs
- [ ] Les tests passent tous
- [ ] Les nouveaux tests sont ajoutés
- [ ] La documentation est mise à jour
- [ ] Les conventions de nommage sont respectées
- [ ] Les commentaires XML sont présents
- [ ] Pas de code commenté inutile
- [ ] Pas de `Console.WriteLine` ou debug statements

## 🐛 Rapporter un bug

### Template d'issue

```markdown
**Description**
Description claire du bug

**Reproduction**
1. Aller à '...'
2. Cliquer sur '...'
3. Voir l'erreur

**Comportement attendu**
Description du comportement attendu

**Captures d'écran**
Si applicable

**Environnement**
- OS: [ex: Windows 11]
- .NET: [ex: 9.0]
- Version: [ex: 1.0.0]
```

## 💡 Proposer une fonctionnalité

### Template d'issue

```markdown
**Problème à résoudre**
Description du problème

**Solution proposée**
Description de la solution

**Alternatives considérées**
Autres solutions possibles

**Contexte additionnel**
Informations supplémentaires
```

## 📚 Ressources

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://fr.wikipedia.org/wiki/SOLID_(informatique))
- [ASP.NET Core Documentation](https://docs.microsoft.com/fr-fr/aspnet/core/)
- [C# Coding Conventions](https://docs.microsoft.com/fr-fr/dotnet/csharp/fundamentals/coding-style/coding-conventions)

## 🙏 Remerciements

Merci à tous les contributeurs qui rendent ce projet meilleur !

---

*Dernière mise à jour : 2026-02-10*
