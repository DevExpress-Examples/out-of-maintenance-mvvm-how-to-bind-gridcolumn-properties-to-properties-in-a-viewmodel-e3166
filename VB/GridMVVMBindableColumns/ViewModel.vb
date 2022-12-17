Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands
Imports System.ComponentModel

Namespace Model

    Public Class ViewModel
        Implements INotifyPropertyChanged

        Private _Source As ObservableCollection(Of Model.DataItem), _Categories As ObservableCollection(Of Model.Category)

        Private categoryColumnVisibilityField As Boolean = True

        Public Property Source As ObservableCollection(Of DataItem)
            Get
                Return _Source
            End Get

            Private Set(ByVal value As ObservableCollection(Of DataItem))
                _Source = value
            End Set
        End Property

        Public Property Categories As ObservableCollection(Of Category)
            Get
                Return _Categories
            End Get

            Private Set(ByVal value As ObservableCollection(Of Category))
                _Categories = value
            End Set
        End Property

        Public Property CategoryColumnVisibility As Boolean
            Get
                Return categoryColumnVisibilityField
            End Get

            Set(ByVal value As Boolean)
                If categoryColumnVisibilityField = value Then Return
                categoryColumnVisibilityField = value
                RaisePropertyChanged("CategoryColumnVisibility")
            End Set
        End Property

        Public Property ShowHideCategoriesColumn As ICommand

        Public Sub New()
            Source = DataItem.Data
            Categories = New ObservableCollection(Of Category)() From {New Category() With {.Id = 0, .Name = "A category"}, New Category() With {.Id = 1, .Name = "B category"}, New Category() With {.Id = 2, .Name = "C category"}, New Category() With {.Id = 3, .Name = "D category"}}
            ShowHideCategoriesColumn = New DelegateCommand(Of Object)(Sub(obj) CategoryColumnVisibility = Not CategoryColumnVisibility, Nothing)
        End Sub

        Private Sub RaisePropertyChanged(ByVal name As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

    Public Class Category

        Public Property Id As Integer

        Public Property Name As String
    End Class

    Public Class DataItem

        Public Shared ReadOnly Property Data As ObservableCollection(Of DataItem)
            Get
                Dim res As ObservableCollection(Of DataItem) = New ObservableCollection(Of DataItem)()
                For i As Integer = 0 To 100 - 1
                    res.Add(New DataItem() With {.Id = i, .Name = "Name" & i.ToString(), .Category = i Mod 4})
                Next

                Return res
            End Get
        End Property

        Public Property Id As Integer

        Public Property Name As String

        Public Property Category As Integer

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
