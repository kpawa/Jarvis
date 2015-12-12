GO
-- FK from Device
IF OBJECT_ID('StoredData') 
IS NOT NULL DROP TABLE StoredData;
-- FK from ProviderAccount, DeviceData
IF OBJECT_ID('Device') 
IS NOT NULL DROP TABLE Device;
IF OBJECT_ID('DeviceData') 
IS NOT NULL DROP TABLE DeviceData;

GO
-- FK from Account
IF OBJECT_ID('ProviderAccount') 
IS NOT NULL DROP TABLE ProviderAccount;

-- FK from Account
IF OBJECT_ID('Details') 
IS NOT NULL DROP TABLE Details;

IF OBJECT_ID('Account') 
IS NOT NULL DROP TABLE Account;


CREATE TABLE Account (
	accountID	INT PRIMARY KEY,
	email		VARCHAR(255),
	password	VARCHAR(255),
	firstname	VARCHAR(255),
	lastname	VARCHAR(255),
	birthdate	DATE,
	type		VARCHAR(255)
)

CREATE TABLE Details (
	accountID	INT PRIMARY KEY,
	familysize	INT,
	children	INT,
	adults		INT,
	rooms		INT,
	address		VARCHAR(255),
	FOREIGN KEY(accountID) REFERENCES Account(accountID)
)

CREATE TABLE ProviderAccount (
	accountID	INT PRIMARY KEY,
	username	VARCHAR(255),
	password	VARCHAR(255),
	provider	VARCHAR(255)
	FOREIGN KEY(accountID) REFERENCES Account(accountID)
)

CREATE TABLE DeviceData (
	dataID		INT PRIMARY KEY,
	state		VARCHAR(255),
	kwhour		FLOAT(2) -- number with 2 decimals
)

CREATE TABLE Device (
	deviceID	INT PRIMARY KEY,
	category	VARCHAR(255),
	accountID	INT,
	dataID		INT,
	FOREIGN KEY(accountID) REFERENCES Account(accountID),
	FOREIGN KEY(dataID) REFERENCES DeviceData(dataID)
)

CREATE TABLE StoredData (
	deviceID	INT PRIMARY KEY,
	state		VARCHAR(255),
	timestamp	TIMESTAMP, -- stores both date and time
	FOREIGN KEY(deviceID) REFERENCES Device(deviceID)

)