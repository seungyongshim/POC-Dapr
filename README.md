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

## kubernetes 초기화
```
dapr init -k
```
```
> dapr status -k
  NAME                   NAMESPACE    HEALTHY  STATUS   REPLICAS  VERSION  AGE  CREATED
  dapr-sidecar-injector  dapr-system  True     Running  1         1.5.1    17s  2021-12-14 10:47.09
  dapr-placement-server  dapr-system  True     Running  1         1.5.1    17s  2021-12-14 10:47.09
  dapr-dashboard         dapr-system  True     Running  1         0.9.0    17s  2021-12-14 10:47.09
  dapr-operator          dapr-system  True     Running  1         1.5.1    17s  2021-12-14 10:47.09
  dapr-sentry            dapr-system  True     Running  1         1.5.1    17s  2021-12-14 10:47.09
```

* Helm으로도 배포 가능
* https://docs.dapr.io/operations/hosting/kubernetes/kubernetes-deploy/#add-and-install-dapr-helm-chart

