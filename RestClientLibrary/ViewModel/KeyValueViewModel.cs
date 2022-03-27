using DataLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.ViewModel
{
    public class KeyValueViewModel : BaseViewModel
    {
        public delegate void OnRemoveHeaderHandler(KeyValueViewModel header);
        public event OnRemoveHeaderHandler Remove;

        public void RaiseRemoveEvent(KeyValueViewModel header)
        {
            if (Remove != null)
            {
                Remove(header);
            }
        }

        public KeyValueViewModel()
        {
            GUID = Guid.NewGuid().ToString();
        }

        private string _guid;
        public string GUID
        {
            get { return _guid; }
            private set { _guid = value; }
        }
        

        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged("Key");
            }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private RelayCommand _removeClickedCommand;
        public RelayCommand RemoveClickedCommand
        {
            get
            {
                if (_removeClickedCommand == null)
                {
                    _removeClickedCommand = new RelayCommand(command => RemoveClicked());
                }
                return _removeClickedCommand;
            }
            set { _removeClickedCommand = value; }
        }

        private void RemoveClicked()
        {
            RaiseRemoveEvent(this);
        }

        public static KeyValueViewModel Parse(Model.KeyValueModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new KeyValueViewModel()
            {
                GUID = input.GUID,
                Key = input.Key,
                Value = input.Value
            };
        }

        public static ObservableCollection<KeyValueViewModel> Parse(List<Model.KeyValueModel> list)
        {
            if (list == null)
            {
                return null;
            }

            var output = new List<KeyValueViewModel>();

            foreach (var item in list)
            {
                output.Add(KeyValueViewModel.Parse(item));
            }

            return new ObservableCollection<KeyValueViewModel>(output);
        }

        public Model.KeyValueModel ToModel()
        {
            return new Model.KeyValueModel()
            {
                GUID = this.GUID,
                Key = this.Key,
                Value = this.Value
            };
        }

    }
}
