# ⚡ Actions Rapides - Activation GitHub Pages

> **Checklist rapide pour activer GitHub Pages en 5 minutes**

---

## ✅ ÉTAPE 1 : Vérifier le Workflow (30 secondes)

1. **Ouvrir :** https://github.com/Gauthier-Damien/AdvancedDevSample/actions

2. **Chercher :** "Deploy MkDocs Documentation" dans la liste

3. **Vérifier :** Le workflow doit être en cours (🟡 jaune) ou terminé (✅ vert)

!!! tip "Workflow en cours"
    Le push que je viens de faire a déclenché automatiquement le workflow.
    Attendez 2-3 minutes qu'il se termine.

---

## ✅ ÉTAPE 2 : Attendre le Déploiement (2-3 minutes)

**Que fait le workflow ?**
- Build de la documentation avec MkDocs
- Création de la branche `gh-pages`
- Publication du contenu

**Comment savoir que c'est terminé ?**
- Le workflow affiche ✅ (vert) au lieu de 🟡 (jaune)
- Status : "Success"

---

## ✅ ÉTAPE 3 : Configurer GitHub Pages (1 minute)

1. **Ouvrir :** https://github.com/Gauthier-Damien/AdvancedDevSample/settings/pages

2. **Build and deployment**
   - Source : Sélectionner **"Deploy from a branch"**

3. **Branch**
   - Branch : Sélectionner **"gh-pages"**
   - Folder : Sélectionner **"/ (root)"**

4. **Sauvegarder**
   - Cliquer sur **"Save"**

5. **Attendre 1-2 minutes** pour la propagation

---

## 🌐 ÉTAPE 4 : Vérifier (30 secondes)

**Ouvrir :** https://gauthier-damien.github.io/AdvancedDevSample/

**Résultat attendu :**
- ✅ La page d'accueil MkDocs s'affiche
- ✅ Navigation fonctionnelle
- ✅ Mode clair/sombre disponible
- ✅ Recherche opérationnelle

---

## 🐛 Si ça ne fonctionne pas

### Erreur : Workflow échoue (❌ rouge)

1. Cliquer sur le workflow en erreur
2. Lire les logs
3. Me contacter avec le message d'erreur

### Erreur : 404 persiste après configuration

1. **Attendre 2-3 minutes** supplémentaires (propagation DNS)
2. **Vider le cache** du navigateur (Ctrl + F5)
3. **Vérifier** que la branche `gh-pages` existe :
   - https://github.com/Gauthier-Damien/AdvancedDevSample/tree/gh-pages

### Erreur : La branche gh-pages n'apparaît pas

Le workflow n'a pas encore terminé ou a échoué.
1. Retourner sur Actions
2. Vérifier le statut
3. Attendre ou relancer si échec

---

## 📊 Timeline Complète

```
T+0min  : Push effectué (par moi) ✅
T+0min  : Workflow démarre automatiquement
T+2min  : Workflow en cours de build
T+3min  : Workflow termine, branche gh-pages créée ✅
T+3min  : Vous configurez GitHub Pages
T+4min  : Configuration sauvegardée
T+5min  : DNS propagé, site accessible ✅
```

**Temps total : ~5 minutes**

---

## 🎯 Résultat Final

Une fois activé :

✅ **URL de la documentation :**  
https://gauthier-damien.github.io/AdvancedDevSample/

✅ **Contenu disponible :**
- Page d'accueil
- Guide d'installation
- Démarrage rapide
- Review SonarQube (prête pour vos captures)

✅ **Mise à jour automatique :**
- Chaque push sur `Docs` redéploie automatiquement
- Pas besoin de reconfigurer

---

## 📞 Aide

**Fichiers de référence :**
- `ACTIVATION_GITHUB_PAGES.md` - Guide complet
- `MKDOCS_README.md` - Documentation MkDocs
- `RECAPITULATIF_MKDOCS.md` - Récapitulatif configuration

**Liens utiles :**
- GitHub Actions : https://github.com/Gauthier-Damien/AdvancedDevSample/actions
- Settings Pages : https://github.com/Gauthier-Damien/AdvancedDevSample/settings/pages

---

**⏰ À FAIRE MAINTENANT :**

1. ✅ Vérifier le workflow : https://github.com/Gauthier-Damien/AdvancedDevSample/actions
2. ⏳ Attendre 2-3 minutes
3. ⚙️ Configurer Pages : https://github.com/Gauthier-Damien/AdvancedDevSample/settings/pages
4. 🌐 Tester : https://gauthier-damien.github.io/AdvancedDevSample/

---

*Actions à effectuer : 10 février 2026*
