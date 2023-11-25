using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hdrezkaWPF
{
    public class Task
    {
        public TaskType TaskType { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public Priority TaskPriority { get; set; }
        public DateTime Deadline { get; set; }
        // Другие свойства...

        public Task(TaskType taskType, User user, string name, Status status, Priority taskPriority, DateTime deadline)
        {
            TaskType = taskType;
            User = user;
            Name = name;
            Status = status;
            TaskPriority = taskPriority;
            Deadline = deadline;
        }
    }

    public class TaskType
    {
        public string TypeName { get; set; }
        // Другие свойства...
    }

    public class User
    {
        public string Name { get; set; }
        // Другие свойства...
    }

    public class Status
    {
        public string StatusName { get; set; }
        // Другие свойства...
    }

    public class Priority
    {
        public string ProrityName { get; set; }
        // Другие свойства...
    }
}
