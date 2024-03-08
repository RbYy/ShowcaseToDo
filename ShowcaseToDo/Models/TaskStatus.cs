using Microsoft.FluentUI.AspNetCore.Components;

namespace ShowCaseToDo.Models
{
    public enum Status
    {
        ToDo = 0,
        InProgress = 1,
        Completed = 2,
        Canceled = 3,

    }
    public class TaskStatus
    {
        public Status Status { get; set; }
        public string BackGroundClass { get; set; }
        public string BadgeClass { get; set; }
        public Icon Icon { get; set; }

        public TaskStatus(Status status, string backGroundClass, string badgeClass, Icon icon)
        {
            Status = status;
            BackGroundClass = backGroundClass;
            BadgeClass = badgeClass;
            Icon = icon;
        }

        public static  List<TaskStatus> CssClasses  = 
        [
            new(Status.ToDo, "status-bg-todo", "status-badge-todo", new Icons.Regular.Size32.Calendar()),
            new(Status.InProgress, "status-bg-progress", "status-badge-progress", new Icons.Regular.Size32.Edit()),
            new (Status.Completed, "status-bg-completed", "status-badge-completed", new Icons.Regular.Size32.Checkmark()),
            new (Status.Canceled, "status-bg-canceled", "status-badge-canceled", new Icons.Regular.Size32.Prohibited()),
        ];
    }


}
