Public Class Tasks
    Implements ITasks

    ''' <summary>
    ''' Starts an example task that just calls thread.sleep to simulate a long task
    ''' Returns a guid that can be used to get the task status as it progresses
    ''' </summary>
    Public Function StartSleepTask() As System.Guid Implements ITasks.StartSleepTask
        Dim task As New SleepyTask
        Dim taskId = LongRunningTaskMonitor.Monitor(task)
        task.DoWorkAsync()
        Return taskId
    End Function

    ''' <summary>
    ''' Get the current task status for the task with matching guid
    ''' </summary>
    Public Function GetTaskStatus(taskId As System.Guid) As LongRunningTaskStatus Implements ITasks.GetTaskStatus
        Return LongRunningTaskMonitor.GetTaskStatus(taskId)
    End Function

End Class
