Public Class LongRunningTaskStatus
    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Private _percentComplete As Integer
    Public Property PercentComplete() As Integer
        Get
            Return _percentComplete
        End Get
        Set(ByVal value As Integer)
            _percentComplete = value
        End Set
    End Property

    Private _finished As Boolean
    Public Property Finished() As Boolean
        Get
            Return _finished
        End Get
        Set(ByVal value As Boolean)
            _finished = value
        End Set
    End Property

    Private _errorFlag As Boolean
    Public Property ErrorFlag() As Boolean
        Get
            Return _errorFlag
        End Get
        Set(ByVal value As Boolean)
            _errorFlag = value
        End Set
    End Property
End Class