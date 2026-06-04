# App insights requests
## ------- START  Section slow API   ------- 

## 1) liste de tous les requetes les plus lentes 
##  on crée des colonnes calculés à travers  extend operateur
##  on 
requests
| where timestamp between (ago(2d) .. now())
| where success == true
| extend timeFrance = timestamp + 2h
| project     
        timeFrance,
        endpoint = name,
        url,
        resultCode,
        duration_ms = duration
| order by duration_ms desc, timeFrance desc

## 2) Identifier les endpoints lents (quote-service simulée)
## Objectif : endpoint lent + durée exacte + impact (nb appels affectés)
## On regroupe les resultat par : name, url 
## On calcule la durée moyenne des appels , le nombre des appels lents , et le temps d'attente le plus haut de l'application
requests
| where timestamp > ago(1h)
| where duration > 2000
| summarize 
    duration = avg(duration),
    nbappel = count(),
    max_duration = max(duration), 
  by name, url


## ------- END  Section slow API   ------- 


## Retrouver la requête SQL, la stacktrace, l'utilisateur impacté
## requestes pour detecter les appels ou la resource n'est pas disponible 400 ou un sql exception est levé 500



