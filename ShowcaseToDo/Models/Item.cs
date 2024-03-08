using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FluentUI.AspNetCore.Components;
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
        public TaskStatus? Styles => TaskStatus.CssClasses.FirstOrDefault((i) => i.Status == Status);
    }



}
