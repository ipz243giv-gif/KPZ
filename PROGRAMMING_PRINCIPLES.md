# Принципи програмування у проєкті

У цьому проєкті реалізовано систему управління авіарейсами. Нижче наведено ключові принципи програмування, використані в коді.

---

## 1. Single Responsibility Principle (SRP)
Кожен клас відповідає за конкретну частину системи. Клас `Date` керує датами, `Airplane` — даними про літак, а `Program` — взаємодією з користувачем.
* **Приклад у коді:** [Клас Date у Date.cs](https://github.com/ipz243giv-gif/KPZ/blob/26abf7562bb7f372e3151fbd96587d5879795468/SimpleClassLibrary/Date.cs#L9-L40)

## 2. DRY (Don't Repeat Yourself)
Повторювані дії, такі як виведення інформації про літак на екран, винесені в окремі методи `PrintAirplane` та `PrintAirplanes`, що дозволяє уникнути дублювання коду.
* **Приклад у коді:** [Метод PrintAirplane у Program.cs](https://github.com/ipz243giv-gif/KPZ/blob/26abf7562bb7f372e3151fbd96587d5879795468/SimpleClassConlsole/Program.cs#L130-L138)

## 3. Інкапсуляція
Дані об'єктів приховані за допомогою модифікатора `protected` та властивостей. Доступ до них здійснюється через публічні методи (гетери), що забезпечує безпеку та цілісність даних.
* **Приклад у коді:** [Властивості в Airplane.cs](https://github.com/ipz243giv-gif/KPZ/blob/26abf7562bb7f372e3151fbd96587d5879795468/SimpleClassLibrary/Airplane.cs#L9-L17)

## 4. KISS (Keep It Simple, Stupid)
Метод обчислення тривалості польоту `GetTotalTime` реалізований максимально просто: час початку та завершення переводиться у хвилини, після чого обчислюється різниця.
* **Приклад у коді:** [Метод GetTotalTime у Airplane.cs](https://github.com/ipz243giv-gif/KPZ/blob/26abf7562bb7f372e3151fbd96587d5879795468/SimpleClassLibrary/Airplane.cs#L39-L45)

## 5. Розділення відповідальності (SoC)
Бізнес-логіка (класи для роботи з літаками) відокремлена від консольного інтерфейсу (клас `Program`). Це дозволяє легко масштабувати проєкт у майбутньому.
* **Приклад у коді:** [namespace SimpleClassLibrary у Airplane.cs](https://github.com/ipz243giv-gif/KPZ/blob/26abf7562bb7f372e3151fbd96587d5879795468/SimpleClassLibrary/Airplane.cs#L7) та [using SimpleClassLibrary у Program.cs](https://github.com/ipz243giv-gif/KPZ/blob/26abf7562bb7f372e3151fbd96587d5879795468/SimpleClassConlsole/Program.cs#L7)
