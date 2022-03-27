using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.Model
{
    public class TestSuiteModel
    {
        public string TestSuiteID { get; set; }
        public string Title { get; set; }
        public List<KeyValueModel> Variables { get; set; }
        public List<TestCaseModel> TestCases { get; set; }

        public static TestSuiteModel Parse(ViewModel.TestSuiteViewModel vm)
        {
            if (vm == null)
            {
                return null;
            }

            return new TestSuiteModel()
            {
                TestSuiteID = vm.TestSuiteID,
                Title = vm.Title,
                Variables = KeyValueModel.Parse(vm.Variables),
                TestCases = TestCaseModel.Parse(vm.TestCasesSelected)
            };
        }

    }
}
