---

# docs/iteration-1.md

```md
# Iteration 1 Report

# Реалізовано

- Створено solution та багатошарову архітектуру.
- Реалізовано базову доменну модель.
- Реалізовано перший vertical slice.
- Налаштовано unit tests.
- Додано GitHub Actions CI.

---

# Поточі артефакти

- vision.md
- backlog.md
- class-diagram.md
- sequence-diagram.md
- README.md
- tests
- GitHub Actions workflow

---

# Що буде розширюватися у Lab 35

- Persistence у JSON
- LINQ-запити
- Розширена бізнес-логіка
- Скасування реєстрацій
- Категорії подій

---

# Ризики та невизначеності

- Можлива зміна структури persistence.
- Може знадобитися додатковий сервіс для роботи з квитками.
- Потрібно продумати fault handling для file I/O.

---

# Підготовка до розширення

Свідомо підготовлені:

- IRepository
- EventService
- Domain entities
- Шарова архітектура
- Можливість заміни repository implementation
