using System.Collections.ObjectModel;

namespace Ödev_2;

public partial class ToDoList : ContentPage
{
    public ObservableCollection<TaskItem> Tasks { get; set; }

    public ToDoList()
    {
        InitializeComponent();
        Tasks = new ObservableCollection<TaskItem>
        {
            new TaskItem { Description = "Görevinizi buraya girin ya da sağ üstten yeni görev ekleyin...", IsCompleted = false }
        
        };
        UpdateTaskNumbers();
        TasksListView.ItemsSource = Tasks;
    }

    private void UpdateTaskNumbers()
    {
        int taskNumber = 1;
        foreach (var task in Tasks)
        {
            task.DisplayDescription = $"{taskNumber++}. {task.Description}";
        }
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Görev Ekle", "Görev:", "Ekle", "İptal");
        if (!string.IsNullOrWhiteSpace(result))
        {
            var newTask = new TaskItem { Description = result, IsCompleted = false };
            Tasks.Add(newTask);
            UpdateTaskNumbers();  
        }
    }

    private async void OnEditTaskClicked(object sender, EventArgs e)
    {
        var task = (TaskItem)((ImageButton)sender).CommandParameter;
        string result = await DisplayPromptAsync("Görev Düzenle", "Görev:", initialValue: task.Description, accept: "Tamam", cancel: "İptal");
        if (!string.IsNullOrWhiteSpace(result))
        {
            task.Description = result;
            UpdateTaskNumbers(); 
            TasksListView.ItemsSource = Tasks;
        }
    }

    private void OnDeleteTaskClicked(object sender, EventArgs e)
    {
        var task = (TaskItem)((ImageButton)sender).CommandParameter;
        Tasks.Remove(task);
        UpdateTaskNumbers();  
    }

    private void OnTaskTapped(object sender, ItemTappedEventArgs e)
    {
        var task = (TaskItem)e.Item;
        task.IsCompleted = !task.IsCompleted;
        UpdateTaskNumbers();  
        TasksListView.ItemsSource = Tasks;
    }
}

public class TaskItem
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string DisplayDescription { get; set; }
} 
