namespace FhictPowerTools.Core.VpnClient
{
    public interface IVpnClient
    {
        void Connect(string host);
        void Disconnect();
        bool IsConnected();
    }
}