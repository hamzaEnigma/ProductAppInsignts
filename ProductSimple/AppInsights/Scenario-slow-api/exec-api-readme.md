# App insights test execution
# scénario exécuter liste de produits : 12 fois
# scénario exécuter slow  api : 15 fois

## Test 1 api
## command to target the api GET PRODUCTS 

curl -v   'https://myapi-product-prod-fuhdb4ghaudth0cp.francecentral-01.azurewebsites.net/api/Product'   


## command bash to target the api chaos slow multiple times 
for i in {1..12} ; do 
	echo "---start requete: $i ---" 
	curl -v 	'https://myapi-product-prod-fuhdb4ghaudth0cp.francecentral-01.azurewebsites.net/api/Product'    
	echo "--- end requete ---" 
done

## command to target the api SLOW API

curl -X 'GET'   'https://myapi-product-prod-fuhdb4ghaudth0cp.francecentral-01.azurewebsites.net/Chaos/slowEnpoint'   -H 'accept: */*'


## command bash to target the api chaos slow multiple times 
for i in {1..15} ; do 
	echo "requete $i" 
	curl -X GET \
	'https://myapi-product-prod-fuhdb4ghaudth0cp.francecentral-01.azurewebsites.net/Chaos/slowEnpoint'   -H 'accept: */*' 
	echo " " 
done

## command to target the get api 
