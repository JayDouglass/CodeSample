Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.Linq

Public Class LongRunningTaskMonitor
    Private Shared TaskStatuses As New Dictionary(Of Guid, LongRunningTaskStatus)
    Private Shared ReadOnly syncRoot As New Object

    ''' <summary>
    ''' Create unique task id and subscribe to task events.
    ''' </summary>
    ''' <param name="task"></param>
    ''' <returns>Task ID</returns>
    ''' <remarks></remarks>
    Public Shared Function Monitor(ByVal task As LongRunningTask) As Guid
        Dim taskId As Guid = Guid.NewGuid
        AddHandler task.ProgressChanged, Function(sender, status) UpdateTaskStatus(taskId, status)
        AddHandler task.TaskCompleted, Function(sender, status) UpdateTaskStatus(taskId, status)

        UpdateTaskStatus(taskId, New LongRunningTaskStatus With { _
                                .Message = "Starting ...", _
                                .PercentComplete = 0})
        RemoveFinishedTasks()

        Return taskId
    End Function

    Private Shared Sub RemoveFinishedTasks()
        Dim finishedTasks = From task In TaskStatuses _
                            Where task.Value.Finished
        SyncLock syncRoot
            For Each task In finishedTasks.ToList
                TaskStatuses.Remove(task.Key)
            Next
        End SyncLock
    End Sub

    Public Shared Function GetTaskStatus(ByVal taskId As Guid) As LongRunningTaskStatus
        Dim taskStatus As New LongRunningTaskStatus
        SyncLock syncRoot
            TaskStatuses.TryGetValue(taskId, taskStatus)
        End SyncLock

        Return If(taskStatus, New LongRunningTaskStatus With {.Finished = True, .Message = "No task found", .ErrorFlag = True})
    End Function

    Private Shared Function UpdateTaskStatus(ByVal taskId As Guid, ByVal status As LongRunningTaskStatus) As Object
        SyncLock syncRoot
            TaskStatuses(taskId) = status
        End SyncLock

        Return Nothing
    End Function
End Class


