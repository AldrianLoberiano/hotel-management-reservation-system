Imports System.Data.SqlClient ' Importing SQLClient namespace for database operations
Imports Guna.UI2.WinForms ' Importing Guna UI namespace for UI controls

Public Class Login ' Declaration of the Login class

    ' Declaring a new SqlConnection object with the connection string to the database
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Aldrian Loberiano\Music\Pinaka LATEST sa lahat ng LATEST\HotelManagementReservationSystem\HotelVbDb.mdf;Integrated Security=True;Connect Timeout=30")


    ' Event handler for form load event
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setting border radius for UI controls to make them rounded
        BtnLog.BorderRadius = 15
        TxtPassword.BorderRadius = 15
        TxtUsername.BorderRadius = 15

        ' Initializing the text boxes with default text
        InitializeTextBox(TxtUsername, "Username")
        InitializeTextBox(TxtPassword, "Password")
    End Sub

    ' Event handler for login button click event
    Private Sub BtnLog_Click(sender As Object, e As EventArgs) Handles BtnLog.Click
        ' Check if username field is empty or default
        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse TxtUsername.Text = "Username" Then
            ' Show message box if username is missing
            MsgBox("Please enter the Staff Name.", MsgBoxStyle.Exclamation, "Missing Information")
            TxtUsername.Focus() ' Set focus to username textbox
            Return ' Exit the subroutine
        End If

        ' Check if password field is empty or default
        If String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse TxtPassword.Text = "Password" Then
            ' Show message box if password is missing
            MsgBox("Please enter the Password.", MsgBoxStyle.Exclamation, "Missing Information")
            TxtPassword.Focus() ' Set focus to password textbox
            Return ' Exit the subroutine
        End If

        ' Check for admin login
        If TxtUsername.Text.Trim() = "admin" AndAlso TxtPassword.Text.Trim() = "admin" Then
            ' If admin credentials are correct, show main form with all tabs visible
            Dim main As New Main
            main.SetTabVisibility(True)
            main.Show()
            Me.Hide() ' Hide the login form
            Return ' Exit the subroutine
        End If

        ' Try to log in as staff
        Try
            Con.Open() ' Open the database connection
            ' SQL query to check if staff exists with given username and password
            Dim Query As String = "SELECT COUNT(*) FROM StaffTbl WHERE StaffName = @StaffName AND StaffPass = @StaffPass"
            Using cmd As New SqlCommand(Query, Con)
                ' Adding parameters to the SQL query
                cmd.Parameters.AddWithValue("@StaffName", TxtUsername.Text.Trim())
                cmd.Parameters.AddWithValue("@StaffPass", TxtPassword.Text.Trim())

                ' Execute the query and get the result
                Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' If no matching record found
                If result = 0 Then
                    MsgBox("Incorrect Username Or Password", MsgBoxStyle.Critical, "Login Failed")
                Else
                    ' If matching record found, show main form with limited tabs visible
                    Dim main As New Main()
                    main.SetTabVisibility(False)
                    main.Show()
                    Me.Hide() ' Hide the login form
                End If
            End Using
        Catch ex As Exception
            ' Show message box if any error occurs during login
            MsgBox("An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Con.Close() ' Close the database connection
        End Try
    End Sub

    ' Method to initialize text boxes with default text and styles
    Private Sub InitializeTextBox(txtBox As Guna2TextBox, boxType As String)
        ' Set default text based on the type of textbox
        If boxType = "Username" Then
            txtBox.Text = "Username"
        ElseIf boxType = "Password" Then
            txtBox.Text = "Password"
            txtBox.PasswordChar = "" ' Clear password character mask for default text
        End If
        txtBox.ForeColor = Color.Silver ' Set text color to silver

        ' Add event handlers for textbox enter and leave events
        AddHandler txtBox.Enter, AddressOf TextBox_Enter
        AddHandler txtBox.Leave, AddressOf TextBox_Leave
    End Sub

    ' Event handler for when textbox gains focus
    Private Sub TextBox_Enter(sender As Object, e As EventArgs)
        Dim txtBox As Guna2TextBox = DirectCast(sender, Guna2TextBox) ' Cast sender to Guna2TextBox
        ' Clear default text and set text color to black
        If txtBox.Text = "Username" OrElse txtBox.Text = "Password" Then
            txtBox.Text = ""
            txtBox.ForeColor = Color.Black
        End If
        ' If password textbox, set password character mask if show password is unchecked
        If txtBox.Name = "TxtPassword" AndAlso Not CheckShowPass.Checked Then
            txtBox.PasswordChar = "•"c
        End If
    End Sub

    ' Event handler for when textbox loses focus
    Private Sub TextBox_Leave(sender As Object, e As EventArgs)
        Dim txtBox As Guna2TextBox = DirectCast(sender, Guna2TextBox) ' Cast sender to Guna2TextBox
        ' If textbox is empty, reset default text and styles
        If String.IsNullOrWhiteSpace(txtBox.Text) Then
            If txtBox.Name = "TxtUsername" Then
                txtBox.Text = "Username"
            ElseIf txtBox.Name = "TxtPassword" Then
                txtBox.Text = "Password"
                txtBox.PasswordChar = "" ' Clear password character mask
            End If
            txtBox.ForeColor = Color.Silver ' Set text color to silver
        End If
    End Sub

    ' Event handler for show password checkbox change event
    Private Sub CheckShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles CheckShowPass.CheckedChanged
        If CheckShowPass.Checked Then
            ' If checked, clear password character mask
            TxtPassword.PasswordChar = ""
        Else
            ' If unchecked, set password character mask if not default text
            If TxtPassword.Text <> "Password" Then
                TxtPassword.PasswordChar = "•"c
            End If
        End If
    End Sub

    ' Event handler for exit button click event
    Private Sub PicExit_Click(sender As Object, e As EventArgs) Handles PicExit.Click
        Application.Exit() ' Exit the application
    End Sub
End Class
