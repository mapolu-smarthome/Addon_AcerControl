using System.Runtime;

namespace IOTLink.Addon.AcerControl.Common
{
    public class SetBrightnessRequest
    {
        public double Brightness { get; set; }

        public int DisplayIndex { get; set; }
    }
}