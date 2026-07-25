/*** Update Query ***/

Protected Sub CustomerCatalogProductsData()

 Try
     QueryString = $"
             UPDATE ShipAddressBook 
             SET 
                 ShipToName =  '{ship.ShipToName}',
                 ShipToEmail = '{ship.ShipToEmail}',
                 ShipToAddress1 = '{ship.ShipToAddress1}',
                 ShipToAddress2 = '{ship.ShipToAddress2}',
                 ShipToAddress3 = '{ship.ShipToAddress3}',
                 ShipToCity = '{ship.ShipToCity}',
                 ShipToState = '{ship.ShipToState}',
                 ShipToCountry = '{ship.ShipToCountry}',
                 ShipToCountryCode = '{ship.ShipToCountryCode}',
                 ShipToPostalCode = '{ship.ShipToPostalCode}',                           
                 ShipToPhone = '{ship.ShipToPhone}'
             WHERE AddressIndex = {ship.AddressIndex}                            
           "
     SqlConn.Open()
     SqlCmd = New System.Data.SqlClient.SqlCommand(QueryString, SqlConn)
     SqlCmd.ExecuteNonQuery()

 Catch ex As Exception
     labels.lblMsgShipToUpdated = "Update process fail"
     labels.IsError = True
 Finally
     If Not SqlCmd.Connection Is Nothing Then
         SqlCmd.Connection.Close()
         SqlCmd.Connection.Dispose()

         labels.lblMsgShipToUpdated = $"The {ship.ShipToName} client was updated"
         labels.lblAccountHeader = ship.ShipToName
         labels.IsError = False
         ' ----> btnCancel.InnerText = "Back"
     Else
         labels.lblMsgShipToUpdated = $"The {ship.ShipToName} client was not updated"
         labels.lblAccountHeader = ship.ShipToName
         labels.IsError = True

     End If
 End Try
 
End Sub
