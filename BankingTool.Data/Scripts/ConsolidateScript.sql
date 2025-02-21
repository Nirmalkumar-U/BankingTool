:setvar Path "D:\Nirmal Learning Projects\Bank\BankingTool\BankingTool.Data\Scripts"
:setvar DBName BankingTool

SET NOCOUNT ON

PRINT '*************************STARTED**************************'
GO

USE $(DBName) 
GO

:r  $(Path)\Schema\Schema.sql
PRINT 'Schema.sql'
GO
:r  $(Path)\Schema\StoreProcedure.sql
PRINT 'StoreProcedure.sql'
GO
:r  $(Path)\MergeScripts\ConsolidateMergeScripts.sql
PRINT 'ConsolidateMergeScripts.sql'
GO

SET NOCOUNT OFF

PRINT '*************************COMPLETED**************************'