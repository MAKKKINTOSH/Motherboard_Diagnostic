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

        public Component(string instrument, int faultId)
        {
            this.Instrument = instrument;
            this.FaultId = faultId;
        }

        protected bool isInstrumentValid(string instrumentName)
        {
            if (instrumentName != Instrument)
            {
                return false;
            }
            return true;
        }

        public void makeDiagnostic(string instrument_name)
        {
            if (Diagnostic.IsRunning)
            {
                if (!isInstrumentValid(instrument_name))
                {
                    EventPanel.AddEvent("Ошибка, этим инструментом сюда нельзя", "warning");
                    return;
                }
                if (isBroken())
                {
                    makeBrokenEvent();
                }
                else
                {
                    makeWorkingEvent();
                }
            }
        }

        protected bool isBroken()
        {
            if (Diagnostic.hasFault(FaultId))
            {
                return true;
            }
            return false;
        }
        protected void makeBrokenEvent()
        {
            EventPanel.AddEvent("Сломано");
        }
        protected void makeWorkingEvent()
        {
            EventPanel.AddEvent("Работает");
        }
    }
}