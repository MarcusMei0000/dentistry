# .NET
Приложение, регулирующее работу стоматологии:
1. Регистрация (ФИО, телефон, пароль, почта, дата рождения, фото, для пациента: полис; для врача: специальность, кабинет приёма, календарь)
2. Авторизация (почта/телефон, пароль).
3. Выйти из приложения
4. Сменить пароль

Пациент
1. Профиль  содержит информацию указанную при регистрации и последнюю запись к врачу (текущую или последнюю),
2. Текущую запись можно отменить (на кнопку "Отменить" и подтвердив действие во всплывающем окне, после чего запись удаляется из профиля) или изменить (переход на Страницу врача, где открыта соответствующая дата и список времени (можно сменить и дату, и время)), и открыть детали записи(время, врач, кабинет приёма).
3. На главной странице - список стоматологов (их карточки) с поиском по ФИО стоматлога; чтобы записаться на приём необходимо нажать на карточку врача.
4. Карточка врача содержит описание и календарь, чтобы выбрать дату и время приёма. 
5. Сначала пользователь выбирает дату, затем ему высвечивается список времени (серым цветом обозначено занятое время, ярким цветом - доступное для записи время), после нажатия кнопки "Подтвердить" информация о текущей записи появляется в профиле.

Врач
1. Устанавливать расписание работы на неделю - выбирать целый день или конкретное время на каждый день (существует значение рабочей недели по умолчанию, т.е. на неделе заполнено N приёмов, которые сразу высвечиваются врачу).
2. Просматривать записавшихся пользователей.
* Врач имеет 1 постоянную специальность в стоматологии
* Врач единовременно прикреплён к 1 кабинету, к которому могут быть прикреплены несколько врачей.

![newDBM2](https://user-images.githubusercontent.com/106516611/198530832-8dfa96ff-6c53-4afb-8f07-33310d3b447c.png)
ОПИСАНИЕ МОДЕЛИ БАЗЫ ДАНЫЫХ:
Чтобы развеять недоразумения прикрепляю описание (пояснения) к своей модели баз данных, которую я создала сама и не меняла с прошлого раза.
1. Есть 2 вида пользователей: пациент и врач (стоматолог). Регистрация и профиль для них разные.
2. В своей базе я всем сущностям добавила искусственный первичный ключ.
3. Пациент имеет атрибуты ФИО, пароль (по Вашей мне присланной наводке/поправке изменён на PasswordHash), email указываемый по регистрации, фото (по Вашей мне присланной наводке/поправке изменён на PhotoLink, т.к. картинки не хранят в базе данных), дата рождения (чтобы знать возраст пациента) и номер полиса (для возможного покрытия лечения).
4. Доктор (врач/стоматолог) имеет ФИО, пароль, почту, фото, кабинет приёма (в ТЗ описано конкретнее) и специальность (терапевт, ортопед, хирург, пародонтолог, челюстно-лицевой хирург).
5. Для этого я и выделела сущность Специальность, которая является словарём с описанием того, что стоматолог конкретной специальности делает, сущность не имеет связи самой к себе, так как нет иерархии и сам список возможных Специальностей небольшой.
6. У каждого врача есть Расписание (это некий набор временных периодов на каждый день, например, 28.02.2022 15:00-16:00). Именно поэтому в начале было 3 атрибута - дата, время начала приёма, время окончания приёма. По Вашей мне присланной наводке/поправке было изменено на 2 атрибута - начало и конец приём, включающиеся в себя дату.
7. Сущность приём хранить конкретный поход (запланированный или совершённый) пациента к стоматологу. Имеется время начала и конца, статус (записан, отменён, посещён), соответствует некому промежутку времени, выделенному врачом. Приём относится к конкретному пациенту.
8. Статус - это справочник всех состояний приёма (записан, отменён, посещён).

Связи:
- Пациент может иметь несколько приёмов.
- У стоматолога может быть только 1 текущая специальность (помечено в ТЗ).
- Есть ограниченный набор временных промежутков Schedule, из которых каждый стоматолог может себе выбрать часы приёма.
- Приём может иметь 1 из статусов.

Прошу простить за недоразумение с клонированием репозитория. Я неправильно интерпретировала Ваше задание.
Мой проект похож на Ваш (но не в коем случае его не копирует), потому что предметная область схожа, прошу присмотреться к деталям.

