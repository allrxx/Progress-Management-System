Public Class Form1
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        ' Declare the expected username and password
        Dim expectedUsername As String = "admin"
        Dim expectedPassword As String = "admin"

        ' Get the user entered username and password
        Dim enteredUsername As String = Guna2TextBox2.Text
        Dim enteredPassword As String = Guna2TextBox1.Text

        ' Check if the entered credentials match the expected credentials
        If enteredUsername = expectedUsername AndAlso enteredPassword = expectedPassword Then
            ' Login success
            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Login failed
            MessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class
