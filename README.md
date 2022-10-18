# Ainda em desenolvimento.

Passos para executar:

No Linux:
```bash

bash configurar-ambiente.sh

```
Manual:
```bash
docker network create --attachable -d bridge mydockernetwork
docker compose build
docker compose up -d
echo "Ambiente pronto"

```

Esse script ira fazer um build das dependências para executar o projeto.\
No momento o projeto deve ser rodando no Rider ou VisualStudio.\
Ainda estou fazendo alguns ajustes no arquivo yaml do Kafka para o projeto rodar dentro do contêiner.

### Kafka
Está com a configuração mais basica sem replicas

### MongoDB
Para armazenar os eventos

### Dozzle
O serviço dozzle serve para visualizar os logs dos container.
