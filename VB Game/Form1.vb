Public Class frmFindTheIcon
    Private random As New Random ' randomise object for the squares
    Private icons = New List(Of String) From {"L", "L", "k", "k", "W", "W", "!", "!", "z", "z", "N", "N", ",", ",", "b", "b", "*", "*", ".", "."} ' icons which work (some that I tested didn't work) because we have put objects into the variable, we have to specify the data type
    Private firstClicked As Label = Nothing 'this points to the label control that the player clicks
    Private secondClicked As Label = Nothing' this points to the second label control, the reason why it is nothing is because these variables are not tracking an object

    Private Sub frmCardSharp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Computer.Audio.Play(My.Resources.U2_I_Still_Haven_t_Found_What_I_m_Looking_For__edited_, AudioPlayMode.BackgroundLoop) ' Adds background music on a loop because that way sound is always there when the game is played
																																  ' we use the dot to access the file system for which the music is stored in 
    End Sub

    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles btnPlay.Click
        AssignIconsToSquares() ' calls the function from below
    End Sub

    Private Sub AssignIconsToSquares()
        ' I used for because it only does what it should do until a specific number (20)        
        ' This for assigns the letters which have been changed to icons and put into their positions
        For Each Control In TableLayoutPanel1.Controls ' we use for each for multiple actions, in this case is the randomise each square
            Dim iconLabel = TryCast(Control, Label)
            If iconLabel IsNot Nothing Then ' I used the if statement because if the label doesn't contain an icon, it adds an icon using a random number and crosses it so it doesn't the same number
                Dim randomNumber = random.Next(icons.Count)
                iconLabel.Text = icons(randomNumber)
                icons.RemoveAt(randomNumber)
            End If
        Next
    End Sub

    Private Sub label(sender As Object, e As EventArgs) Handles Label1.Click, Label2.Click,
        Label3.Click, Label4.Click, Label5.Click, Label6.Click, Label7.Click, Label8.Click,
        Label9.Click, Label10.Click, Label11.Click, Label12.Click, Label13.Click, Label14.Click,
        Label15.Click, Label16.Click, Label17.Click, Label18.Click, Label19.Click, Label20.Click

        If timtaken.Enabled Then Exit Sub ' checks whether the timer has been enabled

        Dim clickedLabel = TryCast(sender, Label)

        If clickedLabel IsNot Nothing Then

            ' If the clicked label is chartreuse then an icon has already been revealed --  
            ' ignore the click 

            If clickedLabel.ForeColor = Color.Chartreuse Then Exit Sub
            ' If firstClicked is Nothing, this is the first icon   
            ' so set firstClicked to the label that the player 
            ' clicked, change its colour to Chartreuse, and return

            timHideIcon.Stop()

            If firstClicked Is Nothing Then
                firstClicked = clickedLabel
                firstClicked.ForeColor = Color.Chartreuse
                Exit Sub
            End If

            timHideIcon.Start()

            secondClicked = clickedLabel
            secondClicked.ForeColor = Color.Chartreuse

            CheckForWinner() ' calls the function from below each time

            If firstClicked.Text = secondClicked.Text Then
                firstClicked = Nothing 'If the icons are the same, then it resets the first click and second click
                secondClicked = Nothing
                Exit Sub
            End If

            timtaken.Start()
        End If
    End Sub
    Private Sub timTaken_tick() Handles timtaken.Tick
        timtaken.Stop() ' stop the timer

        firstClicked.ForeColor = firstClicked.BackColor
        secondClicked.ForeColor = secondClicked.BackColor ' Hiding both icons

        'Reset firstClicked and secondClicked  
        ' so the next time a label is 
        ' clicked, the program knows it's the first click
        firstClicked = Nothing
        secondClicked = Nothing
    End Sub

    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        End ' just in case for players who want to quit the game early, just ends the program
    End Sub

    Private Sub CheckForWinner()
        For Each Control In TableLayoutPanel1.Controls '' Go through all of the labels in the TableLayoutPanel, as we use for each  
														' checking each one to see if its icon is matched
            Dim iconLabel = TryCast(Control, Label)
            If iconLabel IsNot Nothing AndAlso
                iconLabel.ForeColor = iconLabel.BackColor Then Exit Sub
        Next
        timTimer.Stop()
        ' If the loop didn't return, it didn't find any unmatched icons 
        ' That means the user won. Show a message and close the form, so the execution stops running because I have used close because there is no reset (as I couldn't do it)
        MessageBox.Show("You have Matched all the icons", "Congratulations")
        End
    End Sub
End Class
