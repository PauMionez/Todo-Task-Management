using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management.Models
{
    internal class TaskModel : Abstracts.ViewModelBase
    {

        public string Titletask { get; set; }
        public string Taskdescription { get; set; }
        public string Typetask { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public string Date { get { return $"{Month}-{Day}-{Year}".Trim(); } }


        private string _Color;
        public string Color
        {
            get { return _Color ?? "Transparent"; }
            set { _Color = value; OnPropertyChanged(); }
        }
    }
}
