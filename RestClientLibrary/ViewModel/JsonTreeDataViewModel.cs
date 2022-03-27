using DataLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestClientLibrary.ViewModel
{
    public class JsonTreeDataViewModel : BaseViewModel
    {
        public JsonTreeDataViewModel()
        {
            IsExpanded = true;
            Key = Guid.NewGuid().ToString();
        }

        #region Properties

        public string Key { get; set; }
        public string Property { get; set; }
        public string Text { get; set; }
        public JsonTag Tag { get; set; }
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        
        private List<JsonTreeDataViewModel> _nodes;
        public List<JsonTreeDataViewModel> Nodes
        {
            get
            {
                if (_nodes == null)
                {
                    _nodes = new List<JsonTreeDataViewModel>();
                }
                return _nodes;
            }
            set
            {
                _nodes = value;
            }
        }
        public JsonTreeDataViewModel Parent { get; set; }

        private RelayCommand _copyNodeCommand;
        public RelayCommand CopyNodeCommand
        {
            get
            {
                if (_copyNodeCommand == null)
                {
                    _copyNodeCommand = new RelayCommand(command => CopyNode(), can => CanCopy());
                }
                return _copyNodeCommand;
            }
            set { _copyNodeCommand = value; }
        }

        private RelayCommand _copyNodeWithHeaderCommand;
        public RelayCommand CopyNodeWithHeaderCommand
        {
            get
            {
                if (_copyNodeWithHeaderCommand == null)
                {
                    _copyNodeWithHeaderCommand = new RelayCommand(command => CopyNodeWithHeader(), can => CanCopy());
                }
                return _copyNodeWithHeaderCommand;
            }
            set { _copyNodeWithHeaderCommand = value; }
        }

        private bool CanCopy()
        {
            return this.Tag.TokenType == JsonToken.PropertyName && (this.Nodes == null || this.Nodes.Count == 0);
        }

        #endregion

        private void CopyNodeWithHeader()
        {
            try
            {
                Clipboard.SetText(Property + Text);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        private string GenerateText()
        {
            StringBuilder sb = new StringBuilder();
            return sb.ToString();

        }

        private void CopyNode()
        {
            try
            {
                Clipboard.SetText(Text);

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        public static List<JsonTreeDataViewModel> JsonToTreeview(string json)
        {
            JsonTreeDataViewModel parentNode = new JsonTreeDataViewModel();
            parentNode.Nodes = new List<JsonTreeDataViewModel>();
            JsonTreeDataViewModel baseData = parentNode;

            try
            {
                JsonTreeDataViewModel current = null;
                var reader = new JsonTextReader(new StringReader(json));
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.None:
                            break;
                        case JsonToken.StartObject:
                            if (current == null || (current.Tag != null && current.Tag.TokenType != JsonToken.PropertyName))
                            {
                                current = new JsonTreeDataViewModel() { Property = "{}", Tag = new JsonTag(reader) };
                                current.Parent = parentNode;
                                parentNode.Nodes.Add(current);
                                parentNode = current;
                            }
                            else
                            {
                                parentNode = current;
                                current = new JsonTreeDataViewModel();
                                current.Tag = new JsonTag(reader);
                                current.Parent = parentNode;
                            }

                            break;
                        case JsonToken.StartArray:

                            if (current != null && current.Tag != null && current.Tag.TokenType == JsonToken.PropertyName)
                            {
                                current.Property += " []";
                                parentNode = current;
                                current.Tag.TokenType = JsonToken.StartArray;
                            }
                            else
                            {
                                current = new JsonTreeDataViewModel() { Property = "[]", Tag = new JsonTag(reader) };

                                current.Parent = parentNode;
                                parentNode.Nodes.Add(current);
                                parentNode = current;
                            }
                            break;
                        case JsonToken.StartConstructor:
                            break;
                        case JsonToken.PropertyName:
                            current = new JsonTreeDataViewModel();
                            current.Tag = new JsonTag(reader);
                            current.Property = "\"" + reader.Value + "\" : ";
                            current.Parent = parentNode;
                            parentNode.Nodes.Add(current);
                            break;
                        case JsonToken.Comment:
                            break;
                        case JsonToken.Raw:
                            break;
                        case JsonToken.Date:
                        case JsonToken.Integer:
                        case JsonToken.Float:
                        case JsonToken.Boolean:
                        case JsonToken.String:
                            var readerValue = "";
                            if (reader.TokenType == JsonToken.String)
                                readerValue = "\"" + reader.Value + "\"";
                            else
                                readerValue = reader.Value.ToString();

                            current.Text = readerValue;
                            break;
                        case JsonToken.Bytes:
                            break;
                        case JsonToken.Null:
                            break;
                        case JsonToken.Undefined:
                            break;
                        case JsonToken.EndObject:
                            current = current.Parent;
                            if (current.Parent != null)
                            {
                                parentNode = current.Parent;
                            }
                            break;
                        case JsonToken.EndArray:
                            if (current != null && current.Tag != null && current.Tag.TokenType == JsonToken.StartArray)
                            {
                                // This means there is a empty list
                                current.Property = current.Property.Insert(current.Property.Length - 1, current.Nodes.Count.ToString());
                                if (current.Parent != null)
                                {
                                    parentNode = current.Parent;
                                }
                                else
                                    parentNode.Nodes = current.Nodes;
                            }
                            else
                            {
                                current = current.Parent;
                                if (current.Property != null && current.Nodes != null && current.Nodes.Count > 0)
                                {
                                    current.Property = current.Property.Insert(current.Property.Length - 1, current.Nodes.Count.ToString());

                                    for (int i = 0; i < current.Nodes.Count; i++)
                                    {
                                        current.Nodes[i].Property = "-" + i + ": " + current.Nodes[i].Property;
                                    }
                                }
                                if (current.Parent != null)
                                {
                                    parentNode = current.Parent;
                                }
                                else
                                    parentNode.Nodes = current.Nodes;
                            }
                            break;
                        case JsonToken.EndConstructor:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
            return baseData.Nodes;
        }

        public class JsonTag
        {
            public JsonTag(JsonReader reader)
            {
                TokenType = reader.TokenType;
                Value = reader.Value;
                ValueType = reader.ValueType;
            }

            public JsonToken TokenType { get; set; }
            public object Value { get; set; }
            public Type ValueType { get; set; }
        }

    }
}
