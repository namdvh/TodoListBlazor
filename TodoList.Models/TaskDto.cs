﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Enumerable;

namespace TodoList.Models
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Priority Priority { get; set; }
        public System.Nullable<Guid> AssigneeId { get; set; }
        public string AssigneeName { get; set; }

        public DateTime CreatedDate { get; set; }

        public Status Status { get; set; }
    }
}
