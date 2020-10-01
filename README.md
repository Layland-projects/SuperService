# Super service

The project goal is to create a software solution for table service restaurants.

### Minimum viable product

* A user type must only be able to see & access areas of the system relevant to them
* All users must have a username to allow them to access the system
* A User must be able to logout of the system

* A server must be able to place a new order only containing valid items
  * A valid item is an item in stock
* Kitchen staff must be able to update the stock count on ingredients
* Kitchen staff must be able to update an order to `InProcess` 
* Kitchen staff must be able to update an order to `ReadyToCollect`
* A server must be able to update an order from `ReadyToCollect` to `Complete`
* The order should no longer be visible once complete

### Expanded functionality

* Colour coding for order state and time spent in the state
* A view of all orders (historic) that can be filtered and drilled into
* A way of tracking amount of menu items already prepped for ETA's
* Request shipment ability when managing stock

## Diagrams

### Login System

![Login Diagram](https://github.com/Layland-projects/SuperService/blob/master/ReadmeContent/LoginSystemDiagram.png?raw=true)

### Order System

![Order System Diagram](https://github.com/Layland-projects/SuperService/blob/master/ReadmeContent/OrderSystemDiagram.png?raw=true)

### Stock System

![Stock System Diagram](https://github.com/Layland-projects/SuperService/blob/master/ReadmeContent/StockSystemDiagram.png?raw=true)

### Sprint 1

#### <u>Goals</u>

* Create github repo and VS solution with all project layers
* Implement a basic login/logout feature
* Implement a stock system
  * Should be able to view all ingredients
  * Should be able to update stock counter on ingredients

#### <u>Tickets</u>

All tickets were in the backlog at the beginning but have all been successfully implemented for this sprints increment along with accompanying tests.

| Sprint backlog | In progress | Review | Done                                                         |
| -------------- | ----------- | ------ | ------------------------------------------------------------ |
|                |             |        | [Create Repo](https://github.com/Layland-projects/SuperService/projects/1#card-46458226) |
|                |             |        | [Create solution](https://github.com/Layland-projects/SuperService/projects/1#card-46458836) |
|                |             |        | [Implement CodeFirst DB](https://github.com/Layland-projects/SuperService/projects/1#card-46458920) |
|                |             |        | [Setup user types - Admin](https://github.com/Layland-projects/SuperService/projects/1#card-46458342) |
|                |             |        | [System access - Login(Basic)](https://github.com/Layland-projects/SuperService/projects/1#card-46481259) |
|                |             |        | [System access - Logout](https://github.com/Layland-projects/SuperService/projects/1#card-46481356) |
|                |             |        | [Create stock system - view ingredients](https://github.com/Layland-projects/SuperService/projects/1#card-46459182) |
|                |             |        | [Crete stock system - update count](https://github.com/Layland-projects/SuperService/projects/1#card-46459079) |
|                |             |        | [Epic - Create stock system](https://github.com/Layland-projects/SuperService/projects/1#card-46458992) |
|                |             |        | [Create class diagrams](https://github.com/Layland-projects/SuperService/projects/1#card-46524355) |
|                |             |        | [Update README](https://github.com/Layland-projects/SuperService/projects/1#card-46524252) |

#### <u>Retrospective</u>

##### What went well

I think my initial estimation for the workload for the sprint went well.

##### What could have gone better?

I think I should've stayed on top of any documentation updates after each ticket rather than having to make a separate task to catch up.

##### What to try next?

I'm interested to try and implement a type of scrum poker with a scoring system for each ticket.



### Business logic

#### <u>Menu Items</u>

* Menu items should be marked as red If they can't be ordered (ingredients not in stock)

#### <u>Orders</u>

* Orders should be colour coded by their state
  * If an order has been in a particular state for too long (defined in settings) mark as red
  * Blue - Order placed
  * Yellow - In Process
  * Green - Ready to collect
* Orders can only contain meals which we have the ingredients for in stock
* When an order is placed the stock is updated to remove the required ingredients for the order
* If an order is cancelled before it enters `InProcess` then add the ingredients that weren't used back into the stock

#### <u>Manager (?)</u>

* As a manager I should be able to do everything a Kitchen staff or Server can do.
* As a manager I should be able to cancel an order
  * They must be able to cancel the order at any point
* As a manager I should be able to update the nutritional information for a stock item
* As a manager I should be able to add a new item to the stock sheet and set it's nutritional information
