

public SqlDataReader ExecuteReader(string sp_commandText, CommandType commandType)
{
    SqlDataReader reader;
    using (SqlConnection connection = new SqlConnection(GetConnectionStrig()))
    {
        connection.Open();

        using (SqlCommand command = new SqlCommand(sp_commandText, connection))
        {
            command.CommandTimeout = 60;
            command.CommandType = commandType;
            reader = command.ExecuteReader();

            // Operations with dataReader should be inside SqlCommand
            if (reader.Read())
            {
                string xml = "Hello World";
            }

            connection.Close();
        }
    };

    return reader;
}