using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseToDo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Details { get; set; } = "";
        public bool ShowDetails { get; set; }
        public Status Status { get; set; }

        public static Dictionary<Status, Color> StatusColors = new()
        {
            { Status.ToDo, Colors.Orchid },
            {Status.InProgress, Colors.Azure },
            {Status.Completed, Colors.Green },
            {Status.Canceled, Colors.Grey },
        };
    }
    public enum Status
    {
        ToDo = 0,
        InProgress = 1,
        Completed = 2,
        Canceled = 3,

    }


}
