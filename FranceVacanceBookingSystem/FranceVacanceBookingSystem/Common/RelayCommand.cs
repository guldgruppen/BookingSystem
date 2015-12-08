using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication.ViewModel
{

    public class RelayCommand : ICommand
    {
        private Action _handler;

        public RelayCommand(Action handler)
        {
            _handler = handler;
        }

        public RelayCommand(Action handler, Func<bool> p) : this(handler)
        {
            this.p = p;
        }

        private bool _isEnabled;
        private Func<bool> p;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            //return IsEnabled;
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler();
        }
    }


}
