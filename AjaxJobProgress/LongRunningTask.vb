Imports System.Threading

Public MustInherit Class LongRunningTask
    Public Delegate Sub LongRunningTaskProgressChangedEventHandler(ByVal sender As Object, ByVal status As LongRunningTaskStatus)
    Public Delegate Sub LongRunningTaskCompletedEventHandler(ByVal sender As Object, ByVal status As LongRunningTaskStatus)
    Public Event ProgressChanged As LongRunningTaskProgressChangedEventHandler
    Public Event TaskCompleted As LongRunningTaskCompletedEventHandler

    ''' <summary>
    ''' Implement task here.  Can be called async with DoWorkAsync.
    ''' </summary>
    ''' <param name="args">Pass data to new thread.  Must cast it.</param>
    ''' <remarks>Call ReportProgress as task progresses.  Call ReportCompleted at end.</remarks>
    Public MustOverride Sub DoWork(ByVal args As Object)

    Public Sub DoWork()
        DoWork(Nothing)
    End Sub

    ' VB.NET 2008 requires that lambda expressions return a value,
    ' so for Subs we must provide a wrapper function that returns nothing.
    Private Function DoWork_LambdaFix(ByVal args As Object) As Object
        DoWork(args)
        Return Nothing
    End Function

    ''' <summary>
    ''' Invoke DoWork on a new thread.
    ''' </summary>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Public Overridable Sub DoWorkAsync(ByVal args As Object)
        Dim t1 As New Thread(New ThreadStart(Function() DoWork_LambdaFix(args)))
        t1.Start()
    End Sub

    Public Sub DoWorkAsync()
        DoWorkAsync(Nothing)
    End Sub

    ''' <summary>
    ''' Raise ProgressChanged event.
    ''' </summary>
    ''' <param name="status"></param>
    ''' <remarks></remarks>
    Protected Sub ReportProgress(ByVal status As LongRunningTaskStatus)
        RaiseEvent ProgressChanged(Me, status)
    End Sub

    ''' <summary>
    ''' Raise TaskCompleted event.
    ''' </summary>
    ''' <param name="status"></param>
    ''' <remarks></remarks>
    Protected Sub ReportCompleted(ByVal status As LongRunningTaskStatus)
        RaiseEvent TaskCompleted(Me, status)
    End Sub
End Class
