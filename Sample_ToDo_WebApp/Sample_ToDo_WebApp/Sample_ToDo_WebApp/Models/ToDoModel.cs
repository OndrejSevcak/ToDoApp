namespace Sample_ToDo_WebApp.Models
{
    public class ToDoModel
    {
        public List<ToDo> ToDoList { get; set; }
        public List<ToDo> ToDoListNotCompleted { get; set; }

        public List<string> ErrorMessages { get; set; }

        public ToDo_Dto TD_DTO { get; set; }

        public ToDoModel()
        {
            ToDoList = new List<ToDo>();
            ToDoListNotCompleted = new List<ToDo>();
            ErrorMessages = new List<string>();
            TD_DTO = new ToDo_Dto();
        }
    }
}
