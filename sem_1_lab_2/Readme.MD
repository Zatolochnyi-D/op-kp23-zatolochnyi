# Semester 1, Assignment 2

### Перколяція

Задано композитну системи, що складаються з випадково розподілених ізоляційних і металевих матеріалів: яка частка матеріалів має бути металевою, щоб композитна система була електричним провідником? Для моделювання таких ситуацій визначили абстрактний процес, відомий як перколяція.

### Модель

У задачі моделюємо перколяційну систему, використовуючи матрицю n на n. Кожна комірка матриці - відкрита або заблокована. Повна комірка — це відкрита комірка, яка з’єднана з відкритою коміркою у верхньому рядку через ланцюжок сусідніх (зліва, справа, вгору, вниз) відкритих комірок. Система є провідником, якщо в нижньому рядку є повна комірка.

### Тип даних для моделювання перколяції

Щоб змоделювати систему перколяції, створіть тип даних Percolation. Початковий стан. Всі комірки матриці у заблокованому стані.

### Індекси

Рядки: 1 <= i <= n, Стовпці: 1 <= j <= n

### Вимоги до продуктивності

Метод ініціалізації повинен виконуватися за час, пропорційний O(n^2); усі методи мають виконуватися за константний час + постійна кількість викликів union() і find(). У обов’язковому порядку потрібно розробити юніт-тести для для кожного з методів, що використовується у лабораторній роботі.