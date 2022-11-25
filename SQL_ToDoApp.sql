use Applications
go

--CRUD operations for simple Todo Api

--drop table dbo.Temp_TodoUsers-------------------------------------------------
create table dbo.Temp_TodoUsers(
	 Id int not null identity(1,1) primary key
	,UserName varchar(100) not null
	,Email varchar(150) not null
)

insert into dbo.Temp_TodoUsers
values('Larry Page', 'bossof@google.com')

--drop table dbo.Temp_TodoCategory--------------------------------------------------------
create table dbo.Temp_TodoCategory(
	 Id int not null identity(1,1) primary key
	,CatDescription varchar(150) not null
	,ValidFrom datetime2(0) not null default getdate()
	,ValidTo datetime2(0) not null default '3000-01-01'
)

insert into dbo.Temp_TodoCategory(CatDescription) values('Undefined')
insert into dbo.Temp_TodoCategory(CatDescription) values('Work')
insert into dbo.Temp_TodoCategory(CatDescription) values('Home')
insert into dbo.Temp_TodoCategory(CatDescription) values('School')


--drop table dbo.Temp_Todo--------------------------------------------------------
create table dbo.Temp_Todo(
	 Id int identity(1,1) primary key
	,Title varchar(50) not null
	,Description varchar(255) not null
	,TargetDate date null --do not have to be set
	,CreatedDate datetime2(0) not null default getdate()
	,LastUpdatedDate datetime2(0) null
	,Done bit not null default 0 -- 0 not completed, 1 completed
	,FK_Category int not null references dbo.Temp_TodoCategory(Id)
	,FK_User int not null references dbo.Temp_TodoUsers(Id)
)
go

insert into dbo.Temp_Todo(Title, Description, TargetDate, FK_Category, FK_User)
values('User Interface', 'Develop user interface for a tofo app using asp.net core mvc', '2022-11-30', 1,1)

--Create API proc-----------------------------------------------------------------
create or alter procedure dbo.proc_Temp_TodoInsert(
	   @Title varchar(50)
	  ,@Description varchar(255)
	  ,@TargetDate date = null --does not have to be provided
	  ,@FK_Category int = 1 --default value when not provided
	  ,@FK_User int
)
as
begin
	insert into dbo.Temp_Todo(Title,Description,TargetDate,FK_Category,FK_User)
	values(@Title, @Description, @TargetDate,@FK_Category, @FK_User)

	select SCOPE_IDENTITY()
end

exec dbo.proc_Temp_TodoInsert 'Write SQL', 'tables, inserts, selects, procs', @FK_User = 1
go

--Read--------------------------------------------------------------
create or alter procedure dbo.proc_Temp_TodoGetAll
as
begin
	select
		 t.Id
		,t.Title
		,t.Description
		,t.CreatedDate
		,t.LastUpdatedDate
		,t.TargetDate
		,t.Done
		,t.FK_User
		,u.UserName
		,t.FK_Category
		,c.CatDescription as CategoryName
	from dbo.Temp_Todo t
	join dbo.Temp_TodoUsers u
		on u.Id = t.FK_User
	join dbo.Temp_TodoCategory c
		on c.Id = t.FK_Category
end

exec dbo.proc_Temp_TodoGetAll
go

--Update--------------------------------------------------------------
create or alter procedure dbo.proc_Temp_TodoUpdate(
	  @Id int
	 ,@Title varchar(50)
	 ,@Description varchar(255)
	 ,@FK_User int
	 ,@TargetDate date = null
	 ,@FK_Category int
)
as
begin
	update dbo.Temp_Todo
	set Title = @Title, 
		Description = @Description, 
		LastUpdatedDate = getdate(), 
		TargetDate = @TargetDate,
		FK_Category = @FK_Category
	where Id = @Id and FK_User =  @FK_User

	select @@ROWCOUNT  --returns number of affected rows
end

exec dbo.proc_Temp_TodoUpdate 6, 'Updated title', 'Updated descccc', 1, @FK_Category = 2
go

--Delete--------------------------------------------------------------
create or alter procedure dbo.proc_Temp_TodoDelete(
	 @Id int
	,@FK_User int
)
as
begin
	begin try
		delete from dbo.Temp_Todo
		where Id = @Id and FK_User = @FK_User

		select 'ok'  --returns number of affected rows
	end try
	begin catch
		select ERROR_MESSAGE()
	end catch
	
end

exec dbo.proc_Temp_TodoDelete 3, 1
go



