# Career

* .NET 5.0
* MSSQL 2019

### Config
```
To alter DB connection string please change ENV named DB_CONNECTION via docker-compose.yml
MSSQL run on port 1433
App runs on port 3000
```

### Run The Application
```
Firstly close application(s) that using port 1433 and 3000  or change port number(s) in docker-compose.yml file,
docker-compose build
docker-compose up
then check http://localhost:3000
```
