CREATE TABLE "dbo"."AccountCee"
(
   ID int PRIMARY KEY NOT NULL,
   Code varchar(20) NOT NULL,
   Name varchar(60) NOT NULL,
   Active tinyint NOT NULL,
   SeqNo int NOT NULL,
   Summary tinyint NOT NULL,
   Total tinyint NOT NULL,
   Debit int NOT NULL,
   NodeType int NOT NULL,
   ParentID int
)
GO
CREATE TABLE "dbo"."AccountChart"
(
   ID int PRIMARY KEY NOT NULL,
   Code varchar(20) NOT NULL,
   Name varchar(60) NOT NULL,
   Debit int NOT NULL,
   Active tinyint NOT NULL,
   AccountCeeID int
)
GO
CREATE TABLE "dbo"."Avis"
(
   ID int PRIMARY KEY NOT NULL,
   Name varchar(60) NOT NULL,
   Active tinyint NOT NULL,
   dateCreated datetime NOT NULL,
   Address text,
   City text,
   PostalCode varchar(5),
   Region text,
   Email text,
   Phone text,
   ContactName text
)
GO
CREATE TABLE "dbo"."Document"
(
   ID int PRIMARY KEY NOT NULL,
   dateReg datetime NOT NULL,
   dateDoc datetime NOT NULL,
   docNr varchar(20),
   note text,
   amount decimal(18,2) NOT NULL,
   DocumentType_ID int NOT NULL
)
GO
CREATE TABLE "dbo"."DocumentRow"
(
   ID int PRIMARY KEY NOT NULL,
   rowNr int NOT NULL,
   debit int NOT NULL,
   amount decimal(18,2) NOT NULL,
   note text,
   dateCreated datetime NOT NULL,
   Document_ID int NOT NULL,
   AccountChart_ID int NOT NULL
)
GO
CREATE TABLE "dbo"."DocumentType"
(
   ID int PRIMARY KEY NOT NULL,
   Code varchar(20) NOT NULL,
   Name varchar(60) NOT NULL,
   Active tinyint NOT NULL
)
GO
CREATE TABLE "dbo"."Report"
(
   ID int PRIMARY KEY NOT NULL,
   Code varchar(20) NOT NULL,
   Name varchar(60) NOT NULL,
   Active tinyint NOT NULL,
   ModelName text,
   FormatType int,
   OutFileName text,
   ActioneName text,
   ControllerName text,
   dateCreated datetime NOT NULL,
   lastUpdate datetime NOT NULL
)
GO
ALTER TABLE "dbo"."AccountCee"
ADD CONSTRAINT FK_dbo.AccountCee_dbo.AccountCee_ParentID
FOREIGN KEY (ParentID)
REFERENCES "dbo"."AccountCee"(ID)
GO
CREATE UNIQUE INDEX codeIndex ON "dbo"."AccountCee"(Code)
GO
CREATE INDEX IX_ParentID ON "dbo"."AccountCee"(ParentID)
GO
CREATE UNIQUE INDEX PK_dbo.AccountCee ON "dbo"."AccountCee"(ID)
GO
ALTER TABLE "dbo"."AccountChart"
ADD CONSTRAINT FK_dbo.AccountChart_dbo.AccountCee_AccountCeeID
FOREIGN KEY (AccountCeeID)
REFERENCES "dbo"."AccountCee"(ID)
GO
CREATE UNIQUE INDEX PK_dbo.AccountChart ON "dbo"."AccountChart"(ID)
GO
CREATE INDEX IX_AccountCeeID ON "dbo"."AccountChart"(AccountCeeID)
GO
CREATE UNIQUE INDEX codeIndex ON "dbo"."AccountChart"(Code)
GO
CREATE UNIQUE INDEX PK_dbo.Avis ON "dbo"."Avis"(ID)
GO
ALTER TABLE "dbo"."Document"
ADD CONSTRAINT FK_dbo.Document_dbo.DocumentType_DocumentType_ID
FOREIGN KEY (DocumentType_ID)
REFERENCES "dbo"."DocumentType"(ID) ON DELETE CASCADE
GO
CREATE INDEX IX_DocumentType_ID ON "dbo"."Document"(DocumentType_ID)
GO
CREATE UNIQUE INDEX PK_dbo.Document ON "dbo"."Document"(ID)
GO
ALTER TABLE "dbo"."DocumentRow"
ADD CONSTRAINT FK_dbo.DocumentRow_dbo.AccountChart_AccountChart_ID
FOREIGN KEY (AccountChart_ID)
REFERENCES "dbo"."AccountChart"(ID) ON DELETE CASCADE
GO
ALTER TABLE "dbo"."DocumentRow"
ADD CONSTRAINT FK_dbo.DocumentRow_dbo.Document_Document_ID
FOREIGN KEY (Document_ID)
REFERENCES "dbo"."Document"(ID) ON DELETE CASCADE
GO
CREATE INDEX IX_AccountChart_ID ON "dbo"."DocumentRow"(AccountChart_ID)
GO
CREATE INDEX IX_Document_ID ON "dbo"."DocumentRow"(Document_ID)
GO
CREATE UNIQUE INDEX PK_dbo.DocumentRow ON "dbo"."DocumentRow"(ID)
GO
CREATE UNIQUE INDEX codeIndex ON "dbo"."DocumentType"(Code)
GO
CREATE UNIQUE INDEX PK_dbo.DocumentType ON "dbo"."DocumentType"(ID)
GO
CREATE UNIQUE INDEX PK_dbo.Report ON "dbo"."Report"(ID)
GO
CREATE UNIQUE INDEX codeIndex ON "dbo"."Report"(Code)
GO
