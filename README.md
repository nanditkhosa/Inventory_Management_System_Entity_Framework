# BasicCRUDmcvCsharp
Basic Add Edit Delete and Read functionality implemented using MVC


# Inventory management of resources in each facility

# There has to be an Admin to login to system:
To setup an admin for first time please run below query :

Begin Transaction;
INSERT INTO [User](UserName,Role,PasswordHash,IsActive,CreatedTimeStamp,LastModifiedTimeStamp) VALUES  ('admin','admin','password','true',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);
Commit Transaction;


#and Then add the following entities in order:

1. Facilities
2. Users (by assigning Facility to the user)
3. Resources (assigned to specific facility)

# Admin can add, edit, inactivate(as part of edit) for each of the above type of domain object

# Admin can view a report for each facility(with users assigned to it and inventory status of resources assigned to it)

# Mapping is as follows:

User (m)-------(m) Facility
Facility  (1)-----------(m)   Resource

#Web users needs to be assigned to atleast a facility

Webuser on login will be able to see list of facilities he is assigned to 
On click on each of those facilities he can check inventory of the resources in it.

