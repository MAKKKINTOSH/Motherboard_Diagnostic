# Диагностика материнской платы GIGABYTE B660 DS3H
---
## Алгоритм работы разрабатываемого приложения:
- При нажатии кнопки "запустить ПК" выдаётся сообщение об ошибке и генерируется несколько неисправностей
- При выборе инструмента и нажатии на какую-то кнопку данная кнопка остаётся выбранной (Выводится сообщение - __выбрана кнопка ...__)
- При выборе инструмента на панели инструментов каждая кнопка на плате выдаёт разную информацию. Например:
        Выбран осциллограф, нажата кнопка SOCET. _Частота на сокете равна ... МГц_.
- Если пользователь считает данную характеристику из сообщения отклонением от нормы, то он жмёт кнопку __отремонтировать__ и будет либо сообщение об успешнном выполнении, либо всё сломалось и надо начинать сначала
- Если пользователь исправил все неисправности, то при нажатии кнопки __запуск ПК__, будет сообщение о том, что запуск прошёл успешно.
---
## Инструкция
Запустить .exe файл и пользоваться программой
---
## Текущие неисправности платы и их решения:
|Неисправности                                               |Решения неисправностей   |      
|---                                                         |---|
|Короткое замыкание на линиях +12В, +5В, +3.3В               |выполнить поиск неисправного элемента используя схему последовательного запуска и формирования определенных сигналов на контрольных точках|
|Неисправен южный мост                                       |Замена южного моста|
|Неисправность микросхемы BIOS, либо ее прошивки             |Перепрошивка микросхемы BIOS, либо замена в случае ее неисправности   |
|Неисправность RTC                                           |Перепайка кварцевого резонатора, поиск неисправности в системе питания, замена южного моста   |
|Проблемы с разъемом видеокарты, либо линиями передачи данных|Выполнить восстановление линий передачи данных, заменить разъем видеокарты   |
---