using System;

namespace PlanWWSI.Models
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
