namespace UndeadStreets
{
    using GTA;
    using GTA.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class BlipController : Script
    {
        public BlipController()
        {
            base.Tick += new EventHandler(this.OnTick);
        }

        [IteratorStateMachine(typeof(<GetAllBlips>d__4))]
        public static IEnumerable<Blip> GetAllBlips() => 
            new <GetAllBlips>d__4(-2);

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                this.RemoveAllUnrequiredBlips();
                this.PersonalVehicleBlip();
            }
        }

        public void PersonalVehicleBlip()
        {
            if (Character.playerVehicle != null)
            {
                if (Game.Player.Character.IsInVehicle(Character.playerVehicle))
                {
                    if (Character.playerVehicle.CurrentBlip.Exists())
                    {
                        Character.playerVehicle.CurrentBlip.Alpha = 0;
                    }
                }
                else if (Character.playerVehicle.CurrentBlip.Exists())
                {
                    Character.playerVehicle.CurrentBlip.Alpha = 0xff;
                }
            }
            if ((Character.playerVehicle != null) && (Character.playerVehicle.Health == 0))
            {
                Character.playerVehicle.CurrentBlip.Remove();
                Character.playerVehicle = null;
            }
        }

        public void RemoveAllUnrequiredBlips()
        {
            InputArgument[] arguments = new InputArgument[] { true };
            Function.Call(Hash._0xB98236CAAECEF897, arguments);
            foreach (Blip blip in GetAllBlips())
            {
                if (blip.Sprite == BlipSprite.Blimp)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.PersonalVehicleCar)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.PersonalVehicleBike)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.Sub)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.Plane)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.Helicopter)
                {
                    blip.Alpha = 0;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <GetAllBlips>d__4 : IEnumerable<Blip>, IEnumerable, IEnumerator<Blip>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Blip <>2__current;
            private int <>l__initialThreadId;
            private IEnumerator <>s__1;
            private BlipSprite <sprite>5__2;
            private int <Handle>5__3;

            [DebuggerHidden]
            public <GetAllBlips>d__4(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                IDisposable disposable = this.<>s__1 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>s__1 = Enum.GetValues(typeof(BlipSprite)).GetEnumerator();
                        this.<>1__state = -3;
                        goto TR_0005;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                        InputArgument[] arguments = new InputArgument[] { (InputArgument) this.<sprite>5__2 };
                        this.<Handle>5__3 = Function.Call<int>(Hash._0x14F96AA50D6FBEA7, arguments);
                    }
                    else
                    {
                        return false;
                    }
                    goto TR_0009;
                TR_0005:
                    if (this.<>s__1.MoveNext())
                    {
                        this.<sprite>5__2 = (BlipSprite) this.<>s__1.Current;
                        InputArgument[] arguments = new InputArgument[] { (InputArgument) this.<sprite>5__2 };
                        this.<Handle>5__3 = Function.Call<int>(Hash._0x1BEDE233E6CD2A1F, arguments);
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>s__1 = null;
                        return false;
                    }
                TR_0009:
                    while (true)
                    {
                        InputArgument[] arguments = new InputArgument[] { this.<Handle>5__3 };
                        if (Function.Call<bool>(Hash._0xA6DB27D19ECBB7DA, arguments))
                        {
                            this.<>2__current = new Blip(this.<Handle>5__3);
                            this.<>1__state = 1;
                            flag = true;
                        }
                        else
                        {
                            goto TR_0005;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<Blip> IEnumerable<Blip>.GetEnumerator()
            {
                BlipController.<GetAllBlips>d__4 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new BlipController.<GetAllBlips>d__4(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<GTA.Blip>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            Blip IEnumerator<Blip>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

