
namespace Motherboard_Diagnostic.motherboard.components
{
    internal class RAMSlot : Component
    {
        public RAMSlot()
        {
            this.DiagnosticData = new()
            {
                new MultiElementDiagnosticData(
                    instrument: Instruments.RAMSlotTester,
                    faultId: 7,
                    dataType: DiagnosticDataType.Text,
                    buttons: new string[] {"RAMSlot1", "RAMSlot2", "RAMSlot3", "RAMSlot4"},
                    getWorkingData: () => "Тест пройден успешно",
                    getBrokenData: () => "Тест не пройден, слот сломан"
                )
            };
        }
    }
}
