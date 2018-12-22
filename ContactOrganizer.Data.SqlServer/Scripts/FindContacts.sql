USE ContactOrganizer
GO

IF OBJECT_ID(N'FindContacts') IS NOT NULL
	DROP PROCEDURE FindContacts
GO

CREATE PROCEDURE FindContacts
    @FirstName NVarchar(50),
    @LastName NVarchar(50),
	@TelephoneNumber NVarchar(15),
	@Address NVarchar(50),
	@OrderBy NVarchar(50),
	@Offset int,
	@Fetch int,
	@TotalRows int OUT
AS
DECLARE
	@SqlStatement varchar(max)
BEGIN
    SET NOCOUNT ON;
	
	IF @OrderBy IS NULL OR LEN(@OrderBy) = 0
		SET @OrderBy = 'FirstName'

	SELECT @TotalRows = COUNT(*) 
	FROM Contacts 
	WHERE FirstName LIKE @FirstName+'%'
	AND LastName LIKE @LastName+'%'
	AND TelephoneNumber LIKE @TelephoneNumber+'%'
	AND FullAddress LIKE @Address+'%'

	SET @SqlStatement = CONCAT('SELECT Id, FirstName, LastName, TelephoneNumber, StreetNumber, City, PostalCode, Country, FullAddress from Contacts where FirstName like ''', @FirstName, '%'' and LastName like ''', @LastName, '%'' and TelephoneNumber like ''', @TelephoneNumber, '%'' and FullAddress like ''', @Address, '%''', ' order by ', @OrderBy, ' offset ', @Offset, ' rows fetch next ', @Fetch, ' rows only')		
	EXEC(@SqlStatement)
END
GO