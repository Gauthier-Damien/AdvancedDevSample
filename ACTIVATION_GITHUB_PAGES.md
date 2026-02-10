# 🔧 Guide d'Activation GitHub Pages

> **Instructions pas-à-pas pour activer GitHub Pages et résoudre l'erreur 404**

---

## ❌ Erreur Actuelle

```
404 - There isn't a GitHub Pages site here
```

**Cause :** La branche `gh-pages` n'existe pas encore car le workflow GitHub Actions n'a pas encore été exécuté.

---

## ✅ Solution : Activation en 3 Étapes

### Étape 1 : Vérifier que le workflow est bien poussé

```bash
# Vérifier que vous êtes sur la branche Docs
git branch --show-current

# Vérifier que le workflow existe
ls .github/workflows/deploy-docs.yml

# Si tout est OK, passer à l'étape 2
```

### Étape 2 : Déclencher le workflow manuellement

**Option A : Via l'interface GitHub (Recommandé)**

1. Aller sur : https://github.com/Gauthier-Damien/AdvancedDevSample/actions
2. Cliquer sur "Deploy MkDocs Documentation" dans la liste à gauche
3. Cliquer sur "Run workflow" (bouton à droite)
4. Sélectionner la branche "Docs"
5. Cliquer sur "Run workflow" (vert)
6. Attendre 2-3 minutes que le workflow se termine

**Option B : Via un commit vide**

```bash
# Déclencher le workflow avec un commit vide
git commit --allow-empty -m "chore: Trigger GitHub Pages deployment"
git push origin Docs
```

### Étape 3 : Configurer GitHub Pages

Une fois que le workflow a créé la branche `gh-pages` :

1. Aller sur : https://github.com/Gauthier-Damien/AdvancedDevSample/settings/pages
2. **Build and deployment**
   - Source : **Deploy from a branch**
3. **Branch**
   - Sélectionner : **gh-pages**
   - Folder : **/ (root)**
4. Cliquer sur **Save**
5. Attendre 1-2 minutes

**La documentation sera disponible sur :**
https://gauthier-damien.github.io/AdvancedDevSample/

---

## 🔍 Vérification

### 1. Vérifier que la branche gh-pages existe

```bash
git fetch origin
git branch -r | grep gh-pages
# Devrait afficher : origin/gh-pages
```

### 2. Vérifier le workflow

- Aller sur : https://github.com/Gauthier-Damien/AdvancedDevSample/actions
- Le workflow "Deploy MkDocs Documentation" doit être ✅ (vert)

### 3. Vérifier GitHub Pages

- Aller sur : https://github.com/Gauthier-Damien/AdvancedDevSample/settings/pages
- Status doit afficher : "Your site is live at https://gauthier-damien.github.io/AdvancedDevSample/"

---

## 🐛 Dépannage

### Erreur : Workflow échoue

**Vérifier les logs :**
1. GitHub → Actions → Deploy MkDocs Documentation
2. Cliquer sur le workflow en erreur
3. Lire les logs

**Erreurs courantes :**

#### Erreur : "Config value 'nav': ..."

Un fichier référencé dans `mkdocs.yml` n'existe pas.

**Solution :**
```bash
# Vérifier que tous les fichiers existent
cd docs
ls getting-started/installation.md
ls getting-started/quick-start.md
ls quality/sonarqube-review.md
```

#### Erreur : "Permission denied"

Le workflow n'a pas les permissions.

**Solution :**
1. Settings → Actions → General
2. Workflow permissions : **Read and write permissions**
3. Save

### La branche gh-pages existe mais 404 persiste

**Attendre 1-2 minutes** pour la propagation DNS de GitHub Pages.

**Forcer le redéploiement :**
```bash
git commit --allow-empty -m "chore: Force redeploy"
git push origin Docs
```

---

## 📋 Checklist Complète

### Avant activation

- [x] Workflow `deploy-docs.yml` créé
- [x] Fichier `mkdocs.yml` configuré
- [x] Pages principales créées
- [x] Push sur branche Docs effectué

### Activation

- [ ] Déclencher le workflow (Option A ou B)
- [ ] Attendre que le workflow réussisse (✅ vert)
- [ ] Vérifier que la branche `gh-pages` existe
- [ ] Configurer GitHub Pages (Settings → Pages)
- [ ] Sélectionner branche `gh-pages`
- [ ] Sauvegarder

### Vérification

- [ ] Attendre 1-2 minutes
- [ ] Ouvrir https://gauthier-damien.github.io/AdvancedDevSample/
- [ ] La documentation s'affiche correctement ✅

---

## ⚡ Solution Rapide (Résumé)

```bash
# 1. Déclencher le déploiement
git commit --allow-empty -m "chore: Deploy GitHub Pages"
git push origin Docs

# 2. Attendre 2-3 minutes

# 3. Aller sur GitHub
# https://github.com/Gauthier-Damien/AdvancedDevSample/settings/pages
# Source: Deploy from a branch
# Branch: gh-pages / (root)
# Save

# 4. Attendre 1-2 minutes

# 5. Tester
# https://gauthier-damien.github.io/AdvancedDevSample/
```

---

## 🔗 Liens Utiles

- **GitHub Actions :** https://github.com/Gauthier-Damien/AdvancedDevSample/actions
- **Settings Pages :** https://github.com/Gauthier-Damien/AdvancedDevSample/settings/pages
- **Documentation (après activation) :** https://gauthier-damien.github.io/AdvancedDevSample/

---

## ✅ Résultat Attendu

Une fois activé, la documentation MkDocs sera accessible publiquement sur :

🌐 **https://gauthier-damien.github.io/AdvancedDevSample/**

Avec :
- ✅ Page d'accueil
- ✅ Guide d'installation
- ✅ Démarrage rapide
- ✅ Review SonarQube (prête pour vos captures)
- ✅ Navigation Material Design
- ✅ Mode clair/sombre
- ✅ Recherche intégrée

---

**🎯 Action immédiate : Déclencher le workflow pour créer la branche gh-pages**

---

*Date : 10 février 2026*
