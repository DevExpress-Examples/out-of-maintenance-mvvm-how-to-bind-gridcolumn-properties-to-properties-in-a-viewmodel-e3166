﻿Imports System
Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports DevExpress.Utils.Commands
Imports DevExpress.Xpf.Core.Commands
Imports System.Collections
Imports System.Linq
Imports System.ComponentModel

Namespace Model
	Public Class ViewModel
		Implements INotifyPropertyChanged

'INSTANT VB NOTE: The field categoryColumnVisibility was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private categoryColumnVisibility_Conflict As Boolean = True
		Private privateSource As ObservableCollection(Of DataItem)
		Public Property Source() As ObservableCollection(Of DataItem)
			Get
				Return privateSource
			End Get
			Private Set(ByVal value As ObservableCollection(Of DataItem))
				privateSource = value
			End Set
		End Property
		Private privateCategories As ObservableCollection(Of Category)
		Public Property Categories() As ObservableCollection(Of Category)
			Get
				Return privateCategories
			End Get
			Private Set(ByVal value As ObservableCollection(Of Category))
				privateCategories = value
			End Set
		End Property
		Public Property CategoryColumnVisibility() As Boolean
			Get
				Return categoryColumnVisibility_Conflict
			End Get
			Set(ByVal value As Boolean)
				If categoryColumnVisibility_Conflict = value Then
					Return
				End If
				categoryColumnVisibility_Conflict = value
				RaisePropertyChanged("CategoryColumnVisibility")
			End Set
		End Property
		Public Property ShowHideCategoriesColumn() As ICommand
		Public Sub New()
			Source = DataItem.Data
			Categories = New ObservableCollection(Of Category)() From {
				New Category() With {
					.Id=0,
					.Name="A category"
				},
				New Category() With {
					.Id=1,
					.Name="B category"
				},
				New Category() With {
					.Id=2,
					.Name="C category"
				},
				New Category() With {
					.Id=3,
					.Name="D category"
				}
			}
			ShowHideCategoriesColumn = New DelegateCommand(Of Object)(Sub(obj)
				CategoryColumnVisibility = Not CategoryColumnVisibility
			End Sub, Nothing)
		End Sub

		Private Sub RaisePropertyChanged(ByVal name As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
		End Sub

		Public Event PropertyChanged As PropertyChangedEventHandler
	End Class

	Public Class Category
		Public Property Id() As Integer
		Public Property Name() As String
	End Class
	Public Class DataItem
		Public Shared ReadOnly Property Data() As ObservableCollection(Of DataItem)
			Get
				Dim res As New ObservableCollection(Of DataItem)()
				For i As Integer = 0 To 99
					res.Add(New DataItem() With {
						.Id = i,
						.Name = "Name" & i.ToString(),
						.Category = i Mod 4
					})
				Next i
				Return res
			End Get
		End Property
		Public Property Id() As Integer
		Public Property Name() As String
		Public Property Category() As Integer
		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class
End Namespace
