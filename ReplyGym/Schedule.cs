using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ReplyGym
{
    class Schedule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }  // La chiave primaria autoincrementante
        public string Day { get; set; }
        public string Course { get; set; }
    }
}
