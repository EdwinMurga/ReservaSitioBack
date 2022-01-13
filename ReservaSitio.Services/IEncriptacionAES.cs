namespace ReservaSitio.Services
{
    public interface IEncriptacionAES
    {
        string Encriptar(string texto, string key);
    }
}