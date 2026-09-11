-- Production
-- Enterprise Admin

Update SecurityUser Set PrimaryRole = 'Enterprise Admin' Where DisplayName Like '%Jorge Uribe%' and UserID IN (11103) -- CZNET/vijuribe
Update SecurityUser Set PrimaryRole = 'Production' Where DisplayName Like '%Jorge Uribe%' and UserID IN (11157) -- CZNET/a1l_vijuribe

Select * From SecurityUser Where DisplayName Like '%Jorge Uribe%'
and UserID IN (11103, 11157)

Select Distinct PrimaryRole From SecurityUser
