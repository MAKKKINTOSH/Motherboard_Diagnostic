using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void MakeDiagnostic(Instruments instrument)
        {
            if (!Diagnostic.IsRunning && !Diagnostic.PCIsLaunch)
            {
                EventPanel.AddMessageEvent("Попробуй его хотя бы запустить блин!\nЯ так старался, а ты даже кнопку \"Запустить ПК\" не нажмешь:(");
                return;
            }
            if(Diagnostic.PCIsLaunch)
            {
                EventPanel.AddMessageEvent("Диагностика работающего компьютера невозможна", EventType.Warning);
                return;
            }
            if (!IsInstrumentValid(instrument))
            {
                EventPanel.AddMessageEvent("Ошибка, этим инструментом сюда нельзя", EventType.Warning);
                return;
            }
            Fault fault = DiagnosticData.Find((x) => x.Instrument == instrument).Fault;
            Condition condition = IsFaultActive(fault) ? Condition.Broken : Condition.Working;
            MakeEvent(instrument, condition);
        }

        protected bool IsFaultActive(Fault fault)
        {
            return Diagnostic.HasFault(fault);
        }
        protected void MakeEvent(Instruments instrument, Condition condition)
        {
            ElementDiagnosticData diagnosticData = DiagnosticData.Find((x) => x.Instrument == instrument);

            string text = condition switch
            {
                Condition.Working => diagnosticData.GetWorkingData(),
                Condition.Broken => diagnosticData.GetBrokenData()
            };
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