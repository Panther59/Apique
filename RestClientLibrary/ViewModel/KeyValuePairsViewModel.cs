namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref = "KeyValuePairsViewModel"/>
    /// </summary>
    public class KeyValuePairsViewModel : BaseViewModel
    {
        /// <summary>
        /// Defines the _addNewCommand
        /// </summary>
        private RelayCommand _addNewCommand;

        /// <summary>
        /// The items field
        /// </summary>
        private ObservableCollection<KeyValueViewModel> items;

        /// <summary>
        /// The removeClickedCommand field
        /// </summary>
        private RelayCommand<KeyValueViewModel> removeClickedCommand;

        /// <summary>
        /// Gets or sets the AddNewCommand
        /// </summary>
        public RelayCommand AddNewCommand
        {
            get
            {
                if (_addNewCommand == null)
                {
                    _addNewCommand = new RelayCommand(command => AddNew());
                }

                return _addNewCommand;
            }

            set
            {
                _addNewCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the Items
        /// </summary>
        public ObservableCollection<KeyValueViewModel> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                this.items = value;
                this.OnPropertyChanged("Items");
            }
        }

        /// <summary>
        /// Gets the RemoveClickedCommand
        /// </summary>
        public RelayCommand<KeyValueViewModel> RemoveClickedCommand
        {
            get
            {
                if (this.removeClickedCommand == null)
                {
                    this.removeClickedCommand = new RelayCommand<KeyValueViewModel>(command => this.ExecuteRemoveClicked(command));
                }

                return this.removeClickedCommand;
            }
        }

        /// <summary>
        /// The Add New item
        /// </summary>
        private void AddNew()
        {
            try
            {
                if (this.Items == null || this.Items.Count == 0)
                {
                    this.Items = new ObservableCollection<KeyValueViewModel>();
                }

                KeyValueViewModel header = new KeyValueViewModel();
                this.Items.Add(header);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Executes RemoveClicked
        /// </summary>
        private void ExecuteRemoveClicked(KeyValueViewModel item)
        {
            this.Items.Remove(item);
        }

        public List<KeyValuePair<string, string>> GetItems()
        {
            if (this.Items != null)
            {
                return this.items
                    .Where(x => string.IsNullOrEmpty(x.Key) == false)
                    .Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
