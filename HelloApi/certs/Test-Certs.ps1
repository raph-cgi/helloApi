docker compose up --build -d

# vérifier que le mot de passe correspond au PFX
docker compose exec helloapi sh -lc `
  'ls -l /https && openssl pkcs12 -in /https/helloapi.pfx -passin pass:HelloApi-dev-cert -nokeys -info -noout || true'
