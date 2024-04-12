Imports MySql.Data.MySqlClient

Public Class Form4
    Dim connectionString As String = "server=localhost;userid=root;password=admin;database=prog"

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2GroupBox2.Visible = False
        LoadComboBoxData()
        Guna2DataGridView1.DataSource = Nothing
    End Sub

    Private Sub LoadComboBoxData()
        Dim query As String = "SELECT stuid, CONCAT(stuid, ' - ', stuname) AS student_info FROM student"

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim stuid As Integer = reader.GetInt32("stuid")
                            Dim studentInfo As String = reader.GetString("student_info")
                            Guna2ComboBox1.Items.Add(New KeyValuePair(Of Integer, String)(stuid, studentInfo))
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Failed to load data into ComboBox. Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        Dim selectedStudent As KeyValuePair(Of Integer, String) = CType(Guna2ComboBox1.SelectedItem, KeyValuePair(Of Integer, String))
        Dim stuid As Integer = selectedStudent.Key

        LoadReportData(stuid)
    End Sub

    Private Sub LoadReportData(stuid As Integer)
        Dim query As String = "SELECT report_id, total_marks, aggregate FROM report WHERE stuid = @stuid"

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@stuid", stuid)
                    Using adapter As New MySqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)
                        Guna2DataGridView1.DataSource = dataTable
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Failed to load report data. Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Guna2GroupBox2.Visible = True
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Application.Exit()
    End Sub
End Class
