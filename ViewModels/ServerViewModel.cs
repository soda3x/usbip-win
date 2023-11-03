using cut_the_cord.Models;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cut_the_cord.ViewModels
{
    internal class ServerViewModel : INotifyPropertyChanged
    {

        private const string _REFRESH_TOOLTIP_STR = "Refresh connected devices. Please be warned that this will unshare any shared devices!";

        public string RefreshStr { get { return _REFRESH_TOOLTIP_STR; } }
        public ObservableCollection<UsbDevice> UsbDevices { get; set; }

        private UsbDevice _selectedDevice;
        public UsbDevice SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                if (_selectedDevice != value)
                {
                    _selectedDevice = value;
                    OnPropertyChanged(nameof(SelectedDevice));
                }
            }
        }

        private bool _IsItemSelected;

        public bool IsItemSelected
        {
            get { return _IsItemSelected; }
            set
            {
                _IsItemSelected = value;
                OnPropertyChanged(nameof(IsItemSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ServerViewModel()
        {
            UsbDevices = new ObservableCollection<UsbDevice>()
            {
                new UsbDevice(1, "A test usb device"),
                new UsbDevice(2, "Another test usb device"),
                new UsbDevice(3, "A third test usb device")
            };
        }

        public void ShareUsbDevice()
        {
            if (_selectedDevice != null && !_selectedDevice.IsShared)
            {
                Debug.WriteLine($"Sharing usb device with Bus ID {_selectedDevice.BusId}");
                _selectedDevice.SetShared(true);
            }
        }

        public void UnshareUsbDevice()
        {
            if (_selectedDevice != null && _selectedDevice.IsShared)
            {
                Debug.WriteLine($"Unsharing usb device with Bus ID {_selectedDevice.BusId}");
                _selectedDevice.SetShared(false);
            }
        }

        public void UnshareUsbDevice(UsbDevice dev)
        {
            if (dev.IsShared)
            {
                Debug.WriteLine($"Unsharing usb device with Bus ID {dev.BusId}");
                dev.SetShared(false);
            }
        }

        public void RefreshUsbDevices()
        {
            Debug.WriteLine("Refreshing USB Devices...");
            Debug.WriteLine("Unsharing currently shared devices first...");
            foreach (UsbDevice dev in UsbDevices)
            {
                UnshareUsbDevice(dev);
            }
        }
    }
}
