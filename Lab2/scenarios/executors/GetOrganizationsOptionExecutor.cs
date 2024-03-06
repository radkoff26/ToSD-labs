using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class GetOrganizationsOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var organizations = repository.GetOrganizations();
            Console.WriteLine("Результат запроса:");
            organizations.ForEach(o =>
            {
                Console.WriteLine(o.ToString());
            });
        }
    }
}
