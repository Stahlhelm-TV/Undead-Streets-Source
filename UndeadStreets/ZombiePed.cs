namespace UndeadStreets
{
    using GTA;
    using GTA.Math;
    using GTA.Native;
    using System;
    using System.Threading.Tasks;

    public class ZombiePed : UpdaterClass
    {
        public Ped pedEntity;
        public Ped target;
        public bool isRunner = false;
        public bool newSearch = true;

        public ZombiePed(Ped pedEntity)
        {
            this.AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            this.pedEntity = pedEntity;
        }

        private static bool CanHearPed(Ped hearer, Ped target)
        {
            float distance = target.Position.DistanceTo(hearer.Position);
            return ((!IsWeaponWellSilenced(target, distance) || IsBehindZombie(distance)) || IsRunningNoticed(target, distance));
        }

        private Ped FindTarget()
        {
            Ped[] nearbyPeds = World.GetNearbyPeds(this.pedEntity.Position, 50f);
            int index = 0;
            while (true)
            {
                Ped ped2;
                if (index >= nearbyPeds.Length)
                {
                    ped2 = null;
                }
                else
                {
                    Ped target = nearbyPeds[index];
                    if (((target == null) || ((target.RelationshipGroup == Relationships.ZombieGroup) || (!target.IsAlive || !target.IsHuman))) || (!this.pedEntity.HasClearLineOfSight(target, 35f) && !CanHearPed(this.pedEntity, target)))
                    {
                        index++;
                        continue;
                    }
                    ped2 = target;
                }
                return ped2;
            }
        }

        private static bool IsBehindZombie(float distance) => 
            (distance < 1f);

        private static bool IsRunningNoticed(Ped ped, float distance) => 
            (ped.IsSprinting && (distance < 25f));

        private static bool IsWeaponWellSilenced(Ped ped, float distance) => 
            (!ped.IsShooting || (ped.IsCurrentWeaponSileced() && (distance > 15f)));

        public override void Update()
        {
            if ((!this.pedEntity.IsAlive || (Extensions.DistanceBetween(this.pedEntity, Game.Player.Character) > 100.0)) && this.pedEntity.CurrentBlip.Exists())
            {
                this.pedEntity.CurrentBlip.Remove();
            }
            if (!(Population.zombieRunners && this.pedEntity.IsRunning))
            {
                InputArgument[] argumentArray5 = new InputArgument[] { this.pedEntity.Handle, 1 };
                Function.Call(Hash._0x9D64D7405520E3D3, argumentArray5);
                InputArgument[] argumentArray6 = new InputArgument[] { this.pedEntity.Handle, true };
                Function.Call(Hash._0xA9A41C1E940FB0E8, argumentArray6);
            }
            else
            {
                InputArgument[] argumentArray1 = new InputArgument[] { this.pedEntity.Handle, 0 };
                Function.Call(Hash._0x9D64D7405520E3D3, argumentArray1);
                InputArgument[] argumentArray2 = new InputArgument[] { this.pedEntity.Handle, false };
                Function.Call(Hash._0xA9A41C1E940FB0E8, argumentArray2);
                InputArgument[] argumentArray3 = new InputArgument[] { this.pedEntity.Handle, 8, 0, 0 };
                Function.Call(Hash._0xBC9AE166038A5CEC, argumentArray3);
                InputArgument[] argumentArray4 = new InputArgument[] { this.pedEntity.Handle, "burning_1", "facials@gen_male@base" };
                Function.Call(Hash._0xE1E65CA8AC9C00ED, argumentArray4);
            }
            InputArgument[] arguments = new InputArgument[] { this.pedEntity.Handle };
            if (Function.Call<bool>(Hash._0x9072C8B49907BFAD, arguments))
            {
                InputArgument[] argumentArray8 = new InputArgument[] { this.pedEntity.Handle };
                Function.Call(Hash._0xB8BEC0CA6F0EDB0F, argumentArray8);
            }
            if (this.isRunner)
            {
                InputArgument[] argumentArray9 = new InputArgument[] { "move_m@injured" };
                if (!Function.Call<bool>(Hash._0xC4EA073D86FB29B0, argumentArray9))
                {
                    InputArgument[] argumentArray10 = new InputArgument[] { "move_m@injured" };
                    Function.Call(Hash._0x6EA47DAE7FAD0EED, argumentArray10);
                }
                InputArgument[] argumentArray11 = new InputArgument[] { this.pedEntity.Handle, "move_m@injured", 0x3e800000 };
                Function.Call(Hash._0xAF8A94EDE7712BEF, argumentArray11);
            }
            else
            {
                InputArgument[] argumentArray12 = new InputArgument[] { "move_m@drunk@verydrunk" };
                if (!Function.Call<bool>(Hash._0xC4EA073D86FB29B0, argumentArray12))
                {
                    InputArgument[] argumentArray13 = new InputArgument[] { "move_m@drunk@verydrunk" };
                    Function.Call(Hash._0x6EA47DAE7FAD0EED, argumentArray13);
                }
                InputArgument[] argumentArray14 = new InputArgument[] { this.pedEntity.Handle, "move_m@drunk@verydrunk", 0x3e800000 };
                Function.Call(Hash._0xAF8A94EDE7712BEF, argumentArray14);
            }
            if (this.target != null)
            {
                if ((!this.target.IsAlive || (Extensions.DistanceBetween(this.pedEntity, this.target) >= 80.0)) || (this.target.RelationshipGroup == Relationships.ZombieGroup))
                {
                    this.target = null;
                    this.newSearch = true;
                    this.target = this.FindTarget();
                    if ((this.target == null) && this.newSearch)
                    {
                        this.pedEntity.Task.WanderAround();
                        this.newSearch = false;
                        return;
                    }
                }
                if (((this.target == null) || (Extensions.DistanceBetween(this.pedEntity, this.target) > 1.2)) || this.target.IsInVehicle())
                {
                    if (!this.isRunner)
                    {
                        this.pedEntity.Task.GoTo(this.target.Position);
                    }
                    else
                    {
                        InputArgument[] argumentArray18 = new InputArgument[] { "move_m@injured" };
                        if (!Function.Call<bool>(Hash._0xC4EA073D86FB29B0, argumentArray18))
                        {
                            InputArgument[] argumentArray19 = new InputArgument[] { "move_m@injured" };
                            Function.Call(Hash._0x6EA47DAE7FAD0EED, argumentArray19);
                        }
                        InputArgument[] argumentArray20 = new InputArgument[] { this.pedEntity.Handle, "move_m@injured", 0x3e800000 };
                        Function.Call(Hash._0xAF8A94EDE7712BEF, argumentArray20);
                        InputArgument[] argumentArray21 = new InputArgument[] { this.pedEntity.Handle, this.target.Handle, -1, 0f, 5f, 0x40000000, 0 };
                        Function.Call(Hash._0x6A071245EB0D1882, argumentArray21);
                    }
                }
                else
                {
                    if (this.target.IsDead)
                    {
                        InputArgument[] argumentArray15 = new InputArgument[] { this.pedEntity, "amb@world_human_bum_wash@male@high@idle_a", "idle_b", 3 };
                        if (!Function.Call<bool>(Hash._0x1F0B79228E461EC9, argumentArray15))
                        {
                            this.pedEntity.Task.PlayAnimation("amb@world_human_bum_wash@male@high@idle_a", "idle_b", 8f, -1, AnimationFlags.Loop);
                            Task.Delay(8);
                        }
                    }
                    InputArgument[] argumentArray16 = new InputArgument[] { this.pedEntity, "rcmbarry", "bar_1_teleport_aln", 3 };
                    if (!Function.Call<bool>(Hash._0x1F0B79228E461EC9, argumentArray16))
                    {
                        Vector3 vector = this.pedEntity.Rotation - Game.Player.Character.Rotation;
                        this.pedEntity.Rotation = vector;
                        this.pedEntity.Task.PlayAnimation("rcmbarry", "bar_1_teleport_aln", 8f, 0x3e8, AnimationFlags.UpperBodyOnly);
                        Task.Delay(8);
                        if (this.target == Game.Player.Character)
                        {
                            this.target.ApplyDamage(50);
                            this.target.Kill();
                        }
                        else
                        {
                            this.target.ApplyDamage(50);
                            if (RandoMath.CachedRandom.Next(0, 2) != 0)
                            {
                                this.target.Weapons.Drop();
                                this.target.Kill();
                                if (this.target.CurrentBlip.Exists())
                                {
                                    this.target.CurrentBlip.Remove();
                                }
                            }
                            else
                            {
                                InputArgument[] argumentArray17 = new InputArgument[] { this.target.Handle, 0xbb8, 0, 0, false, false, false };
                                Function.Call(Hash._0xAE99FB955581844A, argumentArray17);
                                this.target.Weapons.Drop();
                                if (this.target.CurrentBlip.Exists())
                                {
                                    this.target.CurrentBlip.Remove();
                                }
                                Population.Infect(this.target);
                                this.target.LeaveGroup();
                                this.target.Weapons.Drop();
                                this.target = null;
                                this.newSearch = true;
                                this.target = this.FindTarget();
                                if ((this.target == null) && this.newSearch)
                                {
                                    this.pedEntity.Task.WanderAround();
                                    this.newSearch = false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.target = this.FindTarget();
                if ((this.target == null) && this.newSearch)
                {
                    this.pedEntity.Task.WanderAround();
                    this.newSearch = false;
                }
            }
        }
    }
}

