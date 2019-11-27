create procedure InsertUsersDetails
(@Id int,@FirstName varchar(20), @LastName varchar(20) ,@UserName varchar(30), @Email varchar(30), @password varchar(20), @ProfilePicture varchar(30), @UserType varchar(30), @ServiceType varchar(20))
as
begin
insert into dbo.RegisteredUsers values (@Id,@FirstName, @LastName, @UserName, @Email, @password, @ProfilePicture,@UserType,@ServiceType)
end