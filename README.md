### Short solution description

1. There are 2 entities able to manage using corresponding APIs - TodoList and User; 
2. We can only get, create, or delete users, to focus on to-do list management;
3. Instead of removing the user we mark them as deleted, kind of soft deleting - to avoid possible issues with getting existing TodoList where the user was a creator;
4. Deleted user can't create, update, or share todolist; Also there is no way to share todolist with the deleted user;
5. Flow in general: Api handle a request, if request passed by validator - handler send it to manager, manager adapt object model to entity and send it to repository. Repository do request in to db using MongoDbDriver, and move back the result up to handler;
6. For manual API testing there is should be MongoDB Atlas credentiasl in system environment, otherwise the special validator who check credentials before trying to connect to db will reject it;
7. There is only one unit test just for example how whole project can be covered. 