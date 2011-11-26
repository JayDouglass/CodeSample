Imports System.Threading

Public Class SleepyTask
    Inherits LongRunningTask

    Public Overrides Sub DoWork(ByVal args As Object)
        ReportProgress(New LongRunningTaskStatus With _
                       {.Message = "Starting SleepyTask", .PercentComplete = 0})
        Dim max = 300
        For i = 1 To max
            Thread.Sleep(100)
            ReportProgress(New LongRunningTaskStatus With _
                           {.Message = "i = " & i, .PercentComplete = CInt(i / max * 100)})
        Next
        ReportCompleted(New LongRunningTaskStatus With _
                        {.Finished = True, .PercentComplete = 100, .Message = "Done"})
    End Sub
End Class