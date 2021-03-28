# VITG Code Test
1. Entity Framework Code First
2. Blob
3. Image Upload

# Scenario
This is a basic scenario designed to test your competency with the latest .net technologies. You will have 1 day to complete the scenario, at the end of which we will screen share with you to review your efforts and run through the code. 
Create a project which: 
•	Allows the user to upload an image 
•	Stores the image in azure blob store 
•	Stores metadata about the image in a SQL database
•	Displays all images which have been uploaded in an image carousel
Requirements:
•	Must use entity framework code first database with migrations
•	Must use .net core
•	Use your choice of client framework (optional)
Authentication Details:
•	SQL Database Connection String:
o	Server=tcp:talent-scenarios.database.windows.net,1433;Initial Catalog=talent2014;Persist Security Info=False;User ID=talent2014;Password=RLMjKM$C2Rq_wce!Lt3WB*g5?xqS^f7V;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
o	You might need to mention the database name explicitly in the login dialog (in SSMS or VS) as the default database for connection (See screen shots below for clarification)
•	Azure Blob Storage Container SAS (Shared Access Signature):
o	https://talentblobstore.blob.core.windows.net/talent2014?sp=racwdl&st=2021-03-21T14:40:12Z&se=2021-03-28T22:40:12Z&spr=https&sv=2020-02-10&sr=c&sig=TyiXfN3Bf9eUDQryhc0nvdiyhOrB3xa5PSRQwk2mGfg%3D
o	Container name is “talent2014”
o	A CORS rules are already configured for you. There are no specific security requirements, so it is preferable to serve the blobs directly via the URL of the blob.


  
