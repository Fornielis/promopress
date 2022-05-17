using WEB.ViewModels;

namespace WEB.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        bool Login(UsuarioViewModel ususario);
        void Sair();
    }
}
