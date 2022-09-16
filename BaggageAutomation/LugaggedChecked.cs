using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronBarCode;

namespace BaggageAutomation
{
    internal class LugaggedChecked
    {
        private void QRGenerator()
        {
            QRCodeWriter.CreateQrCode("LuggageID| Airline| Owner| Location", 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPdf("NewQR.pdf");
        }
    }
}
