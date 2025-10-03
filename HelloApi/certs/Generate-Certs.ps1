# 0) Stopper le conteneur (on garde le volume DB)
docker compose down

# 1) Supprimer l'ancien PFX
Remove-Item -Path .\certs\helloapi.pfx -Force

# 2) (au cas où) s'assurer que le dossier existe
New-Item -ItemType Directory -Path .\certs -Force | Out-Null

# 3) Régénérer et approuver le certificat dev
dotnet dev-certs https --trust
dotnet dev-certs https -ep "$PWD\certs\helloapi.pfx" -p "HelloApi!2025"
