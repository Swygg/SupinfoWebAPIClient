using ProjetIMH.Enums;

namespace ProjetIMH.Interfaces
{
    public interface IConsoleIHM
    {
        void ShowCreateInterface();
        void ShowReadAllInterface();
        void ShowReadOneInterface();
        void ShowUpdateInterface();
        void ShowDeleteInterface();
        EChoices ShowMenuInterface();
    }
}
