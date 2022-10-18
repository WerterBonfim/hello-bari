

Passos para executar:

No Linux:
```bash

sh configurar-ambiente.sh

```
Manual:
```bash
docker network create --attachable -d bridge mydockernetwork
docker-compose build
docker-compose up -d
echo "Ambiente pronto"

```

Esse script ira fazer um build das dependências para executar o projeto.\
No momento o projeto deve ser rodando no Rider ou VisualStudio.\
Ainda estou fazendo alguns ajustes no arquivo yaml do Kafka para o projeto rodar dentro do contêiner.


