# App insights requests

## liste des requetes les plus courtes

requests
| where timestamp between (ago(1h) .. now())
| where duration > 2000
| order by duration
|project timestamp, name,url, resultCode, duration 



