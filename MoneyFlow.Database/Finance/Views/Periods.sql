CREATE VIEW Finance.Periods
AS
	SELECT DISTINCT
		-- Period = MONTH
		DATEADD(MONTH, DATEDIFF(MONTH, 0, [Date]), 0) AS [Date],
		AccountId

	FROM Finance.Costs