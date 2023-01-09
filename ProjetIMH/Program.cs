// See https://aka.ms/new-console-template for more information
using ProjetIMH;
using ProjetIMH.Enums;

EChoices userChoice = default;
var programmContinue = true;

var ihm = new ConsoleIHM();

do
{
    userChoice = ihm.ShowMenuInterface();

    switch (userChoice)
    {
        case EChoices.Create:
            ihm.ShowCreateInterface();
            break;
        case EChoices.Update:
            ihm.ShowUpdateInterface();
            break;
        case EChoices.Delete:
            ihm.ShowDeleteInterface();
            break;
        case EChoices.ReadOne:
            ihm.ShowReadOneInterface();
            break;
        case EChoices.ReadAll:
            ihm.ShowReadAllInterface();
            break;
        case EChoices.Stop:
            programmContinue = false;
            break;
        default:
            break;
    }
}
while (programmContinue);