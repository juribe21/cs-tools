/*
Use of CTE, subQuery
*/

Select top 10 * from DetallesPagoEfectivo Order By SubTotal Desc

Select Max (SubTotal) from DetallesPagoEfectivo
Where SubTotal < ( Select Max (SubTotal ) From DetallesPagoEfectivo)

/*
Select N hight pago
*/

-- Top 2 hight Pago
Select Distinct Top 2 SubTotal From DetallesPagoEfectivo
Order by SubTotal Desc

Select top 1 SubTotal From
(Select Distinct Top 2 SubTotal From DetallesPagoEfectivo
Order by SubTotal Desc)
Result order by SubTotal Asc

/*
DENSE_RANK and CTE
*/

Select Count (SubTotal), SubTotal From DetallesPagoEfectivo wHERE SubTotal = 3174 Group By SubTotal;

Select TOP 10 SubTotal, Count (SubTotal) as NoPagos, Dense_Rank() Over (Order By SubTotal Desc) DENSERANK From DetallesPagoEfectivo Group By SubTotal;

With ResultSet as
(
	Select Top 100 SubTotal, Dense_Rank() Over (Order By SubTotal Desc) DENSERANK From DetallesPagoEfectivo Group By SubTotal
)
Select Top 1 SubTotal
From ResultSet Where ResultSet.DENSERANK = 6

/*
DENSE_RANK and CTE - OrderTrackingID
*/

Select TOP 10 IPC.OrderTrackingID, Count (IPC.OrderTrackingID) as CountIDs, Dense_Rank() Over (Order By IPC.OrderTrackingID Desc) DENSERANK 
From TempRxCalcSubmitAcknowledge TRCSA 
	INNER JOIN  RxOrderXref ROX ON TRCSA.OrderTrackingID = ROX.OrderTrackingID 
	INNER JOIN InvProcessControl IPC ON TRCSA.OrderTrackingID = IPC.OrderTrackingID 
WHERE IPC.RxCalculationStatus = 'P' AND IPC.OrderTrackingID In (1047572572,1047572571,1047572570,1047572562,1047572558,1047572576,1047572575)
Group By IPC.OrderTrackingID;

