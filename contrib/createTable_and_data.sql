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


INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (1,'Root','Completo',1,0,0,0,1,0,null)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (2,'Patri','Patrimoniale',1,10,0,1,1,1,1)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (3,'Econo','Economico',1,20,0,1,1,2,1)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (4,'Attivo','Attivo',1,10,1,1,1,3,2)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (5,'Passivo','Passivo',1,20,1,1,-1,4,2)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (6,'Costi','Costi',1,10,1,1,1,5,3)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (7,'Ricavi','Ricavi',1,20,1,1,-1,6,3)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (8,'A','Crediti verso soci per versamento quote',1,10,1,1,1,7,4)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (9,'B','Immobilizzazioni',1,20,1,1,1,7,4)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (10,'C','Attivo circolante',1,30,1,1,1,7,4)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (11,'D','Ratei e risconti attivi',1,50,1,1,1,7,4)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (12,'B1','Immobilizzazioni immateriali',1,10,1,0,1,7,9)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (13,'B2','Immobilizzazioni materiali',1,20,1,0,1,7,9)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (14,'B3','Immobilizzazioni finanziarie',1,30,1,0,1,7,9)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (15,'C1','Rimanenze',1,10,1,0,1,7,10)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (16,'C2','Crediti',1,20,1,0,1,7,10)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (17,'C3','Attività finanziarie che non costituiscono immobilizzazioni',1,30,1,0,1,7,10)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (18,'C4','Disponibilità liquide',1,40,1,0,1,7,10)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (19,'D11','Ratei attivi',1,10,0,0,1,7,11)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (20,'D12','Risconti attivi',1,20,0,0,1,7,11)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (21,'B11','Spese costituzione e modifiche statutarie',1,10,0,0,1,7,12)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (22,'C11','materiale per benemerenze',1,10,0,0,1,7,15)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (23,'B12','Costi di ricerca, sviluppo e pubblicità',1,20,0,0,1,7,12)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (24,'C12','materiale per la propaganda',1,20,0,0,1,7,15)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (25,'B13','Spese manutenzioni straordinarie da ammortizzare',1,30,0,0,1,7,12)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (26,'C13','materiale acquistato per attività di fund raising',1,30,0,0,1,7,15)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (27,'B14','Oneri pluriennali',1,40,0,0,1,7,12)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (28,'C14','materiale donato da terzi per attività di fund raising',1,40,0,0,1,7,15)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (29,'B15','Altre immobilizzazioni immateriali',1,50,0,0,1,7,12)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (30,'C15','altro materiale',1,50,0,0,1,7,15)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (31,'C16','lavorazioni in corso ed acconti',1,60,0,0,1,7,15)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (32,'B21','Terreni e fabbricati',1,10,0,0,1,7,13)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (33,'C21','Crediti per rimborsi su donazioni effettuate',1,10,0,0,1,7,16)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (34,'B22','Impianti ed attrezzature per la donazione',1,20,0,0,1,7,13)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (35,'C22','Crediti per liberalità da ricevere',1,20,0,0,1,7,16)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (36,'B23','Mobili e macchine d''ufficio',1,30,0,0,1,7,13)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (37,'C23','Crediti verso altre Avis',1,30,0,0,1,7,16)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (38,'B24','Altri beni',1,40,0,0,1,7,13)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (39,'C24','Crediti per contributi da "cinque per mille"',1,40,0,0,1,7,16)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (40,'B25','Immobilizzazioni materiali in corso ed acconti',1,50,0,0,1,7,13)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (41,'C25','Crediti tributari',1,50,0,0,1,7,16)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (42,'C26','Crediti verso altri',1,60,0,0,1,7,16)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (43,'B31','Partecipazioni',1,10,0,0,1,7,14)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (44,'C31','Partecipazioni',1,10,0,0,1,7,17)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (45,'B32','Crediti (che costituiscono immobilizz.) v/altre Avis',1,20,0,0,1,7,14)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (46,'C32','Investimenti finanziari',1,20,0,0,1,7,17)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (47,'B33','Crediti (che costituiscono immobilizz.) v/altri soggetti',1,30,0,0,1,7,14)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (48,'C33','Altri titoli',1,30,0,0,1,7,17)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (49,'B34','Investimenti finanziari pluriennali',1,40,0,0,1,7,14)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (50,'B35','Altri titoli (che costituiscono immobilizzazioni)',1,50,0,0,1,7,14)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (51,'C41','Depositi bancari',1,10,0,0,1,7,18)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (52,'C42','Conto corrente postale',1,20,0,0,1,7,18)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (53,'C43','Assegni e titoli di credito a vista',1,30,0,0,1,7,18)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (54,'C44','Denaro contante e valori in cassa',1,40,0,0,1,7,18)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (55,'E','Patrimonio netto',1,10,1,1,-1,7,5)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (56,'F','Fondi per rischi ed oneri',1,20,1,1,-1,7,5)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (57,'G','T.F.R. lavoro subordinato',1,30,1,1,-1,7,5)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (58,'H','Debiti',1,40,1,1,-1,7,5)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (59,'K','Ratei e risconti passivi',1,50,1,1,-1,7,5)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (60,'E1','Patrimonio libero',1,10,1,0,-1,7,55)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (61,'E2','Fondo di dotazione (se previsto)',1,20,1,0,-1,7,55)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (62,'E3','Patrimonio vincolato',1,30,1,0,-1,7,55)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (63,'F11','Fondi per trattamenti di quiescenza e simili',1,10,0,0,-1,7,56)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (64,'F12','Altri fondi di accantonamento',1,20,0,0,-1,7,56)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (65,'G11','Trattamento di fine rapporto di lavoro subordinato',1,10,0,0,-1,7,57)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (66,'H11','Debiti per contributi da erogare',1,10,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (67,'H12','Debiti verso banche',1,20,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (68,'H13','Debiti verso altri finanziatori',1,30,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (69,'H14','Debiti verso fornitori',1,40,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (70,'H15','Debiti tributari',1,50,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (71,'H16','Debiti verso istituti di previdenza',1,60,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (72,'H17','Debiti verso altre Avis',1,70,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (73,'H18','Debiti per rimborsi spese nei confronti di soci volontari',1,80,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (74,'H19','Acconti ricevuti',1,90,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (75,'H20','Altri debiti',1,100,0,0,-1,7,58)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (76,'K11','Ratei passivi',1,10,0,0,-1,7,59)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (77,'K12','Risconti passivi',1,20,0,0,-1,7,59)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (78,'E11','Avanzo o disavanzo di gestione dell''esercizio',1,10,0,0,-1,7,60)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (79,'E12','Riserve accantonate di esercizi precedenti',1,20,0,0,-1,7,60)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (80,'E13','Contributi in conto capitalenon vincolati',1,30,0,0,-1,7,60)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (81,'E14','Rivalutazioni di beni patrimoniali',1,40,0,0,-1,7,60)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (82,'E21','Fondo di dotazione',1,10,0,0,-1,7,61)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (83,'E31','Fondi vincolati destinati da terzi',1,10,0,0,-1,7,62)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (84,'E32','Fondi vincolati per decisione di organi istituzionali',1,20,0,0,-1,7,62)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (85,'E33','Contributi in conto capitale vincolati da  terzi',1,30,0,0,-1,7,62)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (86,'E34','Riserve statutarie (se previste)',1,40,0,0,-1,7,62)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (87,'E35','Accantonamenti vincolati per scopi specifici',1,50,0,0,-1,7,62)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (88,'I','Oneri e spese da attività istituzionale',1,10,1,0,1,7,6)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (89,'J','Oneri e spese per attività di raccolta fondi',1,20,1,0,1,7,6)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (90,'M','Oneri e spese per attività accessorie',1,30,1,0,1,7,6)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (91,'N','Oneri e spese finanziarie e patrimoniali',1,40,1,0,1,7,6)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (92,'O','Oneri straordinari',1,50,1,0,1,7,6)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (93,'P','Oneri e spese di carattere generale ed indivisibile',1,60,1,0,1,7,6)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (94,'I.11','Acquisti',1,10,1,0,1,7,88)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (95,'I.12','Servizi',1,20,1,0,1,7,88)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (96,'I.13','Godimento beni di terzi',1,30,1,0,1,7,88)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (97,'N.11','Oneri da depositi bancari',1,10,1,0,1,7,91)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (98,'P.11','Acquisti',1,10,1,0,1,7,93)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (99,'P.12','Servizi',1,20,1,0,1,7,93)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (100,'P.13','Godimento beni di terzi di supporto generale',1,30,1,0,1,7,93)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (101,'J.11','Oneri per attività 1',1,10,0,0,1,7,89)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (102,'J.12','Oneri per attività 2',1,20,0,0,1,7,89)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (103,'J.13','Oneri per attività 3',1,30,0,0,1,7,89)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (104,'J.14','Oneri per attività ordinaria di raccolta fondi',1,40,0,0,1,7,89)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (105,'M.11','Acquisti',1,10,0,0,1,7,90)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (106,'M.12','Servizi',1,20,0,0,1,7,90)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (107,'M.13','Personale dipendente e collaboratori autonomi',1,30,0,0,1,7,90)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (108,'M.14','Godimento beni di terzi',1,40,0,0,1,7,90)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (109,'M.15','Ammortamenti immobilizzaz. attività accessorie',1,50,0,0,1,7,90)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (110,'M.16','Oneri diversi di gestione accessoria',1,60,0,0,1,7,90)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (111,'N.12','Interessi ed oneri su altri prestiti',1,20,0,0,1,7,91)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (112,'N.13','Oneri da patrimonio edilizio',1,30,0,0,1,7,91)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (113,'N.14','Oneri da altri beni patrimoniali',1,40,0,0,1,7,91)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (114,'O.11','Oneri da attività finanziarie',1,10,0,0,1,7,92)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (115,'O.12','Oneri da attività immobiliari',1,20,0,0,1,7,92)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (116,'O.13','Oneri da altre attività',1,30,0,0,1,7,92)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (117,'I.11.1','Materiale sanitario per raccolta',1,10,0,0,1,7,94)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (118,'N.11.1','Interessi passivi',1,10,0,0,1,7,97)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (119,'P.11.1','Cancelleria',1,10,0,0,1,7,98)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (120,'I.11.2','Materiale per benemerenze',1,20,0,0,1,7,94)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (121,'N.11.2','Oneri e spese bancarie',1,20,0,0,1,7,97)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (122,'P.11.2','Riviste e pubblicazioni',1,20,0,0,1,7,98)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (123,'I.11.3','Materiale di consumo',1,30,0,0,1,7,94)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (124,'N.11.3','Ritenute fiscali su interessi attivi',1,30,0,0,1,7,97)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (125,'I.12.1','Aree promozione e propaganda*',1,10,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (126,'P.12.1','Spese postali e telefoniche',1,10,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (127,'I.12.2','Personale e collaboratori per la raccolta',1,20,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (128,'P.12.2','Manutenzioni',1,20,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (129,'I.12.3','Spese trasporto sangue',1,30,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (130,'P.12.3','Omaggi e regalie',1,30,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (131,'I.12.4','Spese per servizio civile',1,40,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (132,'P.12.4','Spese pulizia',1,40,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (133,'I.12.5','Spese per assemblee e oneri connessi',1,50,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (134,'P.12.5','Assicurazioni volontari',1,50,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (135,'I.12.6','Quote associative Avis provinciale/regionale/nazionale',1,60,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (136,'P.12.6','Spese funzionamento organi associativi',1,60,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (137,'I.12.7','Convegni  e formazione volontari e collaboratori',1,70,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (138,'P.12.7','Erogazioni liberali',1,70,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (139,'I.12.8','Carburante, assic., manutenz.auto (attività istituz.)',1,80,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (140,'P.12.8','Servizi diversi',1,80,0,0,1,7,99)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (141,'I.12.9','30° anniversario di fondazione*',1,90,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (142,'I.12.10','Altre spese per servizi',1,100,0,0,1,7,95)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (143,'I.13.1','Canoni li leasing beni destinati alla raccolta',1,10,0,0,1,7,96)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (144,'P.13.1','Canoni di leasing',1,10,0,0,1,7,100)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (145,'I.13.2','Locazione locali destinati alla raccolta',1,20,0,0,1,7,96)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (146,'P.13.2','Locazione locali sede amministrativa',1,20,0,0,1,7,100)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (147,'I.14','Ammortamenti immobilizzazioni (attività istituz.)',1,30,0,0,1,7,96)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (148,'P.14','Spese per il personale di supporto generale',1,30,0,0,1,7,100)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (149,'I.15','Oneri diversi di gestione',1,40,0,0,1,7,96)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (150,'P.15','Ammortamenti beni di supporto generale',1,40,0,0,1,7,100)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (151,'P.16','Altri oneri di supporto generale',1,50,0,0,1,7,100)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (152,'T','Proventi da attività tipiche',1,10,1,0,-1,7,7)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (153,'U','Proventi da attività di raccolta fondi',1,20,1,0,-1,7,7)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (154,'V','Proventi da attività accessorie',1,30,1,0,-1,7,7)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (155,'W','Proventi finanziari e patrimoniali',1,40,1,0,-1,7,7)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (156,'X','Proventi straordinari',1,50,1,0,-1,7,7)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (157,'T.11','Convenzioni con Aziende Sanitarie',1,10,1,0,-1,7,152)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (158,'T.12','Contributi su progetti',1,20,0,0,-1,7,152)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (159,'T.14','Altri proventi',1,60,0,0,-1,7,152)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (160,'U.11','Proventi da attività 1',1,10,0,0,-1,7,153)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (161,'U.12','Proventi da attività 2',1,20,0,0,-1,7,153)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (162,'U.13','Proventi da attività 3',1,30,0,0,-1,7,153)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (163,'U.14','Proventi da "5 per mille"',1,40,0,0,-1,7,153)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (164,'V.11','Contributi e ricavi per progetti ed iniziative marginali',1,10,0,0,-1,7,154)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (165,'V.12','Contributi da soci ed associati',1,20,0,0,-1,7,154)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (166,'V.13','Contributi da altri soggetti',1,30,0,0,-1,7,154)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (167,'V.14','Altri proventi',1,40,0,0,-1,7,154)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (168,'W.11','Proventi lordi da depositi bancari',1,10,0,0,-1,7,155)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (169,'W.12','Proventi da investimenti ed altre attività finanziarie',1,20,0,0,-1,7,155)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (170,'W.13','Proventi dal patrimonio edilizio',1,30,0,0,-1,7,155)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (171,'W.14','Proventi da altri beni patrimoniali',1,40,0,0,-1,7,155)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (172,'X.11','Proventi da attività finanziarie',1,10,0,0,-1,7,156)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (173,'X.12','Proventi da attività immobiliari',1,20,0,0,-1,7,156)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (174,'X.13','Proventi da altre attività',1,30,0,0,-1,7,156)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (175,'X.14','Ripresa fondo per festa sociale',1,40,0,0,-1,7,156)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (176,'T.11.1','Rimborsi per donazioni',1,10,0,0,-1,7,157)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (177,'T.11.2','Rimborsi per trasporto sangue',1,20,0,0,-1,7,157)
GO

INSERT INTO "dbo"."AccountCee" (ID,Code,Name,Active,SeqNo,Summary,Total,Debit,NodeType,ParentID) VALUES (178,'cod','name',1,10,1,1,1,7,14)
GO



INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (1,'B11','Spese costituzione e modifiche statutarie',1,1,21)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (2,'B12','Costi di ricerca, sviluppo e pubblicità',1,1,23)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (3,'B13','Spese manutenzioni straordinarie da ammortizzare',1,1,25)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (4,'B14','Oneri pluriennali',1,1,27)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (5,'B15','Altre immobilizzazioni immateriali',1,1,29)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (6,'B21','Terreni e fabbricati',1,1,32)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (7,'B22','Impianti ed attrezzature per la donazione',1,1,34)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (8,'B23','Mobili e macchine d''ufficio',1,1,36)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (9,'B24','Altri beni',1,1,38)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (10,'B25','Immobilizzazioni materiali in corso ed acconti',1,1,40)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (11,'B31','Partecipazioni',1,1,43)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (12,'B32','Crediti (che costituiscono immobilizz.) v/altre Avis',1,1,45)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (13,'B33','Crediti (che costituiscono immobilizz.) v/altri soggetti',1,1,47)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (14,'B34','Investimenti finanziari pluriennali',1,1,49)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (15,'B35','Altri titoli (che costituiscono immobilizzazioni)',1,1,50)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (16,'C11','materiale per benemerenze',1,1,22)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (17,'C12','materiale per la propaganda',1,1,24)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (18,'C13','materiale acquistato per attività di fund raising',1,1,26)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (19,'C14','materiale donato da terzi per attività di fund raising',1,1,28)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (20,'C15','altro materiale',1,1,30)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (21,'C16','lavorazioni in corso ed acconti',1,1,31)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (22,'C21','Crediti per rimborsi su donazioni effettuate',1,1,33)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (23,'C22','Crediti per liberalità da ricevere',1,1,35)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (24,'C23','Crediti verso altre Avis',1,1,37)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (25,'C24','Crediti per contributi da "cinque per mille"',1,1,39)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (26,'C25','Crediti tributari',1,1,41)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (27,'C26','Crediti verso altri',1,1,42)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (28,'C31','Partecipazioni',1,1,44)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (29,'C32','Investimenti finanziari',1,1,46)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (30,'C33','Altri titoli',1,1,48)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (31,'C41','Depositi bancari',1,1,51)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (32,'C42','Conto corrente postale',1,1,52)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (33,'C43','Assegni e titoli di credito a vista',1,1,53)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (34,'C44','Denaro contante e valori in cassa',1,1,54)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (35,'D11','Ratei attivi',1,1,19)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (36,'D12','Risconti attivi',1,1,20)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (37,'E11','Avanzo o disavanzo di gestione dell''esercizio',-1,1,78)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (38,'E12','Riserve accantonate di esercizi precedenti',-1,1,79)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (39,'E13','Contributi in conto capitalenon vincolati',-1,1,80)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (40,'E14','Rivalutazioni di beni patrimoniali',-1,1,81)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (41,'E21','Fondo di dotazione',-1,1,82)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (42,'E31','Fondi vincolati destinati da terzi',-1,1,83)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (43,'E32','Fondi vincolati per decisione di organi istituzionali',-1,1,84)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (44,'E33','Contributi in conto capitale vincolati da  terzi',-1,1,85)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (45,'E34','Riserve statutarie (se previste)',-1,1,86)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (46,'E35','Accantonamenti vincolati per scopi specifici',-1,1,87)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (47,'F11','Fondi per trattamenti di quiescenza e simili',-1,1,63)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (48,'F12','Altri fondi di accantonamento',-1,1,64)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (49,'G11','Trattamento di fine rapporto di lavoro subordinato',-1,1,65)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (50,'H11','Debiti per contributi da erogare',-1,1,66)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (51,'H12','Debiti verso banche',-1,1,67)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (52,'H13','Debiti verso altri finanziatori',-1,1,68)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (53,'H14','Debiti verso fornitori',-1,1,69)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (54,'H15','Debiti tributari',-1,1,70)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (55,'H16','Debiti verso istituti di previdenza',-1,1,71)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (56,'H17','Debiti verso altre Avis',-1,1,72)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (57,'H18','Debiti per rimborsi spese nei confronti di soci volontari',-1,1,73)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (58,'H19','Acconti ricevuti',-1,1,74)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (59,'H20','Altri debiti',-1,1,75)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (60,'I.11.1','Materiale sanitario per raccolta',1,1,117)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (61,'I.11.2','Materiale per benemerenze',1,1,120)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (62,'I.11.3','Materiale di consumo',1,1,123)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (63,'I.12.1','Aree promozione e propaganda*',1,1,125)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (64,'I.12.10','Altre spese per servizi',1,1,142)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (65,'I.12.2','Personale e collaboratori per la raccolta',1,1,127)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (66,'I.12.3','Spese trasporto sangue',1,1,129)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (67,'I.12.4','Spese per servizio civile',1,1,131)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (68,'I.12.5','Spese per assemblee e oneri connessi',1,1,133)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (69,'I.12.6','Quote associative Avis provinciale/regionale/nazionale',1,1,135)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (70,'I.12.7','Convegni  e formazione volontari e collaboratori',1,1,137)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (71,'I.12.8','Carburante, assic., manutenz.auto (attività istituz.)',1,1,139)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (72,'I.12.9','30° anniversario di fondazione*',1,1,141)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (73,'I.13.1','Canoni li leasing beni destinati alla raccolta',1,1,143)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (74,'I.13.2','Locazione locali destinati alla raccolta',1,1,145)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (75,'I.14','Ammortamenti immobilizzazioni (attività istituz.)',1,1,147)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (76,'I.15','Oneri diversi di gestione',1,1,149)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (77,'J.11','Oneri per attività 1',1,1,101)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (78,'J.12','Oneri per attività 2',1,1,102)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (79,'J.13','Oneri per attività 3',1,1,103)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (80,'J.14','Oneri per attività ordinaria di raccolta fondi',1,1,104)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (81,'K11','Ratei passivi',-1,1,76)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (82,'K12','Risconti passivi',-1,1,77)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (83,'M.11','Acquisti',1,1,105)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (84,'M.12','Servizi',1,1,106)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (85,'M.13','Personale dipendente e collaboratori autonomi',1,1,107)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (86,'M.14','Godimento beni di terzi',1,1,108)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (87,'M.15','Ammortamenti immobilizzaz. attività accessorie',1,1,109)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (88,'M.16','Oneri diversi di gestione accessoria',1,1,110)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (89,'N.11.1','Interessi passivi',1,1,118)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (90,'N.11.2','Oneri e spese bancarie',1,1,121)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (91,'N.11.3','Ritenute fiscali su interessi attivi',1,1,124)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (92,'N.12','Interessi ed oneri su altri prestiti',1,1,111)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (93,'N.13','Oneri da patrimonio edilizio',1,1,112)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (94,'N.14','Oneri da altri beni patrimoniali',1,1,113)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (95,'O.11','Oneri da attività finanziarie',1,1,114)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (96,'O.12','Oneri da attività immobiliari',1,1,115)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (97,'O.13','Oneri da altre attività',1,1,116)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (98,'P.11.1','Cancelleria',1,1,119)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (99,'P.11.2','Riviste e pubblicazioni',1,1,122)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (100,'P.12.1','Spese postali e telefoniche',1,1,126)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (101,'P.12.2','Manutenzioni',1,1,128)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (102,'P.12.3','Omaggi e regalie',1,1,130)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (103,'P.12.4','Spese pulizia',1,1,132)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (104,'P.12.5','Assicurazioni volontari',1,1,134)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (105,'P.12.6','Spese funzionamento organi associativi',1,1,136)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (106,'P.12.7','Erogazioni liberali',1,1,138)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (107,'P.12.8','Servizi diversi',1,1,140)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (108,'P.13.1','Canoni di leasing',1,1,144)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (109,'P.13.2','Locazione locali sede amministrativa',1,1,146)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (110,'P.14','Spese per il personale di supporto generale',1,1,148)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (111,'P.15','Ammortamenti beni di supporto generale',1,1,150)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (112,'P.16','Altri oneri di supporto generale',1,1,151)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (113,'T.11.1','Rimborsi per donazioni',-1,1,176)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (114,'T.11.2','Rimborsi per trasporto sangue',-1,1,177)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (115,'T.12','Contributi su progetti',-1,1,158)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (116,'T.14','Altri proventi',-1,1,159)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (117,'U.11','Proventi da attività 1',-1,1,160)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (118,'U.12','Proventi da attività 2',-1,1,161)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (119,'U.13','Proventi da attività 3',-1,1,162)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (120,'U.14','Proventi da "5 per mille"',-1,1,163)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (121,'V.11','Contributi e ricavi per progetti ed iniziative marginali',-1,1,164)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (122,'V.12','Contributi da soci ed associati',-1,1,165)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (123,'V.13','Contributi da altri soggetti',-1,1,166)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (124,'V.14','Altri proventi',-1,1,167)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (125,'W.11','Proventi lordi da depositi bancari',-1,1,168)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (126,'W.12','Proventi da investimenti ed altre attività finanziarie',-1,1,169)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (127,'W.13','Proventi dal patrimonio edilizio',-1,1,170)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (128,'W.14','Proventi da altri beni patrimoniali',-1,1,171)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (129,'X.11','Proventi da attività finanziarie',-1,1,172)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (130,'X.12','Proventi da attività immobiliari',-1,1,173)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (131,'X.13','Proventi da altre attività',-1,1,174)
GO

INSERT INTO "dbo"."AccountChart" (ID,Code,Name,Debit,Active,AccountCeeID) VALUES (132,'X.14','Ripresa fondo per festa sociale',-1,1,175)
GO



INSERT INTO "dbo"."Avis" (ID,Name,Active,dateCreated,Address,City,PostalCode,Region,Email,Phone,ContactName) VALUES (1,'Comunale Morrovalle',1,{ts '2015-11-16 11:06:22.'},'piazza Vittorio Emanuele II n.12','Morrovalle','62010','MC','morrovalle.comunale@avis.it','0733/222405','Dott. Signorini Mario')
GO



INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (1,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'1','Nota su doc 1',30.00,1)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (2,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'2','Nota su doc 2',50.00,2)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (3,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'3','Nota su doc 3',30.00,3)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (4,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'4','Nota su doc 4',50.00,4)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (5,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'5','Nota su doc 5',30.00,5)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (6,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'6','Nota su doc 6',50.00,6)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (7,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'7','Nota su doc 7',30.00,7)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (8,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'8','Nota su doc 8',50.00,8)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (9,{ts '2015-11-16 11:10:14.'},{ts '2015-11-16 11:10:14.'},'9','Nota su doc 9',30.00,9)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (10,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'10','Nota su doc 10',28.00,1)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (11,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'11','Nota su doc 11',48.00,2)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (12,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'12','Nota su doc 12',28.00,3)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (13,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'13','Nota su doc 13',48.00,4)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (14,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'14','Nota su doc 14',28.00,5)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (15,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'15','Nota su doc 15',48.00,6)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (16,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'16','Nota su doc 16',28.00,7)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (17,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'17','Nota su doc 17',48.00,8)
GO

INSERT INTO "dbo"."Document" (ID,dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (18,{ts '2014-11-16 11:10:14.'},{ts '2014-11-16 11:10:14.'},'18','Nota su doc 18',28.00,9)
GO



INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,1,30.00,'attivo dare',{ts '2015-11-16 11:10:14.'},1,1)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,2,-1,20.00,'ricavo avere',{ts '2015-11-16 11:10:14.'},1,113)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:14.'},1,37)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (4,1,-1,50.00,'passivo avere',{ts '2015-11-16 11:10:14.'},2,38)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (5,2,1,45.00,'costo dare',{ts '2015-11-16 11:10:14.'},2,60)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (6,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:14.'},2,2)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (7,1,1,30.00,'attivo dare',{ts '2015-11-16 11:10:14.'},3,3)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (8,2,-1,20.00,'ricavo avere',{ts '2015-11-16 11:10:14.'},3,114)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (9,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:14.'},3,39)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (10,1,-1,50.00,'passivo avere',{ts '2015-11-16 11:10:15.'},4,40)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (11,2,1,45.00,'costo dare',{ts '2015-11-16 11:10:15.'},4,61)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (12,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:15.'},4,4)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (13,1,1,30.00,'attivo dare',{ts '2015-11-16 11:10:15.'},5,5)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (14,2,-1,20.00,'ricavo avere',{ts '2015-11-16 11:10:15.'},5,115)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (15,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:15.'},5,41)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (16,1,-1,50.00,'passivo avere',{ts '2015-11-16 11:10:15.'},6,42)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (17,2,1,45.00,'costo dare',{ts '2015-11-16 11:10:15.'},6,62)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (18,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:15.'},6,6)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (19,1,1,30.00,'attivo dare',{ts '2015-11-16 11:10:15.'},7,7)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (20,2,-1,20.00,'ricavo avere',{ts '2015-11-16 11:10:15.'},7,116)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (21,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:15.'},7,43)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (22,1,-1,50.00,'passivo avere',{ts '2015-11-16 11:10:15.'},8,44)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (23,2,1,45.00,'costo dare',{ts '2015-11-16 11:10:15.'},8,63)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (24,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:15.'},8,8)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (25,1,1,30.00,'attivo dare',{ts '2015-11-16 11:10:16.'},9,9)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (26,2,-1,20.00,'ricavo avere',{ts '2015-11-16 11:10:16.'},9,117)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (27,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:16.'},9,45)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (28,1,1,28.00,'attivo dare',{ts '2015-11-16 11:10:16.'},10,10)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (29,2,-1,18.00,'ricavo avere',{ts '2015-11-16 11:10:16.'},10,118)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (30,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:16.'},10,46)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (31,1,-1,48.00,'passivo avere',{ts '2015-11-16 11:10:16.'},11,47)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (32,2,1,43.00,'costo dare',{ts '2015-11-16 11:10:16.'},11,65)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (33,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:16.'},11,11)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (34,1,1,28.00,'attivo dare',{ts '2015-11-16 11:10:16.'},12,12)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (35,2,-1,18.00,'ricavo avere',{ts '2015-11-16 11:10:16.'},12,119)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (36,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:16.'},12,48)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (37,1,-1,48.00,'passivo avere',{ts '2015-11-16 11:10:16.'},13,49)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (38,2,1,43.00,'costo dare',{ts '2015-11-16 11:10:16.'},13,66)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (39,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:16.'},13,13)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (40,1,1,28.00,'attivo dare',{ts '2015-11-16 11:10:17.'},14,14)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (41,2,-1,18.00,'ricavo avere',{ts '2015-11-16 11:10:17.'},14,120)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (42,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:17.'},14,50)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (43,1,-1,48.00,'passivo avere',{ts '2015-11-16 11:10:17.'},15,51)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (44,2,1,43.00,'costo dare',{ts '2015-11-16 11:10:17.'},15,67)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (45,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:17.'},15,15)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (46,1,1,28.00,'attivo dare',{ts '2015-11-16 11:10:17.'},16,16)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (47,2,-1,18.00,'ricavo avere',{ts '2015-11-16 11:10:17.'},16,121)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (48,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:17.'},16,52)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (49,1,-1,48.00,'passivo avere',{ts '2015-11-16 11:10:17.'},17,53)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (50,2,1,43.00,'costo dare',{ts '2015-11-16 11:10:17.'},17,68)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (51,3,1,5.00,'attivo dare',{ts '2015-11-16 11:10:17.'},17,17)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (52,1,1,28.00,'attivo dare',{ts '2015-11-16 11:10:18.'},18,18)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (53,2,-1,18.00,'ricavo avere',{ts '2015-11-16 11:10:18.'},18,122)
GO

INSERT INTO "dbo"."DocumentRow" (ID,rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (54,3,-1,10.00,'passivo avere',{ts '2015-11-16 11:10:18.'},18,54)
GO



INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (1,'acqFat','Fattura di Acquisto',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (2,'acqRic','Ricevuta di Acquisto',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (3,'venFat','Fattura di Vendita',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (4,'venRic','Ricevuta di Vendita',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (5,'cassaUscita','Uscita di cassa',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (6,'cassaEntrata','Entrata di cassa',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (7,'bancaAddebito','Addebito bancario',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (8,'bancaAccredito','Accredito bancario',1)
GO

INSERT INTO "dbo"."DocumentType" (ID,Code,Name,Active) VALUES (9,'giro','Giroconti',1)
GO



INSERT INTO "dbo"."Report" (ID,Code,Name,Active,ModelName,FormatType,OutFileName,ActioneName,ControllerName,dateCreated,lastUpdate) VALUES (1,'balance','Bilancio CEE',0,null,null,null,'Balance','AccountCee',{ts '2015-11-16 11:08:57.'},{ts '2015-11-16 11:08:57.'})
GO

