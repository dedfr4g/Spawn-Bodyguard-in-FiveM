using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Main : BaseScript
    {
        public Main()
        {
            API.RegisterCommand("test", new Action(TestCommand), false);
        }
        private async static void TestCommand()
        {
            Ped player = Game.Player.Character;
            API.RequestModel((uint)PedHash.ChemSec01SMM);
            while(!API.HasModelLoaded((uint)PedHash.ChemSec01SMM))
            {
                Debug.WriteLine("Waiting model to load");
                await BaseScript.Delay(100);
            }
            Ped bodyguard = await World.CreatePed(PedHash.ChemSec01SMM, player.Position + (player.ForwardVector * 2));
            bodyguard.Task.LookAt(player);
            API.SetPedAsGroupMember(bodyguard.Handle, API.GetPedGroupIndex(player.Handle));
            API.SetPedCombatAbility(bodyguard.Handle, 2);
            API.GiveWeaponToPed(bodyguard.Handle, (uint)WeaponHash.AssaultRifleMk2, 500, false, true);
            bodyguard.PlayAmbientSpeech("GENERIC_HI");
        }
    }
}
