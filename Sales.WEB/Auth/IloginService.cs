namespace Sales.WEB.Auth
{
    public interface ILoginService
    {
        Task LoginAsync(string token);   //adiciona el token

        Task LogoutAsync();// quita el token
    }
}
