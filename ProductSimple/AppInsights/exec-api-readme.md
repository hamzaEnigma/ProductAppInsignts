# App insights test execution

## command to target the api chaos slow

curl -X 'GET'   'https://myapi-product-prod-fuhdb4ghaudth0cp.francecentral-01.azurewebsites.net/Chaos/slowEnpoint'   -H 'accept: */*'

## command to target the api chaos/sql 
curl -X 'GET'   'https://myapi-product-prod-fuhdb4ghaudth0cp.francecentral-01.azurewebsites.net/Chaos/sqlException'   -H 'accept: */*'


