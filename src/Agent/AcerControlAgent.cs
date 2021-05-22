using IOTLink.Addon.AcerControl.Common;
using IOTLink.Addon.AcerControl.Display;
using IOTLinkAPI.Addons;
using IOTLinkAPI.Helpers;
using IOTLinkAPI.Platform.Events;

namespace IOTLink.Addon.AcerControl.Agent
{
    public class AcerControlAgent : AgentAddon
    {
        private DisplayController _displayController;

        public override void Init(IAddonManager addonManager)
        {
            base.Init(addonManager);

            OnConfigReloadHandler += OnConfigReload;
            OnAgentRequestHandler += OnAgentRequest;

            _displayController = new DisplayController();
            _displayController.Init();
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
                    SetBrightnessRequest request = e.Data.requestData.ToObject<SetBrightnessRequest>();
                    SetBrightness(request);
                    break;

                default: break;
            }
        }

        private void SetBrightness(SetBrightnessRequest request)
        {
            LoggerHelper.Verbose("AcerControlAgent::SetBrightness - b: {0} idx: {1}", request.Brightness, request.DisplayIndex);

            _displayController.SetBrightness(request.DisplayIndex, (int)request.Brightness);
        }
    }
}
