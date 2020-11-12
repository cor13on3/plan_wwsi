using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public enum MenuItemType
    {
        PlanZajec,
        Wykladowcy,
        ZmienGrupe
    }

    public class AppMenuItem
    {
        public MenuItemType Id { get; set; }
        public string Title { get; set; }
    }
}
