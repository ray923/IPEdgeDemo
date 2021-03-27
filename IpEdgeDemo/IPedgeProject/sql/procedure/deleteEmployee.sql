SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * 
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteEmployee]') AND type in (N'P',N'PC'))
BEGIN
  EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_DeleteEmployee] AS'
END
GO

ALTER PROCEDURE [dbo].[usp_DeleteEmployee]
(
    @EmployeeId        int
)
AS

Delete Employee where employeeId = @EmployeeId