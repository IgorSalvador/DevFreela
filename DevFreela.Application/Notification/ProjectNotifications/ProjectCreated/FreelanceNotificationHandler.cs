using MediatR;

namespace DevFreela.Application.Notification.ProjectNotifications.ProjectCreated;

public class FreelanceNotificationHandler : INotificationHandler<ProjectCreatedNotification>
{
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Project '{notification.Title}' created with ID {notification.Id} and cost {notification.TotalCost}");

        return Task.CompletedTask;
    }
}