To run Audit Storage System:
1. Create any (for example 'audittest') scheme in MySQL db server 
2. Update MySQLConnection in web.config

Notes:
1. Every time when application will start all default DB values will be rewritten
2. Authentication by AD
3. Authorization by custom roles. Assigned role can be modificated through:
	- DB table (directly),
	or
	- code before launch application => AuditStorageSystem\Models\SamplesData.cs\GetUserInRole()