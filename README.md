![CI](../../workflows/CI/badge.svg)

## docker 초기화
```
dapr init
```
```
> docker ps
CONTAINER ID   IMAGE               COMMAND                  CREATED         STATUS                   PORTS                                                 NAMES
6f11f9146c7b   daprio/dapr:1.5.1   "./placement"            4 minutes ago   Up 4 minutes             0.0.0.0:6050->50005/tcp, :::6050->50005/tcp           dapr_placement
bd23c1c2d69d   redis               "docker-entrypoint.s…"   4 minutes ago   Up 4 minutes             0.0.0.0:6379->6379/tcp, :::6379->6379/tcp             dapr_redis
9348c4a53737   openzipkin/zipkin   "start-zipkin"           4 minutes ago   Up 4 minutes (healthy)   9410/tcp, 0.0.0.0:9411->9411/tcp, :::9411->9411/tcp   dapr_zipkin
```