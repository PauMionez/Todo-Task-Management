
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Task_Management.Models;

namespace Task_Management.ViewModels
{

    class MainViewModel : Abstracts.ViewModelBase
    {
        #region Commands
        public DelegateCommand AddNewTaskCommand { get; set; }
        public DelegateCommand CancelNewTaskCommand { get; set; }
        public AsyncCommand SaveNewTaskCommand { get; set; }
        public AsyncCommand<TaskModel> TaskListSelectionChangedCommand { get; set; }




        #endregion

        public MainViewModel()
        {
            AddNewTaskCommand = new DelegateCommand(AddNewTask);
            CancelNewTaskCommand = new DelegateCommand(CancelNewTask);
            SaveNewTaskCommand = new AsyncCommand(SaveNewTaskAsync);
            TaskListSelectionChangedCommand = new AsyncCommand<TaskModel>(TaskListSelectionChangedAsync);


            TypeTask = new List<string> { "ToDo", "Doing", "Done" };



            TodoTaskDataCollection = new ObservableCollection<TaskModel>();
            DoingTaskDataCollection = new ObservableCollection<TaskModel>();
            DoneTaskDataCollection = new ObservableCollection<TaskModel>();
        }

        #region Properties

        private bool _isModalVisible;
        public bool IsModalVisible
        {
            get => _isModalVisible;
            set
            {
                _isModalVisible = value;
                OnPropertyChanged(nameof(IsModalVisible));
            }
        }

        private string _taskTitle;
        public string TaskTitle
        {
            get { return _taskTitle; }
            set { _taskTitle = value; OnPropertyChanged(); }
        }

        private string _taskDescription;

        public string TaskDescription
        {
            get { return _taskDescription; }
            set { _taskDescription = value; OnPropertyChanged(); }
        }

        private List<string> _typeTask;

        public List<string> TypeTask
        {
            get { return _typeTask; }
            set { _typeTask = value; OnPropertyChanged(); }
        }

        private string _selectedTypeTask;

        public string SelectedTypeTask
        {
            get { return _selectedTypeTask; }
            set { _selectedTypeTask = value; OnPropertyChanged(); }
        }

        private string _dayInput;

        public string DayInput
        {
            get { return _dayInput; }
            set { _dayInput = value; OnPropertyChanged(); }
        }

        private string _monthInput;

        public string MonthInput
        {
            get { return _monthInput; }
            set { _monthInput = value; OnPropertyChanged(); }
        }

        private string _yearInput;

        public string YearInput
        {
            get { return _yearInput; }
            set { _yearInput = value; OnPropertyChanged(); }
        }

        private bool _readonly;

        public bool Readonly
        {
            get { return _readonly; }
            set { _readonly = value; OnPropertyChanged(); }
        }


        #region TODO properties
        private ObservableCollection<TaskModel> _TodoTaskDataCollection;

        public ObservableCollection<TaskModel> TodoTaskDataCollection
        {
            get { return _TodoTaskDataCollection; }
            set { _TodoTaskDataCollection = value; OnPropertyChanged(); }
        }
        private TaskModel _SelectedTodoTaskItem;

        public TaskModel SelectedTodoTaskItem
        {
            get { return _SelectedTodoTaskItem; }
            set { _SelectedTodoTaskItem = value; OnPropertyChanged(); }
        }

        private int _SelectedTodoTaskIndex;

        public int SelectedTodoTaskIndex
        {
            get { return _SelectedTodoTaskIndex; }
            set { _SelectedTodoTaskIndex = value; OnPropertyChanged(); }
        }
        #endregion

        #region DOING properties
        private ObservableCollection<TaskModel> _DoingTaskDataCollection;

        public ObservableCollection<TaskModel> DoingTaskDataCollection
        {
            get { return _DoingTaskDataCollection; }
            set { _DoingTaskDataCollection = value; OnPropertyChanged(); }
        }
        private TaskModel _SelectedDoingTaskItem;

        public TaskModel SelectedDoingTaskItem
        {
            get { return _SelectedDoingTaskItem; }
            set { _SelectedDoingTaskItem = value; OnPropertyChanged(); }
        }

        private int _SelectedDoingTaskIndex;

        public int SelectedDoingTaskIndex
        {
            get { return _SelectedDoingTaskIndex; }
            set { _SelectedDoingTaskIndex = value; OnPropertyChanged(); }
        }
        #endregion

        #region DONE properties
        private ObservableCollection<TaskModel> _DoneTaskDataCollection;

        public ObservableCollection<TaskModel> DoneTaskDataCollection
        {
            get { return _DoneTaskDataCollection; }
            set { _DoneTaskDataCollection = value; OnPropertyChanged(); }
        }
        private TaskModel _SelectedDoneTaskItem;

        public TaskModel SelectedDoneTaskItem
        {
            get { return _SelectedDoneTaskItem; }
            set { _SelectedDoneTaskItem = value; OnPropertyChanged(); }
        }

        private int _SelectedDoneTaskIndex;

        public int SelectedDoneTaskIndex
        {
            get { return _SelectedDoneTaskIndex; }
            set { _SelectedDoneTaskIndex = value; OnPropertyChanged(); }
        }
        #endregion


