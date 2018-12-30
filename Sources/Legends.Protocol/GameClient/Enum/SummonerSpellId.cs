using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Enum
{
    public enum SummonerSpellId : uint
    {
        SummonerRevive = 0x05C8B3A5,
        SummonerSmite = 0x065E8695,
        SummonerExhaust = 0x08A8BAE4,
        SummonerBarrier = 0x0CCFB982,
        SummonerTeleport = 0x004F1364,
        SummonerGhost = 0x064ACC95,
        SummonerHeal = 0x0364AF1C,
        SummonerCleanse = 0x064D2094,
        SummonerClarity = 0x03657421,
        SummonerIgnite = 0x06364F24,
        SummonerPromote = 0x0410FF72,
        SummonerClair = 0x09896765,
        SummonerFlash = 0x06496EA8,
        SummonerTest = 0x0103D94C,
    };
}
