namespace BudgetFirst.ViewModel.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;


    /// <summary>
    /// A base ViewModel that has a header and knows how to close itself, 
    /// to represent windows or closeable tabs.
    /// </summary>
    public class CloseableViewModel : ViewModelBase
    {
        private string header;

        public string Header
        {
            get { return this.header; }
            set
            {
                this.header = value; RaisePropertyChanged();
            }
        }
        private bool closeable;

        public bool Closeable
        {
            get { return this.closeable; }
            set { this.closeable = value; CloseCommand.RaiseCanExecuteChanged(); RaisePropertyChanged(); }
        }

        public CloseableViewModel()
        {
            CloseCommand = new RelayCommand(() => Close(), () => Closeable);
            Closeable = true;
        }

        public RelayCommand CloseCommand { get; set; }

        private void Close()
        {
            RaiseRequestClose();
        }

        public event EventHandler RequestClose;

        public virtual void RaiseRequestClose()
        {
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

    }
}


