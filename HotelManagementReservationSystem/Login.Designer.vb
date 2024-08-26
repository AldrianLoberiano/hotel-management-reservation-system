<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.GunaElipsePanel1 = New Guna.UI.WinForms.GunaElipsePanel()
        Me.PicPass = New Guna.UI.WinForms.GunaPictureBox()
        Me.PicUser = New Guna.UI.WinForms.GunaPictureBox()
        Me.CheckShowPass = New Guna.UI2.WinForms.Guna2CheckBox()
        Me.PicUserLogo = New Guna.UI.WinForms.GunaPictureBox()
        Me.TxtPassword = New Guna.UI2.WinForms.Guna2TextBox()
        Me.BtnLog = New Guna.UI2.WinForms.Guna2Button()
        Me.TxtUsername = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ElipseBG = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.Guna2PictureBox1 = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.ElipseForm = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.ElipseUsername = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.ElipsePass = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.PicExit = New Guna.UI.WinForms.GunaPictureBox()
        Me.GunaElipsePanel1.SuspendLayout()
        CType(Me.PicPass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicUserLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Guna2PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicExit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GunaElipsePanel1
        '
        Me.GunaElipsePanel1.BackColor = System.Drawing.Color.Transparent
        Me.GunaElipsePanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.GunaElipsePanel1.BaseColor = System.Drawing.Color.Transparent
        Me.GunaElipsePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GunaElipsePanel1.Controls.Add(Me.PicPass)
        Me.GunaElipsePanel1.Controls.Add(Me.PicUser)
        Me.GunaElipsePanel1.Controls.Add(Me.CheckShowPass)
        Me.GunaElipsePanel1.Controls.Add(Me.PicUserLogo)
        Me.GunaElipsePanel1.Controls.Add(Me.TxtPassword)
        Me.GunaElipsePanel1.Controls.Add(Me.BtnLog)
        Me.GunaElipsePanel1.Controls.Add(Me.TxtUsername)
        Me.GunaElipsePanel1.Controls.Add(Me.Label1)
        Me.GunaElipsePanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GunaElipsePanel1.Location = New System.Drawing.Point(0, 0)
        Me.GunaElipsePanel1.Name = "GunaElipsePanel1"
        Me.GunaElipsePanel1.Size = New System.Drawing.Size(327, 633)
        Me.GunaElipsePanel1.TabIndex = 0
        '
        'PicPass
        '
        Me.PicPass.BackColor = System.Drawing.Color.Black
        Me.PicPass.BaseColor = System.Drawing.Color.Transparent
        Me.PicPass.Image = Global.HotelManagementReservationSystem.My.Resources.Resources._448424889_885892223366888_6136269900551590527_n
        Me.PicPass.Location = New System.Drawing.Point(30, 307)
        Me.PicPass.Name = "PicPass"
        Me.PicPass.Size = New System.Drawing.Size(33, 31)
        Me.PicPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicPass.TabIndex = 23
        Me.PicPass.TabStop = False
        '
        'PicUser
        '
        Me.PicUser.BackColor = System.Drawing.Color.Black
        Me.PicUser.BaseColor = System.Drawing.Color.Transparent
        Me.PicUser.Image = Global.HotelManagementReservationSystem.My.Resources.Resources._448775649_490168126850783_8831513444114822407_n
        Me.PicUser.Location = New System.Drawing.Point(29, 252)
        Me.PicUser.Name = "PicUser"
        Me.PicUser.Size = New System.Drawing.Size(33, 31)
        Me.PicUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicUser.TabIndex = 24
        Me.PicUser.TabStop = False
        '
        'CheckShowPass
        '
        Me.CheckShowPass.AutoSize = True
        Me.CheckShowPass.CheckedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CheckShowPass.CheckedState.BorderRadius = 3
        Me.CheckShowPass.CheckedState.BorderThickness = 0
        Me.CheckShowPass.CheckedState.FillColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CheckShowPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.CheckShowPass.ForeColor = System.Drawing.Color.White
        Me.CheckShowPass.Location = New System.Drawing.Point(66, 347)
        Me.CheckShowPass.Name = "CheckShowPass"
        Me.CheckShowPass.Size = New System.Drawing.Size(102, 17)
        Me.CheckShowPass.TabIndex = 4
        Me.CheckShowPass.Text = "Show Password"
        Me.CheckShowPass.UncheckedState.BorderColor = System.Drawing.Color.White
        Me.CheckShowPass.UncheckedState.BorderRadius = 3
        Me.CheckShowPass.UncheckedState.BorderThickness = 0
        Me.CheckShowPass.UncheckedState.FillColor = System.Drawing.Color.White
        '
        'PicUserLogo
        '
        Me.PicUserLogo.BackColor = System.Drawing.Color.Black
        Me.PicUserLogo.BaseColor = System.Drawing.Color.Transparent
        Me.PicUserLogo.Image = Global.HotelManagementReservationSystem.My.Resources.Resources._448815136_496159449608752_7711807486978637933_n
        Me.PicUserLogo.Location = New System.Drawing.Point(111, 71)
        Me.PicUserLogo.Name = "PicUserLogo"
        Me.PicUserLogo.Size = New System.Drawing.Size(99, 93)
        Me.PicUserLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicUserLogo.TabIndex = 22
        Me.PicUserLogo.TabStop = False
        '
        'TxtPassword
        '
        Me.TxtPassword.Animated = True
        Me.TxtPassword.AutoRoundedCorners = True
        Me.TxtPassword.BorderRadius = 19
        Me.TxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPassword.DefaultText = "Password"
        Me.TxtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPassword.Font = New System.Drawing.Font("Berlin Sans FB", 14.25!)
        Me.TxtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.TxtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPassword.Location = New System.Drawing.Point(66, 300)
        Me.TxtPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TxtPassword.PlaceholderText = ""
        Me.TxtPassword.SelectedText = ""
        Me.TxtPassword.Size = New System.Drawing.Size(218, 41)
        Me.TxtPassword.TabIndex = 3
        '
        'BtnLog
        '
        Me.BtnLog.Animated = True
        Me.BtnLog.AutoRoundedCorners = True
        Me.BtnLog.BackColor = System.Drawing.Color.Transparent
        Me.BtnLog.BorderColor = System.Drawing.Color.Transparent
        Me.BtnLog.BorderRadius = 21
        Me.BtnLog.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton
        Me.BtnLog.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.BtnLog.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.BtnLog.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.BtnLog.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.BtnLog.FillColor = System.Drawing.SystemColors.ActiveCaption
        Me.BtnLog.Font = New System.Drawing.Font("Berlin Sans FB Demi", 21.75!, System.Drawing.FontStyle.Bold)
        Me.BtnLog.ForeColor = System.Drawing.Color.Black
        Me.BtnLog.IndicateFocus = True
        Me.BtnLog.Location = New System.Drawing.Point(32, 391)
        Me.BtnLog.Name = "BtnLog"
        Me.BtnLog.Size = New System.Drawing.Size(252, 45)
        Me.BtnLog.TabIndex = 2
        Me.BtnLog.Text = "Log In"
        '
        'TxtUsername
        '
        Me.TxtUsername.AutoRoundedCorners = True
        Me.TxtUsername.BorderRadius = 19
        Me.TxtUsername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtUsername.DefaultText = "Username"
        Me.TxtUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.TxtUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TxtUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.TxtUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtUsername.Font = New System.Drawing.Font("Berlin Sans FB", 14.25!)
        Me.TxtUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.TxtUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtUsername.Location = New System.Drawing.Point(66, 246)
        Me.TxtUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtUsername.Name = "TxtUsername"
        Me.TxtUsername.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TxtUsername.PlaceholderText = ""
        Me.TxtUsername.SelectedText = ""
        Me.TxtUsername.Size = New System.Drawing.Size(218, 41)
        Me.TxtUsername.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Berlin Sans FB Demi", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Label1.Location = New System.Drawing.Point(46, 177)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(228, 31)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "ACCOUNT LOGIN"
        '
        'ElipseBG
        '
        Me.ElipseBG.BorderRadius = 10
        Me.ElipseBG.TargetControl = Me.Guna2PictureBox1
        '
        'Guna2PictureBox1
        '
        Me.Guna2PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2PictureBox1.Image = CType(resources.GetObject("Guna2PictureBox1.Image"), System.Drawing.Image)
        Me.Guna2PictureBox1.ImageRotate = 0!
        Me.Guna2PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2PictureBox1.Name = "Guna2PictureBox1"
        Me.Guna2PictureBox1.Size = New System.Drawing.Size(1104, 633)
        Me.Guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Guna2PictureBox1.TabIndex = 1
        Me.Guna2PictureBox1.TabStop = False
        '
        'ElipseForm
        '
        Me.ElipseForm.TargetControl = Me
        '
        'PicExit
        '
        Me.PicExit.BackColor = System.Drawing.Color.Transparent
        Me.PicExit.BaseColor = System.Drawing.Color.Transparent
        Me.PicExit.Image = CType(resources.GetObject("PicExit.Image"), System.Drawing.Image)
        Me.PicExit.Location = New System.Drawing.Point(1064, 4)
        Me.PicExit.Name = "PicExit"
        Me.PicExit.Size = New System.Drawing.Size(33, 31)
        Me.PicExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicExit.TabIndex = 22
        Me.PicExit.TabStop = False
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(57, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1104, 633)
        Me.Controls.Add(Me.PicExit)
        Me.Controls.Add(Me.GunaElipsePanel1)
        Me.Controls.Add(Me.Guna2PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.GunaElipsePanel1.ResumeLayout(False)
        Me.GunaElipsePanel1.PerformLayout()
        CType(Me.PicPass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicUserLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Guna2PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicExit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GunaElipsePanel1 As Guna.UI.WinForms.GunaElipsePanel
    Friend WithEvents Label1 As Label
    Friend WithEvents ElipseBG As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2PictureBox1 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents ElipseForm As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents ElipseUsername As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents ElipsePass As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents BtnLog As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents TxtPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents TxtUsername As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents CheckShowPass As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents PicUserLogo As Guna.UI.WinForms.GunaPictureBox
    Friend WithEvents PicPass As Guna.UI.WinForms.GunaPictureBox
    Friend WithEvents PicUser As Guna.UI.WinForms.GunaPictureBox
    Friend WithEvents PicExit As Guna.UI.WinForms.GunaPictureBox
End Class
