# ProductCatalogProject
Tools Used for development

.net 8, Ef core(orm), Mssql database 2019

setup process
1. clone the github repo
2. create a database on your local machine
3. point the sql instances to the project
4. adjust the connection strings using your database settings
5. then run migration by clicking tools-> project management console
6. ensure the dropdown window is pointing persistence project
7. run command update-database -context ProductCatalogDbContext

application flow
-> user onboard using the register endpoint
-> user is required to login and generate jwt token before they can access this endpoin: create product,update product,delete product,create order and get orders
-
