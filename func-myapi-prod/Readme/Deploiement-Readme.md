# azure function md


## to deployer  azure fonction, 
## il faut passer par azure cli, et s'authentifier d'abord
	az login

## si az login ne marche pas avec git bash 
	az login --use-device-code --tenant "PUT TENANT HERE"

## telechargez la bib func puis lancer le d�ploiement
## vu qu'on passe par le "consommation flexible" plan on a que ce moyen pour d�ployer la ressource
	cd func-myapi-prod
	func azure functionapp publish func-myapi-prod


 ## pinger la fonction
	curl  "https://func-myapi-prod-cef2fzgehwgyazdx.francecentral-01.azurewebsites.net/api/exportproductfunction"
 
 ## pinger la fonction verbose
	curl -v "https://func-myapi-prod-cef2fzgehwgyazdx.francecentral-01.azurewebsites.net/api/exportproductfunction"
