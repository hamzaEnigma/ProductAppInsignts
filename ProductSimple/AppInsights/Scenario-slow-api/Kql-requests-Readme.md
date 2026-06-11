# App insights requests
## ------- START  Section slow API   ------- 

## 1) Toutes les requêtes ≥ 2000ms, heure Paris
##  on crée des colonnes calculés à travers  extend operateur

requests
| where timestamp between (ago(1d) .. now())
| where success == true
| where duration >= 1500
| extend timeFrance = datetime_add('hour',2,timestamp) 
| project     
        timeFrance,
        endpoint = name,
        url,
        resultCode,
        duration_ms = duration
| order by duration_ms desc, timeFrance desc

## 2) Grouper par endpoint : moyenne, nb success / failures
## Objectif : endpoint lent + durée exacte + impact (nb appels affectés)
## On regroupe les resultat par : name, url 
## On calcule la durée moyenne des appels , le nombre des appels lents , et le temps d'attente le plus haut de l'application

requests
| where timestamp > ago(30m)
| summarize 
    moyenne_duration = avg(duration),
    la_plus_lente_duration = max(duration),
    nombre_appels = count(),
    nb_success = countif(success == true),
    nb_faillures = countif(success == false)
  by name, url
| extend taux_erreur = nb_faillures *100 / nombre_appels
| order by moyenne_duration desc 

## 3)  Impact utilisateurs : combien impactés par la lenteur 
## combien d'IPs distinctes ont touché un endpoint lent
## combien de fois total

requests
| where duration > 2000
| where timestamp > ago(1h)
| summarize
    nb_users_impactes  = dcount(client_IP),
    nb_appels_lents    = count(),
    max_ms             = max(duration),
    moyenne_ms         = avg(duration)
  by endpoint = name
| extend moyenne_ms = round(moyenne_ms) // pour eviter décimales à rallenge
| extend max_ms = round(max_ms)
| order by nb_users_impactes desc

## 4) — Chronologie minute par minute de la latence
## bin(timeFrance, 1m) — granularité minute, par minute
## render timechart — visualisation directe
## durée  = avg(duration)
## volume d'appels = count()
requests
| where timestamp between(ago(3h) .. now())
| extend timeFrance = datetime_add('hour', 2, timestamp)
 | summarize 
  avg_duration = avg(duration) by bin(timeFrance, 1m)
  // ,avg_nb_appel = count() by bin(timeFrance, 1m)
| order by timeFrance asc
| render timechart

## ------- END  Section slow API   ------- 


## Retrouver la requête SQL, la stacktrace, l'utilisateur impacté
## requestes pour detecter les appels ou la resource n'est pas disponible 400 ou un sql exception est levé 500



