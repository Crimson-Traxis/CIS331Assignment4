'------------------------------------------------------------ 
'-               File Name : DegreeComboBoxItem.vb          - 
'-                Part of Project: Assignment 4             - 
'------------------------------------------------------------
'-                Written By: Trent Killinger               - 
'-                Written On: 2-1-17                        - 
'------------------------------------------------------------ 
'- File Purpose:                                            - 
'-                                                          - 
'- This file contains the combobox item that stores the     -
'- degree object                                            -
'------------------------------------------------------------
'- Variable Dictionary                                      - 
'- _degree - degree                                         -
'------------------------------------------------------------

Public Class DegreeComboBoxItem
    Inherits ComboBoxItem

    Private _degree As Degree

    '------------------------------------------------------------ 
    '-                Property Name: Degree                     - 
    '------------------------------------------------------------
    '-                Written By: Trent Killinger               - 
    '-                Written On: 2-1-17                        - 
    '------------------------------------------------------------
    '- Property Purpose:                                        - 
    '-                                                          - 
    '- This Property gets the Degree                            -
    '------------------------------------------------------------ 
    Public Property Degree() As Degree
        Get
            Return _degree
        End Get
        Set(ByVal value As Degree)
            _degree = value
            Content = value.DegreePrefix + " - " + value.DegreeName
        End Set
    End Property
End Class
