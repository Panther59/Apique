using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.Model
{
    public class TestClientModel
    {
        public int TestCaseIDCounter { get; set; }
        public int TestSuiteIDCounter { get; set; }

        public List<TestCaseModel> TestCases { get; set; }
        public List<TestSuiteModel> TestSuites { get; set; }
    }
}
