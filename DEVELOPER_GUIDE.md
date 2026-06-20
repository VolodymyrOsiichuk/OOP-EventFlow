# DEVELOPER GUIDE

## Призначення

Цей документ описує архітектуру проєкту EventFlow, правила розширення функціональності та процес розробки.

---

# Архітектура

Проєкт побудований за принципом багатошарової архітектури.

```text
Console
↓
Application
↓
Domain
↓
Infrastructure
```

---

## Domain Layer

Містить:

- бізнес-сутності;
- інваріанти;
- бізнес-правила;
- інтерфейси репозиторіїв;
- Result pattern.

### Основні сутності

- Event
- Participant
- Registration
- Venue

---

## Application Layer

Містить:

- DTO;
- сервіси;
- use cases;
- LINQ-запити.

### Основні сервіси

#### EventService

Відповідає за:

- створення подій;
- реєстрацію учасників;
- скасування реєстрацій.

#### EventQueryService

Відповідає за:

- фільтрацію;
- пошук;
- статистику.

---

## Infrastructure Layer

Містить:

- репозиторії;
- persistence;
- JSON storage.

### Основні компоненти

#### InMemoryEventRepository

Тимчасове сховище даних у пам'яті.

#### JsonEventStore

Виконує збереження та завантаження даних.

#### EventPersistenceService

Координує роботу між репозиторієм та файловим сховищем.

#### EventMapper

Перетворює доменні сутності у DTO для серіалізації.

---

## Console Layer

Містить:

- консольне меню;
- взаємодію з користувачем;
- запуск сервісів.

---

# Використані принципи SOLID

## Single Responsibility Principle

Кожен сервіс має одну відповідальність.

Приклад:

- EventService — бізнес-операції.
- EventQueryService — запити.
- EventPersistenceService — persistence.

---

## Open/Closed Principle

Функціональність розширюється через нові сервіси та реалізації інтерфейсів без зміни існуючого коду.

---

## Liskov Substitution Principle

Реалізації репозиторіїв можуть замінювати одна одну через інтерфейси.

---

## Interface Segregation Principle

Залежності описуються через вузькі інтерфейси.

---

## Dependency Inversion Principle

Сервіси працюють через абстракції репозиторіїв та сховищ даних.

---

# Використані патерни

## Repository Pattern

Відокремлює бізнес-логіку від способу збереження даних.

## Factory Pattern

Використовується для створення подій.

## Result Pattern

Використовується для контрольованої обробки помилок без винятків.

---

# Додавання нового Use Case

При додаванні нового сценарію необхідно:

1. Оновити Domain.
2. Додати DTO.
3. Розширити Application Service.
4. Додати тести.
5. Оновити документацію.

---

# Запуск проєкту

```bash
dotnet restore
dotnet build
dotnet run --project src/EventFlow.Console
```

---

# Запуск тестів

```bash
dotnet test
```

---

# Генерація Coverage

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

---

# Структура проєкту

```text
src/
├── EventFlow.Console
├── EventFlow.Application
├── EventFlow.Domain
└── EventFlow.Infrastructure

tests/
└── EventFlow.Tests

docs/
├── iteration-1.md
├── iteration-2.md
├── iteration-3.md
├── release-plan.md
├── test-strategy.md
└── test-matrix.md
```

---

# Правила підтримки проєкту

Перед кожною новою функцією необхідно:

- оновити backlog;
- додати або оновити тести;
- перевірити CI;
- оновити документацію;
- перевірити coverage.
