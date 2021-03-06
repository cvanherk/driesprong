﻿CREATE TABLE [dbo].[FormEntryAccepted]
(
	[RecordGUID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (newid()), 
	[Name] VARCHAR(50) NOT NULL,
	[ref_Horse] UNIQUEIDENTIFIER NOT NULL,
	[Music] VARBINARY(MAX) NULL, 
    CONSTRAINT FK_ref_Horse FOREIGN KEY ([ref_Horse]) REFERENCES Horses(RecordGUID)
)
