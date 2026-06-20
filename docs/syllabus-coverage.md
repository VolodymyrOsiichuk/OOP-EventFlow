# Syllabus Coverage

## Мета

Цей документ показує, які теми курсу були використані в проєкті EventFlow та яким чином вони інтегровані у фінальне рішення.

---

# 1. Основи ООП

## Використано

- Класи
- Об'єкти
- Інкапсуляція
- Композиція

### Приклади

- Event
- Participant
- Registration
- Venue

**Статус:** Повністю покрито

---

# 2. Абстракції, поліморфізм та інтерфейси

## Використано

- Інтерфейси
- Абстракції
- Поліморфна взаємодія

### Приклади

- IEventRepository
- IDataStore<T>

**Статус:** Повністю покрито

---

# 3. Generics, колекції та LINQ

## Використано

### Generics

- IDataStore<T>

### Колекції

- List<T>
- IReadOnlyCollection<T>
- Dictionary<TKey, TValue>

### LINQ

- Where()
- Select()
- GroupBy()
- OrderByDescending()
- ToDictionary()

### Приклади

- EventQueryService
- EventMapper

**Статус:** Повністю покрито

---

# 4. Обробка помилок та Persistence

## Використано

### Result Pattern

- Result.Success()
- Result.Failure()

### Persistence

- JSON Storage
- SaveAsync()
- LoadAsync()

### Fault Handling

- відсутній файл;
- пошкоджений JSON;
- некоректні дані.

**Статус:** Повністю покрито

---

# 5. SOLID

## Single Responsibility Principle

- EventService
- EventQueryService
- EventPersistenceService

## Open/Closed Principle

- розширення через нові сервіси та інтерфейси

## Liskov Substitution Principle

- заміна реалізацій репозиторіїв

## Interface Segregation Principle

- вузькі контракти

## Dependency Inversion Principle

- залежність від абстракцій

**Статус:** Повністю покрито

---

# 6. Патерни проєктування

## Використано

### Repository Pattern

Доступ до даних через абстракції.

### Factory Pattern

Створення об'єктів подій.

### Result Pattern

Контрольована обробка помилок.

**Статус:** Повністю покрито

---

# 7. UML

## Використано

- Class Diagram
- Sequence Diagram

Документація розташована у папці docs.

**Статус:** Повністю покрито

---

# 8. Тестування

## Unit Tests

Перевірка бізнес-логіки.

## Integration Tests

Перевірка взаємодії між компонентами.

## Fault Handling Tests

Перевірка негативних сценаріїв.

## Coverage

Автоматичний збір покриття через Coverlet.

**Статус:** Повністю покрито

---

# 9. Рефакторинг

## Виконано

- розділення відповідальностей;
- виділення persistence-шару;
- виділення query-сервісів;
- покращення тестованості;
- усунення дублювання логіки.

**Статус:** Повністю покрито

---

# Підсумок

Проєкт EventFlow демонструє інтеграцію більшості тем курсу ООП у єдиному завершеному застосунку.

Основні теми курсу реалізовані практично та підтверджені тестами, документацією і CI/CD процесом.
