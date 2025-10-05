using MediatR;

namespace DevFreela.Application.Notification.ProjectNotifications.ProjectCreated;

public class GenerateProjectBoardHandler : INotificationHandler<ProjectCreatedNotification>
{
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Creating board for project '{notification.Title}' with ID {notification.Id}.");

        return Task.CompletedTask;
    }
}