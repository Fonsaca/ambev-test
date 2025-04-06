# Developer Evaluation Project

## Instructions
See [Instructions](/.doc/instructions.md)

## Premises

As the goal of the test is just to create a complete CRUD of sales records then:
- User or Customer is considered as an External Domain. The initial implementation is taken as an example and that's why the foreign key between User and Sale doesn't exists.
- Product is considered as an External Domain.

## Approach: Update of an Item

When updating a sale the decrease or cancelation of an item reflects in a record with status `IsCanceled = true` to keep trace of what had been canceled.


## Out-of-scope

- User or Customer CRUD.
- Product CRUD.
- Cart CRUD.
- Authetication and Authorization.
- Correction of the Response Object (which contains it-self inside de `data` property) since that issue was already in the template project.

## How to run
- Install Docker Desktop and run
- Install Git
- Clone the git repository
```bash
   git clone https://github.com/Fonsaca/ambev-test.git
```
- Open the terminal at the repository folder `./backend` and run
```bash
   docker compose up -d
```
- Open the API swagger at http://localhost/swagger/index.html
- See the details of the api at [Sales API](/.doc/sales-api.md)  