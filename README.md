# PokerCombinationHelper
Проект создан для ознакомительных целей с использованием различных возможностей языка.<br>
Представляет собой (на 06.03.23) консольное приложение, которое выводит победителя исходя из<br> 
заданного количества игроков и карт на столе по правилам [Техасского Холдема](https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F4b%2Fa4%2F01%2F4ba401254959e98caf598a96f1dcf94c.jpg&f=1&nofb=1&ipt=b2ac9525e3944bcd0a88d645f9cc598875be7b0e05f58d08f225edff18e58e07&ipo=images)<br>
Состоит из 3 подпроектов:<br>
+ **[CardGameBase](https://github.com/xSgVx/PokerCombinationHelper/tree/master/CardGameBase)** - Является основой для создания карточных игр.<br>
Реализованы интерфейсы игральных карт, игроков, стола (папка [Interfaces](https://github.com/xSgVx/PokerCombinationHelper/tree/master/CardGameBase/Interfaces)).<br>
Реализованы абстрактные классы для:
	+ Создания игры [(/Factories/CardGame)](https://github.com/xSgVx/PokerCombinationHelper/blob/master/CardGameBase/Factories/CardGame.cs)
	+ Создания колоды [(/Factories/Deck)](https://github.com/xSgVx/PokerCombinationHelper/blob/master/CardGameBase/Factories/Deck.cs)
	+ Упрощенного создания кард из заданной строки [(/Factories/CardGenerator)](https://github.com/xSgVx/PokerCombinationHelper/blob/master/CardGameBase/Factories/CardGenerator.cs)
* **[Poker](https://github.com/xSgVx/PokerCombinationHelper/tree/master/Poker)** - проект созданный на основе CardGameBase, реализующий его интерфейсы и абстрактные классы.<br>
Содержит в себе модель для определения комбинаций ([Models/CombinationHelper](https://github.com/xSgVx/PokerCombinationHelper/blob/master/Poker/Models/CombinationHelper.cs)).<br>
На данный момент вывод победителя происходит в консоль.<br>
* **[UnitTests](https://github.com/xSgVx/PokerCombinationHelper/tree/master/UnitTests)** - юнит тесты