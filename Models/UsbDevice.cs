using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cut_the_cord.Models
{
    internal class UsbDevice
    {
        private bool _IsShared;
        private int _Port;
        public int BusId { get; set; }
        public string Description { get; set; }

        public bool IsShared
        {
            get { return _IsShared; }
            set
            {
                if (_IsShared != value)
                {
                    _IsShared = value;
                    FriendlySharedStatus = value ? "Shared" : "Not Shared";
                }
            }
        }

        public string FriendlySharedStatus { get; private set; }

        public int Port
        {
            get { return _Port; }
            set
            {
                if (_Port != value)
                {
                    _Port = value;
                    FriendlyPort = value == 0 ? "--" : value.ToString();
                }
            }
        }

        public string FriendlyPort { get; private set; }

        public UsbDevice(int busId, string desc)
        {
            BusId = busId;
            Description = desc;
            // Default state of the USB Device is that it is not shared
            IsShared = false;
            FriendlySharedStatus = "Not Shared";
            // Port has to be set twice otherwise FriendlyPort doesn't get set to '--'
            // the first time around as Port would already be zero
            Port = -1;
            Port = 0;
        }

        public void SetShared(bool isShared)
        {
            IsShared = isShared;
        }

        public void SetPort(int port)
        {
            Port = port;
        }
    }
}
