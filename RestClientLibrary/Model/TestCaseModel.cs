using RestClientLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.Model
{
    public class TestCaseModel
    {
        public string TestCaseID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Request { get; set; }
        public string Header { get; set; }
        public List<ValidationRuleModel> Validations { get; set; }
        public List<KeyValueModel> Variables { get; set; }
        
        public static TestCaseModel Parse(TestCaseViewModel vm)
        {
            if (vm == null)
            {
                return null;
            }

            return new TestCaseModel()
            {
                TestCaseID = vm.TestCaseID,
                Title = vm.Title,
                Url = vm.RestClient.UrlBase.Url,
                Request = vm.RestClient.RequestContent,
                Header = vm.RestClient.HeadersBase.GetHeaderContent(),
                Variables = KeyValueModel.Parse(vm.Variables),
                Validations = ValidationRuleModel.Parse(vm.Validations)
            };
        }

        public static List<TestCaseModel> Parse(System.Collections.ObjectModel.ObservableCollection<TestCaseViewModel> inputList)
        {
            if (inputList == null)
            {
                return null;
            }

            List<TestCaseModel> outputList = new List<TestCaseModel>();
            foreach (var input in inputList)
            {
                outputList.Add(TestCaseModel.Parse(input));
            }
            return outputList;
        }
    }
}
