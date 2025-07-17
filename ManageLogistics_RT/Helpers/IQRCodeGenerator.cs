namespace ManageLogistics_RT.Helpers
{
    public interface IQRCodeGenerator
    {
        byte[] GenerateQRCode(string text);
    }
}
