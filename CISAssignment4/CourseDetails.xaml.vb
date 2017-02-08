'------------------------------------------------------------ 
'-               File Name : CourseDetails.xaml.vb          - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains window for editing the course         -
'- description                                              -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- _degreePrefix - degree prefix                            -
'------------------------------------------------------------
Public Class CourseDetails

    Private _description As CourseDescription

    '------------------------------------------------------------ 
    '-                Subprogram Name: New                      - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine creates the gui and instantiates default -
    '- member data/objects                                      -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- course - full course name                                - 
    '- description - course description object                  -
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (None)                                                   - 
    '------------------------------------------------------------
    Public Sub New(course As String, description As CourseDescription)
        InitializeComponent()
        _description = description
        textBlockCourse.Text = course
        textBlockDescription.Text = description.Description
        textBlockPrerequisite.Text = description.Prerequisite
        textBlockCredits.Text = description.Credits
        buttonSave.Visibility = Visibility.Hidden
    End Sub

    '------------------------------------------------------------ 
    '-              Subprogram Name: buttonSave_Click           - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine saves the input to the desscription      -
    '- object                                                   -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- tempInt - temporary integer                              -
    '------------------------------------------------------------
    Private Sub buttonSave_Click(sender As Object, e As RoutedEventArgs) Handles buttonSave.Click
        Dim tempInt As Integer = 0
        _description.Credits = textBlockCredits.Text
        _description.Description = textBlockDescription.Text
        _description.Prerequisite = textBlockPrerequisite.Text
        buttonSave.Visibility = Visibility.Hidden
    End Sub

    '------------------------------------------------------------ 
    '-    Subprogram Name: textBlockDescription_TextChanged     - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine allows the user to save changes          -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (none)                                                   -
    '------------------------------------------------------------
    Private Sub textBlockDescription_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBlockDescription.TextChanged
        buttonSave.Visibility = Visibility.Visible
    End Sub

    '------------------------------------------------------------ 
    '-    Subprogram Name: textBlockDescription_TextChanged     - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine allows the user to save changes          -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (none)                                                   -
    '------------------------------------------------------------
    Private Sub textBlockPrerequisite_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBlockPrerequisite.TextChanged
        buttonSave.Visibility = Visibility.Visible
    End Sub

    '------------------------------------------------------------ 
    '-    Subprogram Name: textBlockDescription_TextChanged     - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      - 
    '-                                                          - 
    '- This subroutine allows the user to save changes          -
    '------------------------------------------------------------ 
    '- Parameter Dictionary:                                    - 
    '- sender – Identifies which particular control raised the  - 
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -    
    '------------------------------------------------------------ 
    '- Local Variable Dictionary:                               - 
    '- (none)                                                   -
    '------------------------------------------------------------
    Private Sub textBlockCredits_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBlockCredits.TextChanged
        buttonSave.Visibility = Visibility.Visible
    End Sub
End Class
