using IOTLink.Addon.AcerControl.Common;
using IOTLink.Addon.AcerControl.Display;
using IOTLinkAPI.Addons;
using IOTLinkAPI.Helpers;
using IOTLinkAPI.Platform.Events;

namespace IOTLink.Addon.AcerControl.Agent
{
    public class AcerControlAgent : AgentAddon
    {
        private DisplayItem _display;

        public override void Init(IAddonManager addonManager)
        {
            base.Init(addonManager);

            OnConfigReloadHandler += OnConfigReload;
            OnAgentRequestHandler += OnAgentRequest;

            _display = new DisplayItem();
            _display.Init();
        }

        private void OnConfigReload(object sender, ConfigReloadEventArgs e)
        {
            LoggerHelper.Verbose("AcerControlAgent::OnConfigReload");
        }

        private void OnAgentRequest(object sender, AgentAddonRequestEventArgs e)
        {
            LoggerHelper.Verbose("AcerControlAgent::OnAgentRequest");

            AddonRequestType requestType = e.Data.requestType;
            switch (requestType)
            {
                case AddonRequestType.DISPLAY_SET_BRIGHTNESS:
                    SetBrightness(e.Data.requestData);
                    break;

                default: break;
            }
        }

        private void SetBrightness(dynamic data)
        {
            LoggerHelper.Verbose("AcerControlAgent::SetBrightness - {0}", data);
            int level = data.ToObject<int>();

            _display.SetBrightness(level);
        }
    }
}
