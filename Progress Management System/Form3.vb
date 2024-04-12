Imports MySql.Data.MySqlClient

Public Class Form3
    Dim connectionString As String = "server=localhost;userid=root;password=admin;database=prog"

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboBoxData()
    End Sub

    Private Sub LoadComboBoxData()
        Dim query As String = "SELECT stuid, stuname FROM student"

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim stuid As Integer = reader.GetInt32("stuid")
                            Dim stuname As String = reader.GetString("stuname")
                            Dim displayText As String = stuid.ToString() & " - " & stuname
                            Guna2ComboBox1.Items.Add(New KeyValuePair(Of Integer, String)(stuid, displayText))
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Failed to load data into ComboBox. Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Get the selected stuid from the ComboBox
        Dim selectedStudent As KeyValuePair(Of Integer, String) = CType(Guna2ComboBox1.SelectedItem, KeyValuePair(Of Integer, String))
        Dim stuid As Integer = selectedStudent.Key

        ' Get values from the TextBoxes for subjects and marks
        Dim sub1 As String = Guna2TextBox1.Text
        Dim sub2 As String = Guna2TextBox2.Text
        Dim sub3 As String = Guna2TextBox3.Text
        Dim sub4 As String = Guna2TextBox6.Text
        Dim sub5 As String = Guna2TextBox5.Text
        Dim mark1 As Integer = Convert.ToInt32(Guna2TextBox10.Text)
        Dim mark2 As Integer = Convert.ToInt32(Guna2TextBox9.Text)
        Dim mark3 As Integer = Convert.ToInt32(Guna2TextBox8.Text)
        Dim mark4 As Integer = Convert.ToInt32(Guna2TextBox7.Text)
        Dim mark5 As Integer = Convert.ToInt32(Guna2TextBox4.Text)

        ' Insert the data into the stusub table
        Dim query As String = "INSERT INTO stusub (sub1, sub2, sub3, sub4, sub5, mark1, mark2, mark3, mark4, mark5, stuid) " &
                              "VALUES (@sub1, @sub2, @sub3, @sub4, @sub5, @mark1, @mark2, @mark3, @mark4, @mark5, @stuid)"

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@sub1", sub1)
                    command.Parameters.AddWithValue("@sub2", sub2)
                    command.Parameters.AddWithValue("@sub3", sub3)
                    command.Parameters.AddWithValue("@sub4", sub4)
                    command.Parameters.AddWithValue("@sub5", sub5)
                    command.Parameters.AddWithValue("@mark1", mark1)
                    command.Parameters.AddWithValue("@mark2", mark2)
                    command.Parameters.AddWithValue("@mark3", mark3)
                    command.Parameters.AddWithValue("@mark4", mark4)
                    command.Parameters.AddWithValue("@mark5", mark5)
                    command.Parameters.AddWithValue("@stuid", stuid)

                    command.ExecuteNonQuery()
                End Using
                MessageBox.Show("Data inserted successfully.")
            Catch ex As Exception
                MessageBox.Show("Failed to insert data. Error: " & ex.Message)
            End Try
        End Using
    End Sub
End Class
