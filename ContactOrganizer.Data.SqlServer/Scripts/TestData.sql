use ContactOrganizer
go

delete Contacts;

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Joe',
	'Gomez',
	'+328481512',
	'Wavetree 23',
	'Liverpool',
	'L2 2DP',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Alisson',
	'Becker',
	'+328481513',
	'Wavetree 12',
	'Liverpool',
	'L2 2DR',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Trent',
	'Alexander-Arnold',
	'+328481113',
	'Wavetree 2',
	'Liverpool',
	'L1 1DR',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Virgil',
	'van Dijk',
	'+3284817613',
	'Wavetree 45',
	'Liverpool',
	'L1 1FR',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Andy',
	'Robertson',
	'+3280817613',
	'Anfield 1',
	'Liverpool',
	'L1 1AR',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Jordan',
	'Henderson',
	'+3284917613',
	'Wavetree 85',
	'Liverpool',
	'L1 1AM',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Naby',
	'Keita',
	'+3280913613',
	'Wavetree 11',
	'Liverpool',
	'L1 2PM',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'James',
	'Milner',
	'+3214917613',
	'Stanley Park 11',
	'Liverpool',
	'L1 1AZ',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Roberto',
	'Firmino',
	'+32049437613',
	'Stanley Park 21',
	'Liverpool',
	'L1 1AI',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Sadio',
	'Mane',
	'+3314517613',
	'Anfield 31',
	'Liverpool',
	'L1 1BZ',
	'UK'
);

insert into Contacts (
	Id,
	FirstName,
	LastName,
	TelephoneNumber,
	StreetNumber,
	City,
	PostalCode,
	Country
) values (
	NEWID(),
	'Mohamed',
	'Salah',
	'+32145617613',
	'Egyptian King 1',
	'Liverpool',
	'L1 1AL',
	'UK'
);

UPDATE Contacts
SET FullAddress = CONCAT(StreetNumber,'\n',PostalCode,' ',City, '\n', Country);