Часть 1: Хранение информации о картах, реализация расположения карт на столе. 
* 
Создать архитектурную основу для карточной игры, используя паттерн MVC. ● CardAsset:ScriptableObject - ассет, содержащий такие данные как Название карты, Цвет карты и Изображение на ней. Данные. 
● CardInstance - объект, хранящийся в памяти, соответствующие данные для каждой карты на столе. Модель. 
В конструкторе должен принимать CardAsset в качестве аргумента и хранить ссылку на него. 
● CardView:MonoBehaviour - компонент, находящийся на каждой карте в сцене. Представление. 
Должен содержать метод Init, принимающий в качестве аргумента CardInstance и сохраняющий ссылку на него. 
Добавить в проект компоненты, позволяющие перемещать и располагать карты. Для этого создайте два скрипта: 
** 
Синглтон CardGame, который будет хранить словарь, в котором ключами будут CardInstance, а значениями CardView. 
Создайте публичный список из начальных карт List<CardAsset>. 
Добавьте в него метод StartGame, который должен для каждого игрока создавать CardInstance для каждого CardAsset из начальных карт. После для каждой созданной карты создавать в сцене префаб с CardView на нём и инициализировать этот префаб при помощи CardInstance. А также перемещать созданные карты в колоды игроков. 
Этот метод делает много разных вещей, поэтому разумно будет разбить его на несколько других методов: 
*** 
CreateCardView - принимает CardInstance в качестве аргумента, создаёт для него префаб с CardView на нём и инициализирует этот CardView методом Init. Выставляет значение в словаре с картами равным CardView на созданном объекте. 
CreateCard - принимает в качестве аргумента CardAsset и номер layout-a. Создаёт cardInstance для CardAsset, добавляет его в словарь со всеми картами, вызывает CreateCardView а после перемещает карту в нужный layout при помощи MoveToLayout. 
Для перемещения карты между layout-ами добавьте информацию о том, на каком layout-e и на какой позиции находится карта в CardInstance. 
public int LayoutId - индекс Layout-a, на котором находится карта 
public int CardPosition - текущая позиция карты в layout-e
создайте в CardInstance метод 
MoveToLayout - принимает id layout-a, куда нужно переместить карту. Он должен изменять LayoutId и CardPosition карты при перемещении. 
Часть 2: Отображение карт, перемещение. 
* 
Создайте компонент 
CardLayout:MonoBehaviour - находится на пустых объектах с RectTransform-ом в сцене, соответствующих областям, где могут находиться карты: рука игрока, колода, сброс… 
Добавьте в него следующие поля: 
public int LayoutId - индекс Layout-а 
public Vector2 Offsеt - на сколько сдвигается каждая последующая карта в layout-e 
В Update CardLayout должен проходиться по всем картам, чей LayoutId совпадает с LayoutId layout-a, делать transform CardView, соответствующий карте дочерним, выставлять ему локальную позицию в зависимости от CardPosition и Offsеt и siblingIndex в зависимости от CardPosition. 
Для того чтобы иметь возможность обратиться ко всем картам, принадлежащим layout-у, создайте в CardGame метод GetCardsInLayout, принимающий Id layout-а и возвращающий список из всех карт, чей LayoutId совпадает с ним. 
** 
Для того чтобы реализовать поворот карты рубашкой вверх мы можем добавить в CardLayout параметр bool FaceUp, который отвечает за то, какой стороной повёрнуты карты в области. В Update будем вызывать на картах в layout-e (на CardView) метод Rotate(bool up), который должен поворачивать карту рубашкой вверх или вниз (Можно внутри объекта карты создать Image с рубашкой и включать/выключать его). 
Модифицируйте Init на CardView так, чтобы он изменял отображение карты: название, цвет и изображение.

Если вы попробуете перемещать карты между layout-ами, используя метод MoveToLayout, то заметите, что место в layout-e откуда ушла карта не освобождается. Добавьте в CardGame метод RecalculateLayout(int layoutId), который проходится по всем оставшимся картам в layout-e и убирает пробелы в позициях. 0 - 2 - 3 - 5 => 0 - 1 - 2- 3 
Вызывайте RecalculateLayout, когда перемещаете карту из одного layout-a в другой. 
*** 
Добавьте в CardGame метод StartTurn, раздающий каждому игроку в руку карты из колоды до HandCapacity карт. HandCapacity - публичная переменная, отвечающая за то, сколько карт должно быть у игрока к началу хода. 
Создайте метод ShuffleLayout(int layoutId), перемешивающий все карты, находящиеся в layout-е, меняя их CardPosition. 
Добавьте в CardView метод PlayCard, который вызывается при клике на карту и помещает ее в центр стола.