# TESTING

## Запуск тестів

```bash
dotnet test
```

---

## Запуск тестів із coverage

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

---

## Типи тестів

### Unit Tests

Перевіряють:

- Event
- Participant
- EventService
- EventQueryService

### Integration Tests

Перевіряють:

- SaveAsync()
- LoadAsync()
- JSON Persistence
- Повний цикл роботи системи

### Fault Handling Tests

Перевіряють:

- дублікати реєстрацій;
- перевищення ліміту;
- пошкоджений JSON;
- відсутній файл;
- некоректні стани.

---

## Критичні сценарії

1. Створення події.
2. Реєстрація учасника.
3. Скасування реєстрації.
4. Збереження даних.
5. Завантаження даних.
6. Робота LINQ-запитів.

---

## Очікуваний результат

Усі тести повинні проходити успішно.

Coverage використовується для контролю якості та пошуку непокритих ділянок коду.
