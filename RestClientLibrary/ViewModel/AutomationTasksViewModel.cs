namespace RestClientLibrary.ViewModel
{
    using System.Collections.ObjectModel;
    using DataLibrary;
    using RestClientLibrary.ViewModel.Automations;

    /// <summary>
    /// Defines the <see cref = "AutomationTasksViewModel"/>
    /// </summary>
    public class AutomationTasksViewModel : BaseViewModel
    {
        /// <summary>
        /// The availableTasks field
        /// </summary>
        private ObservableCollection<string> availableTasks;

        /// <summary>
        /// The moveBottomCommand field
        /// </summary>
        private RelayCommand moveBottomCommand;

        /// <summary>
        /// The moveDownCommand field
        /// </summary>
        private RelayCommand moveDownCommand;

        /// <summary>
        /// The moveTopCommand field
        /// </summary>
        private RelayCommand moveTopCommand;

        /// <summary>
        /// The moveUpCommand field
        /// </summary>
        private RelayCommand moveUpCommand;

        /// <summary>
        /// The selectedAvailableTask field
        /// </summary>
        private string selectedAvailableTask;

        /// <summary>
        /// The selectedAvailableTaskChangedCommand field
        /// </summary>
        private RelayCommand selectedAvailableTaskChangedCommand;

        /// <summary>
        /// The selectedUsedTask field
        /// </summary>
        private BaseAutomationTaskViewModel selectedUsedTask;

        /// <summary>
        /// The usedTasks field
        /// </summary>
        private ObservableCollection<BaseAutomationTaskViewModel> usedTasks;

        /// <summary>
        /// Gets or sets the AvailableTasks
        /// </summary>
        public ObservableCollection<string> AvailableTasks
        {
            get
            {
                return this.availableTasks;
            }

            set
            {
                this.availableTasks = value;
                this.OnPropertyChanged("AvailableTasks");
            }
        }

        /// <summary>
        /// Gets the MoveBottomCommand
        /// </summary>
        public RelayCommand MoveBottomCommand
        {
            get
            {
                if (this.moveBottomCommand == null)
                {
                    this.moveBottomCommand = new RelayCommand(command => this.ExecuteMoveBottom(), can => this.CanMoveBottomExecute());
                }

                return this.moveBottomCommand;
            }
        }

        /// <summary>
        /// Gets the MoveDownCommand
        /// </summary>
        public RelayCommand MoveDownCommand
        {
            get
            {
                if (this.moveDownCommand == null)
                {
                    this.moveDownCommand = new RelayCommand(command => this.ExecuteMoveDown(), can => this.CanMoveDownExecute());
                }

                return this.moveDownCommand;
            }
        }

        /// <summary>
        /// Gets the MoveTopCommand
        /// </summary>
        public RelayCommand MoveTopCommand
        {
            get
            {
                if (this.moveTopCommand == null)
                {
                    this.moveTopCommand = new RelayCommand(command => this.ExecuteMoveTop(), can => this.CanMoveTopExecute());
                }

                return this.moveTopCommand;
            }
        }

        /// <summary>
        /// Gets the MoveUpCommand
        /// </summary>
        public RelayCommand MoveUpCommand
        {
            get
            {
                if (this.moveUpCommand == null)
                {
                    this.moveUpCommand = new RelayCommand(command => this.ExecuteMoveUp(), can => this.CanMoveUpExecute());
                }

                return this.moveUpCommand;
            }
        }

        /// <summary>
        /// Gets or sets the SelectedAvailableTask
        /// </summary>
        public string SelectedAvailableTask
        {
            get
            {
                return this.selectedAvailableTask;
            }

            set
            {
                this.selectedAvailableTask = value;
                this.OnPropertyChanged("SelectedAvailableTask");
            }
        }

        /// <summary>
        /// Gets the SelectedAvailableTaskChangedCommand
        /// </summary>
        public RelayCommand SelectedAvailableTaskChangedCommand
        {
            get
            {
                if (this.selectedAvailableTaskChangedCommand == null)
                {
                    this.selectedAvailableTaskChangedCommand = new RelayCommand(command => this.ExecuteSelectedAvailableTaskChanged());
                }

                return this.selectedAvailableTaskChangedCommand;
            }
        }

        /// <summary>
        /// Gets or sets the SelectedUsedTask
        /// </summary>
        public BaseAutomationTaskViewModel SelectedUsedTask
        {
            get
            {
                return this.selectedUsedTask;
            }

            set
            {
                this.selectedUsedTask = value;
                this.OnPropertyChanged("SelectedUsedTask");
            }
        }

        /// <summary>
        /// Gets or sets the UsedTasks
        /// </summary>
        public ObservableCollection<BaseAutomationTaskViewModel> UsedTasks
        {
            get
            {
                return this.usedTasks;
            }

            set
            {
                this.usedTasks = value;
                this.OnPropertyChanged("UsedTasks");
            }
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        public void LoadData()
        {
            this.AvailableTasks = new ObservableCollection<string>();
            this.AvailableTasks.Add("Select");
            this.AvailableTasks.Add("Set Global Variable");

            this.SelectedAvailableTask = this.AvailableTasks[0];
        }

        /// <summary>
        /// Determines whether MoveBottom can be executed or not
        /// </summary>
        private bool CanMoveBottomExecute()
        {
            if (SelectedUsedTask != null)
            {
                var index = this.UsedTasks.IndexOf(this.SelectedUsedTask);
                if (index != -1 && index < this.UsedTasks.Count - 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether MoveDown can be executed or not
        /// </summary>
        private bool CanMoveDownExecute()
        {
            if (SelectedUsedTask != null)
            {
                var index = this.UsedTasks.IndexOf(this.SelectedUsedTask);
                if (index != -1 && index < this.UsedTasks.Count - 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether MoveTop can be executed or not
        /// </summary>
        private bool CanMoveTopExecute()
        {
            if (SelectedUsedTask != null)
            {
                var index = this.UsedTasks.IndexOf(this.SelectedUsedTask);
                if (index != -1 && index != 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether MoveUp can be executed or not
        /// </summary>
        private bool CanMoveUpExecute()
        {
            if (SelectedUsedTask != null)
            {
                var index = this.UsedTasks.IndexOf(this.SelectedUsedTask);
                if (index != -1 && index != 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Executes MoveBottom
        /// </summary>
        private void ExecuteMoveBottom()
        {
        }

        /// <summary>
        /// Executes MoveDown
        /// </summary>
        private void ExecuteMoveDown()
        {
        }

        /// <summary>
        /// Executes MoveTop
        /// </summary>
        private void ExecuteMoveTop()
        {
        }

        /// <summary>
        /// Executes MoveUp
        /// </summary>
        private void ExecuteMoveUp()
        {
        }

        /// <summary>
        /// Executes SelectedAvailableTaskChanged
        /// </summary>
        private void ExecuteSelectedAvailableTaskChanged()
        {
            if (SelectedAvailableTask != null && SelectedAvailableTask == "Set Global Variable")
            {
                if (this.UsedTasks == null)
                {
                    this.UsedTasks = new ObservableCollection<BaseAutomationTaskViewModel>();
                }

                var newTask = new SetGlobalVariableTaskViewModel();
                this.UsedTasks.Add(newTask);

                this.SelectedUsedTask = newTask;
            }
        }
    }
}
