#!/usr/bin/env bash

# configurar-ambiente.sh
#
# Titulo:       Levanta um ambiente docker do teste ...
# Autor:        Werter Bonfim
# Manutenção:   Werter Bonfim
# Site:         https://github.com/WerterBonfim
# Data:         18-12-2022
# Versão:       1.0.0

#
# ------------------------------------------------------------------------ #
#  Este programa irá configurar todo ambiente necessario para executar o projeto.
#  Os seguintes serviços serão executados: 
#  Kafka, zookeeper, mongodb e dozzle.
#
#  Exemplos:
#      $ ./configurar-ambiente.sh
#      ou bash configurar-ambiente.sh
#      
# ------------------------------------------------------------------------ #

# Docker disponivel?
[[ $(type -P docker) ]] || {
    echo "Necessido do docker, para continuar"
    exit 1
}

# docker-compose disponivel ?
[[ $(type -P docker-compose) ]] || {
    echo "Necessido do docker-compose, para continuar"
    exit 1
}

docker network create --attachable -d bridge mydockernetwork

docker compose build

docker compose up -d

echo "Ambiente pronto"