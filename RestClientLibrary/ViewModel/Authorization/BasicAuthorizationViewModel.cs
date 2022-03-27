namespace RestClientLibrary.ViewModel.Authorization
{
    using System;
    using System.Text;

    /// <summary>
    /// Defines the <see cref = "BasicAuthorizationViewModel"/>
    /// </summary>
    public class BasicAuthorizationViewModel : BaseAuthorizationViewModel
    {
        /// <summary>
        /// The passwords field
        /// </summary>
        private string passwords;

        /// <summary>
        /// The username field
        /// </summary>
        private string username;

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password
        {
            get
            {
                return this.passwords;
            }

            set
            {
                this.passwords = value;
                this.OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                this.username = value;
                this.OnPropertyChanged("Username");
            }
        }

        /// <inheritdoc/>
        public override KeyValueViewModel GetAuthorizationHeader()
        {
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            return new KeyValueViewModel()
            { Key = "Authorization", Value = "Basic " + Convert.ToBase64String(encoding.GetBytes(this.Username + ":" + this.Password)) };
        }

        /// <inheritdoc/>
        public override void SetAuthorizationHeader(KeyValueViewModel header)
        {
            if (header != null && header.Key.Equals("Authorization", StringComparison.CurrentCultureIgnoreCase) && header.Value.StartsWith("Basic "))
            {
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                try
                {
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(header.Value.Replace("Basic ", string.Empty)));
                    string[] parts = usernamePassword.Split(':');
                    if (parts.Length == 2)
                    {
                        this.Username = parts[0];
                        this.Password = parts[1];
                    }
                }
                catch (Exception ex)
                {
                    this.Username = string.Empty;
                    this.Password = string.Empty;
                    this.LogException(ex);
                }
            }
        }

        public override bool IsValidAuthorization(KeyValueViewModel header)
        {
            if (header != null && header.Key.Equals("Authorization", StringComparison.CurrentCultureIgnoreCase) && header.Value.StartsWith("Basic "))
            {
                return true;
            }

            return false;
        }
    }
}
