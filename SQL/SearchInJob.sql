/* Search  string [Table or any other object] in Job*/

-- Option A
Select j.name JobName, s.step_name StepName
From msdb.dbo.sysjobsteps s
	join msdb.dbo.sysjobs j on j.job_id=s.job_id
Where s.command like '%TempCzvRxCalcJobResults%'

-- Option B
SELECT  js.database_name as DatabaseName,
                 jobs.Name as JobName,
                 js.step_id as StepID,
                 js.step_name as StepName, 
                 js.command as StepCommand
FROM     msdb.dbo.sysjobs as jobs
                INNER JOIN msdb.dbo.sysjobsteps as js ON jobs.job_id = js.job_id
WHERE js.command LIKE  '%TempCzvRxCalcJobResults%' --OR database_name = 'FileGeneration_Details'
ORDER BY jobs.Name,js.step_id
