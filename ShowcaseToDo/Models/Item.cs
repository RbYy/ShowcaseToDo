using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FluentUI.AspNetCore.Components;
using Newtonsoft.Json;

namespace ShowCaseToDo.Models
{
    public class Item: IIdentifiable<string>
    {
        public string Id { get; set; }

        public string Title { get; set; } = "";
        public string Details { get; set; } = "";
        public Status Status { get; set; }

        public Item()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

}
