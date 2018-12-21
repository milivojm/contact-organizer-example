DROP PROCEDURE FindContacts
GO

CREATE PROCEDURE FindContacts
    @FirstName Varchar(50),
    @LastName Varchar(50),
	@TelephoneNumber Varchar(15),
	@Address Varchar(50),
	@OrderBy Varchar(50),
	@Offset int,
	@Fetch int,
	@NumberOfResults int OUTPUT
AS
DECLARE
	@SqlStatement varchar(max), @CountStatement varchar(max)
BEGIN
    SET NOCOUNT ON;

	SET @SqlStatement = CONCAT('select Id, FirstName, LastName, TelephoneNumber, StreetNumber, City, PostalCode, Country from Contacts where FirstName like ''', @FirstName, '%'' and LastName like ''', @LastName, '%'' and TelephoneNumber like ''', @TelephoneNumber, '%'' and FullAddress like ''', @Address, '%''', ' order by ', @OrderBy, ' offset ', @Offset, ' rows fetch next ', @Fetch, ' rows only')	
	SET @CountStatement = CONCAT('select @NumberOfResults = COUNT(*) from Contacts where FirstName like ''', @FirstName, '%'' and LastName like ''', @LastName, '%'' and TelephoneNumber like ''', @TelephoneNumber, '%'' and FullAddress like ''', @Address, '%''')	
	EXEC(@SqlStatement)
	EXEC(@CountStatement)
END
GO