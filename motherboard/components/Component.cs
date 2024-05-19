using System.Collections.Generic;

namespace Motherboard_Diagnostic
{
    abstract class Component
    {
        public string Instrument { get; } = string.Empty;
        protected List<ElementDiagnosticData> DiagnosticData { get; set; } = null;

        protected bool IsInstrumentValid(Instruments instrument)
        {
            foreach (var item in DiagnosticData)
            {
                if (instrument == item.Instrument)
                {
                    return true;
                }
            }
            return false;
        }

        public void MakeDiagnostic(Instruments instrument, string ?buttonName = null)
        {
            if (!Diagnostic.IsRunning && !Diagnostic.PCIsLaunch)
            {
                EventPanel.AddMessageEvent("Попробуй его хотя бы запустить блин!\nЯ так старался, а ты даже кнопку \"Запустить ПК\" не нажмешь:(");
                return;
            }
            if(Diagnostic.PCIsLaunch)
            {
                EventPanel.AddMessageEvent("Диагностика работающего компьютера невозможна", EventType.Bad);
                return;
            }
            if (!IsInstrumentValid(instrument))
            {
                EventPanel.AddMessageEvent("Ошибка, этим инструментом сюда нельзя", EventType.Bad);
                return;
            }
            Diagnostic.CanRepair = true;
            Fault fault = DiagnosticData.Find((x) => x.Instrument == instrument).Fault;
            Condition condition = IsFaultActive(fault) ? Condition.Broken : Condition.Working;
            MakeEvent(instrument, condition, buttonName);
        }

        protected bool IsFaultActive(Fault fault)
        {
            return Diagnostic.HasFault(fault);
        }
        protected void MakeEvent(Instruments instrument, Condition condition, string ?buttonName)
        {
            ElementDiagnosticData diagnosticData = DiagnosticData.Find((x) => x.Instrument == instrument);
            string text = "";

            if (condition == Condition.Working)
            {
                text = diagnosticData.GetWorkingData();
            }
            else if (buttonName != null)
            {
                bool isBroken = ((MultiElementDiagnosticData)diagnosticData).MultiElements.Find(x => x.Item1 == buttonName).Item2;
                text = isBroken switch
                {
                    true => diagnosticData.GetWorkingData(),
                    false => diagnosticData.GetBrokenData()
                };
            }
            else
            {
                text = diagnosticData.GetBrokenData();
            }

            switch (diagnosticData.DataType)
            {
                case DiagnosticDataType.Text:
                    EventPanel.AddMessageEvent(text);
                    break;
                case DiagnosticDataType.Chart:
                    EventPanel.AddChartEvent(text);
                    break;
                default:
                    break;
            }
        }
    }

    enum Condition
    {
        Working,
        Broken
    }
}