# GitHub Actions Workflows

Ce dossier contient les workflows d'intégration continue (CI) pour le projet AdvancedDevSample.

## Workflows disponibles

### 1. SonarCloud Analysis (`sonarcloud.yml`)

**Objectif :** Analyse automatique de la qualité du code avec SonarCloud

**Déclenchement :**
- Push sur `master` ou `Codding`
- Pull Request vers ces branches

**Actions :**
1. Setup JDK 17 (requis par SonarScanner)
2. Setup .NET 9.0
3. Installation SonarScanner pour .NET
4. Restauration des dépendances
5. Build avec analyse SonarCloud
6. Exécution des tests avec couverture de code
7. Envoi des résultats à SonarCloud

**Durée :** 3-5 minutes

**Dashboard SonarCloud :**  
[https://sonarcloud.io/project/overview?id=Gauthier-Damien_AdvancedDevSample](https://sonarcloud.io/project/overview?id=Gauthier-Damien_AdvancedDevSample)

**Badges :**
- Quality Gate Status
- Coverage
- Bugs
- Code Smells
- Security Rating

### 2. Build and Test (`build.yml`)

**Objectif :** Build et test rapide sans analyse SonarCloud

**Déclenchement :**
- Push sur `master` ou `Codding`
- Pull Request vers ces branches

**Actions :**
1. Setup .NET 9.0
2. Restauration des dépendances
3. Build en configuration Release
4. Exécution des tests
5. Rapport de tests

**Durée :** 1-2 minutes

## Configuration requise

### Secrets GitHub

Les workflows nécessitent les secrets suivants :

| Secret | Description | Requis pour |
|--------|-------------|-------------|
| `GITHUB_TOKEN` | Token GitHub automatique | Tous (auto-fourni) |
| `SONAR_TOKEN` | Token d'accès SonarCloud | sonarcloud.yml |

### Comment ajouter SONAR_TOKEN

1. Aller sur [SonarCloud.io](https://sonarcloud.io)
2. Se connecter avec GitHub
3. My Account → Security → Generate Token
4. Copier le token
5. GitHub Repository → Settings → Secrets → New repository secret
6. Name: `SONAR_TOKEN`, Value: coller le token

## Badges dans le README

Les badges suivants sont disponibles et se mettent à jour automatiquement :

```markdown
[![Build and Test](https://github.com/Gauthier-Damien/AdvancedDevSample/actions/workflows/build.yml/badge.svg)](...)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=...)](...)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=...)](...)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=...)](...)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=...)](...)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=...)](...)
```

## Statuts des Workflows

Voir les exécutions :
- Repository → Actions
- Sélectionner le workflow
- Voir l'historique et les logs

## Dépannage

### Workflow SonarCloud échoue

**Vérifier :**
1. Le secret `SONAR_TOKEN` est configuré
2. Le projet existe sur SonarCloud
3. Les propriétés dans `sonar-project.properties` sont correctes
4. Les tests passent localement

### Workflow Build échoue

**Vérifier :**
1. Le code compile localement : `dotnet build`
2. Les tests passent localement : `dotnet test`
3. Pas de dépendances manquantes

## Documentation complète

Pour plus de détails, consulter :
- [INTEGRATION_SONARCLOUD.md](../INTEGRATION_SONARCLOUD.md)
- [Docs/GUIDE_CORRECTEUR_SONARCLOUD.md](../Docs/GUIDE_CORRECTEUR_SONARCLOUD.md)

---

*Workflows configurés le 10 février 2026*
