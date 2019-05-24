# Employee Benefits Calculation

To run the code locally, simply clone this repository, open *EmployeeBenefitsCalculation.sln*, and hit *F5* or *Start without debugging* in Visual Studio.

To build just the UI, navigate to the *ClientApp* folder in a command window, and run the command *ng build*.


### [Check out the demo site here!](https://employeebenefitscalculation.azurewebsites.net)

## Implementation and technical details:

* The implementation uses a **.NET Core** Web Application with **Angular** in the front end.
* The web API is split into the following layers:
	* **The controller layer:** This layer receives the request and performs input validation before passing it on to the next layer.
	* **The manager layer:** This layer is where all the business logic happens. It calls the next layer to get any data from the database.
	* **The repository layer:** This layer interfaces with the databases.
* In order to not violate the open/closed principle, we have to take the following steps to add/remove/update a discount:
	* Add a `discount` class that implements the `IDiscount` interface, in the *Discounts* folder in the `EmployeeBenefitsCalculation.Managers` project.
	* Add the new class name to the `Discounts.json` file in the *Discounts* folder.
	* Fix the `DiscountHelper` tests to account for the changes. 
	* This implementation ensures that we do not have to modify an existing class if and when the business rules around discounts change.
* This repository is hooked up for **continuous builds and deployments**. Any new changes checked into the *master* branch, will be automatically deployed (via an Azure Devops pipeline) to a web app hosted in Azure.
