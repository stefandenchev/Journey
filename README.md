# Journey
Game Store

The Journey Game Store is project developed for a number of months, based on the ASP.NET-Core-Template by NikolayIT.

On the user side, you can browse the entire list of games, look for the game under a particular genre or those published by a certain company. You can also search by keyword. All the results of these operations can be sorted by title and price. The user can also browse the games that are currently on sale and order them by title, price, or discount amount.
The site has a Wishlist and Cart funcionality as well as a basic payment system and validation for adding credit cards (Visa and MasterCard only!). The user has access to all the games they have purchased in their Library and the list of all their Orders which can be sorted by date of purchase and total price. They can also print a pseudo-receipt in JSON format.
The users can also give a 5-star rating for each game and discover other games by the same publisher at the bottom of each individual game page if there are any.

On the admin side, you can add a new game, filling the forms for its Title, Price, Description, including which languages it's translated in etc. When adding the images for the Game, there is one caveat - the image that is to be used as a main image needs to have "cover" in it's name.
Additionally, you can edit the game's Title, Price, Description, Genre, Publisher and Requirements. You can also add additional languages and tags (e.g. "Crossplatform", "Co-op" etc.) The admin has the ability to put any game on sale, choosing what percentage to cut from the base price. Finally, the admin can add a new Publisher, if the game/s they want to add aren't by an already existing one.
