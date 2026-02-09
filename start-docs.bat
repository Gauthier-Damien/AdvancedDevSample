@echo off
echo ========================================
echo   MkDocs - AdvancedDevSample
echo ========================================
echo.
echo Demarrage du serveur MkDocs...
echo.
cd /d "%~dp0"
mkdocs serve
pause
