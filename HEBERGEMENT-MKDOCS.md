# 📚 Documentation MkDocs - Guide d'hébergement

## ⚠️ IMPORTANT - Le dossier `site/` ne doit PAS être committé

Le dossier `site/` contient les fichiers HTML générés automatiquement.  
Il est **ignoré par Git** (voir `.gitignore`).

---

## 🌐 Hébergement de la documentation

### Option 1 : GitHub Pages (Recommandé) ✅

**Avantages** :
- ✅ 100% gratuit
- ✅ Hébergé par GitHub (pas par vous)
- ✅ Une seule commande pour déployer
- ✅ Mise à jour facile

**Déploiement** :

```bash
# Déployer la documentation sur GitHub Pages
mkdocs gh-deploy
```

Cette commande va :
1. Builder automatiquement le site
2. Créer/mettre à jour la branche `gh-pages`
3. Pousser les fichiers générés sur cette branche
4. GitHub hébergera automatiquement le site

**Accès** :

Votre documentation sera accessible sur :
```
https://yourusername.github.io/AdvancedDevSample
```

**Configuration GitHub** :

1. Aller sur GitHub.com → Votre repository → Settings → Pages
2. Vérifier que "Source" = `Deploy from a branch`
3. Sélectionner la branche `gh-pages` et le dossier `/` (root)
4. Sauvegarder

Le déploiement prend 1-2 minutes, puis votre site est en ligne !

---

### Option 2 : Netlify

**Avantages** :
- ✅ 100% gratuit
- ✅ Déploiement automatique à chaque push
- ✅ URL personnalisable
- ✅ HTTPS automatique

**Étapes** :

1. Créer un compte sur [netlify.com](https://www.netlify.com/)
2. Cliquer sur "Add new site" → "Import an existing project"
3. Connecter votre compte GitHub
4. Sélectionner votre repository `AdvancedDevSample`
5. Configuration du build :
   - **Build command** : `mkdocs build`
   - **Publish directory** : `site`
6. Cliquer sur "Deploy site"

Netlify rebuildera automatiquement la documentation à chaque push sur `main` !

**URL** : `https://your-site-name.netlify.app`

---

### Option 3 : Vercel

Même principe que Netlify :

1. Créer un compte sur [vercel.com](https://vercel.com/)
2. Importer votre repository GitHub
3. Configuration :
   - **Build Command** : `mkdocs build`
   - **Output Directory** : `site`
4. Déployer

**URL** : `https://your-site-name.vercel.app`

---

## 📦 Ce qui est sur GitHub

### ✅ Fichiers sources (commités)

```
AdvancedDevSample/
├── Docs/                    ✅ Sources Markdown
│   ├── index.md
│   ├── api/
│   ├── architecture/
│   ├── domain/
│   ├── application/
│   ├── infrastructure/
│   ├── tests/
│   └── development/
├── mkdocs.yml               ✅ Configuration MkDocs
├── requirements.txt         ✅ Dépendances Python
├── deploy-docs.ps1          ✅ Script de déploiement
├── start-docs.bat           ✅ Script de lancement
├── README-MKDOCS.md         ✅ Documentation
└── .gitignore               ✅ Ignore site/
```

### ❌ Fichiers générés (ignorés)

```
AdvancedDevSample/
└── site/                    ❌ Fichiers HTML générés (1000+ fichiers)
    ├── index.html
    ├── api/
    ├── css/
    ├── js/
    └── ...
```

Ces fichiers sont **reconstruits automatiquement** lors du déploiement.

---

## 🚀 Déploiement recommandé

### Première fois

```bash
# 1. Assurez-vous que tout est committé
git add .
git commit -m "docs: ajout documentation MkDocs complète"
git push origin main

# 2. Déployez sur GitHub Pages
mkdocs gh-deploy
```

### Mises à jour futures

```bash
# 1. Modifiez vos fichiers Markdown dans Docs/
# 2. Commitez et poussez
git add Docs/
git commit -m "docs: mise à jour de la documentation"
git push origin main

# 3. Redéployez
mkdocs gh-deploy
```

C'est tout ! Votre documentation est en ligne.

---

## 🔍 Vérification

### Localement (développement)

```bash
mkdocs serve
```
Accessible sur : `http://127.0.0.1:8000`

### En production (après déploiement)

- **GitHub Pages** : `https://yourusername.github.io/AdvancedDevSample`
- **Netlify** : `https://your-site.netlify.app`
- **Vercel** : `https://your-site.vercel.app`

---

## 💡 Résumé

| Question | Réponse |
|----------|---------|
| Dois-je héberger le site moi-même ? | ❌ Non, GitHub/Netlify/Vercel s'en charge |
| Les fichiers `site/` doivent être sur GitHub ? | ❌ Non, ils sont ignorés (`.gitignore`) |
| Comment déployer ? | ✅ `mkdocs gh-deploy` pour GitHub Pages |
| C'est gratuit ? | ✅ Oui, 100% gratuit |
| Combien de temps pour déployer ? | ✅ 1-2 minutes |
| La doc est accessible depuis Internet ? | ✅ Oui, une fois déployée |

---

## 📞 Aide

Pour toute question :
- Voir `README-MKDOCS.md` pour le guide complet
- Voir `MKDOCS-FINAL.md` pour les détails techniques
- Utiliser le script interactif : `.\deploy-docs.ps1`

---

**🎯 Recommandation : Utilisez GitHub Pages (`mkdocs gh-deploy`)**

C'est la solution la plus simple et directement intégrée à votre workflow Git !

---

*Dernière mise à jour : 9 février 2026*
