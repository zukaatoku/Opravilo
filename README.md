To build api image:

In root directory
```
docker build -f ./docker/api/Dockerfile . --tag opravilo.ap
```

Before running api in compose:
```
cd docker
docker compose up
```

To run api in docker for development - 

```
cd docker/api
docker compose up
```
