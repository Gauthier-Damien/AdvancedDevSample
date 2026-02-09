# Script de déploiement de la documentation MkDocs
# Auteur: Gautier
# Date: Février 2026

Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "  Documentation MkDocs - AdvancedDevSample" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host ""

# Menu principal
function Show-Menu {
    Write-Host "Que souhaitez-vous faire ?" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "1. Lancer le serveur de développement (http://127.0.0.1:8000)" -ForegroundColor Green
    Write-Host "2. Construire la documentation (site statique)" -ForegroundColor Green
    Write-Host "3. Déployer sur GitHub Pages" -ForegroundColor Green
    Write-Host "4. Nettoyer les fichiers générés" -ForegroundColor Green
    Write-Host "5. Vérifier la configuration" -ForegroundColor Green
    Write-Host "6. Ouvrir la documentation dans le navigateur" -ForegroundColor Green
    Write-Host "7. Quitter" -ForegroundColor Red
    Write-Host ""
}

function Start-DevServer {
    Write-Host "Démarrage du serveur de développement..." -ForegroundColor Cyan
    Write-Host "La documentation sera accessible sur: http://127.0.0.1:8000" -ForegroundColor Green
    Write-Host "Appuyez sur Ctrl+C pour arrêter le serveur" -ForegroundColor Yellow
    Write-Host ""
    mkdocs serve
}

function Build-Documentation {
    Write-Host "Construction de la documentation..." -ForegroundColor Cyan
    mkdocs build --clean
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Documentation construite avec succès!" -ForegroundColor Green
        Write-Host "Les fichiers sont dans le dossier: site/" -ForegroundColor Green
    } else {
        Write-Host "✗ Erreur lors de la construction" -ForegroundColor Red
    }
}

function Deploy-GitHubPages {
    Write-Host "Déploiement sur GitHub Pages..." -ForegroundColor Cyan
    Write-Host "⚠ Assurez-vous que le repository est configuré pour GitHub Pages" -ForegroundColor Yellow
    $confirm = Read-Host "Continuer? (O/N)"
    if ($confirm -eq 'O' -or $confirm -eq 'o') {
        mkdocs gh-deploy
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✓ Déploiement réussi!" -ForegroundColor Green
        } else {
            Write-Host "✗ Erreur lors du déploiement" -ForegroundColor Red
        }
    }
}

function Clean-GeneratedFiles {
    Write-Host "Nettoyage des fichiers générés..." -ForegroundColor Cyan
    if (Test-Path "site") {
        Remove-Item -Recurse -Force "site"
        Write-Host "✓ Dossier 'site/' supprimé" -ForegroundColor Green
    } else {
        Write-Host "⚠ Aucun fichier à nettoyer" -ForegroundColor Yellow
    }
}

function Test-Configuration {
    Write-Host "Vérification de la configuration..." -ForegroundColor Cyan
    mkdocs build --strict
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Configuration valide!" -ForegroundColor Green
    } else {
        Write-Host "✗ Erreurs détectées dans la configuration" -ForegroundColor Red
    }
}

function Open-Documentation {
    Write-Host "Ouverture de la documentation dans le navigateur..." -ForegroundColor Cyan
    if (Test-Path "site\index.html") {
        Start-Process "site\index.html"
        Write-Host "✓ Documentation ouverte" -ForegroundColor Green
    } else {
        Write-Host "✗ Documentation non construite. Utilisez l'option 2 d'abord." -ForegroundColor Red
    }
}

# Boucle principale
do {
    Show-Menu
    $choice = Read-Host "Votre choix"
    Write-Host ""
    
    switch ($choice) {
        '1' { Start-DevServer }
        '2' { Build-Documentation }
        '3' { Deploy-GitHubPages }
        '4' { Clean-GeneratedFiles }
        '5' { Test-Configuration }
        '6' { Open-Documentation }
        '7' { 
            Write-Host "Au revoir!" -ForegroundColor Cyan
            exit 
        }
        default { 
            Write-Host "✗ Choix invalide. Veuillez choisir entre 1 et 7." -ForegroundColor Red 
        }
    }
    
    if ($choice -ne '7' -and $choice -ne '1') {
        Write-Host ""
        Write-Host "Appuyez sur une touche pour continuer..." -ForegroundColor Gray
        $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
        Clear-Host
    }
    
} while ($choice -ne '7')
