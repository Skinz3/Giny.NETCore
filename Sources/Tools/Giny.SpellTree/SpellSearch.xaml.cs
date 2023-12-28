using Giny.ORM;
using Giny.Protocol.Enums;
using Giny.SpellTree.Graphics;
using Giny.World;
using Giny.World.Managers.Effects;
using Giny.World.Records.Effects;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Giny.World.Managers.Effects.Targets;

namespace Giny.SpellTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SpellSearch : Window
    {

        public SpellSearch()
        {
            InitializeComponent();

            searchType.Items.Add("Altération état");
            searchType.Items.Add("Parents");

            searchType.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<SpellRecord> results = new List<SpellRecord>();

            if (searchType.SelectedIndex == 1)
            {
                var levels = SpellLevelRecord.GetLevelsCastingSpell(short.Parse(searchText.Text));

                foreach (var level in levels)
                {
                    SpellRecord record = SpellRecord.GetSpellRecord(level.SpellId);

                    if (!results.Contains(record))
                    {
                        results.Add(record);
                    }
                }
            }
            if (searchType.SelectedIndex == 0)
            {
                foreach (var level in SpellLevelRecord.GetSpellLevels())
                {
                    foreach (var effect in level.Effects.OfType<EffectDice>())
                    {
                        foreach (var target in effect.GetTargets())
                        {
                            var stateCr = target as StateCriterion;
                            if (stateCr == null)
                            {
                                continue;
                            }
                            var state = SpellStateRecord.GetSpellStateRecord(stateCr.State);

                            if (state == null)
                            {
                                continue;
                            }
                            var spellRecord = SpellRecord.GetSpellRecord(level.SpellId);

                            if (state.Name.ToLower().Contains(searchText.Text.ToLower())
                            && !results.Contains(spellRecord))
                            {
                                results.Add(spellRecord);
                            }
                        }
                        if (effect.EffectEnum == EffectsEnum.Effect_AddState
                            || effect.EffectEnum == EffectsEnum.Effect_DisableState
                            || effect.EffectEnum == EffectsEnum.Effect_DispelState)
                        {
                            var state = SpellStateRecord.GetSpellStateRecord(effect.Value);


                            if (state != null)
                            {

                                var spellRecord = SpellRecord.GetSpellRecord(level.SpellId);

                                if (state.Name.ToLower().Contains(searchText.Text.ToLower())
                               && !results.Contains(spellRecord))
                                {
                                    results.Add(spellRecord);
                                }
                            }

                        }
                    }
                }
            }


            spells.Items.Clear();


            foreach (var result in results)
            {
                spells.Items.Add(result);
            }
        }

        private void spells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var spellRecord = (SpellRecord)spells.SelectedItem;

            if (spellRecord == null)
            {
                return;
            }
            MainWindow.Instance.DrawSpell(spellRecord, spellRecord.Levels.First());
        }
    }
}
