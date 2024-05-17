
using System.Collections.Generic;
using System.Linq;

namespace Motherboard_Diagnostic
{
    class DiagnosticHandbook
    {
        public static List<Fault> Faults = new()
        {
            new Fault(
            id: 0,
            name: "Заводской брак в текстолите, продиагностировать не получается",
            description: "Ремонт не поможет, тут только менять мать (логично будет предположить, что этот вариант самый нежелательный)",
            solution: new Solution(
                description: "Покупка новой материнской платы"
            )
        ),
            new Fault(
            id: 1,
            name: "Короткое замыкание на линиях +12В, +5В, +3.3В",
            description: "Система не стартует, может сильно нагреваться вышедший из строя компонент или микросхема. Показатели Омметра близки к 0",
            solution: new Solution(
                description: "выполнить поиск неисправного элемента используя схему последовательного запуска и формирования определенных сигналов на контрольных точках"
            )
        ),
            new Fault(
            id: 2,
            name: "Неисправен южный мост",
            description: "Показатели отличаются на сотни мВ или равны 0, либо есть КЗ между линиями D+ и D-",
            solution: new Solution(
                description: "Замена южного моста"
            )
        ),
            new Fault(
            id: 3,
            name: "Неисправность микросхемы BIOS, либо ее прошивки",
            description: "Отсутствуют сигналы (импульсы) на ножках input/output микросхемы bios, означающие обмен данными с П.У. отсутствует сигнал CS. Проверка осуществляется осциллографом",
            solution: new Solution(
                description: "Перепрошивка микросхемы BIOS, либо замена в случае ее неисправности"
            )
        ),
            new Fault(
            id: 4,
            name: "Неисправность RTC",
            description: "Отсутствие осциллограммы, либо несинусоидальная форма, либо неправильная частота",
            solution: new Solution(
                description: "Перепайка кварцевого резонатора, поиск неисправности в системе питания, замена южного моста"
            )
        ),
            new Fault(
            id: 5,
            name: "Проблемы с разъемом видеокарты, либо линиями передачи данных",
            description: "Есть обрыв в линиях передачи данных и это видно на тестере разъема видеокарты",
            solution: new Solution(
                description: "выполнить восстановление линий передачи данных, заменить разъем видеокарты"
            )
        ),
            new Fault(  // Диагностируется вольтметром, не должно быть больших отклонений, не больше 5%
            id: 6,
            name: "Проблемы c напряжением по основным линиям питания",
            description: "Большие отклонения в значениях напряжения по линиям питания +5V, +12V, +3.3V",
            solution: new Solution(
                description: "Поиск и замена поврежденных элементов или диагностика и замена в случае неисправности блока питания"
            )
        ),
            new Fault(  //Диагностируется тестером слота оперативной памяти, должен быть положительный результат
            id: 7,
            name: "Не работает слот оперативной памяти",
            description: "Проблемы со слотом оперативной памяти, имеются разрывы в цепи или слот полностью не функционирует",
            solution: new Solution(
                description: "Замена сломанного слота(ов) оперативной памяти"
            )
        ),
            new Fault(  // ну тут просто вставить кабель)
            id: 8,
            name: "Отсутствует изображение, не подключен кабель",
            description: "Забыли подключить кабель монитора",
            solution: new Solution(
                description: "Подключить кабель монитора к разъему"
            )
        ),
            new Fault(  //Диагостируется вольтметром по батарейке, напряжение должно быть 3В +- 0.2В
            id: 9,
            name: "Закончился ресурс батарейки CMOS", 
            description: "Батарейка биоса села",
            solution: new Solution(
                description: "Замена батарейки"
            )
        ),
            new Fault(  //Диагостируется прозвонкой конденсаторов, конденсатор не должен издавать звук
            id: 10,
            name: "Пробит конденсатор",
            description: "Нарушена изоляция в конденсаторе, он пропускает через себя ток", 
            solution: new Solution(
                description: "Замена неисправного конденсатора(ов)"
            )
        ),
        };
        public static Dictionary<Instruments, string> InstrumentsDictionary = new()
        {
            {Instruments.Ohmmeter, "ohmmeter"},
            {Instruments.VoltmeterBase, "voltmeterBase" },
            {Instruments.VoltmeterVoltageDown, "voltmeterVoltageDown" },
            {Instruments.Oscilloscope, "oscilloscope" },
            {Instruments.Videotester, "videotester"},
            {Instruments.Calling, "calling" },
            {Instruments.RAMSlotTester, "RAMTest" }
        };
        public static Dictionary<string, string> RusInstrumentsNames = new()
        {
            {"ohmmeter", "Омметр" },
            {"voltmeterBase", "Вольтметр" },
            {"voltmeterVoltageDown", "Падение напряжения"},
            {"oscilloscope", "Осциллограф" },
            {"videotester", "Тестер PCIe" },
            {"calling", "Прозвонка"},
            {"RAMTest", "Тестер RAM"}
        };
    }
}
