# Super service

A software tool for table service restaurants.

### User types

* Admin - System Admin
* Manager (?) - Shift manager
* Kitchen staff - Kitchen workers
* Server - Front of house workers

### Base system requirements

* A user type must only be able to see & access areas of the system relevant to them
* All users must have a username & password to allow them to access the system
* A User must be able to logout of the system

### Minimum viable product

* A server must be able to place a new order only containing valid items
  * A valid item is an item in stock
* Kitchen staff must be able to update the stock count on ingredients
* Kitchen staff must be able to update an order to `InProcess` 
* Kitchen staff must be able to update an order to `ReadyToCollect`
* A server must be able to update an order from `ReadyToCollect` to `Complete`
* The order should no longer be visible once complete

### Nice to have's

* Colour coding for order state and time spent in the state
* A view of all orders (historic) that can be filtered and drilled into
* A way of tracking amount of menu items already prepped for ETA's
* Request shipment ability when managing stock

### Business logic

#### <u>Menu Items</u>

* Menu items should be marked as red If they can't be ordered (ingredients not in stock)

#### <u>Stock</u>

* Items out of stock should be marked by a red colour in the list.

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
