# Azure CLI — Commandes utiles projet ProductSimple

## Structure commande azure 
## az <groupe> <sous-groupe> <action> [paramètres]
## la commande az est utulisé pour gérer et intéragir avec les ressousrces azure

## Authentification
	az login --use-device-code
	az account show

## Lister les ressources
az resource list -o table

## App Service
az webapp show --name .... --resource-group .... -o table
az webapp config appsettings list --name .... --resource-group .... -o table
