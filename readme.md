# Museum Management Software

This is a software to manage internal administration of an art museum

## Functionalities

The software allows to create and manage users and roles

Grant role specific access to some features such as restore an artwork, ask for/grant/deny an artwork lending, manage storage and display locations etc.

The softare also warns the chief restaurator when an artwork needs to be restored, given its last restauration date.

Chief Restaurator can also see all the artwork restauration history on detail.

## Roles
- Guest: basic user, can only see the catalog
- Chief Restaurator: Person in charge of artwork restaurations
- Museum CEO: Head of the museum, manages the lendings and new users of the platform

## Technologies

This whole project was developed in C#, using ASP Dotnet 6
using a multilayer architechture.

For Database Entity Framework was used as ORM.
For Authentication it was used Identity.

UI was developed using razor pages and embeded in razor pages, Bootstrap 5.

