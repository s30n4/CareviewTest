CREATE OR ALTER   proc [dbo].[spReportData]
(
      @sortField NVARCHAR(128) = null,
      @Ascending VARCHAR(5) = null,
      @MutlipleServices bit = null,
      @TotalValueLessThanAmount MONEY= null
) as
BEGIN

    DECLARE @SQL VARCHAR(MAX)='';
	DECLARE @havingSQL VARCHAR(MAX)='';
    DECLARE @orderBySQL VARCHAR(MAX)='';

    -- SETUP HAVING CLAUSE
    IF @MutlipleServices=1
        SET @havingSQL = ' HAVING(COUNT(S.ServiceName) > 1)'
    ELSE IF @MutlipleServices=0
        SET @havingSQL = ' HAVING(COUNT(S.ServiceName) = 1)'
    
    IF @TotalValueLessThanAmount IS NOT NULL
    BEGIN
        IF @MutlipleServices IS NULL
          SET @havingSQL = ' HAVING(SUM(S.Cost) <  ' + CONVERT(VARCHAR(10),@TotalValueLessThanAmount ) + ' )'
        ELSE
        SET @havingSQL = @havingSQL + ' AND (SUM(S.Cost) <  ' + CONVERT(VARCHAR(10),@TotalValueLessThanAmount ) + ' )'
    END



    -- SETUP ORDER BY CLAUSE
	IF @sortField = 'ClientName'
		SET @orderBySQL = ' ORDER BY C.FirstName, C.LastName '
	ELSE 
        SET @orderBySQL = ' ORDER BY ' + @sortField


    IF @Ascending ='false'
    	SET @orderBySQL = @orderBySQL + ' DESC'



    SET @SQL = 
	' 
		SELECT
		CONCAT(C.FirstName, '' '', C.LastName) AS ClientName , COUNT(S.ServiceName) NoOfService, SUM(S.Cost) AS TotalCost
		FROM 
			[dbo].[Client ] C
			INNER JOIN [dbo].[Services ] S on C.ClientID  = S.ClientID
	    GROUP BY 
            C.FirstName, C.LastName
	' + @havingSQL + @orderBySQL

	EXEC(@SQL)
END