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
        protected Dictionary<string, List<Func<string>>> Responses { get; set; } = null;

        public Component(int faultId)
        {
            this.FaultId = faultId;
        }

        protected bool IsInstrumentValid(string instrumentName)
        {
            foreach (var item in Responses.Keys)
            {
                if (instrumentName == item)
                {
                    return true;
                }
            }
            return false;
        }

        public void MakeDiagnostic(string instrument_name)
        {
            if (Diagnostic.IsRunning)
            {
                if (!IsInstrumentValid(instrument_name))
                {
                    EventPanel.AddEvent("Ошибка, этим инструментом сюда нельзя", "warning");
                    return;
                }
                Condition condition = IsBroken() ? Condition.Broken : Condition.Working;
                foreach (var instrument in Responses.Keys)
                {
                    if (instrument_name == instrument)
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
        protected void MakeEvent(string instrument, Condition condition)
        {
            EventPanel.AddEvent(Responses[instrument][(int)condition]());
        }
    }

    enum Condition
    {
        Working = 0,
        Broken = 1
    }
}