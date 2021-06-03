namespace FhictPowerTools.Core.FhictVpn
{
    public interface IFhictVpn
    {
        void Connect(string host);
        void Disconnect();
        bool IsConnected();
    }
}