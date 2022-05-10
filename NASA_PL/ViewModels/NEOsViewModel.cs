using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NASA_BE;
using NASA_BE.Annotations;
using NASA_PL.Commands;
using NASA_PL.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using TextBox = System.Windows.Controls.TextBox;

namespace NASA_PL.ViewModels
{
    public class NEOsViewModel : INotifyPropertyChanged
    {
        private string _start;
        private string _end;
        private int _diameter;

        private bool _hazardous;
        public bool Hazardous
        {
            get => _hazardous;
            set
            {
                _hazardous = value;
                OnPropertyChanged(nameof(Hazardous));
            }
        }

        public ICommand Filter { get; set; }
        public ICommand UpdateStartDateCommand { get; set; }
        public ICommand UpdateEndDateCommand { get; set; }
        public ICommand UpdateDiameterCommand { get; set; }
        public ICommand UpdateIsHazardousCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }

        public IAsyncRelayCommand SearchNeosCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NEOsModel _model;

        public NEOsViewModel()
        {
            _model = new NEOsModel();
            Filter = new FilterCommand(this);

            UpdateStartDateCommand = new RelayCommand<DatePicker>(picker =>
            {
                _start = DateTime.Parse(picker.Text).ToString("yyyy-MM-dd");
            });

            UpdateEndDateCommand = new RelayCommand<DatePicker>(picker =>
            {
                _end = DateTime.Parse(picker.Text).ToString("yyyy-MM-dd");
            });

            UpdateDiameterCommand = new RelayCommand<TextBox>(textBox =>
            {
                if (textBox.Text != string.Empty)
                {
                    int.TryParse(textBox.Text, out _diameter);
                }
            }, textBox => textBox.Text != string.Empty);

            UpdateIsHazardousCommand = new RelayCommand<ToggleButton>(toggle =>
            {
                if (toggle.IsChecked != null)
                {
                    _hazardous = toggle.IsChecked.Value;
                }
            });

            ExportToExcelCommand = new RelayCommand(() =>
            {
                ExportToExcel(_nearEarthObj);
            });

            SearchNeosCommand = new AsyncRelayCommand(async () =>
            {
                ShowDataGrid = Visibility.Collapsed;
                NearEarthObj = new ObservableCollection<NearEarthObject>();
                await Task.Run(() => SearchNeo(_start, _end, _diameter, _hazardous));
                ShowDataGrid = Visibility.Visible;
            }, _start != string.Empty && _end != string.Empty);
        }

        private ObservableCollection<NearEarthObject> _nearEarthObj;
        public ObservableCollection<NearEarthObject> NearEarthObj
        {
            get => _nearEarthObj;
            set
            {
                _nearEarthObj = value;
                OnPropertyChanged(nameof(NearEarthObj));
                CanExport = _nearEarthObj is { Count: > 0 };
                OnPropertyChanged(nameof(CanExport));
            }
        }

        private bool _canExport;
        public bool CanExport
        {
            get => _canExport;
            set
            {
                _canExport = value;
                OnPropertyChanged(nameof(CanExport));
            }
        }

        private Visibility _showDataGrid;
        public Visibility ShowDataGrid
        {
            get => _showDataGrid;
            set
            {
                _showDataGrid = value;
                OnPropertyChanged(nameof(ShowDataGrid));
            }
        }


        // This function is working but needs some improvements
        private void ExportToExcel(ObservableCollection<NearEarthObject> nearEarthObj)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excel.Workbooks.Add(Type.Missing);
            Worksheet worksheet = workbook.ActiveSheet;
            
            worksheet.Name = "NEOs";

            // Fill in the header of the Excel file
            worksheet.Cells[1, 1] = "Name";
            worksheet.Cells[1, 2] = "Id";
            worksheet.Cells[1, 3] = "Diameter";
            worksheet.Cells[1, 4] = "Velocity";
            worksheet.Cells[1, 5] = "Hazardous";
            worksheet.Cells[1, 6] = "CloseApproach";
            worksheet.Cells[1, 7] = "MissDistance";

            // Fill in the data
            for (var i = 0; i < nearEarthObj.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = nearEarthObj[i].Name;
                worksheet.Cells[i + 2, 2] = nearEarthObj[i].Id;
                worksheet.Cells[i + 2, 3] = nearEarthObj[i].Diameter;
                worksheet.Cells[i + 2, 4] = nearEarthObj[i].Velocety;
                worksheet.Cells[i + 2, 5] = nearEarthObj[i].Hazardous;
                worksheet.Cells[i + 2, 6] = nearEarthObj[i].CloseApproach;
                worksheet.Cells[i + 2, 7] = nearEarthObj[i].MissDistance;
            }

            
            excel.DefaultFilePath = @"C:\";

            excel.Quit();

            // release the COM objects (very important!)
            Marshal.ReleaseComObject(worksheet);
        }


        public async Task SearchNeo(string start, string end, double diameter, bool hazardous)
        {
            NearEarthObj = await _model.GetNearEarthObject(start, end, diameter);
            HazardOnly(hazardous);
        }

        public void HazardOnly(bool hazardous = false)
        {
            if (_model.NeoList == null)
            {
                return;
            }
            //NearEarthObj = hazardous ? _model.neoList.Where(neo => neo.Hazardous).ToList() : _model.neoList.ToList();
            if (NearEarthObj is not null)
            {
                NearEarthObj = new ObservableCollection<NearEarthObject>(hazardous ?
                        _model.NeoList.Where(neo => neo.Hazardous).ToList()
                        :
                        _model.NeoList.ToList());
            }
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

