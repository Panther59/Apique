namespace RestClientLibrary
{
    public class RuntimeExecution
    {
        public void PreExecution(IRestClientPreExecutionAutomation restClient)
        {
            restClient.ClearGlobalVariable("Client");
        }

        public void PostExecution(IRestClientPostExecutionAutomation restClient)
        {
            restClient.ClearGlobalVariable("Client");
        }
    }
}