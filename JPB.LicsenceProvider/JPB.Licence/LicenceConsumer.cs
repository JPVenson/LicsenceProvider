using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPB.Licence
{
    public class LicenceConsumer
    {
        public LicenceConsumer()
        {

        }

        public event Func<string> OnUnknownAppKey;
        public event EventHandler OnInvalidAppKey;
        public event EventHandler OnInvalidLicenceKey;
        public event EventHandler OnLicenceReturned;
        public event EventHandler OnServerUnavailable;

        private string RaiseOnUnknownAppKey()
        {
            var handler = OnUnknownAppKey;
            if (handler != null)
            {
                return handler();
            }
            return null;
        }

        private void RaiseOnInvalidAppKey()
        {
            var handler = OnInvalidAppKey;
            if (handler != null)
            {
                handler(this, null);
            }
            else
            {
                HardReset();
            }
        }

        private void RaiseOnInvalidLicenceKey()
        {
            var handler = OnInvalidLicenceKey;
            if (handler != null)
            {
                handler(this, null);
            }
            else
            {
                HardReset();
            }
        }

        private void RaiseOnServerUnavailable()
        {
            var handler = OnServerUnavailable;
            if (handler != null)
            {
                handler(this, null);
            }
            else
            {
                HardReset();
            }
        }

        private void RaiseOnLicenceReturned()
        {
            var handler = OnLicenceReturned;
            if (handler != null)
            {
                handler(this, null);
            }
            else
            {
                HardReset();
            }
        }

        private void HardReset()
        {
            //TODO Remove files from dir
            Process.GetCurrentProcess().Kill();
        }

        public void CheckLicence(string appKey)
        {
            var statusCode = GetStatusCode(appKey);

            switch (statusCode)
            {
                case Common.LicenceReturnState.Valid:
                    return;
                case Common.LicenceReturnState.Invalid:
                    RaiseOnInvalidAppKey();
                    break;
                case Common.LicenceReturnState.InvalidKey:
                    RaiseOnInvalidLicenceKey();
                    break;
                case Common.LicenceReturnState.InvalidKeyReturned:
                    RaiseOnLicenceReturned();
                    break;
                case Common.LicenceReturnState.Returend:
                    RaiseOnLicenceReturned();
                    break;
                default:
                    RaiseOnServerUnavailable();
                    break;
            }
        }

        private Common.LicenceReturnState GetStatusCode(string appKey)
        {
            return Common.LicenceReturnState.Valid;
        }        
    }
}
