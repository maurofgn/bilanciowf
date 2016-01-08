--SELECT * FROM document
--
--delete document
--
--DBCC CHECKIDENT ('document', RESEED, 0) GO		--reset id=0 per la tabella document 
--DBCC CHECKIDENT ('documentRow', RESEED, 0) GO		--reset id=0 per la tabella documentRow
--
--SELECT * FROM document	--18


INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'1','Nota su doc 1',30.00,1)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'2','Nota su doc 2',50.00,2)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'3','Nota su doc 3',30.00,3)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'4','Nota su doc 4',50.00,4)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'5','Nota su doc 5',30.00,5)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'6','Nota su doc 6',50.00,6)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'7','Nota su doc 7',30.00,7)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'8','Nota su doc 8',50.00,8)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, getdate()),convert(date, getdate()),'9','Nota su doc 9',30.00,9)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'10','Nota su doc 10',28.00,1)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'11','Nota su doc 11',48.00,2)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'12','Nota su doc 12',28.00,3)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'13','Nota su doc 13',48.00,4)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'14','Nota su doc 14',28.00,5)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'15','Nota su doc 15',48.00,6)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'16','Nota su doc 16',28.00,7)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'17','Nota su doc 17',48.00,8)
GO

INSERT INTO "dbo"."Document" (dateReg,dateDoc,docNr,note,amount,DocumentType_ID) VALUES (convert(date, dateAdd(yy,-1, getDate())),convert(date, dateAdd(yy,-1, getDate())),'18','Nota su doc 18',28.00,9)
GO



INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,30.00,'attivo dare', getdate(),1,1)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,20.00,'ricavo avere', getdate(),1,113)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),1,37)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,50.00,'passivo avere', getdate(),2,38)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,45.00,'costo dare', getdate(),2,60)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),2,2)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,30.00,'attivo dare', getdate(),3,3)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,20.00,'ricavo avere', getdate(),3,114)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),3,39)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,50.00,'passivo avere', getdate(),4,40)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,45.00,'costo dare', getdate(),4,61)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),4,4)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,30.00,'attivo dare', getdate(),5,5)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,20.00,'ricavo avere', getdate(),5,115)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),5,41)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,50.00,'passivo avere', getdate(),6,42)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,45.00,'costo dare', getdate(),6,62)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),6,6)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,30.00,'attivo dare', getdate(),7,7)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,20.00,'ricavo avere', getdate(),7,116)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),7,43)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,50.00,'passivo avere', getdate(),8,44)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,45.00,'costo dare', getdate(),8,63)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),8,8)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,30.00,'attivo dare', getdate(),9,9)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,20.00,'ricavo avere', getdate(),9,117)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),9,45)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,28.00,'attivo dare', getdate(),10,10)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,18.00,'ricavo avere', getdate(),10,118)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),10,46)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,48.00,'passivo avere', getdate(),11,47)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,43.00,'costo dare', getdate(),11,65)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),11,11)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,28.00,'attivo dare', getdate(),12,12)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,18.00,'ricavo avere', getdate(),12,119)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),12,48)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,48.00,'passivo avere', getdate(),13,49)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,43.00,'costo dare', getdate(),13,66)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),13,13)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,28.00,'attivo dare', getdate(),14,14)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,18.00,'ricavo avere', getdate(),14,120)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),14,50)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,48.00,'passivo avere', getdate(),15,51)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,43.00,'costo dare', getdate(),15,67)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),15,15)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,28.00,'attivo dare', getdate(),16,16)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,18.00,'ricavo avere', getdate(),16,121)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),16,52)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,-1,48.00,'passivo avere', getdate(),17,53)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,1,43.00,'costo dare', getdate(),17,68)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,1,5.00,'attivo dare', getdate(),17,17)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (1,1,28.00,'attivo dare', getdate(),18,18)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (2,-1,18.00,'ricavo avere', getdate(),18,122)
GO

INSERT INTO "dbo"."DocumentRow" (rowNr,debit,amount,note,dateCreated,Document_ID,AccountChart_ID) VALUES (3,-1,10.00,'passivo avere', getdate(),18,54)
GO
