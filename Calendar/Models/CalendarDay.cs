using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calendar.Models
{
    public class CalendarDay
    {
        public string DisplayText { get; set; }
        public ICommand ClickCommand { get; set; }
    }
}
