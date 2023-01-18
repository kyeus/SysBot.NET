using System;
using System.Buffers.Binary;
using PKHeX.Core;

namespace SysBot.Pokemon
{
    public sealed class TradePartnerSV
    {
        public string TID7 { get; }
        public string SID7 { get; }
        public string TrainerName { get; }
        public byte Game { get; }
        public byte Gender { get; }
        public byte Language { get; }

        public TradePartnerSV(TradeMyStatus info)
        {
            TID7 = info.DisplayTID.ToString("D6");
            SID7 = info.DisplaySID.ToString("D4");
            TrainerName = info.OT;
            Game = info.Game;
            Gender = info.Gender;
            Language = info.Language;
        }
    }

    public sealed class TradeMyStatus
    {
        public readonly byte[] Data = new byte[0x30];

        public uint DisplaySID => BinaryPrimitives.ReadUInt32LittleEndian(Data.AsSpan(0)) / 1_000_000;
        public uint DisplayTID => BinaryPrimitives.ReadUInt32LittleEndian(Data.AsSpan(0)) % 1_000_000;

        public byte Game => Data[4];
        public byte Gender => Data[5];
        public byte Language => Data[6];

        public string OT => StringConverter8.GetString(Data.AsSpan(8, 24));
    }
}
