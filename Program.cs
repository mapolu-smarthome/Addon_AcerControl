using IOTLink.Addon.AcerControl.AcerDisplay;

namespace IOTLink.Addon.AcerControl
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new Display();

            display.Init();

            display.SetBrightness(80);
        }
    }
}
