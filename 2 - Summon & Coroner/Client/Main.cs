using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;

namespace Client
{
    public class Main : BaseScript
    {
        public Main()
        {
            API.RegisterCommand("coroner", new Action(SummonCoroner), false);
        }

        private void SummonCoroner()
        {
            Coroner.Summon();
        }
    }
}
