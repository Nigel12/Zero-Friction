# Zero-Friction

## Setup Instructions

You will need:
1. .Net 6
2. IDE - Visual Studio/VS Code
3. DB - Cosmos DB Emulator/Instance

* Clone the repository - https://github.com/Nigel12/Zero-Friction.git (development branch)
* Navigate to appsettings.json and modify it with your database URI and Primary key:

![DDD-Layers-Image](https://github.com/Nigel12/Zero-Friction/blob/development/Images/CosmosDBSettings.JPG?raw=true)

* Ensure that the database is running and start the project. Startup Project should be **InvoiceApp**
* The application will start and create a container
* The API can be tested using the postman collection or swagger-UI

## My approach

The domain is the Invoice and InvoiceItems, which are contained within it.
With Domain Driven Design practices in mind, the Invoice would be the Aggregate Root, InvoiceItem would be an entity. InvoiceItems should only be referenced through the Invoice

This is the structure of the Invoice:
```json
{
  "date": "2023-02-15T09:40:48.0505211+05:30",
  "description": {
    "value": "test description"
  },
  "totalAmount": {
    "value": 100
  },
  "items": [
    {
      "amount": 100,
      "quantity": 10,
      "unitPrice": 10,
      "id": {
        "value": "518d54e5-6354-4711-9dd8-93804e4e0d63"
      }
    }
  ],
  "id": {
    "value": "7eee5190-6e30-434e-a18e-b7de2d5c9d9a"
  }
}
```

I created a web API and 2 class libraries:
* InvoiceApp - API project that holds the controllers and contracts
* InvoiceApp.Infrastructure - this is the data access layer, where our database/repositories reside
* InvoiceApp.Application - the application layer, holds our services and commands
* InvoiceApp.Domain - the domain layer. Holds our entities. Innermost layer


![DDD-Layers-Image](https://github.com/Nigel12/Zero-Friction/blob/development/Images/dddlayers.png?raw=true)


Contracts - A contract is an agreement between the client and the server about which data is required and the structure in which it is passed. The DTO object is then converted to the domain structure.

## Persistence

I used Azure Cosmos DB (emulator) for data persistence

## API Details

Postman collection is available in the repo. Additionally, swagger-ui is also available.


## Unit Tests

InvoiceService tested using XUnit and Moq

