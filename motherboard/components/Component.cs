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
        protected int FaultId { get; } = 0;
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
            if (Diagnostic.IsRunning)
            {
                if (!IsInstrumentValid(instrument))
                {
                    EventPanel.AddEvent("Ошибка, этим инструментом сюда нельзя", "warning");
                    return;
                }
                Condition condition = IsBroken() ? Condition.Broken : Condition.Working;
                foreach (var parameters in DiagnosticData)
                {
                    if (instrument == parameters.Instrument)
                    {
                        MakeEvent(instrument, condition);
                        return;
                    }
                }
            }
            else if(Diagnostic.PCIsLaunch)
            {
                EventPanel.AddEvent("Диагностика работающего компьютера невозможна", "warning");
            }
            else
            {
                EventPanel.AddEvent("Попробуй его хотя бы запустить блин!\nЯ так старался, а ты даже кнопку \"Запустить ПК\" не нажмешь:(");
            }
        }

        protected bool IsBroken()
        {
            if (Diagnostic.HasFault(FaultId))
            {
                return true;
            }
            return false;
        }
        protected void MakeEvent(Instruments instrument, Condition condition)
        {
            ElementDiagnosticData diagnosticData = DiagnosticData.Find((x) => x.Instrument == instrument);

            string message = condition switch
            {
                Condition.Working => diagnosticData.GetWorkingData(),
                Condition.Broken => diagnosticData.GetBrokenData()
            };
            EventPanel.AddEvent(message);
        }
    }

    enum Condition
    {
        Working,
        Broken
    }
}