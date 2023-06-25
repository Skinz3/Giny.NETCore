using Giny.World.Records.Idols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Idols
{
    public class IdolXp
    {
        public int ExperienceBonusPercent
        {
            get;
            private set;
        }
        public int LootBonusPercent
        {
            get;
            private set;
        }
        public int TotalScore
        {
            get;
            set;
        }
        public IdolXp(IEnumerable<IdolRecord> idols)
        {
            int totalScoreWithoutCoeff = 0;
            double totalScore = 0;
            double totalExp = 0;
            double totalLoot = 0;

            foreach (var idol in idols)
            {
                double coeff = GetIdolCoeff(idol, idols);

                totalScore = totalScore + Math.Floor(idol.Score * coeff);
                totalExp = totalExp + Math.Floor(idol.ExperienceBonus * coeff);
                totalLoot = totalLoot + Math.Floor(idol.DropBonus * coeff);
                totalScoreWithoutCoeff = totalScoreWithoutCoeff + idol.Score;
            }


            this.TotalScore = (int)totalScore;
            this.ExperienceBonusPercent = (int)totalExp;
            this.LootBonusPercent = (int)totalLoot;
        }

        public double GetIdolCoeff(IdolRecord idol, IEnumerable<IdolRecord> idols, bool pAddSynergy = true)
        {
            int i = 0;
            int j = 0;
            double coeff = 1;

            int numActiveIdols = idols.Count();

            for (i = 0; i < numActiveIdols; i++)
            {
                for (j = 0; j < idol.SynergyIdolsIds.Count; j++)
                {
                    if (idol.SynergyIdolsIds[j] == idols.ElementAt(i).Id)
                    {
                        coeff = coeff * idol.SynergyIdolsCoeff[j];
                    }
                }
            }
            return coeff;
        }

        public IdolXp(int experienceBonusPercent, int lootBonusPercent)
        {
            this.ExperienceBonusPercent = experienceBonusPercent;
            this.LootBonusPercent = lootBonusPercent;
        }
    }
}
