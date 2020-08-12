namespace UndeadStreets
{
    using GTA;
    using System;

    public class SurvivorPed : UpdaterClass
    {
        public Ped pedEntity;
        public PedTasks tasks = PedTasks.None;

        public SurvivorPed(Ped pedEntity)
        {
            this.AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            this.pedEntity = pedEntity;
        }

        public override void Update()
        {
        }
    }
}

