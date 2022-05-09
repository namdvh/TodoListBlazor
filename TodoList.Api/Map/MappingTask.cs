using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Api.Map
{
    public class MappingTask:Profile
    {
        public MappingTask()
        {
            CreateMap<Entities.Task, TaskCreateRequest>();
        }
    }
}
