# App insights requests
## ------- START  Section slow API   ------- 

## 1) Calculer le nombre d'erreurs dans la prod seulement
exceptions
| extend time_france = datetime_add('hour',2,timestamp)
| where cloud_RoleName  == "myapi-product-prod"
| where timestamp  between (ago(1h) .. now() )
| summarize
    nombre_erreur = count() 
    by type, outerMessage

## 2) Le volume d exceptions par utilisateur
exceptions
| where timestamp > ago(1h)
| where cloud_RoleName  == "myapi-product-prod"
| extend time_france = datetime_add('hour',2,timestamp)
| project 
    timestamp,
    type,
    message = outerMessage,
    stacktrace = details[0].parsedStack,
    user        = tostring(customDimensions["impactedUser"]),
    endpoint   = tostring(customDimensions["enpoint"])
| summarize 
    nb_erreurs = count(user)
    by user, endpoint, type, message
| order by nb_erreurs desc

## 3) TimeLine des erreurs par requete  
requests
| where timestamp between(ago(2h) .. now())
| extend timeFrance = datetime_add('hour', 2, timestamp)
| where cloud_RoleName  == "myapi-product-prod"
| where resultCode == 500
| summarize 
  nb_erreurs = count() 
  by bin(timeFrance, 1m)
| order by timeFrance asc
| render timechart

## 4) cette requete réponds à ces 3 questions 
## Quelle couche a throwé (Controller ? Service ? Repository ?) => le level
## Quelle ligne exacte dans le fichier 
## Quel utilisateur était impacté

exceptions
| where timestamp between (ago(1d) .. now())
| where cloud_RoleName == "myapi-product-prod"
| where customDimensions["chaos"] == "sql"
| mv-expand trace = details
| mv-expand stack = trace.parsedStack
| project
    timestamp ,
    outerMessage,
    method = tostring(stack.method),
    fileName = tostring(stack.fileName),
    assembly = tostring(stack.assembly),
    ligne_erreur = tostring(stack.line),
    level = toint(stack.level)
| where isnotempty(method)
| order by timestamp desc, level asc
