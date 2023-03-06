namespace Poker.Source
{
    internal static class Messages
    {
        internal const string EnterPlayersCount = "Введите кол-во игроков от 2 до 6:";

        internal const string NextActionButton = "Нажмите \"a\" чтобы добавить карту на стол, " +
                    "либо \"r\" для перезапуска, \"esc\" для выхода";

        internal const string NotReadyToLaunch = "Не готово к запуску.\n" +
                    "Необходимо указать число игроков";

        internal const string CardOnBoard = "Карты на столе:\n";

        internal const string ErrorAddingBoardCards = "Ошибка при добавлении кард на стол.\n" +
                    "Максимальное количество кард на столе: 5";

        internal const string WinnerWithCombo = "Победитель c комбинацией: ";
        internal const string WinnersWithCombo = "Победители c комбинацией: ";
        internal const string ErrorOnSettingPlayersCount = "Ошибка, введите другое число";
        internal const string PlayersCountInfo = "Участвует игроков: ";
        internal const string PlayerAdded = "Добавлен игрок: ";
        internal const string InputError = "Ошибка ввода";

        internal const string ErrorAddingPlayer = "Ошибка при попытке добавления игрока.\n" +
            "Максимальное количество игроков: ";

        internal const string ErrorDeletingPlayer = "Ошибка при попытке удалить игрока.\n" +
            "Текущее количество игроков: ";

    }
}
