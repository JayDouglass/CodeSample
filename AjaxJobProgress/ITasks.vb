Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ITasks" in both code and config file together.
<ServiceContract()>
Public Interface ITasks

    <OperationContract()>
    Function StartSleepTask() As Guid

    <OperationContract()>
    Function GetTaskStatus(ByVal taskId As Guid) As LongRunningTaskStatus
End Interface
