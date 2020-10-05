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

![OrderSystemDiagram.png](https://github.com/Layland-projects/SuperService/blob/Sprint_2/main/ReadmeContent/OrderSystemDiagram.png?raw=true)

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

### Sprint 2

#### <u>Goals</u>

* Implement an order system
  * A server can place an order
  * Servers and kitchen staff can interact with the progress of the order
* Have validation to prevent you from ordering items out of stock
* Implement the kitchen staff and server user roles
  * Limit the functionality each role is able to access based off what is relevant to their role

#### <u>Tickets</u>

| Sprint backlog | In progress | Review | Done                                                         |
| -------------- | ----------- | ------ | ------------------------------------------------------------ |
|                |             |        | [Create order system - Place new order](https://github.com/Layland-projects/SuperService/projects/1#card-46459384) |
|                |             |        | [Create order system - Add orders page](https://github.com/Layland-projects/SuperService/projects/1#card-46592386) |
|                |             |        | [Create order system - Make total order cost visible](https://github.com/Layland-projects/SuperService/projects/1#card-46579346) |
|                |             |        | [Create order system - update order to InProgress](https://github.com/Layland-projects/SuperService/projects/1#card-46459501) |
|                |             |        | [Create order system - update order to ReadyToCollect](https://github.com/Layland-projects/SuperService/projects/1#card-46459559) |
|                |             |        | [Create order system - Update order to Complete](https://github.com/Layland-projects/SuperService/projects/1#card-46459630) |
|                |             |        | [Epic - Create order system](https://github.com/Layland-projects/SuperService/projects/1#card-46459309) |
|                |             |        | [Update ingredient stocks as order is being placed](https://github.com/Layland-projects/SuperService/projects/1#card-46638005) |
|                |             |        | [Disable and highlight menu items out of stock](https://github.com/Layland-projects/SuperService/projects/1#card-46636931) |
|                |             |        | [Setup user types - Kitchen staff](https://github.com/Layland-projects/SuperService/projects/1#card-46458649) |
|                |             |        | [Setup user types - FOH/Server](https://github.com/Layland-projects/SuperService/projects/1#card-46458673) |

#### <u>Retrospective</u>

##### What went well

I think my time management was overall good.

##### What could have gone better?

I think I did end up spending a more than ideal amount of time thinking about implementations and how to go about things when working on a task.

##### What to try next?

Next I want to try a rapid prototyping style, rather than spending as much time thinking about different approaches, just try them and see what works.

### Sprint 3

#### <u>Goals</u>

* Implement a custom styling for elements across the application

#### <u>Tickets</u>

| Sprint backlog | In progress | Review | Done                                                         |
| -------------- | ----------- | ------ | ------------------------------------------------------------ |
|                |             |        | [Epic - Add styling](https://github.com/Layland-projects/SuperService/projects/1#card-46755716) |
|                |             |        | [Add styling - Add custom button styling](https://github.com/Layland-projects/SuperService/projects/1#card-46755768) |
|                |             |        | [Add styling - Add custom combo box styling](https://github.com/Layland-projects/SuperService/projects/1#card-46755815) |
|                |             |        | [Add styling - Add custom text block styling](https://github.com/Layland-projects/SuperService/projects/1#card-46755875) |
|                |             |        | [Add styling - Add custom List view styling](https://github.com/Layland-projects/SuperService/projects/1#card-46755990) |