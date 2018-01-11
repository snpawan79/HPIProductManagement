Design and implement simple REST web service for storing product-price pairs

Code Walkthrough
1.	HPI.BusinessEntities :- This project contains the only Product model having a product code and price that is used to get and post data through REST web service.

2.	HPI.BusinessServices :- This project contains the business functionality to retrieve and add products using ProductRepository and UnitOfWork. This project also implements the rule engine to validate price based on the first character of the product code as listed below

A (product, price) pair is valid if:
• If product starts with ‘A’ – price should be higher than 0 and lower than 100
• If product starts with ‘B’ – price should be equal or higher than 100 and lower than 1000
• If product starts with ‘C’ – price should be equal or higher than 1000
All other cases - (product, price) is invalid.

3.	HPI.DataAccessLayer :- This project contains the definition of the product datamodel that corresponds to the Product table in MySQL db. The table has been created using the code first approach. The reviewer needs to execute “Update-Migration” using the Nuget package manager console with default project being selected as “HPI.DataAccessLayer”. 
The connection string stored in the app.config of HPI.DataAccessLayer project needs to be modified with the details of the MySQL server of the reviewer’s environment.
Execution of the Update-Migration command will create the Product table along with the Migration history in MySQL server.

4.	HPI.NUnit.Tests :- This project implements unit tests on the HPI.BusinessServices by mocking the ProductRepository to test retrieval and saving of Product data through the HPI.BusinessServices.ProductService class.

5.	HPI.Resolver :- Resolves the dependency for the ProductService on the ProductController and dependency for UnitOfWork on the ProductService. 

6.	HPI.ValidationRuleEngine :- This project implements Labmda-based Expression Trees where rules are set as Lists that construct a list of binary expressions at runtime, and then it compiles and assigns them to a Lambda Tree data structure. Once assigned, you can navigate an object through the tree in order to determine whether or not that object’s data meets your business rule criteria. The ValidationProcessor in HPI.BusinessServices passes the list of validation rules to the rule engine that dynamically compiles the rules to determine whether the associated object satisfies the rules or not. 

7.	HPI.WebAPI :- This is the ASP.Net web api project that implements the Rest APIs for retrieving and storing the product key value pair from and to MySql database via the ProductService that in turn calls the DataAccessLayer using entity framework.

This project has been set as a startup project in this solution and can be run within visual studio IDE. 

Click on API link on the tool bar on your browser. This will navigate to http://localhost:<port>/Help. This page will provide the list of APIs implemented in the ProductController for GetAll, GetProductByCode and Insert a new product. 

Clicking on any of these links will show button to test the Rest API on the bottom right hand corner of the browser window.

 
Clicking on this button will open a dialog to send the request to the API and retrieve the status. 




