using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class DeleteOrganizationByIdOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var organizations = repository.GetOrganizations();
            organizations.ForEach(organization =>
            {
                Console.WriteLine(organization.Organization);
            });
            Console.WriteLine("Введите номер удаляемой организации:");
            var input = Console.ReadLine()!;
            try
            {
                var id = long.Parse(input);
                var isDeleted = repository.DeleteOrganizationById(id);
                if (isDeleted)
                {
                    Console.WriteLine("Успешно удалено!");
                }
                else
                {
                    Console.WriteLine("Такой организации не существует!");
                }
            }
            catch
            {
                Console.WriteLine("Введено неверное значение!");
            }
        }
    }
}
