using System;
using System.Dynamic;
using IOTLink.Addon.AcerControl.Common;
using IOTLinkAPI.Addons;
using IOTLinkAPI.Helpers;
using IOTLinkAPI.Platform.Events.MQTT;

namespace IOTLink.Addon.AcerControl.Service
{
    public class AcerControlService : ServiceAddon
    {
        public override void Init(IAddonManager addonManager)
        {
            base.Init(addonManager);

            GetManager().SubscribeTopic(this, "display/brightness", OnSetBrightnessMessage);
        }

        private void OnSetBrightnessMessage(object sender, MQTTMessageEventEventArgs e)
        {
            string data = e.Message.GetPayload();
            
            LoggerHelper.Verbose("OnSetBrightnessMessage: Message received: {0}", data);
            
            if (string.IsNullOrWhiteSpace(data))
                return;

            if (!double.TryParse(data, out double brightnessLevel) || brightnessLevel < 0 || brightnessLevel > 100)
                LoggerHelper.Error("OnSetBrightnessMessage failure: Invalid data. Expected: 0-100");

            LoggerHelper.Verbose("OnSetBrightnessMessage: Message received: {0}", data);

            try
            {
                dynamic addonData = new ExpandoObject();
                addonData.requestType = AddonRequestType.DISPLAY_SET_BRIGHTNESS;
                addonData.requestData = (int)brightnessLevel;

                GetManager().SendAgentRequest(this, addonData);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("OnSetBrightnessMessage failure: {0}", ex.Message);
            }
        }
    }
}