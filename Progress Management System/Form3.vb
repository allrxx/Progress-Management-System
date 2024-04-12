Imports MySql.Data.MySqlClient

Public Class Form3
    Dim connectionString As String = "server=localhost;userid=root;password=admin;database=prog"

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboBoxData()
        Guna2GroupBox2.Visible = False
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Guna2GroupBox2.Visible = True
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

        ' Calculate total marks
        Dim totalMarks As Integer = CalculateTotalMarks(mark1, mark2, mark3, mark4, mark5)

        ' Calculate aggregate
        Dim aggregate As Double = CalculateAggregate(totalMarks)

        ' Insert data into the report table
        InsertReportData(stuid, totalMarks, aggregate)
    End Sub

    Private Function CalculateTotalMarks(mark1 As Integer, mark2 As Integer, mark3 As Integer, mark4 As Integer, mark5 As Integer) As Integer
        ' Calculate total marks
        Return mark1 + mark2 + mark3 + mark4 + mark5
    End Function

    Private Function CalculateAggregate(totalMarks As Integer) As Double
        ' Assuming the aggregate calculation logic here
        ' Calculate percentage
        ' Assuming total marks out of 500
        Dim totalSubjects As Integer = 5 ' Assuming 5 subjects
        Dim totalMarksPossible As Integer = totalSubjects * 100 ' Assuming each subject has 100 marks
        Dim percentage As Double = (totalMarks / totalMarksPossible) * 100
        Return percentage
    End Function


    Private Sub InsertReportData(stuid As Integer, totalMarks As Integer, aggregate As Double)
        Dim query As String = "INSERT INTO report (total_marks, aggregate, stuid) VALUES (@totalMarks, @aggregate, @stuid)"

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@totalMarks", totalMarks)
                    command.Parameters.AddWithValue("@aggregate", aggregate)
                    command.Parameters.AddWithValue("@stuid", stuid)

                    command.ExecuteNonQuery()
                End Using
                MessageBox.Show("Data inserted into report table successfully.")
            Catch ex As Exception
                MessageBox.Show("Failed to insert data into report table. Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Application.Exit()
    End Sub
End Class
