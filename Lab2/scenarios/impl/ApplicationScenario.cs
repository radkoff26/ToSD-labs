using Lab2.core;
using Lab2.repository;
using Lab2.repository.impl;
using Lab2.scenarios.executors;
using static Lab2.core.ScenarioData;

namespace Lab2.scenarios.impl
{
    internal class ApplicationScenario : Scenario
    {
        private Repository repository = new RepositoryImpl();

        public ScenarioData ProduceData()
        {
            return new ScenarioData(
                [
                    new Option(
                        new OptionKey('1', "Добавить организацию"),
                        new AddNewOrganizationOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('2', "Добавить здание"),
                        new AddNewHouseOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('3', "Удалить организацию"),
                        new DeleteOrganizationByIdOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('4', "Удалить здание"),
                        new DeleteHouseByIdOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('5', "Отсортировать здания по названию"),
                        new SortHousesByNameOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('6', "Отсортировать здания по рейтингу"),
                        new SortHousesByRateOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('7', "Отсортировать организации по названию"),
                        new SortOrganizationsByNameOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('8', "Посчитать здания"),
                        new CountHousesOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('9', "Посчитать организации"),
                        new CountOrganizationsOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('a', "Получить организации"),
                        new GetOrganizationsOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('b', "Получить здания"),
                        new GetHousesOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('c', "Получить здания по городу"),
                        new GetHousesByCityOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('d', "Получить здание по номеру"),
                        new GetHouseByIdOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('e', "Получить организацию по номеру"),
                        new GetOrganizationByIdOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('f', "Фильтровать организации по региону нахождения"),
                        new GetOrganizationsFilteredInLocationRegionOptionExecutor(repository)
                    ),
                    new Option(
                        new OptionKey('g', "Получить среднее значение рейтинга домов"),
                        new GetAverageHousesRateOptionExecutor(repository)
                    )

                ],
                new OptionKey('0', "Выход"),
                "Выберите действие для работы с данными об организациях и их зданиях:"
            );
        }
    }
}
