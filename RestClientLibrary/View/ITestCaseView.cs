using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.View
{
    public interface ITestCaseView : IBaseView
    {
        IRestClientView RestClientView { get; }
    }
}
