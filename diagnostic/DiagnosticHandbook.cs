
using System.Collections.Generic;

namespace Motherboard_Diagnostic
{
    class DiagnosticHandbook
    {
        public static List<Fault> Faults = new()
        {
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
        )
        };
        public static Dictionary<Instruments, string> InstrumentsDictionary = new()
        {
            {Instruments.Ohmmeter, "ohmmeter"},
            {Instruments.Voltmeter, "voltmeter" },
            {Instruments.Ammeter, "ammeter" },
            {Instruments.Oscilloscope, "oscilloscope" },
            {Instruments.Videotester, "videotester"}
        };
        public static Dictionary<string, string> RusInstrumentsNames = new()
        {
            {"ohmmeter", "Омметр"},
            { "voltmeter", "Вольтметр" },
            { "ammeter", "Амперметр" },
            { "oscilloscope", "Осциллограф" },
            { "videotester", "Тестер PCIe" }
        };
    }
}
