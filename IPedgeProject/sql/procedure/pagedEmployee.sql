SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * 
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetPagedEmployee]') AND type in (N'P',N'PC'))
BEGIN
  EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_GetPagedEmployee] AS'
END
GO

ALTER PROCEDURE [dbo].[usp_GetPagedEmployee]
(
    @ReturnFields    nvarchar(3000) = '*',    -- 需要返回的列
    @PageSize        int = 10,                -- 每页记录数
    @PageIndex        int = 1,                -- 当前页码
    @Where            nvarchar(3000) = '',        -- 查询条件
    @OrderBy        nvarchar(200) = 'EmployeeID',            -- 排序字段名 最好为唯一主键
    @OrderType        int = 1,                    -- 排序类型 1:降序 其它为升序
    @TotalPage        int OUTPUT,
    @TotalRecord      int OUTPUT
)

AS
    DECLARE @CurrentPageSize int
    DECLARE @TotalRecordForPageIndex int
    declare @CountSql nvarchar(4000)

    if @OrderType = 1
        BEGIN
            set @OrderBy = ' Order by ' + REPLACE(@OrderBy,',',' desc,') + ' desc '
        END
    else
        BEGIN
            set @OrderBy = ' Order by ' + REPLACE(@OrderBy,',',' asc,') + ' asc '
        END

    -- 总记录
    set @CountSql='SELECT @TotalRecord=Count(0) From Employee ' + @Where
    execute sp_executesql @CountSql,N'@TotalRecord int out',@TotalRecord out
    SET @TotalPage=(@TotalRecord-1)/@PageSize+1

    -- 查询页数不得大于总页数
    if(@PageIndex > @TotalPage)
        set @PageIndex = @TotalPage

    SET @CurrentPageSize=(@PageIndex-1)*@PageSize

    -- 返回记录
    set @TotalRecordForPageIndex=@PageIndex*@PageSize
    exec    ('SELECT *
            FROM (SELECT TOP '+@TotalRecordForPageIndex+' '+@ReturnFields+', ROW_NUMBER() OVER ('+@OrderBy+') AS ROWNUM
            FROM Employee ' + @Where +' ) AS TempTable
            WHERE TempTable.ROWNUM >
            '+@CurrentPageSize)

    -- 返回总页数和总记录
    SELECT @TotalPage as PageCount,@TotalRecord as RecordCount
