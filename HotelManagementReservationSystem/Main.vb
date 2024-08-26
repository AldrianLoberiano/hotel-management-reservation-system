Imports System.Data.SqlClient 'database
Imports System.Windows.Forms.DataVisualization.Charting 'for pie graph
Imports Guna.UI.WinForms 'framework
Imports Guna.UI2.WinForms 'framework

Public Class Main
    ' Flag to track if updates are currently happening
    Private IsUpdating As Boolean = False

    ' Flag to track if the form is initializing
    Private IsInitializing As Boolean = False

    ' Variable for key management, initialized to 0
    Dim key As Integer = 0

    ' Connection string for the database
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Aldrian Loberiano\Music\Pinaka LATEST sa lahat ng LATEST\HotelManagementReservationSystem\HotelVbDb.mdf;Integrated Security=True;Connect Timeout=30"


    ' Load event handler for the main form
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Temporarily disable event handlers
        RemoveHandler RoomNoComBox.SelectedIndexChanged, AddressOf RoomNoComBox_SelectedIndexChanged
        RemoveHandler ClientRoomType.SelectedIndexChanged, AddressOf ClientRoomType_SelectedIndexChanged

        ' Apply cell styling to DataGridViews
        CellStyle(DataGridView_Client)
        CellStyle(DataGridView_Rooms)
        CellStyle(DataGridView_Staff)
        CellStyle(DataGridView_Bookings)

        ' Set initial values for date pickers
        CheckInPicker.Value = DateTime.Now
        CheckOutPicker.Value = DateTime.Now

        ' Set the initializing flag to True after the form is loaded
        IsInitializing = True

        ' Display room availability charts for different room types
        GenerateRoomChart("Single", ChartSingle, "Single Rooms")
        GenerateRoomChart("Double", ChartDouble, "Double Rooms")
        GenerateRoomChart("Family", ChartFamily, "Family Rooms")

        ' Set the size of tab buttons
        SetTabSize(180, 60)

        ' Populate RoomNoComBox with room numbers
        PopulateRoomNoComBox()

        ' Initialize search text boxes
        InitializeSearchTextBox(SearchTxtBox_Client)
        InitializeSearchTextBox(SearchTxtBox_Rooms)
        InitializeSearchTextBox(SearchTxtBox_Bookings)
        InitializeSearchTextBox(SearchTxtBox_Staff)

        ' Populate DataGridViews with data from database queries
        PopulateDataGridView(DataGridView_Staff, "SELECT * FROM StaffTbl")
        PopulateDataGridView(DataGridView_Client, "SELECT * FROM ClientTbl")
        PopulateDataGridView(DataGridView_Bookings, "SELECT * FROM ClientTbl") ' Note: This might need correction to a different table
        PopulateDataGridView(DataGridView_Rooms, "SELECT * FROM RoomTbl")

        ' Re-enable event handlers
        AddHandler RoomNoComBox.SelectedIndexChanged, AddressOf RoomNoComBox_SelectedIndexChanged
        AddHandler ClientRoomType.SelectedIndexChanged, AddressOf ClientRoomType_SelectedIndexChanged
    End Sub

    ' Method to populate RoomNoComBox with optional selected room ID parameter
    Private Sub PopulateRoomNoComBox(Optional selectedRoomId As Integer = -1)
        Try
            ' Open connection to the database
            Using Con As New SqlConnection(connectionString)
                Con.Open()

                ' Modify the query to only select rooms that are not booked or include the currently selected room
                Dim query As String = "SELECT RId, RoomNum FROM RoomTbl WHERE RoomStatus <> 'Booked' OR RId = @SelectedRoomId"

                ' Execute query with parameters
                Using cmd As New SqlCommand(query, Con)
                    ' Add the parameter for the currently selected room ID, if any
                    cmd.Parameters.AddWithValue("@SelectedRoomId", If(selectedRoomId = -1, DBNull.Value, selectedRoomId))

                    ' Execute query and read results into DataTable
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Dim roomTable As New DataTable()
                        roomTable.Load(reader)

                        ' Set data source, display member, and value member for RoomNoComBox
                        RoomNoComBox.DataSource = roomTable
                        RoomNoComBox.DisplayMember = "RoomNum"
                        RoomNoComBox.ValueMember = "RId"

                        ' Set selected value or clear selection based on selectedRoomId parameter
                        If selectedRoomId <> -1 Then
                            RoomNoComBox.SelectedValue = selectedRoomId
                        Else
                            RoomNoComBox.SelectedIndex = -1
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Display error message if exception occurs
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    ' Method to populate RoomNoComBox with rooms of a specific room type
    Private Sub PopulateRoomNoComBox(roomType As String)
        Try
            ' Open connection to the database
            Using Con As New SqlConnection(connectionString)
                Con.Open()

                ' Modify the query to select rooms based on the selected room type
                Dim query As String = "SELECT RId, RoomNum FROM RoomTbl WHERE RoomType = @RoomType AND RoomStatus <> 'Booked'"

                ' Execute query with parameters
                Using cmd As New SqlCommand(query, Con)
                    cmd.Parameters.AddWithValue("@RoomType", roomType)

                    ' Execute query and read results into DataTable
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Dim roomTable As New DataTable()
                        roomTable.Load(reader)

                        ' Set data source, display member, and value member for RoomNoComBox
                        RoomNoComBox.DataSource = roomTable
                        RoomNoComBox.DisplayMember = "RoomNum"
                        RoomNoComBox.ValueMember = "RId"

                        ' Clear selection in RoomNoComBox
                        RoomNoComBox.SelectedIndex = -1
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Display error message if exception occurs
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    ' Event handler when ClientRoomType selection changes
    Private Sub ClientRoomType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ClientRoomType.SelectedIndexChanged
        ' Check if form is initializing
        If IsInitializing Then
            ' Exit if no room type is selected
            If ClientRoomType.SelectedIndex = -1 Then
                Exit Sub
            End If

            ' Get selected room type as string
            Dim selectedRoomType As String = ClientRoomType.SelectedItem.ToString()

            ' Clear the room rate TextBox
            TxtRoomRate.Text = ""

            ' Populate RoomNoComBox based on selected RoomType
            PopulateRoomNoComBox(selectedRoomType)
        End If
    End Sub

    ' Method to fetch and display room rate based on room ID
    Private Sub FetchRoomRate(roomId As Integer)
        Try
            ' Open connection to the database
            Using Con As New SqlConnection(connectionString)
                Con.Open()

                ' Query to fetch room price based on the selected room ID
                Dim query As String = "SELECT RoomPrice FROM RoomTbl WHERE RId = @RoomId"

                ' Execute query with parameters
                Using cmd As New SqlCommand(query, Con)
                    cmd.Parameters.AddWithValue("@RoomId", roomId)

                    ' Execute scalar query to get room price
                    Dim roomPrice As Object = cmd.ExecuteScalar()

                    ' Display room price in TxtRoomRate TextBox if found, otherwise clear TextBox
                    If roomPrice IsNot Nothing Then
                        TxtRoomRate.Text = roomPrice.ToString()
                    Else
                        TxtRoomRate.Text = ""
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Display error message if exception occurs
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    ' Event handler when RoomNoComBox selection changes
    Private Sub RoomNoComBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RoomNoComBox.SelectedIndexChanged
        ' Check if form is initializing
        If IsInitializing Then
            ' Check if RoomNoComBox selection is a DataRowView
            If RoomNoComBox.SelectedItem IsNot Nothing AndAlso TypeOf RoomNoComBox.SelectedItem Is DataRowView Then
                ' Get selected room ID as integer
                Dim selectedRoomId As Integer = Convert.ToInt32(DirectCast(RoomNoComBox.SelectedItem, DataRowView)("RId"))

                ' Fetch and display the room price based on the selected room ID
                FetchRoomRate(selectedRoomId)
            End If
        End If
    End Sub

    ' Method to apply cell styling to DataGridView
    Private Sub CellStyle(dataGridView As DataGridView)
        ' Set default font for rows and alternating rows in DataGridView
        dataGridView.RowsDefaultCellStyle.Font = New Font("Verdana", 13, FontStyle.Regular)
        dataGridView.AlternatingRowsDefaultCellStyle.Font = New Font("Verdana", 13, FontStyle.Regular)
    End Sub

    ' Method to populate ComboBox with room types
    Private Sub PopulateRoomTypeComBox(comboBox As ComboBox, Optional roomTypeValue As String = Nothing)
        ' Clear items in ComboBox
        comboBox.Items.Clear()

        ' Add room types to ComboBox
        comboBox.Items.Add("Single")
        comboBox.Items.Add("Double")
        comboBox.Items.Add("Family")

        ' Set selected item in ComboBox if roomTypeValue is provided
        If roomTypeValue IsNot Nothing Then
            comboBox.SelectedItem = roomTypeValue
        End If
    End Sub

    Private Sub SetTabSize(width As Integer, height As Integer)
        ' Method to set the size of the tab buttons in TabControl
        TabControl.TabButtonSize = New Size(width, height)
    End Sub

    Private Sub PictureBox_Click(sender As Object, e As EventArgs) Handles PicClient.Click, PicRoom.Click, PicRes.Click, PicStaff.Click
        ' Event handler when any of the PictureBoxes is clicked
        Dim pictureBox As GunaPictureBox = DirectCast(sender, GunaPictureBox) ' Get the PictureBox that was clicked
        Dim tabIndex As Integer = GetTabIndexFromPictureBox(pictureBox) ' Get the tab index associated with the PictureBox
        If tabIndex >= 0 Then
            TabControl.SelectedIndex = tabIndex ' Select the corresponding tab if the index is valid
        End If
    End Sub

    Private Function GetTabIndexFromPictureBox(pictureBox As GunaPictureBox) As Integer
        ' Function to return the tab index based on the PictureBox name
        Select Case pictureBox.Name
            Case "PicClient"
                Return 0
            Case "PicRoom"
                Return 1
            Case "PicRes"
                Return 2
            Case "PicStaff"
                Return 3
            Case Else
                Return -1 ' Return -1 if PictureBox name does not match expected cases
        End Select
    End Function

    Private Sub InitializeSearchTextBox(txtBox As Guna2TextBox)
        ' Method to initialize search text boxes
        txtBox.Text = "Search" ' Set default text
        txtBox.ForeColor = Color.Silver ' Set default text color

        ' Handle Enter event
        AddHandler txtBox.Enter, Sub(sender As Object, e As EventArgs)
                                     If txtBox.Text = "Search" Then
                                         txtBox.Text = ""
                                         txtBox.ForeColor = Color.Black
                                     End If
                                 End Sub

        ' Handle Leave event
        AddHandler txtBox.Leave, Sub(sender As Object, e As EventArgs)
                                     If String.IsNullOrWhiteSpace(txtBox.Text) Then
                                         txtBox.Text = "Search"
                                         txtBox.ForeColor = Color.Silver
                                     End If
                                 End Sub
    End Sub

    Private Sub PopulateDataGridView(dgv As DataGridView, query As String)
        ' Method to populate DataGridView with data from database based on query
        Try
            Using Con As New SqlConnection(connectionString)
                Con.Open()
                Using sda As New SqlDataAdapter(query, Con)
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    dgv.DataSource = dt ' Set DataGridView's DataSource to the retrieved DataTable
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error: " & ex.Message) ' Display error message if exception occurs
        End Try
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''BUTTONS AND FUNCTIONS FOR CLIENT PAGE'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub BtnAdd_Client_Click(sender As Object, e As EventArgs) Handles BtnAdd_Client.Click
        ' Button click event to add a new client record
        ' Check if any required information is missing
        If String.IsNullOrWhiteSpace(ClientNameTxtBox.Text) Or
         String.IsNullOrWhiteSpace(PhoneTxtBox.Text) Or
         String.IsNullOrWhiteSpace(RoomNoComBox.Text) Or
         String.IsNullOrWhiteSpace(ClientRoomType.Text) Or
         String.IsNullOrWhiteSpace(TxtRoomRate.Text) Or
         String.IsNullOrWhiteSpace(CmbNumOfGuest.Text) Or
         String.IsNullOrWhiteSpace(TxtTotal.Text) Then
            MsgBox("Missing Information")
            ' Check if the phone number is exactly 11 digits and numeric
        ElseIf PhoneTxtBox.Text.Length <> 11 OrElse Not IsNumeric(PhoneTxtBox.Text) Then
            MsgBox("Phone number must be 11 digits and contain only numbers")

        Else
            Try
                Using Con As New SqlConnection(connectionString)
                    Con.Open()
                    ' SQL query to insert new client record into ClientTbl
                    Dim query As String = "INSERT INTO ClientTbl (CLName, CLPhone, RoomType, RoomID, RoomRate, NumOfGuest, CheckIn, CheckOut, TotalDays, Total) VALUES(@ClientName, @ClientPhone, @RoomType, @RoomID, @RoomRate, @NumOfGuest, @CheckIn, @CheckOut, @TotalDays, @Total)"
                    Using cmd As New SqlCommand(query, Con)
                        ' Set parameter values for the SQL query
                        cmd.Parameters.AddWithValue("@ClientName", ClientNameTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@ClientPhone", PhoneTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@RoomType", ClientRoomType.SelectedItem.ToString())
                        cmd.Parameters.AddWithValue("@RoomID", RoomNoComBox.SelectedValue) ' Use RoomNoComBox.SelectedValue (RId)
                        cmd.Parameters.AddWithValue("@RoomRate", Convert.ToDecimal(TxtRoomRate.Text))
                        cmd.Parameters.AddWithValue("@NumOfGuest", Convert.ToInt32(CmbNumOfGuest.Text))
                        cmd.Parameters.AddWithValue("@CheckIn", CheckInPicker.Value)
                        cmd.Parameters.AddWithValue("@CheckOut", CheckOutPicker.Value)
                        cmd.Parameters.AddWithValue("@TotalDays", Convert.ToInt32(TxtTotalDays.Text))
                        cmd.Parameters.AddWithValue("@Total", Decimal.TryParse(TxtTotal.Text, Globalization.NumberStyles.Currency))
                        cmd.ExecuteNonQuery()
                        MsgBox("Room Booked Successfully")
                    End Using
                    ' Update room status after booking
                    UpdateRoom(RoomNoComBox.SelectedValue.ToString())
                End Using
                PopulateDataGridView(DataGridView_Client, "SELECT * FROM ClientTbl")
                PopulateDataGridView(DataGridView_Rooms, "SELECT * FROM RoomTbl")
                PopulateDataGridView(DataGridView_Bookings, "SELECT * FROM ClientTbl")
                ClearClientTab()
                PopulateRoomNoComBox() ' Refresh the room list
                'Refresh the Pie Graph
                GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                GenerateRoomChart("Family", ChartFamily, "Family Rooms")
                RoomNoComBox.Enabled = True
                ClientRoomType.Enabled = True
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnUpdate_Client_Click(sender As Object, e As EventArgs) Handles BtnUpdate_Client.Click
        ' Button click event to update an existing client record
        If String.IsNullOrWhiteSpace(ClientNameTxtBox.Text) Or
         String.IsNullOrWhiteSpace(PhoneTxtBox.Text) Or
         String.IsNullOrWhiteSpace(RoomNoComBox.Text) Or
         String.IsNullOrWhiteSpace(ClientRoomType.Text) Or
         String.IsNullOrWhiteSpace(TxtRoomRate.Text) Or
         String.IsNullOrWhiteSpace(CmbNumOfGuest.Text) Or
         String.IsNullOrWhiteSpace(TxtTotal.Text) Then
            MsgBox("Missing Information")
            ' Check if the phone number is exactly 11 digits and numeric
        ElseIf PhoneTxtBox.Text.Length <> 11 OrElse Not IsNumeric(PhoneTxtBox.Text) Then
            MsgBox("Phone number must be 11 digits and contain only numbers")

        Else
            IsUpdating = True
            Try
                Using Con As New SqlConnection(connectionString)
                    Con.Open()
                    ' SQL query to update client record in ClientTbl based on ClientId
                    Dim query As String = "UPDATE ClientTbl SET CLName = @ClientName, CLPhone = @ClientPhone, RoomType = @RoomType, RoomID = @RoomID, RoomRate = @RoomRate, NumOfGuest = @NumOfGuest, CheckIn = @CheckIn, CheckOut = @CheckOut, Total = @Total WHERE Id = @ClientId"
                    Using cmd As New SqlCommand(query, Con)
                        ' Set parameter values for the SQL query
                        cmd.Parameters.AddWithValue("@ClientName", ClientNameTxtBox.Text.Trim())
                        cmd.Parameters.AddWithValue("@ClientPhone", PhoneTxtBox.Text.Trim())
                        cmd.Parameters.AddWithValue("@RoomType", ClientRoomType.SelectedItem.ToString())
                        cmd.Parameters.AddWithValue("@RoomID", RoomNoComBox.SelectedValue) ' Use RoomNoComBox.SelectedValue (RId)
                        cmd.Parameters.AddWithValue("@RoomRate", Convert.ToDecimal(TxtRoomRate.Text))
                        cmd.Parameters.AddWithValue("@NumOfGuest", Convert.ToInt32(CmbNumOfGuest.Text))
                        cmd.Parameters.AddWithValue("@CheckIn", CheckInPicker.Value)
                        cmd.Parameters.AddWithValue("@CheckOut", CheckOutPicker.Value)
                        cmd.Parameters.AddWithValue("@Total", Convert.ToInt32(TxtTotal.Text))
                        cmd.Parameters.AddWithValue("@TotalDays", Convert.ToInt32(TxtTotalDays.Text))
                        cmd.Parameters.AddWithValue("@ClientId", key)
                        cmd.ExecuteNonQuery()
                        MsgBox("Client Record Updated Successfully")
                    End Using
                End Using
                ClearClientTab()
                'Refresh the DataGridViews
                PopulateDataGridView(DataGridView_Client, "SELECT * FROM ClientTbl")
                PopulateDataGridView(DataGridView_Rooms, "SELECT * FROM RoomTbl")
                PopulateDataGridView(DataGridView_Bookings, "SELECT * FROM ClientTbl")
                PopulateRoomNoComBox()

                'Refresh the Pie Graph
                GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                GenerateRoomChart("Family", ChartFamily, "Family Rooms")
                key = 0
                RoomNoComBox.Enabled = True
                ClientRoomType.Enabled = True
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            Finally
                IsUpdating = False
            End Try
        End If
    End Sub

    Private Sub BtnDelete_Client_Click(sender As Object, e As EventArgs) Handles BtnDelete_Client.Click
        ' Button click event to delete a client record
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to delete the selected client?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirmResult = DialogResult.Yes Then
                Dim Con As SqlConnection = Nothing
                Dim transaction As SqlTransaction = Nothing
                Try
                    Con = New SqlConnection(connectionString)
                    Con.Open()
                    transaction = Con.BeginTransaction()

                    ' Retrieve the RoomID directly from the ClientTbl based on the client's ID
                    Dim roomId As String = Nothing
                    Dim query As String = "SELECT RoomID FROM ClientTbl WHERE Id = @ClientId"
                    Using cmd As New SqlCommand(query, Con, transaction)
                        cmd.Parameters.AddWithValue("@ClientId", key)
                        Dim queryResult = cmd.ExecuteScalar()
                        If queryResult IsNot Nothing Then
                            roomId = queryResult.ToString()
                        End If
                    End Using

                    If roomId Is Nothing Then
                        Throw New Exception("Room number not found for the given client.")
                    End If

                    ' Delete the client record
                    query = "DELETE FROM ClientTbl WHERE Id = @ClientId"
                    Using cmd As New SqlCommand(query, Con, transaction)
                        cmd.Parameters.AddWithValue("@ClientId", key)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Update the room status to "Available"
                    UpdateRoom(roomId, "Available")

                    transaction.Commit()
                    MsgBox("Client Record Deleted Successfully")

                Catch ex As Exception
                    MsgBox("Error: " & ex.Message)
                    If Not transaction Is Nothing Then
                        Try
                            transaction.Rollback()
                        Catch ex2 As Exception
                            MsgBox("Rollback Exception Type: {0}. Message: {1}", ex2.GetType().ToString(), ex2.Message)
                        End Try
                    End If
                Finally
                    If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                        Con.Close()
                    End If
                End Try
                'Refresh the DataGridViews
                PopulateDataGridView(DataGridView_Client, "SELECT * FROM ClientTbl")
                PopulateDataGridView(DataGridView_Rooms, "SELECT * FROM RoomTbl")
                PopulateDataGridView(DataGridView_Bookings, "SELECT * FROM ClientTbl")

                'Refresh the Pie Graph
                GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                GenerateRoomChart("Family", ChartFamily, "Family Rooms")

                PopulateRoomNoComBox() ' Refresh the room list
                ClearClientTab()
                RoomNoComBox.Enabled = True
                ClientRoomType.Enabled = True
            End If
        End If
    End Sub

    Private Sub UpdateRoom(roomId As String, Optional status As String = "Booked")
        ' Method to update RoomTbl with given roomId and optional status
        Try
            Using Con As New SqlConnection(connectionString)
                Con.Open()
                ' SQL query to update RoomTbl with RoomStatus based on RId
                Dim query As String = "UPDATE RoomTbl SET RoomStatus = @RoomStatus WHERE RId = @RoomId"
                Using cmd As New SqlCommand(query, Con)
                    cmd.Parameters.AddWithValue("@RoomStatus", status)
                    cmd.Parameters.AddWithValue("@RoomId", roomId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear_Client.Click
        ' Button click event to clear input fields in Client tab
        ClearClientTab()
    End Sub

    Private Sub ClearClientTab()
        ' Method to clear input fields in Client tab
        ClientNameTxtBox.Clear()
        PhoneTxtBox.Clear()
        ClientRoomType.SelectedIndex = -1
        RoomNoComBox.SelectedIndex = -1
        TxtTotalDays.Text = String.Empty
        TxtRoomRate.Clear()
        CmbNumOfGuest.SelectedIndex = -1
        CheckInPicker.Value = DateTime.Now
        CheckOutPicker.Value = DateTime.Now
        TxtTotal.Clear()
        RoomNoComBox.Enabled = True
        ClientRoomType.Enabled = True
        ' Reset the search text box only if it does not contain the default text "Search"
        If SearchTxtBox_Client.Text <> "Search" Then
            SearchTxtBox_Client.Text = "Search"
            SearchTxtBox_Client.ForeColor = Color.Silver

        End If
    End Sub

    Private Sub DataGridView_Client_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView_Client.CellMouseClick
        IsUpdating = True
        ' DataGridView cell click event to populate fields in Client tab based on selected row
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView_Client.Rows(e.RowIndex)

            ' Set ClientNameTxtBox and PhoneTxtBox
            ClientNameTxtBox.Text = row.Cells(1).Value.ToString().Trim()
            PhoneTxtBox.Text = row.Cells(2).Value.ToString().Trim()

            Dim roomTypeValue As String = row.Cells(3).Value.ToString().Trim()
            PopulateRoomTypeComBox(ClientRoomType, roomTypeValue)

            ' Assuming RoomNum is stored in the third column (index 2)
            Dim selectedRoomId As Integer = Convert.ToInt32(row.Cells(4).Value)
            PopulateRoomNoComBox(selectedRoomId)

            TxtRoomRate.Text = row.Cells(5).Value.ToString().Trim()
            CmbNumOfGuest.SelectedItem = row.Cells(6).Value.ToString().Trim()

            ' Set CheckInPicker and CheckOutPicker
            CheckInPicker.Value = Convert.ToDateTime(row.Cells(7).Value)
            CheckOutPicker.Value = Convert.ToDateTime(row.Cells(8).Value)
            TxtTotal.Text = row.Cells(10).Value.ToString().Trim()

            ' Set key
            key = Convert.ToInt32(row.Cells(0).Value)
            IsUpdating = False ' Set IsUpdating to False after populating fields
            RoomNoComBox.Enabled = False ' Disable RoomNoComBox
            ClientRoomType.Enabled = False ' Disable ClientRoomType
        End If
    End Sub


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''BUTTONS AND FUNCTIONS FOR ROOMS PAGE'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub BtnAdd_Rooms_Click(sender As Object, e As EventArgs) Handles BtnAdd_Rooms.Click
        ' Button click event to add a new room record
        ' Check if any required information is missing
        If String.IsNullOrWhiteSpace(RoomNumTxtBox.Text) Or
       String.IsNullOrWhiteSpace(CmbRoomType.Text) Or
       String.IsNullOrWhiteSpace(RoomPriceTxtBox.Text) Or
       String.IsNullOrWhiteSpace(TxtTelephone.Text) Or
       String.IsNullOrWhiteSpace(RoomStatus.Text) Then
            MsgBox("Missing Information")
        ElseIf Not IsNumeric(TxtTelephone.Text) Then
            MsgBox("Telephone number must contain only numbers")

        Else
            Try
                Using Con As New SqlConnection(connectionString)
                    Con.Open()
                    ' Check if the room number already exists
                    Dim checkQuery As String = "SELECT COUNT(*) FROM RoomTbl WHERE RoomNum = @RoomNum"
                    Using checkCmd As New SqlCommand(checkQuery, Con)
                        checkCmd.Parameters.AddWithValue("@RoomNum", RoomNumTxtBox.Text.Trim)
                        Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                        If exists > 0 Then
                            MsgBox("Room number already exists. Please enter a different room number.", MsgBoxStyle.Critical)
                            Return
                        End If
                    End Using

                    ' If the room number does not exist, insert the new room
                    Dim query As String = "INSERT INTO RoomTbl (RoomNum, RoomType, RoomPrice, Telephone, RoomStatus) VALUES (@RoomNum, @RoomType, @RoomPrice, @Telephone, @RoomStatus)"
                    Using cmd As New SqlCommand(query, Con)
                        ' Set parameter values
                        cmd.Parameters.AddWithValue("@RoomNum", RoomNumTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@RoomType", CmbRoomType.SelectedItem.ToString())
                        cmd.Parameters.AddWithValue("@RoomPrice", Convert.ToDecimal(RoomPriceTxtBox.Text.Trim))
                        cmd.Parameters.AddWithValue("@Telephone", TxtTelephone.Text.Trim())
                        cmd.Parameters.AddWithValue("@RoomStatus", RoomStatus.SelectedItem.ToString())
                        cmd.ExecuteNonQuery()
                        MsgBox("Room Record Added Successfully")
                    End Using
                End Using
                ClearRoomTab()
                PopulateDataGridView(DataGridView_Rooms, "SELECT * FROM RoomTbl")
                PopulateRoomNoComBox()
                'Refresh the Pie Graph
                GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                GenerateRoomChart("Family", ChartFamily, "Family Rooms")
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnUpdate_Rooms_Click(sender As Object, e As EventArgs) Handles BtnUpdate_Rooms.Click
        ' Button click event to update an existing room record
        ' Check if any required information is missing
        If String.IsNullOrWhiteSpace(RoomNumTxtBox.Text) Or
       String.IsNullOrWhiteSpace(CmbRoomType.Text) Or
       String.IsNullOrWhiteSpace(RoomPriceTxtBox.Text) Or
       String.IsNullOrWhiteSpace(TxtTelephone.Text) Or
       String.IsNullOrWhiteSpace(RoomStatus.Text) Then
            MsgBox("Missing Information")
        ElseIf Not IsNumeric(TxtTelephone.Text) Then
            MsgBox("Telephone number must contain only numbers")

        Else
            Try
                Using Con As New SqlConnection(connectionString)
                    Con.Open()
                    Dim query As String = "UPDATE RoomTbl SET RoomNum = @RoomNum, RoomType = @RoomType, RoomPrice = @RoomPrice, Telephone = @Telephone, RoomStatus = @RoomStatus WHERE RId = @RoomId"
                    Using cmd As New SqlCommand(query, Con)
                        ' Set parameter values
                        cmd.Parameters.AddWithValue("@RoomNum", RoomNumTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@RoomType", CmbRoomType.SelectedItem.ToString())
                        cmd.Parameters.AddWithValue("@RoomPrice", Convert.ToDecimal(RoomPriceTxtBox.Text.Trim))
                        cmd.Parameters.AddWithValue("@Telephone", TxtTelephone.Text.Trim())
                        cmd.Parameters.AddWithValue("@RoomStatus", RoomStatus.SelectedItem.ToString())
                        cmd.Parameters.AddWithValue("@RoomId", key)
                        cmd.ExecuteNonQuery()
                        MsgBox("Room Record Updated Successfully")
                        PopulateRoomNoComBox()
                    End Using
                End Using
                ClearRoomTab()
                PopulateDataGridView(DataGridView_Rooms, "SELECT * FROM RoomTbl")
                'Refresh the Pie Graph
                GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                GenerateRoomChart("Family", ChartFamily, "Family Rooms")
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnDelete_Rooms_Click(sender As Object, e As EventArgs) Handles BtnDelete_Rooms.Click
        ' Button click event to delete a room record
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            ' Ask for confirmation before deleting
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the selected room?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Try
                    Using Con As New SqlConnection(connectionString)
                        Con.Open()
                        Dim query As String = "DELETE FROM RoomTbl WHERE RId = @RoomId"
                        Using cmd As New SqlCommand(query, Con)
                            ' Set parameter value
                            cmd.Parameters.AddWithValue("@RoomId", key)
                            cmd.ExecuteNonQuery()
                            MsgBox("Room Record Deleted Successfully")
                        End Using
                    End Using
                    PopulateDataGridView(DataGridView_Rooms, "SELECT * FROM RoomTbl")
                    ClearRoomTab()
                    'Refresh the Pie Graph
                    GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                    GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                    GenerateRoomChart("Family", ChartFamily, "Family Rooms")
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub ClearRoomTab()
        ' Method to clear input fields in Room tab
        RoomNumTxtBox.Clear()
        RoomPriceTxtBox.Clear()
        RoomStatus.SelectedIndex = -1
        TxtTelephone.Clear()
        CmbRoomType.SelectedIndex = -1
        If SearchTxtBox_Client.Text <> "Search" Then
            SearchTxtBox_Client.Text = "Search"
            SearchTxtBox_Client.ForeColor = Color.Silver
        End If
    End Sub

    Private Sub BtnClear_Rooms_Click(sender As Object, e As EventArgs) Handles BtnClear_Rooms.Click
        ' Button click event to clear input fields in Room tab
        ClearRoomTab()
    End Sub

    Private Sub DataGridView_Rooms_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView_Rooms.CellMouseClick
        ' DataGridView cell click event to populate fields in Room tab based on selected row
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView_Rooms.Rows(e.RowIndex)
            RoomNumTxtBox.Text = row.Cells(1).Value.ToString.Trim
            Dim roomTypeValue As String = row.Cells(2).Value.ToString().Trim
            PopulateRoomTypeComBox(CmbRoomType, roomTypeValue)
            RoomPriceTxtBox.Text = row.Cells(3).Value.ToString.Trim
            TxtTelephone.Text = row.Cells(4).Value.ToString.Trim
            RoomStatus.SelectedItem = row.Cells(5).Value.ToString.Trim

            key = Convert.ToInt32(row.Cells(0).Value.ToString) ' Assuming RId is stored in the first column (index 0)
        End If
    End Sub


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''BUTTONS AND FUNCTIONS FOR STAFF PAGE'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub BtnAdd_Staff_Click(sender As Object, e As EventArgs) Handles BtnAdd_Staff.Click
        ' Button click event to add a new staff record
        ' Check if any required information is missing
        If String.IsNullOrWhiteSpace(StaffNameTxtBox.Text) Or
       String.IsNullOrWhiteSpace(StaffAgeTxtBox.Text) Or
       String.IsNullOrWhiteSpace(StaffPhoneTxtBox.Text) Or
       String.IsNullOrWhiteSpace(StaffSexComBox.Text) Or
       String.IsNullOrWhiteSpace(StaffPassTxtBox.Text) Then
            MsgBox("Missing Information")
        ElseIf StaffPhoneTxtBox.Text.Length <> 11 OrElse Not IsNumeric(StaffPhoneTxtBox.Text) Then
            MsgBox("Phone number must be 11 digits and contain only numbers")

        Else
            Try
                Using Con As New SqlConnection(connectionString)
                    Con.Open()
                    Dim query As String = "INSERT INTO StaffTbl (StaffName, StaffAge, StaffPhone, StaffSex, StaffPass) VALUES (@StaffName, @StaffAge, @StaffPhone, @StaffSex, @StaffPass)"
                    Using cmd As New SqlCommand(query, Con)
                        ' Set parameter values
                        cmd.Parameters.AddWithValue("@StaffName", StaffNameTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@StaffAge", StaffAgeTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@StaffPhone", StaffPhoneTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@StaffSex", StaffSexComBox.SelectedItem.ToString().Trim)
                        cmd.Parameters.AddWithValue("@StaffPass", StaffPassTxtBox.Text.Trim)
                        cmd.ExecuteNonQuery()
                        MsgBox("Staff Record Created Successfully")
                    End Using
                End Using
                PopulateDataGridView(DataGridView_Staff, "SELECT * FROM StaffTbl") 'Refresh the DataGridView
                ClearStaffTab()
                'Refresh the Pie Graph
                GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                GenerateRoomChart("Family", ChartFamily, "Family Rooms")
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnUpdate_Staff_Click(sender As Object, e As EventArgs) Handles BtnUpdate_Staff.Click
        ' Button click event to update an existing staff record
        ' Check if any required information is missing
        If String.IsNullOrWhiteSpace(StaffNameTxtBox.Text) Or
       String.IsNullOrWhiteSpace(StaffAgeTxtBox.Text) Or
       String.IsNullOrWhiteSpace(StaffPhoneTxtBox.Text) Or
       String.IsNullOrWhiteSpace(StaffSexComBox.Text) Or
       String.IsNullOrWhiteSpace(StaffPassTxtBox.Text) Then
            MsgBox("Missing Information")
        ElseIf StaffPhoneTxtBox.Text.Length <> 11 OrElse Not IsNumeric(StaffPhoneTxtBox.Text) Then
            MsgBox("Phone number must be 11 digits and contain only numbers")

        Else
            Try
                Using Con As New SqlConnection(connectionString)
                    Con.Open()
                    Dim query As String = "UPDATE StaffTbl SET StaffName = @StaffName, StaffAge = @StaffAge, StaffPhone = @StaffPhone, StaffSex = @StaffSex, StaffPass = @StaffPass WHERE StaffId = @StaffId"
                    Using cmd As New SqlCommand(query, Con)
                        ' Set parameter values
                        cmd.Parameters.AddWithValue("@StaffName", StaffNameTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@StaffAge", StaffAgeTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@StaffPhone", StaffPhoneTxtBox.Text.Trim)
                        Dim staffSex As String = If(StaffSexComBox.SelectedItem IsNot Nothing, StaffSexComBox.SelectedItem.ToString(), "")
                        cmd.Parameters.AddWithValue("@StaffSex", staffSex)
                        cmd.Parameters.AddWithValue("@StaffPass", StaffPassTxtBox.Text.Trim)
                        cmd.Parameters.AddWithValue("@StaffId", key)
                        cmd.ExecuteNonQuery()
                        MsgBox("Staff Record Updated Successfully")
                    End Using
                End Using
                ClearStaffTab()
                PopulateDataGridView(DataGridView_Staff, "SELECT * FROM StaffTbl") 'Refresh the DataGridView

                'Refresh the Pie Graph
                GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                GenerateRoomChart("Family", ChartFamily, "Family Rooms")
                key = 0
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnDelete_Staff_Click(sender As Object, e As EventArgs) Handles BtnDelete_Staff.Click
        ' Button click event to delete a staff record
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            ' Ask for confirmation before deleting
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the selected record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Try
                    Using Con As New SqlConnection(connectionString)
                        Con.Open()
                        Dim query As String = "DELETE FROM StaffTbl WHERE StaffId = @StaffId"
                        Using cmd As New SqlCommand(query, Con)
                            ' Set parameter value
                            cmd.Parameters.AddWithValue("@StaffId", key)
                            cmd.ExecuteNonQuery()
                            MsgBox("Staff Record Deleted Successfully")
                        End Using
                    End Using
                    PopulateDataGridView(DataGridView_Staff, "SELECT * FROM StaffTbl") 'Refresh the DataGridView
                    ClearStaffTab()
                    'Refresh the Pie Graph
                    GenerateRoomChart("Single", ChartSingle, "Single Rooms")
                    GenerateRoomChart("Double", ChartDouble, "Double Rooms")
                    GenerateRoomChart("Family", ChartFamily, "Family Rooms")
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub BtnClear_Staff_Click(sender As Object, e As EventArgs) Handles BtnClear_Staff.Click
        ' Button click event to clear input fields in Staff tab
        ClearStaffTab()
    End Sub

    Private Sub ClearStaffTab()
        ' Method to clear input fields in Staff tab
        StaffNameTxtBox.Clear()
        StaffAgeTxtBox.Clear()
        StaffPhoneTxtBox.Clear()
        StaffSexComBox.SelectedIndex = -1
        StaffPassTxtBox.Clear()

        If SearchTxtBox_Client.Text <> "Search" Then
            SearchTxtBox_Client.Text = "Search"
            SearchTxtBox_Client.ForeColor = Color.Silver
        End If
    End Sub

    Private Sub DataGridView_Staff_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView_Staff.CellMouseClick
        ' DataGridView cell click event to populate fields in Staff tab based on selected row
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView_Staff.Rows(e.RowIndex)
            StaffNameTxtBox.Text = row.Cells(1).Value.ToString.Trim()
            StaffAgeTxtBox.Text = row.Cells(2).Value.ToString.Trim()
            StaffPhoneTxtBox.Text = row.Cells(3).Value.ToString.Trim()
            StaffSexComBox.SelectedItem = row.Cells(4).Value.ToString.Trim()
            StaffPassTxtBox.Text = row.Cells(5).Value.ToString.Trim()

            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub PerformSearch(searchBox As Guna.UI2.WinForms.Guna2TextBox, dataGridView As DataGridView, tableName As String, ParamArray columns() As String)
        Dim searchText As String = "%" & searchBox.Text.Trim().ToLower() & "%"

        ' Adjust searchText if it's "%search%" to match anything
        If searchText = "%search%" Then
            searchText = "%%"
        End If

        Try
            Using Con As New SqlConnection(connectionString)
                Con.Open()
                Dim query As New System.Text.StringBuilder("SELECT * FROM " & tableName & " WHERE ")
                For i As Integer = 0 To columns.Length - 1
                    query.Append("LOWER(" & columns(i) & ") LIKE @SearchText")
                    If i < columns.Length - 1 Then
                        query.Append(" OR ")
                    End If
                Next

                Using sda As New SqlDataAdapter(query.ToString(), Con)
                    sda.SelectCommand.Parameters.AddWithValue("@SearchText", searchText)
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    dataGridView.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while searching: " & ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SearchTxtBox_Client_TextChanged(sender As Object, e As EventArgs) Handles SearchTxtBox_Client.TextChanged
        PerformSearch(SearchTxtBox_Client, DataGridView_Client, "ClientTbl", "CLName", "CLPhone", "RoomID")
    End Sub

    Private Sub SearchTxtBox_Rooms_TextChanged(sender As Object, e As EventArgs) Handles SearchTxtBox_Rooms.TextChanged
        PerformSearch(SearchTxtBox_Rooms, DataGridView_Rooms, "RoomTbl", "RoomNum", "RoomType", "RoomPrice", "Telephone", "RoomStatus")
    End Sub

    Private Sub SearchTxtBox_Staff_TextChanged(sender As Object, e As EventArgs) Handles SearchTxtBox_Staff.TextChanged
        PerformSearch(SearchTxtBox_Staff, DataGridView_Staff, "StaffTbl", "StaffName", "StaffAge", "StaffPhone", "StaffSex")
    End Sub

    Private Sub PicBack_Click(sender As Object, e As EventArgs) Handles PicBack.Click
        ' Show the login form and hide the current form
        Dim login As New Login
        login.Show()
        Me.Hide()
    End Sub

    Private Sub ComputeDays()
        Dim totalDays As Integer = (CheckOutPicker.Value.Date - CheckInPicker.Value.Date).Days
        ' Display the total days in a label or use as needed
        TxtTotalDays.Text = totalDays
    End Sub

    ' Keep a reference to the tab page you want to show/hide
    Private tabPageToToggle As TabPage

    Public Sub SetTabVisibility(isAdmin As Boolean)
        ' Initialize the reference to the tab page if it hasn't been done already
        If tabPageToToggle Is Nothing AndAlso TabControl.TabPages.Count > 3 Then
            tabPageToToggle = TabControl.TabPages(3)
        End If

        ' Add or remove the tab page based on the isAdmin flag
        If isAdmin Then
            ' Add the tab page only if it's not already in the TabControl
            If Not TabControl.TabPages.Contains(tabPageToToggle) Then
                TabControl.TabPages.Add(tabPageToToggle)
            End If
            ' Ensure the PicStaff PictureBox is visible for admins
            PicStaff.Visible = True
        Else
            ' Remove the tab page if it exists in the TabControl
            If TabControl.TabPages.Contains(tabPageToToggle) Then
                TabControl.TabPages.Remove(tabPageToToggle)
            End If
            ' Hide the PicStaff PictureBox for non-admin users
            PicStaff.Visible = False
        End If
    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' METHODS FOR BOOKINGS PAGE''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub GenerateRoomChart(roomType As String, chart As Chart, title As String)
        ' Establishing a connection to the database using the provided connection string
        Using connection As New SqlConnection(connectionString)
            Try
                ' Open the database connection
                connection.Open()

                ' SQL query to retrieve room availability data for a specific room type
                Dim query As String = "SELECT 
                                      COUNT(*) AS TotalRooms, 
                                      SUM(CASE WHEN RoomStatus = 'Booked' THEN 1 ELSE 0 END) AS BookedRooms,
                                      SUM(CASE WHEN RoomStatus = 'Available' THEN 1 ELSE 0 END) AS AvailableRooms
                                   FROM RoomTbl
                                   WHERE RoomType = @RoomType
                                   GROUP BY RoomType"

                ' Creating a SqlCommand object with the SQL query and the connection
                Using command As New SqlCommand(query, connection)
                    ' Adding a parameter to the SQL command to specify the room type
                    command.Parameters.AddWithValue("@RoomType", roomType)

                    ' Executing the SQL command and retrieving the data using a SqlDataReader
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    ' Checking if there is data available to read
                    If reader.Read() Then
                        ' Extracting the number of booked and available rooms from the SqlDataReader
                        Dim totalBooked As Integer = Convert.ToInt32(reader("BookedRooms"))
                        Dim totalAvailable As Integer = Convert.ToInt32(reader("AvailableRooms"))

                        ' Clearing any existing series in the chart and adding a new series for rooms
                        chart.Series.Clear()
                        chart.Series.Add("Rooms")
                        chart.Series("Rooms").ChartType = SeriesChartType.Pie

                        ' Configuring the series "Rooms" in the chart
                        With chart.Series("Rooms")
                            ' Clearing any existing points and adding new points for booked and available rooms
                            .Points.Clear()
                            .Points.AddXY("Booked", totalBooked)
                            .Points.AddXY("Available", totalAvailable)

                            ' Configuring labels for the points in the pie chart
                            .IsValueShownAsLabel = True
                            .LabelForeColor = Color.Black
                            .BorderColor = Color.Black
                            .BorderWidth = 2
                            .Font = New Font("Verdana", 12, FontStyle.Regular)
                            .SetCustomProperty("PieLabelStyle", "Outside")
                            .SmartLabelStyle.CalloutLineColor = Color.Black
                        End With

                        ' Configuring the overall appearance of the chart
                        With chart.ChartAreas(0)
                            .BackColor = Color.LightGray
                            .BorderColor = Color.Black
                            .BorderWidth = 2
                            .Area3DStyle.Enable3D = True
                            .Area3DStyle.Inclination = 45
                        End With

                        With chart
                            .BackColor = Color.LightGray
                            .BorderlineColor = Color.Black
                            .BorderlineWidth = 2
                            .BorderlineDashStyle = ChartDashStyle.Solid
                        End With

                        ' Adding a title to the chart
                        chart.Titles.Clear()
                        chart.Titles.Add(title)
                        chart.Titles(0).Font = New Font("Verdana", 15, FontStyle.Bold)
                        chart.Titles(0).Alignment = ContentAlignment.TopLeft
                        chart.Titles(0).ForeColor = Color.Black

                        ' Clearing any existing legends in the chart and adding a new legend
                        chart.Legends.Clear()
                        Dim legend As New Legend()
                        legend.Docking = Docking.Bottom
                        chart.Legends.Add(legend)
                    End If
                End Using
            Catch ex As Exception
                ' Displaying an error message box if an exception occurs during database operations
                MessageBox.Show("An error occurred while generating the room chart: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub


    Private Sub TxtTotalDays_TextChanged(sender As Object, e As EventArgs) Handles TxtTotalDays.TextChanged
        CalculateTotal()
    End Sub

    Private Sub CalculateTotal()
        Dim roomRate As Decimal
        Dim totalDays As Integer
        Dim total As Decimal

        If Decimal.TryParse(TxtRoomRate.Text, roomRate) AndAlso Integer.TryParse(TxtTotalDays.Text, totalDays) Then
            total = roomRate * totalDays
            ' Include the peso sign in the displayed total
            TxtTotal.Text = "₱" & total.ToString("N2") ' Format as number with 2 decimal places, prefixed with the peso sign
        Else
            TxtTotal.Text = "Invalid input"
        End If
    End Sub

    Private Sub CheckInPicker_ValueChanged(sender As Object, e As EventArgs) Handles CheckInPicker.ValueChanged
        If IsInitializing AndAlso Not IsUpdating AndAlso CheckInPicker.Value.Date < DateTime.Now.Date Then
            MessageBox.Show("You cannot book a past date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CheckInPicker.Value = DateTime.Now ' Reset to current date
            Me.Activate()
        End If
    End Sub

    Private Sub CheckOutPicker_ValueChanged(sender As Object, e As EventArgs) Handles CheckOutPicker.ValueChanged
        If IsInitializing AndAlso Not IsUpdating AndAlso CheckOutPicker.Value.Date < DateTime.Now.Date Then
            MessageBox.Show("You cannot book a past date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CheckOutPicker.Value = DateTime.Now ' Reset to current date
            Me.Activate()
        ElseIf IsInitializing Then
            ComputeDays()
        End If
    End Sub

End Class
