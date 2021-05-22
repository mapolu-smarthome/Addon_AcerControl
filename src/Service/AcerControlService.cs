using System;
using System.Dynamic;
using IOTLink.Addon.AcerControl.Common;
using IOTLinkAPI.Addons;
using IOTLinkAPI.Helpers;
using IOTLinkAPI.Platform.Events.MQTT;
using Newtonsoft.Json;

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
            var payload = e.Message.GetPayload();
            
            LoggerHelper.Verbose("OnSetBrightnessMessage: Payload received: {0}", payload);
            
            var data = JsonConvert.DeserializeObject<SetBrightnessRequest>(payload);
            
            if (data.Brightness is < 0 or > 100)
                LoggerHelper.Error("OnSetBrightnessMessage failure: Invalid data. Expected: 0-100");

            try
            {
                dynamic addonData = new ExpandoObject();
                addonData.requestType = AddonRequestType.DISPLAY_SET_BRIGHTNESS;
                addonData.requestData = data;

                GetManager().SendAgentRequest(this, addonData);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("OnSetBrightnessMessage failure: {0}", ex.Message);
            }
        }
    }
}