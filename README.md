# Тестовое Задание "Планетарный редуктор"

Видео геймплея: [https://youtu.be/gsZd11q1wpk](https://youtu.be/gsZd11q1wpk)

---

ТЗ:
- проект должен иметь минимум две сцены, меню и рабочая сцена;
- в меню должны быть две кнопки;
- первая кнопка должна загружать рабочую сцену, вторая - выход;
- в рабочей сцене должна быть кнопка назад, которая возвращает в сцену меню;
- проект должен быть на URP и собираться в standalone(для Windows);
- программа должна корректно отображаться на FullHD мониторе;
 
Проект должен быть написан на c#, unity.

Основное взаимодействие в рабочей сцене заключается в реализации анимированной взрыв-схемы для простой детали — планетарного редуктора. Деталь приложена к письму. Рабочая сцена делится на две части, левую (0.3 от ширины экрана), и правую (все остальное пространство).

В левой части рабочей сцены должен быть нажимаемый список компонентов редуктора, и само название редуктора.

В правой части сцены должна быть сама деталь. Деталь нужно иметь возможность осмотреть со всех сторон (орбитальный обзор). В любом состоянии при любом повороте все компоненты детали не должны выходить за рамки правой части сцены.

При нажатии на название редуктора в левой части происходит слёт-разлёт взрыв-схемы и сворачивание/разворачивание списка деталей.

При нажатии на название компонента редуктора происходит анимированный подлёт к нему и орбитальный обзор, остальные детали должны быть скрыты.

При повторном нажатии на этот же компонент производится обратное действие.  Должна быть адекватная обработка нажатий кнопок компонентов и кнопки редуктора в любом порядке, всё должно работать предсказуемо.
 
Будет плюсом:
- Внешний вид детали как на скриншоте(отблеск на металлической поверхности).
- Адекватная работа на разных разрешениях экрана.
 
Срок выполнения: 2-3 дня
