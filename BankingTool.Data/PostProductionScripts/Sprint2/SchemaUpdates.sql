If(OBJECT_ID('[FK_CalendarEvent_Users_UserId]') Is Not Null)
BEGIN
	ALTER TABLE [CalendarEvent] DROP CONSTRAINT [FK_CalendarEvent_Users_UserId]
END
GO
IF EXISTS (SELECT * FROM sys.tables where name = N'CalendarEvent')
BEGIN
	DROP TABLE CalendarEvent
END
GO

CREATE TABLE CalendarEvent (
    CalendarEventId INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT CONSTRAINT FK_CalendarEvent_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId) NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    StartDateTime DATETIME2 NOT NULL,
    EndDateTime DATETIME2 NULL,
    EventType NVARCHAR(50) NOT NULL,
    Recurrence NVARCHAR(50) NULL,
    Duration NVARCHAR(50) NULL,
    Priority NVARCHAR(50) NOT NULL DEFAULT 'Normal',
    IsAllDay BIT NOT NULL,
    IsPublic BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME2 NOT NULL,
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    IsDeleted BIT NOT NULL
);
------------------------------------------------------------------------------------------------------------------------
If(OBJECT_ID('[FK_CalendarNote_Users_UserId]') Is Not Null)
BEGIN
	ALTER TABLE CalendarNote DROP CONSTRAINT [FK_CalendarNote_Users_UserId]
END
GO
IF EXISTS (SELECT * FROM sys.tables where name = N'CalendarNote')
BEGIN
	DROP TABLE CalendarNote
END
GO
CREATE TABLE CalendarNote (
    CalendarNoteId INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT CONSTRAINT FK_CalendarNote_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    Date DATE NOT NULL,
    CreatedDate DATETIME2 NOT NULL,
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedDate DATETIME2 NULL,
    ModifiedBy NVARCHAR(100) NULL,
    IsDeleted BIT NOT NULL
);