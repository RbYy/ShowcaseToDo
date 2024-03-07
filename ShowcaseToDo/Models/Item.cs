using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShowCaseToDo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Details { get; set; } = "";
        public Status Status { get; set; }
        [JsonIgnore]
        public Css? CssClasses => Css.CssClasses.FirstOrDefault((i) => i.Status == Status);
    }
    public class Css
    {
        public Status Status { get; set; }
        public string BackGroundClass { get; set; }
        public string BadgeClass { get; set; }

        public Css(Status status, string backGroundClass, string badgeClass)
        {
            Status = status;
            BackGroundClass = backGroundClass;
            BadgeClass = badgeClass;
        }

        public static  List<Css> CssClasses  = 
        [
            new(status: Status.ToDo, backGroundClass: "status-bg-todo", badgeClass: "status-badge-todo"),
            new(status: Status.InProgress, backGroundClass: "status-bg-progress", badgeClass: "status-badge-progress"),
            new (status: Status.Completed, backGroundClass: "status-bg-completed", badgeClass: "status-badge-completed"),
            new (status: Status.Canceled, backGroundClass: "status-bg-canceled", badgeClass: "status-badge-canceled"),
        ];
    }
    //public static Dictionary<Status, string> StatusColors = new()
    //{
    //    { Status.ToDo, "status-bg-todo"},
    //    { Status.InProgress, "status-bg-progress" },
    //    { Status.Completed, "status-bg-completed" },
    //    { Status.Canceled, "status-bg-canceled" },
    //};
    public enum Status
    {
        ToDo = 0,
        InProgress = 1,
        Completed = 2,
        Canceled = 3,

    }


}
