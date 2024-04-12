Imports MySql.Data.MySqlClient
Public Class Form2
    Dim connectionString As String = "server=localhost;userid=root;password=admin;database=prog"

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = False
    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Guna2GroupBox2.Visible = True
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Guna2GroupBox3.Visible = True
    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        ' Save edited changes to the database when the save button is clicked
        DisplayStudentData()
    End Sub
    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Form3.Show()
        Me.Hide()
    End Sub
    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        Form4.Show()
        Me.Hide()
    End Sub
    Private Sub Guna2Button7_Click(sender As Object, e As EventArgs) Handles Guna2Button7.Click
        Application.Exit()
    End Sub
    Private Sub InsertDataIntoDatabase()
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "INSERT INTO student (stuname, stupno, stuage, studob, stuclass, stuemail) VALUES (@name, @phone, @age, @dob, @class, @email)"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@name", Guna2TextBox1.Text)
                    command.Parameters.AddWithValue("@phone", Guna2TextBox2.Text)
                    command.Parameters.AddWithValue("@age", Guna2TextBox3.Text)
                    command.Parameters.AddWithValue("@dob", Guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@class", Guna2TextBox6.Text)
                    command.Parameters.AddWithValue("@email", Guna2TextBox5.Text)

                    command.ExecuteNonQuery()
                End Using
                MessageBox.Show("Data inserted successfully.")
            Catch ex As Exception
                MessageBox.Show("Failed to insert data. Error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Validate Name (assuming no numbers or special characters)
        If Not System.Text.RegularExpressions.Regex.IsMatch(Guna2TextBox1.Text, "^[a-zA-Z\s]*$") Then
            MessageBox.Show("Invalid name. Only letters and spaces are allowed.")
            Return
        End If

        ' Validate Phone Number (must be exactly 10 digits)
        If Not System.Text.RegularExpressions.Regex.IsMatch(Guna2TextBox2.Text, "^\d{10}$") Then
            MessageBox.Show("Invalid phone number. Exactly 10 digits are required.")
            Return
        End If

        ' Validate Age (must be digits only)
        If Not System.Text.RegularExpressions.Regex.IsMatch(Guna2TextBox3.Text, "^\d+$") Then
            MessageBox.Show("Invalid age. Only digits are allowed.")
            Return
        End If

        ' Validate Email
        If Not System.Text.RegularExpressions.Regex.IsMatch(Guna2TextBox5.Text, "^\S+@\S+\.\S+$") Then
            MessageBox.Show("Invalid email format.")
            Return
        End If

        ' All validations passed, proceed to insert data into the database
        InsertDataIntoDatabase()
    End Sub
    Private Sub DisplayStudentData()
        Dim query As String = "SELECT stuid, stuname, stupno, stuage, studob, stuclass, stuemail FROM student"

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)
                        Guna2DataGridView1.DataSource = dataTable
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Failed to retrieve data from database. Error: " & ex.Message)
            End Try
        End Using
    End Sub
End Class