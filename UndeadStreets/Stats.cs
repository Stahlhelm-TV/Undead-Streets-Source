namespace UndeadStreets
{
    using GTA;
    using NativeUI;
    using System;
    using System.Drawing;

    public class Stats : Script
    {
        private TimerBarPool hudPool;
        private BarTimerBar hungerBar;
        private BarTimerBar thirstBar;
        private BarTimerBar energyBar;

        public Stats()
        {
            base.Tick += new EventHandler(this.OnTick);
            this.hudPool = new TimerBarPool();
            this.hungerBar = new BarTimerBar("~y~HUNGER");
            this.thirstBar = new BarTimerBar("~b~THIRST");
            this.energyBar = new BarTimerBar("~g~ENERGY");
            this.hungerBar.BackgroundColor = Color.Gray;
            this.hungerBar.ForegroundColor = Color.Yellow;
            this.thirstBar.BackgroundColor = Color.Gray;
            this.thirstBar.ForegroundColor = Color.Blue;
            this.energyBar.BackgroundColor = Color.Gray;
            this.energyBar.ForegroundColor = Color.Green;
            this.hudPool.Add(this.energyBar);
            this.hudPool.Add(this.thirstBar);
            this.hudPool.Add(this.hungerBar);
        }

        public void Draw(float hunger, float thirst, float energy)
        {
            this.hungerBar.Percentage = hunger;
            this.thirstBar.Percentage = thirst;
            this.energyBar.Percentage = energy;
            this.hudPool.Draw();
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                this.Draw(Character.currentHungerLevel, Character.currentThirstLevel, Character.currentEnergyLevel);
            }
        }
    }
}

