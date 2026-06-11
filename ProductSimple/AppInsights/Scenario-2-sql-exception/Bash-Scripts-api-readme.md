# App insights test execution # les scripts sont en bash
# Scénario noraml : tester liste de produits : 1 fois
# Scénario exception: tester sql exception  api : 1 fois
# Script combiné — mix réaliste : 70% normal / 30% chaos

## initialisations 
Base_url="https://myapi-product-prod-fuhdb4ghaudth0cp.francecentral-01.azurewebsites.net"
Base_url_local="https://localhost:7218";
USERS=("alice" "bob" "carol" "dave" "anonymous")

## ----------------------------------------------------------------------
## partie 1: Scénario noraml - GET /api/Product
## ----------------------------------------------------------------------
   echo "=== GET /api/Product ==="
   curl -s -o /dev/null -w "HTTP %{http_code} | %{time_total}s\n"  "$Base_url/api/Product"

## ----------------------------------------------------------------------
## partie 2: Scénario des exceptions 
## ----------------------------------------------------------------------
   echo "=== GET /api/chaos/sqlException ==="
   curl -v      -H "X-User-Id: alice"   "$Base_url/Chaos/sqlException" 

## ----------------------------------------------------------------------
## partie 3: Script combiné — mix réaliste : 70%  normal / 30%  chaos
## on affecte les utilisateurs
## ----------------------------------------------------------------------
   

for i in {1..50} ; do 
	USER=${USERS[$((i % ${#USERS[@]}))]}
	if (($i % 3 == 0)); then 
		echo "---chaos requete: $i user= $USER ---" 
		curl -s -o /dev/null -w "HTTP %{http_code} | %{time_total}s\n"  -H "X-User-Id: $USER" "$Base_url/Chaos/sqlException"
	else 
		echo "---normale requete: $i ---"
		  curl -s -o /dev/null -w "HTTP %{http_code} | %{time_total}s\n"  "$Base_url/api/Product"
	fi
	sleep 0.6
done

