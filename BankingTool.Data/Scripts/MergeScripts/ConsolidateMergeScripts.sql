PRINT '*************************Merge Script STARTED**************************'
GO

:r  $(path)\MergeScripts\BankTable.sql
GO
:r  $(path)\MergeScripts\StateTable.sql
GO
:r  $(path)\MergeScripts\CityTable.sql
GO
:r  $(path)\MergeScripts\CodeValue.sql
GO
:r  $(path)\MergeScripts\RoleTable.sql
GO
:r  $(path)\MergeScripts\UsersTable.sql
GO
:r  $(path)\MergeScripts\ActionTable.sql
GO

PRINT '*************************Merge Script COMPLETED**************************'