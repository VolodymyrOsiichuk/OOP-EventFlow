# EventFlow

EventFlow — це консольна система управління подіями, створена як підсумковий міні-проєкт для курсу ООП.

Проєкт демонструє:
- багатошарову архітектуру;
- принципи SOLID;
- роботу з доменною моделлю;
- патерни проєктування;
- unit testing;
- CI/CD через GitHub Actions.

---

# Можливості системи

На поточній ітерації реалізовано:

- створення події;
- реєстрація учасника;
- перегляд інформації про подію;
- перевірка доступності місць;
- захист від дублювання реєстрацій;
- in-memory збереження даних.

---

# Архітектура проєкту

Проєкт побудований за принципом багатошарової архітектури:

```text
Console
↓
Application
↓
Domain
↓
Infrastructure
```

## Шари

### EventFlow.Domain
Містить:
- сутності;
- інтерфейси;
- доменні правила;
- value objects;
- enum-и.

### EventFlow.Application
Містить:
- application services;
- DTO;
- use cases;
- координацію бізнес-логіки.

### EventFlow.Infrastructure
Містить:
- repository implementation;
- persistence logic;
- file I/O (наступні ітерації).

### EventFlow.Console
Консольний інтерфейс користувача.

### EventFlow.Tests
Unit tests для domain та application layer.

---

# Використані принципи та патерни

## SOLID

- SRP — розділення відповідальностей;
- DIP — залежність від абстракцій;
- OCP — система підготовлена до розширення.

## Патерни

- Repository Pattern;
- Factory Pattern.

---

# Технології

- C#
- .NET
- xUnit
- GitHub Actions
- Mermaid UML

---

# Як запустити проєкт

## 1. Клонувати репозиторій

```bash
git clone <repository-url>
```

## 2. Перейти у директорію

```bash
cd EventFlow
```

## 3. Запустити застосунок

```bash
dotnet run --project src/EventFlow.Console
```

---

# Запуск тестів

```bash
dotnet test
```

---

# Поточний статус

## Iteration 1 (Lab 34)

Реалізовано:
- базову архітектуру;
- предметну модель;
- vertical slice;
- unit tests;
- CI foundation.
