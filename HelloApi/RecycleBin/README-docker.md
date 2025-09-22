# Dockerisation de HelloApi

Ce dossier contient les fichiers nécessaires pour exécuter **HelloApi** dans Docker, en conservant la base **SQLite** dans un volume **DBPersons_Data**.

## Prérequis
- Docker Desktop (Linux containers)
- Un volume nommé **DBPersons_Data** (déjà créé d'après votre message)

> Si besoin : `docker volume create DBPersons_Data`

## Fichiers
- `Dockerfile` : image multi-étapes .NET 9 (SDK -> runtime) + script d'init de la base.
- `docker-compose.yml` : mappe le port **5000** (hôte) vers **8080** (conteneur) et monte le volume **DBPersons_Data** dans `/app/Data`.
- `.dockerignore` : évite d'envoyer les binaires/objets au build.

## Lancement rapide
Depuis la **racine** de votre solution (là où se trouve le dossier `HelloApi/`), copiez ces fichiers puis :
```bash
docker compose up --build
```
Accès API : http://localhost:5000 (Swagger sur `/` si activé, sinon `/swagger`).

## Détails SQLite & persistance
- La chaîne de connexion actuelle est `Data Source=Data/DBPersons.sqlite` dans `appsettings.json`.
- Le conteneur travaille dans `/app`, donc le chemin relatif `Data/DBPersons.sqlite` devient `/app/Data/DBPersons.sqlite`.
- Le volume **DBPersons_Data** est monté sur `/app/Data`. Il **persiste** votre fichier `DBPersons.sqlite`.
- Au **premier démarrage**, si le fichier n’existe pas dans le volume, un **script** copie la version embarquée dans l’image (`/app/seed/DBPersons.sqlite`) vers le volume.

## Commandes utiles
- Construire l’image sans compose :
  ```bash
  docker build -t helloapi:dev .
  docker run --rm -it -p 5000:8080 -v DBPersons_Data:/app/Data --name helloapi helloapi:dev
  ```
- Inspecter le volume :
  ```bash
  docker run --rm -it -v DBPersons_Data:/data alpine ls -l /data
  ```

## Notes
- Si vous changez le port hôte, modifiez `ports: - "6000:8080"` par exemple.
- Si vous déplacez la base ou renommez le fichier, adaptez le montage du volume et la chaîne de connexion.
- Pour un HTTPS local, placez un reverse-proxy (Nginx/TLS) devant, ou gérez des certificats dans le conteneur (plus complexe).


##Volumes Docker
docker run --rm -v DBPersons_Data:/data -v ${PWD}\HelloApi\Data:/seed alpine sh -c "cp -n /seed/DBPersons.sqlite /data/DBPersons.sqlite && ls -l /data"

