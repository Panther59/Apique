namespace RestClientLibrary.ViewModel.Authorization
{
    using System;
    using System.Text;

    /// <summary>
    /// Defines the <see cref = "NoAuthorizationViewModel"/>
    /// </summary>
    public class NoAuthorizationViewModel : BaseAuthorizationViewModel
    {
        /// <inheritdoc/>
        public override KeyValueViewModel GetAuthorizationHeader()
        {
            return null;
        }

        public override bool IsValidAuthorization(KeyValueViewModel header)
        {
            return false;
        }

        /// <inheritdoc/>
        public override void SetAuthorizationHeader(KeyValueViewModel header)
        {
        }
    }
}