        #endregion

        #region Fields
        private const string Todo_Color = "LightYellow";
        private const string Doing_Color = "LightBlue";
        private const string Done_Color = "LightGreen";

        #endregion


        private async Task TaskListSelectionChangedAsync(TaskModel selectdata)
        {
            try
            {
                TaskModel applydata = await GetCollectionData(selectdata);

                TaskTitle = applydata.Titletask;
                TaskDescription = applydata.Taskdescription;
                SelectedTypeTask = applydata.Typetask;
                DayInput = applydata.Day;
                MonthInput = applydata.Month;
                YearInput = applydata.Year;

                Readonly = true;
                IsModalVisible = true;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

        }


        private async Task SaveNewTaskAsync()
        {
            try
            {
                if (SelectedTypeTask == null) { return; }

                TaskModel taskModel = new TaskModel
                {
                    Titletask = TaskTitle,
                    Taskdescription = TaskDescription,
                    Typetask = SelectedTypeTask,
                    Day = DayInput,
                    Month = MonthInput,
                    Year = YearInput,

                };

               
                TaskModel existingTask = await GetCollectionData(taskModel);

                if (existingTask != null)
                {
                    existingTask.Taskdescription = TaskDescription;
                    existingTask.Typetask = SelectedTypeTask;
                    existingTask.Day = DayInput;
                    existingTask.Month = MonthInput;
                    existingTask.Year = YearInput;


                    // Optionally, update color or other properties if needed
                    if (SelectedTypeTask == "ToDo") existingTask.Color = Todo_Color;
                    else if (SelectedTypeTask == "Doing") existingTask.Color = Doing_Color;
                    else if (SelectedTypeTask == "Done") existingTask.Color = Done_Color;
                    await RefreshTaskCollection(existingTask.Typetask);
                }
                else
                {

                    var taskMap = new Dictionary<string, (ObservableCollection<TaskModel>, string)>
                                {
                                    { "ToDo", (TodoTaskDataCollection, Todo_Color) },
                                    { "Doing", (DoingTaskDataCollection, Doing_Color) },
                                    { "Done", (DoneTaskDataCollection, Done_Color) }
                                };

                    // Check if the selected type exists in the map
                    if (taskMap.ContainsKey(SelectedTypeTask))
                    {
                        var (collection, color) = taskMap[SelectedTypeTask];
                        taskModel.Color = color;
                        collection.Add(taskModel);
                        await RefreshTaskCollection(SelectedTypeTask);
                    }

                    //// If it's a new task, add it to the corresponding collection
                    //if (SelectedTypeTask == "ToDo")
                    //{
                    //    taskModel.Color = Todo_Color;
                    //    TodoTaskDataCollection.Add(taskModel);
                    //}
                    //else if (SelectedTypeTask == "Doing")
                    //{
                    //    taskModel.Color = Doing_Color;
                    //    DoingTaskDataCollection.Add(taskModel);
                    //}
                    //else if (SelectedTypeTask == "Done")
                    //{
                    //    taskModel.Color = Done_Color;
                    //    DoneTaskDataCollection.Add(taskModel);
                    //}
                }

                
                CancelNewTask();

            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

        }

        public Task<TaskModel> GetCollectionData(TaskModel selectedTypeTask)
        {
            TaskModel modeldata = null;

            if (selectedTypeTask.Typetask == "ToDo")
            {
                modeldata = TodoTaskDataCollection.FirstOrDefault(t => t.Titletask == selectedTypeTask.Titletask);
            }
            else if (selectedTypeTask.Typetask == "Doing")
            {
                modeldata = DoingTaskDataCollection.FirstOrDefault(t => t.Titletask == selectedTypeTask.Titletask);
            }
            else if (selectedTypeTask.Typetask == "Done")
            {
                modeldata = DoneTaskDataCollection.FirstOrDefault(t => t.Titletask == selectedTypeTask.Titletask);
            }
            return Task.FromResult(modeldata);
        }


        private Task RefreshTaskCollection(string taskType)
        {
            // Trigger a refresh by resetting the collection or forcing the ListView to reload
            // This could be customized to your specific implementation.
            if (taskType == "ToDo")
            {
                TodoTaskDataCollection = new ObservableCollection<TaskModel>(TodoTaskDataCollection);
            }
            else if (taskType == "Doing")
            {
                DoingTaskDataCollection = new ObservableCollection<TaskModel>(DoingTaskDataCollection);
            }
            else if (taskType == "Done")
            {
                DoneTaskDataCollection = new ObservableCollection<TaskModel>(DoneTaskDataCollection);
            }

            return Task.CompletedTask;
        }

        private void AddNewTask()
        {
            Readonly = false;
            IsModalVisible = true;
        }

        private void CancelNewTask()
        {
            Readonly = false;
            ResetAll();
            IsModalVisible = false;
        }

        private void ResetAll()
        {
            TaskTitle = TaskDescription = string.Empty;
            SelectedTypeTask = null;
            DayInput = MonthInput = YearInput = string.Empty;
        }

    }
}
