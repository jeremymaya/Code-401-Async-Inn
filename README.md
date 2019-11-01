# Code-401-Async-Inn

## My Fitst MVC App

Lab 13 - Intro to Entity Framework

*Author: Kyungrae Kim*

----

## Description
This is a ASP.NET Core MVC web application that will allow Async Hotel to better manage the assets in their hotels. This application can modify and manage rooms, amenities, and new hotel locations. The data ebtered by the user will persist across a relational database and maintain its integrity as changes are made to the system.

---

### Getting Started
Clone this repository to your local machine.

```
$ git clone https://github.com/jeremymaya/Code-401-Async-Inn.git
```

### To run the program from Visual Studio:
Select ```File``` -> ```Open``` -> ```Code-401-Async-Inn``

Next navigate to the location you cloned the Repository.

Double click on the ```AsyncInn``` directory.

Then select and open ```AsyncInn.sln```

---

### Entity Relationship Diagram
![Image 1](https://github.com/jeremymaya/Code-401-Async-Inn/blob/master/assets/AsyncInn2.png)

* Hotel table has one to many relationship with HotelRoom table
* Room table has one to many relationship with HotelRoom table
* Amenities table has one to many relationship with RoomAmenities table
* HotelRoom table is a joint table with payload
* RoomAmenities table is pure join table
* Layout is a enum

---

### Credits
[Code Fellows](https://codefellows.github.io/code-401-dotnet-guide/Resources/MVCSetup)

---

### Change Log
1.0: *Lab 13 Completed, Initial Submission* - 30 Oct 2019 10:45 PM
1.0: *Lab 13 Started* - 30 Oct 2019 9:30 AM
