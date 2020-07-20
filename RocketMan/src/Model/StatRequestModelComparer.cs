using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RocketMan
{
    public class StatRequestModelComparer : EqualityComparer<StatRequestModel>
    {
        public static StatRequestModelComparer Instance { get; } = new StatRequestModelComparer();

        public override bool Equals(StatRequestModel x, StatRequestModel y)
        {
            if (x.Stat != y.Stat)
                return false;

            StatRequest xReq = x.StatRequest;
            StatRequest yReq = y.StatRequest;

            if (xReq.Thing == yReq.Thing
                && xReq.StuffDef == yReq.StuffDef
                && xReq.QualityCategory == yReq.QualityCategory
                && xReq.Def == yReq.Def
                && xReq.Faction == yReq.Faction
                && xReq.Pawn == yReq.Pawn
                && x.ApplyPostProcess == y.ApplyPostProcess)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode(StatRequestModel obj)
        {
            StatRequest statRequest = obj.StatRequest;

            unchecked
            {
                int hash;
                hash = HashOne(obj.Stat.shortHash);
                hash = HashOne(statRequest.Thing?.thingIDNumber ?? 0, hash);
                hash = HashOne(statRequest.StuffDef?.GetHashCode() ?? 0, hash);
                hash = HashOne((int)statRequest.QualityCategory, hash);
                hash = HashOne(statRequest.Def?.GetHashCode() ?? 0, hash);
                hash = HashOne(statRequest.Faction?.loadID ?? 0, hash);
                hash = HashOne(statRequest.Pawn?.thingIDNumber ?? 0, hash);
                hash = HashOne(obj.ApplyPostProcess ? 1 : 0, hash);

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return obj as StatRequestModelComparer != null;
        }

        public override int GetHashCode()
        {
            return typeof(StatRequestModelComparer).Name.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int HashOne(int numberToHash, int previousHash = 17)
        {
            return previousHash * 7919 + numberToHash;
        }

    }
}
