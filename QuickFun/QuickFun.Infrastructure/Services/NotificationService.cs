namespace QuickFun.Infrastructure.Services;

public class NotificationService
{
    private readonly List<NotificationMessage> _notifications = new();

    public event Action<NotificationMessage>? OnNotify;
    public event Action? OnChange;

    public IReadOnlyList<NotificationMessage> Notifications => _notifications.AsReadOnly();

    public void Notify(string message, NotificationType type = NotificationType.Info, int durationMs = 3000)
    {
        var notification = new NotificationMessage
        {
            Id = Guid.NewGuid(),
            Message = message,
            Type = type,
            Timestamp = DateTime.UtcNow,
            DurationMs = durationMs
        };

        _notifications.Add(notification);
        OnNotify?.Invoke(notification);
        OnChange?.Invoke();

        // Auto-remove after duration
        if (durationMs > 0)
        {
            Task.Delay(durationMs).ContinueWith(_ => Remove(notification.Id));
        }
    }

    public void Success(string message, int durationMs = 3000)
    {
        Notify(message, NotificationType.Success, durationMs);
    }

    public void Error(string message, int durationMs = 5000)
    {
        Notify(message, NotificationType.Error, durationMs);
    }

    public void Warning(string message, int durationMs = 4000)
    {
        Notify(message, NotificationType.Warning, durationMs);
    }

    public void Info(string message, int durationMs = 3000)
    {
        Notify(message, NotificationType.Info, durationMs);
    }

    public void Remove(Guid id)
    {
        var notification = _notifications.FirstOrDefault(n => n.Id == id);
        if (notification != null)
        {
            _notifications.Remove(notification);
            OnChange?.Invoke();
        }
    }

    public void Clear()
    {
        _notifications.Clear();
        OnChange?.Invoke();
    }
}

public class NotificationMessage
{
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public DateTime Timestamp { get; set; }
    public int DurationMs { get; set; }
}

public enum NotificationType
{
    Success,
    Error,
    Warning,
    Info
}