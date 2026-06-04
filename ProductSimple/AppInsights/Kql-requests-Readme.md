# App insights requests
## ------- START    ------- 

## liste des requetes KQL génériques
## liste des API erronés

requests 
| where resultCode startswith("4") or    resultCode startswith "5"
| where timestamp  > ago(1d)

