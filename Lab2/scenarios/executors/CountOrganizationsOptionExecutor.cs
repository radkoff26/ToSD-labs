using Lab2.core;
using Lab2.repository;

namespace Lab2.scenarios.executors
{
    internal class CountOrganizationsOptionExecutor(Repository repository) : OptionExecutor
    {
        private Repository repository = repository;

        public void Execute()
        {
            var organizationsCount = repository.CountOrganizations();
            Console.WriteLine("Результат");
            Console.WriteLine(organizationsCount);
        }
    }
}
