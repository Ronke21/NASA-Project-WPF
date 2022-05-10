using NASA_PL.ViewModels;
using System;
using System.Windows.Input;

namespace NASA_PL.Commands
{
    public class FilterCommand : ICommand
    {
        private readonly NEOsViewModel _neosVm;

        public FilterCommand(NEOsViewModel viewModel)
        {
            _neosVm = viewModel;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var val = bool.Parse(parameter.ToString());
            if (val)
            {
                _neosVm.HazardOnly(true);
            }
            else
            {
                _neosVm.HazardOnly();
            }
        }
    }
}
