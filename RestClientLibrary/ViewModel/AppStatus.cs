namespace RestClientLibrary.ViewModel
{
	using System.Collections.ObjectModel;

	public class AppStatus
	{
		public ObservableCollection<RestClientViewModel> RestClients { get; set; }

		public string SelectedEnvironment { get; set; }

		public string SelectedRestClient { get; set; }

		public string SelectedWorkspace { get; set; }
	}
}
