# General requetes pour détecter les erreurs 
	exceptions
	| where timestamp > ago(1d)
	| where cloud_RoleName has "func"
	| project timestamp, outerMessage, innermostMessage
	| order by timestamp desc
	| take 20




