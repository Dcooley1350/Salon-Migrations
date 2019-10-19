# _Eau Claire's Salon_

#### _Manage all Stylist and Client Relationships for Eau Claire's Salon, {10/18/2019}_

#### By _**Devin Cooley**_

## Description

_The purpose of this application is to give Eau Claire's Salon employees the ability to manage clients and Stylists. Employees will be able to use this application to view,create, update and delete stylists and their associated clients._

## Specifications

| Spec                      |Input          | Output |
|:---------------------------|:-------------|:------|
|Create: Allows user to create Clients and Stylists.|"name", "details"|Client/Stylist: name,details|
|Display: Shows a list of all Clients and Stylists.|"Stylist","Stylist2"|Stylists: Stylist,Stylist2|
|Delete: Allows user to delete a Client or Stylist.|"Delete"|"Client/Stylist has been deleted"|
|Stylist Details: Allows user to see all clients associated with a stylist.|"Stylist1"|Clients: Client1, Client2|
|Associates Clients to a specific Stylist|"New Client1 in Stylist1"| Stylist1: Client1|

## Setup/Installation Requirements

* #### Run Instructions ####
    _Make sure you have the neccessary software installed to run C# and .NET from your console. Clone the project repository and navigate into the root directory with your console. Nagigate to Salon directory and run command "dotnet restore" from your console in that directory. Run command "dotnet run" from your console in that directory. Click the link in your console to open the application in default web browser._
* #### Database Setup Commands ####
  1. _Create Salon Database:_  
    CREATE SCHEMA `salon` ;
  2. _Create Clients Table:_  
    CREATE TABLE `salon`.`clients` (  
  `Name` VARCHAR(255) NOT NULL,  
  `ClientId` INT NOT NULL AUTO_INCREMENT,  
  `StylistId` Int(11)) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ClientId`));
  3. _Create Stylists Table:_  
    CREATE TABLE `salon`.`stylists` (  
  `name` VARCHAR(255) NOT NULL,  
  `specialty` VARCHAR(255) NOT NULL,  
  `stylistId` INT NOT NULL DEFAULT 0 AUTO_INCREMENT,
  PRIMARY KEY (`stylistId`));  
  *_To use this Database you will need to create a file "appsettings.json" in the Salon directory of the project. Add this text to the file:_  
  {  
    "ConnectionStrings": {  
        "SalonConnection": "Server=localhost;Port=3306;database=devin_cooley;uid=root;pwd=epicodus;"  
    }  
  }  
  _This will connect your application to the previously created Databases._


## Known Bugs

_There are no known bugs at this time._

## Support and contact details

_Send any questions or comments to Devin Cooley at dcooley1350@gmail.com._

## Technologies Used

_This program was written using HTML, CSS, C# and the .NET Framework. Database were created using MySQL. The application is viewed in a web browser or other client._

### License

*This software is licensed under the MIT license.*

Copyright (c) 2019 **_Devin Cooley_**