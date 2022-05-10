using CommunityToolkit.Mvvm.Input;
using NASA_BE.Annotations;
using NASA_PL.Models;
using NASA_PL.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace NASA_PL.ViewModels
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        MainWindowView wnd = (MainWindowView)Application.Current.MainWindow;

        public event PropertyChangedEventHandler PropertyChanged;

        private LoginWindowModel _model;

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public LoginWindowViewModel()
        {
            _model = new LoginWindowModel();

            CloseCommand = new RelayCommand<Window>(window =>
            {
                window.Close();
                wnd.Close();
            });

            LoginCommand = new RelayCommand<Window>(window =>
            {
                if (_model.CheckUserAndPassword(Username, Password))
                {
                    window.Close();
                    wnd.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
            });
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}