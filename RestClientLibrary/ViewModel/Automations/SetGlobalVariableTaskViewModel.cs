using System;

namespace RestClientLibrary.ViewModel.Automations
{
    /// <summary>
    /// Defines the <see cref="SetGlobalVariableTaskViewModel" />
    /// </summary>
    public class SetGlobalVariableTaskViewModel : BaseAutomationTaskViewModel
    {
        /// <inheritdoc />
        public override string Name => "Set Global Variable";
        
        /// <summary>
                                                             /// The variableName field
                                                             /// </summary>
        private string variableName;

        /// <summary>
        /// The variableValue field
        /// </summary>
        private string variableValue;

        /// <summary>
        /// Gets or sets the VariableName
        /// </summary>
        public string VariableName
        {
            get
            {
                return this.variableName;
            }

            set
            {
                this.variableName = value;
                this.OnPropertyChanged("VariableName");
            }
        }

        /// <summary>
        /// Gets or sets the VariableValue
        /// </summary>
        public string VariableValue
        {
            get
            {
                return this.variableValue;
            }

            set
            {
                this.variableValue = value;
                this.OnPropertyChanged("VariableValue");
            }
        }
    }
}
