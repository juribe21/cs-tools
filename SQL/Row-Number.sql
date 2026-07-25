/*
Use of CTE, subQuery, Row_Number
*/

Select Top 100 SubTotal, Row_Number() Over (Order By SubTotal Desc) ROWNUMBER From DetallesPagoEfectivo Group By SubTotal;

With ResultSet as
(
	Select Top 1000  *, Row_Number() Over (PARTITION By SubTotal ORDER BY SUBTOTAL) ROWNUMBER From DetallesPagoEfectivo Order By SubTotal Desc
)
Select SubTotal, Count(SubTotal) cT From ResultSet where ResultSet.RowNumber > 2 Group By SubTotal Order By  ct Desc;