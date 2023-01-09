using ProjetIMH.Enums;
using ProjetIMH.Interfaces;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;
using ProjectHttpClient;

namespace ProjetIMH
{
    internal class ConsoleIHM : IConsoleIHM
    {
        private HttpClient client;
        private HttpClientSpecific<Customer, CustomerCreateInput, CustomerUpdateInput> client2;
        public ConsoleIHM()
        {
            client = new HttpClient();
            client2 = new HttpClientSpecific<Customer, CustomerCreateInput, CustomerUpdateInput>(
                "https://localhost:7093/Customers/",
                "Create",
                "Update",
                "Delete?id=",
                "ReadOne?id=",
                "ReadAll");
        }

        public EChoices ShowMenuInterface()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Menu");
                foreach (var choice in Enum.GetValues(typeof(EChoices)))
                {
                    Console.WriteLine($"{(int)choice} - {choice}");
                }
                Console.WriteLine("Please select your choice");
                Console.WriteLine();
                var userChoiceRaw = Console.ReadKey().KeyChar;
                Console.WriteLine();
                try
                {
                    var userChoice = int.Parse(userChoiceRaw.ToString());
                    var nbChoices = Enum.GetValues(typeof(EChoices)).Length;
                    if (userChoice >= 0 && userChoice <= nbChoices)
                    {
                        return (EChoices)userChoice;
                    }
                    else
                    {
                        throw new Exception("This choice doesn't existe");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }

            } while (true);
        }


        public void ShowCreateInterface()
        {
            Console.Clear();
            Console.WriteLine("===Creation===");
            var firstName = GetString("Please enter the first name");
            var lastName = GetString("Please enter the last name");

            var input = new CustomerCreateInput()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            var success = client2.Create(input);
            if (success)
                Console.WriteLine("Customer created");
            else
                Console.WriteLine("Internal server Error");
            PressAKey();
        }

        public void ShowUpdateInterface()
        {
            Console.Clear();
            Console.WriteLine("===Update===");
            var id = GetInt("Please enter the id");
            var customer = client2.ReadOne(id);
            if (customer == null)
            {
                Console.WriteLine("This customer doesn't existe");
                PressAKey();
                return;
            }
            var firstName = GetString("Please enter the new first name");
            var lastName = GetString("Please enter the new last name");

            var input = new CustomerUpdateInput()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
            };
            var response = client2.Update(input);
            if (response)
            {
                Console.WriteLine("Customer udpated");
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }
            PressAKey();
        }

        public void ShowDeleteInterface()
        {
            Console.Clear();
            Console.WriteLine("===Delete by id===");
            var id = GetInt("Please enter the id");
            var customer = client2.ReadOne(id);
            if (customer == null)
            {
                Console.WriteLine("This customer doesn't existe");
                PressAKey();
                return;
            }
            var response = client2.Delete(id);
            if (response)
            {
                Console.WriteLine("Customer removed");
                PressAKey();
            }
            else
            {
                Console.WriteLine("Internal server Error");
                PressAKey();
            }
        }

        public void ShowReadOneInterface()
        {
            int id = GetInt("Please write the id");
            Console.Clear();
            Console.WriteLine("===Show One===");
            var c = client2.ReadOne(id);
            if (c != null)
            {
                Console.WriteLine($"{c.Id} - {c.FirstName} {c.LastName}");
                PressAKey();
            }
            else
            {
                Console.WriteLine("Internal server Error");
                PressAKey();
            }
        }

        public void ShowReadAllInterface()
        {
            Console.Clear();
            Console.WriteLine("===Show All===");
            var customers = client2.ReadAll();
            if (customers != null)
            {
                foreach (var c in customers)
                {
                    Console.WriteLine($"{c.Id} - {c.FirstName} {c.LastName}");
                }
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }
        }

        private string GetString(string question, int minLenght = 1, int maxLength = 30)
        {
            do
            {
                Console.WriteLine(question);
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) ||
                    input.Length < minLenght ||
                    input.Length > maxLength)
                {
                    Console.WriteLine("Your answer doesn't fit the required parameters");
                    Console.WriteLine($"Min lenght : {minLenght}, max lenght : {maxLength}");
                    Console.WriteLine("Please press a key to continue");
                    Console.ReadKey(true);
                }
                else
                    return input;
            }
            while (true);
        }

        private int GetInt(string question, int min = 0, int max = 9999999)
        {
            do
            {
                Console.WriteLine(question);
                var input = Console.ReadLine();
                try
                {
                    int intInput = int.Parse(input);
                    if (intInput < min ||
                        intInput > max)
                    {
                        Console.WriteLine("Your answer doesn't fit the required parameters");
                        Console.WriteLine($"Min  : {min}, max  : {max}");
                        Console.WriteLine("Please press a key to continue");
                        Console.ReadKey(true);
                    }
                    else
                        return intInput;
                }
                catch
                {

                }
            }
            while (true);
        }

        private void PressAKey()
        {
            Console.WriteLine("Please press a key to continue");
            Console.ReadKey(true);
        }
    }
}
