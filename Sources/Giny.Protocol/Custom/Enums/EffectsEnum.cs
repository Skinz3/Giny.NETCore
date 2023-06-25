namespace Giny.Protocol.Enums
{
    public enum EffectsEnum
    {
        /// <summary>
        /// Téléporte sur la map ciblée
        /// </summary>
        Effect_2 = 2,
        /// <summary>
        /// Fixe le point de respawn
        /// </summary>
        Effect_3 = 3,
        /// <summary>
        /// Téléporte sur la case ciblée
        /// </summary>
        Effect_Teleport = 4,
        /// <summary>
        /// Repousse de #1 case(s)
        /// </summary>
        Effect_PushBack = 5,
        /// <summary>
        /// Attire de #1 case(s)
        /// </summary>
        Effect_PullForward = 6,
        /// <summary>
        /// Divorcer le couple
        /// </summary>
        Effect_7 = 7,
        /// <summary>
        /// Échange de positions
        /// </summary>
        Effect_SwitchPosition = 8,
        /// <summary>
        /// Esquive #1% des coups en reculant de #2 case(s)
        /// </summary>
        Effect_DodgeDamages = 9,
        /// <summary>
        /// Attitude #3
        /// </summary>
        Effect_LearnEmote = 10,
        /// <summary>
        /// 
        /// </summary>
        Effect_12 = 12,
        /// <summary>
        /// Change le temps de jeu d'un joueur
        /// </summary>
        Effect_13 = 13,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_30 = 30,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_31 = 31,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_32 = 32,
        /// <summary>
        /// Débute une quête
        /// </summary>
        Effect_34 = 34,
        /// <summary>
        /// Reset d'une quête
        /// </summary>
        Effect_36 = 36,
        /// <summary>
        /// Démarre une quête (force)
        /// </summary>
        Effect_37 = 37,
        /// <summary>
        /// Porte la cible
        /// </summary>
        Effect_Carry = 50,
        /// <summary>
        /// Lance une entité
        /// </summary>
        Effect_Throw = 51,
        /// <summary>
        /// Vole #1{~1~2 à }#2 PM
        /// </summary>
        Effect_StealMP_77 = 77,
        /// <summary>
        /// Ajoute +#1{~1~2 à }#2 PM
        /// </summary>
        Effect_AddMP = 78,
        /// <summary>
        /// #3% soigné de x#2, sinon dégâts subis x#1
        /// </summary>
        Effect_HealOrMultiply = 79,
        /// <summary>
        /// #1{~1~2 à }#2 (PV rendus)
        /// </summary>
        Effect_HealHP_81 = 81,
        /// <summary>
        /// #1{~1~2 à }#2 PV (vol Neutre fixe)
        /// </summary>
        Effect_StealHPFix = 82,
        /// <summary>
        /// Vole #1{~1~2 à }#2 PA
        /// </summary>
        Effect_StealAP_84 = 84,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de l'attaquant (dommages Eau)
        /// </summary>
        Effect_DamagePercentWater = 85,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de l'attaquant (dommages Terre)
        /// </summary>
        Effect_DamagePercentEarth = 86,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de l'attaquant (dommages Air)
        /// </summary>
        Effect_DamagePercentAir = 87,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de l'attaquant (dommages Feu)
        /// </summary>
        Effect_DamagePercentFire = 88,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de l'attaquant (dommages Neutre)
        /// </summary>
        Effect_DamagePercentNeutral = 89,
        /// <summary>
        /// Donne #1{~1~2 à }#2 % de sa vie
        /// </summary>
        Effect_GiveHPPercent = 90,
        /// <summary>
        /// #1{~1~2 à }#2 (vol Eau)
        /// </summary>
        Effect_StealHPWater = 91,
        /// <summary>
        /// #1{~1~2 à }#2 (vol Terre)
        /// </summary>
        Effect_StealHPEarth = 92,
        /// <summary>
        /// #1{~1~2 à }#2 (vol Air)
        /// </summary>
        Effect_StealHPAir = 93,
        /// <summary>
        /// #1{~1~2 à }#2 (vol Feu)
        /// </summary>
        Effect_StealHPFire = 94,
        /// <summary>
        /// #1{~1~2 à }#2 (vol Neutre)
        /// </summary>
        Effect_StealHPNeutral = 95,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Eau)
        /// </summary>
        Effect_DamageWater = 96,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Terre)
        /// </summary>
        Effect_DamageEarth = 97,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Air)
        /// </summary>
        Effect_DamageAir = 98,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Feu)
        /// </summary>
        Effect_DamageFire = 99,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Neutre)
        /// </summary>
        Effect_DamageNeutral = 100,
        /// <summary>
        /// -#1{~1~2 à -}#2 PA
        /// </summary>
        Effect_LostAP = 101,

        Effect_RemoveAP = 104,
        /// <summary>
        /// Dommages réduits de #1{~1~2 à }#2
        /// </summary>
        Effect_AddGlobalDamageReduction_105 = 105,
        /// <summary>
        /// Renvoie un sort de niveau #2 maximum
        /// </summary>
        Effect_ReflectSpell = 106,
        /// <summary>
        /// Dommages retournés : #1{~1~2 à }#2
        /// </summary>
        Effect_ReflectDamage = 107,
        /// <summary>
        /// #1{~1~2 à }#2 (PV rendus)
        /// </summary>
        Effect_HealHP_108 = 108,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages au lanceur)
        /// </summary>
        Effect_109 = 109,
        /// <summary>
        /// #1{~1~2 à }#2 Vie
        /// </summary>
        Effect_AddHealth = 110,
        /// <summary>
        /// #1{~1~2 à }#2 PA
        /// </summary>
        Effect_AddAP_111 = 111,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages
        /// </summary>
        Effect_AddDamageBonus = 112,
        /// <summary>
        /// Double les dommages ou rend  #1{~1~2 à }#2 PDV
        /// </summary>
        Effect_113 = 113,
        /// <summary>
        /// Multiplie les dommages par #1
        /// </summary>
        Effect_AddDamageMultiplicator = 114,
        /// <summary>
        /// #1{~1~2 à }#2% Critique
        /// </summary>
        Effect_AddCriticalHit = 115,
        /// <summary>
        /// -#1{~1~2 à -}#2 PO
        /// </summary>
        Effect_SubRange = 116,
        /// <summary>
        /// #1{~1~2 à }#2 PO
        /// </summary>
        Effect_AddRange = 117,
        /// <summary>
        /// #1{~1~2 à }#2 Force
        /// </summary>
        Effect_AddStrength = 118,
        /// <summary>
        /// #1{~1~2 à }#2 Agilité
        /// </summary>
        Effect_AddAgility = 119,
        /// <summary>
        /// Ajoute +#1{~1~2 à }#2 PA
        /// </summary>
        Effect_RegainAP = 120,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages
        /// </summary>
        Effect_AddDamageBonus_121 = 121,
        /// <summary>
        /// #1{~1~2 à }#2 Échecs Critiques
        /// </summary>
        Effect_AddCriticalMiss = 122,
        /// <summary>
        /// #1{~1~2 à }#2 Chance
        /// </summary>
        Effect_AddChance = 123,
        /// <summary>
        /// #1{~1~2 à }#2 Sagesse
        /// </summary>
        Effect_AddWisdom = 124,
        /// <summary>
        /// #1{~1~2 à }#2 Vitalité
        /// </summary>
        Effect_AddVitality = 125,
        /// <summary>
        /// #1{~1~2 à }#2 Intelligence
        /// </summary>
        Effect_AddIntelligence = 126,
        /// <summary>
        /// -#1{~1~2 à -}#2 PM
        /// </summary>
        Effect_LostMP = 127,
        /// <summary>
        /// #1{~1~2 à }#2 PM
        /// </summary>
        Effect_AddMP_128 = 128,
        /// <summary>
        /// Vole #1{~1~2 à }#2 Kamas
        /// </summary>
        Effect_StealKamas = 130,
        /// <summary>
        /// #1 PA utilisés font perdre #2 PV
        /// </summary>
        Effect_LoseHPByUsingAP = 131,
        /// <summary>
        /// Enlève les envoûtements
        /// </summary>
        Effect_DispelMagicEffects = 132,
        /// <summary>
        /// PA perdus pour le lanceur : #1{~1~2 à }#2
        /// </summary>
        Effect_LosingAP = 133,
        /// <summary>
        /// PM perdus pour le lanceur : #1{~1~2 à }#2
        /// </summary>
        Effect_LosingMP = 134,
        /// <summary>
        /// Portée du lanceur réduite de : #1{~1~2 à }#2
        /// </summary>
        Effect_SubRange_135 = 135,
        /// <summary>
        /// Portée du lanceur augmentée de : #1{~1~2 à }#2
        /// </summary>
        Effect_AddRange_136 = 136,
        /// <summary>
        /// Dommages physiques du lanceur augmentés de : #1{~1~2 à }#2
        /// </summary>
        Effect_AddPhysicalDamage_137 = 137,
        /// <summary>
        /// #1{~1~2 à }#2 Puissance
        /// </summary>
        Effect_IncreaseDamage_138 = 138,
        /// <summary>
        /// Rend #1{~1~2 à }#2 points d'énergie
        /// </summary>
        Effect_RestoreEnergyPoints = 139,
        /// <summary>
        /// Tour annulé
        /// </summary>
        Effect_SkipTurn = 140,
        /// <summary>
        /// Tue la cible
        /// </summary>
        Effect_Kill = 141,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Physiques
        /// </summary>
        Effect_AddPhysicalDamage_142 = 142,
        /// <summary>
        /// #1{~1~2 à }#2 (PV rendus)
        /// </summary>
        Effect_HealHP_143 = 143,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Neutre fixe)
        /// </summary>
        Effect_DamageNeutralFix = 144,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages
        /// </summary>
        Effect_SubDamageBonus = 145,
        /// <summary>
        /// Change les paroles
        /// </summary>
        Effect_146 = 146,
        /// <summary>
        /// Ressuscite un allié
        /// </summary>
        Effect_ReviveAlly = 147,
        /// <summary>
        /// Quelqu'un vous suit !
        /// </summary>
        Effect_Followed = 148,
        /// <summary>
        /// Change l'apparence
        /// </summary>
        Effect_ChangeAppearance = 149,
        /// <summary>
        /// Rend le personnage invisible
        /// </summary>
        Effect_Invisibility = 150,
        /// <summary>
        /// -#1{~1~2 à -}#2 Chance
        /// </summary>
        Effect_SubChance = 152,
        /// <summary>
        /// -#1{~1~2 à -}#2 Vitalité
        /// </summary>
        Effect_SubVitality = 153,
        /// <summary>
        /// -#1{~1~2 à -}#2 Agilité
        /// </summary>
        Effect_SubAgility = 154,
        /// <summary>
        /// -#1{~1~2 à -}#2 Intelligence
        /// </summary>
        Effect_SubIntelligence = 155,
        /// <summary>
        /// -#1{~1~2 à -}#2 Sagesse
        /// </summary>
        Effect_SubWisdom = 156,
        /// <summary>
        /// -#1{~1~2 à -}#2 Force
        /// </summary>
        Effect_SubStrength = 157,
        /// <summary>
        /// #1{~1~2 à }#2 Pods
        /// </summary>
        Effect_IncreaseWeight = 158,
        /// <summary>
        /// -#1{~1~2 à }#2 Pods
        /// </summary>
        Effect_DecreaseWeight = 159,
        /// <summary>
        /// #1{~1~2 à }#2 Esquive PA
        /// </summary>
        Effect_AddDodgeAPProbability = 160,
        /// <summary>
        /// #1{~1~2 à }#2 Esquive PM
        /// </summary>
        Effect_AddDodgeMPProbability = 161,
        /// <summary>
        /// -#1{~1~2 à }#2 Esquive PA
        /// </summary>
        Effect_SubDodgeAPProbability = 162,
        /// <summary>
        /// -#1{~1~2 à }#2 Esquive PM
        /// </summary>
        Effect_SubDodgeMPProbability = 163,
        /// <summary>
        /// Dommages réduits de #1%
        /// </summary>
        Effect_AddGlobalDamageReduction = 164,
        /// <summary>
        /// #2% Dommages #1
        /// </summary>
        Effect_AddDamageBonusPercent = 165,
        /// <summary>
        /// PA retournés : #1{~1~2 à }#2
        /// </summary>
        Effect_166 = 166,
        /// <summary>
        /// -#1{~1~2 à -}#2 PA
        /// </summary>
        Effect_SubAP = 168,
        /// <summary>
        /// -#1{~1~2 à -}#2 PM
        /// </summary>
        Effect_SubMP = 169,
        /// <summary>
        /// -#1{~1~2 à }#2% Critique
        /// </summary>
        Effect_SubCriticalHit = 171,
        /// <summary>
        /// -#1{~1~2 à }#2 Réduction Magique
        /// </summary>
        Effect_SubMagicDamageReduction = 172,
        /// <summary>
        /// -#1{~1~2 à }#2 Réduction Physique
        /// </summary>
        Effect_SubPhysicalDamageReduction = 173,
        /// <summary>
        /// #1{~1~2 à }#2 Initiative
        /// </summary>
        Effect_AddInitiative = 174,
        /// <summary>
        /// -#1{~1~2 à }#2 Initiative
        /// </summary>
        Effect_SubInitiative = 175,
        /// <summary>
        /// #1{~1~2 à }#2 Prospection
        /// </summary>
        Effect_AddProspecting = 176,
        /// <summary>
        /// -#1{~1~2 à }#2 Prospection
        /// </summary>
        Effect_SubProspecting = 177,
        /// <summary>
        /// #1{~1~2 à }#2 Soins
        /// </summary>
        Effect_AddHealBonus = 178,
        /// <summary>
        /// -#1{~1~2 à }#2 Soins
        /// </summary>
        Effect_SubHealBonus = 179,
        /// <summary>
        /// Crée un double du lanceur de sort
        /// </summary>
        Effect_Double = 180,
        /// <summary>
        /// Invoque : #1
        /// </summary>
        Effect_Summon = 181,
        /// <summary>
        /// #1{~1~2 à }#2 Invocations
        /// </summary>
        Effect_AddSummonLimit = 182,
        /// <summary>
        /// #1{~1~2 à }#2 Réduction Magique
        /// </summary>
        Effect_AddMagicDamageReduction = 183,
        /// <summary>
        /// #1{~1~2 à }#2 Réduction Physique
        /// </summary>
        Effect_AddPhysicalDamageReduction = 184,
        /// <summary>
        /// Invoque : #1 (statique)
        /// </summary>
        Effect_185 = 185,
        /// <summary>
        /// -#1{~1~2 à }#2 Puissance
        /// </summary>
        Effect_SubDamageBonusPercent = 186,
        /// <summary>
        /// Changer l'alignement
        /// </summary>
        Effect_188 = 188,
        /// <summary>
        /// 
        /// </summary>
        Effect_192 = 192,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_193 = 193,
        /// <summary>
        /// Gagner #1{~1~2 à }#2 Kamas
        /// </summary>
        Effect_GiveKamas = 194,
        /// <summary>
        /// Transforme en #1
        /// </summary>
        Effect_197 = 197,
        /// <summary>
        /// Pose un objet au sol
        /// </summary>
        Effect_201 = 201,
        /// <summary>
        /// Dévoile tous les objets invisibles
        /// </summary>
        Effect_RevealsInvisible = 202,
        /// <summary>
        /// Ressuscite la cible
        /// </summary>
        Effect_206 = 206,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Terre
        /// </summary>
        Effect_AddEarthResistPercent = 210,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Eau
        /// </summary>
        Effect_AddWaterResistPercent = 211,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Air
        /// </summary>
        Effect_AddAirResistPercent = 212,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Feu
        /// </summary>
        Effect_AddFireResistPercent = 213,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Neutre
        /// </summary>
        Effect_AddNeutralResistPercent = 214,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Terre
        /// </summary>
        Effect_SubEarthResistPercent = 215,
        /// <summary>
        /// -#1{~1~2 à }#2 % Résistance Eau
        /// </summary>
        Effect_SubWaterResistPercent = 216,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Air
        /// </summary>
        Effect_SubAirResistPercent = 217,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Feu
        /// </summary>
        Effect_SubFireResistPercent = 218,
        /// <summary>
        /// -#1{~1~2 à }#2 % Résistance Neutre
        /// </summary>
        Effect_SubNeutralResistPercent = 219,
        /// <summary>
        /// Renvoie #1{~1~2 à }#2 dommages
        /// </summary>
        Effect_AddDamageReflection_220 = 220,
        /// <summary>
        /// Qu'y a-t-il là dedans ?
        /// </summary>
        Effect_221 = 221,
        /// <summary>
        /// Qu'y a-t-il là dedans ?
        /// </summary>
        Effect_222 = 222,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Pièges
        /// </summary>
        Effect_AddTrapBonus = 225,
        /// <summary>
        /// #1{~1~2 à }#2 Puissance (pièges)
        /// </summary>
        Effect_AddTrapBonusPercent = 226,
        /// <summary>
        /// Récupère une monture !
        /// </summary>
        Effect_229 = 229,
        /// <summary>
        /// #1 Énergie Perdue
        /// </summary>
        Effect_230 = 230,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_239 = 239,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Terre
        /// </summary>
        Effect_AddEarthElementReduction = 240,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Eau
        /// </summary>
        Effect_AddWaterElementReduction = 241,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Air
        /// </summary>
        Effect_AddAirElementReduction = 242,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Feu
        /// </summary>
        Effect_AddFireElementReduction = 243,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Neutre
        /// </summary>
        Effect_AddNeutralElementReduction = 244,
        /// <summary>
        /// -#1{~1~2 à }#2 Résistance Terre
        /// </summary>
        Effect_SubEarthElementReduction = 245,
        /// <summary>
        /// -#1{~1~2 à }#2 Résistance Eau
        /// </summary>
        Effect_SubWaterElementReduction = 246,
        /// <summary>
        /// -#1{~1~2 à }#2 Résistance Air
        /// </summary>
        Effect_SubAirElementReduction = 247,
        /// <summary>
        /// -#1{~1~2 à }#2 Résistance Feu
        /// </summary>
        Effect_SubFireElementReduction = 248,
        /// <summary>
        /// -#1{~1~2 à }#2 Résistance Neutre
        /// </summary>
        Effect_SubNeutralElementReduction = 249,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Terre JCJ
        /// </summary>
        Effect_AddPvpEarthResistPercent = 250,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Eau JCJ
        /// </summary>
        Effect_AddPvpWaterResistPercent = 251,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Air JCJ
        /// </summary>
        Effect_AddPvpAirResistPercent = 252,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Feu JCJ
        /// </summary>
        Effect_AddPvpFireResistPercent = 253,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance Neutre JCJ
        /// </summary>
        Effect_AddPvpNeutralResistPercent = 254,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Terre JCJ
        /// </summary>
        Effect_SubPvpEarthResistPercent = 255,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Eau JCJ
        /// </summary>
        Effect_SubPvpWaterResistPercent = 256,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Air JCJ
        /// </summary>
        Effect_SubPvpAirResistPercent = 257,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Feu JCJ
        /// </summary>
        Effect_SubPvpFireResistPercent = 258,
        /// <summary>
        /// -#1{~1~2 à }#2% Résistance Neutre JCJ
        /// </summary>
        Effect_SubPvpNeutralResistPercent = 259,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Terre JCJ
        /// </summary>
        Effect_AddPvpEarthElementReduction = 260,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Eau JCJ
        /// </summary>
        Effect_AddPvpWaterElementReduction = 261,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Air JCJ
        /// </summary>
        Effect_AddPvpAirElementReduction = 262,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Feu JCJ
        /// </summary>
        Effect_AddPvpFireElementReduction = 263,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Neutre JCJ
        /// </summary>
        Effect_AddPvpNeutralElementReduction = 264,
        /// <summary>
        /// Dommages réduits de #1{~1~2 à }#2
        /// </summary>
        Effect_ArmorDamageReduction = 265,
        /// <summary>
        /// Vole #1{~1~2 à -}#2 Chance
        /// </summary>
        Effect_StealChance = 266,
        /// <summary>
        /// Vole #1{~1~2 à -}#2 Vitalité
        /// </summary>
        Effect_StealVitality = 267,
        /// <summary>
        /// Vole #1{~1~2 à -}#2 Agilité
        /// </summary>
        Effect_StealAgility = 268,
        /// <summary>
        /// Vole #1{~1~2 à -}#2 Intelligence
        /// </summary>
        Effect_StealIntelligence = 269,
        /// <summary>
        /// Vole #1{~1~2 à -}#2 Sagesse
        /// </summary>
        Effect_StealWisdom = 270,
        /// <summary>
        /// Vole #1{~1~2 à -}#2 Force
        /// </summary>
        Effect_StealStrength = 271,
        /// <summary>
        /// #1{~1~2 à }#2% des PV manquants de l'attaquant (dommages Eau)
        /// </summary>
        Effect_DamageWaterPerHPLost = 275,
        /// <summary>
        /// #1{~1~2 à }#2% des PV manquants de l'attaquant (dommages Terre)
        /// </summary>
        Effect_DamageEarthPerHPLost = 276,
        /// <summary>
        /// #1{~1~2 à }#2% des PV manquants de l'attaquant (dommages Air)
        /// </summary>
        Effect_DamageAirPerHPLost = 277,
        /// <summary>
        /// #1{~1~2 à }#2% des PV manquants de l'attaquant (dommages Feu)
        /// </summary>
        Effect_DamageFirePerHPLost = 278,
        /// <summary>
        /// #1{~1~2 à }#2% des PV manquants de l'attaquant (dommages Neutre)
        /// </summary>
        Effect_DamageNeutralPerHPLost = 279,
        /// <summary>
        /// Augmente la portée minimale du sort #1 de #3
        /// </summary>
        Effect_SpellBoostMinimalRange = 280,
        /// <summary>
        /// Augmente la PO du sort #1 de #3
        /// </summary>
        Effect_SpellBoostRange = 281,
        /// <summary>
        /// Rend la portée du sort #1 modifiable
        /// </summary>
        Effect_282 = 282,
        /// <summary>
        /// +#3 Dommages sur le sort #1
        /// </summary>
        Effect_IncreaseDamageOfTheSpell = 283,
        /// <summary>
        /// +#3 Soins sur le sort #1
        /// </summary>
        Effect_AddHealBonusOnSpell = 284,
        /// <summary>
        /// Réduit de #3 le coût en PA du sort #1
        /// </summary>
        Effect_SpellReduceApCost = 285,
        /// <summary>
        /// Réduit de #3 le délai de relance du sort #1
        /// </summary>
        Effect_ReduceSpellCooldownDelay = 286,
        /// <summary>
        /// +#3% Critique sur le sort #1
        /// </summary>
        Effect_AddCriticalForSpell = 287,
        /// <summary>
        /// Désactive le lancer en ligne du sort #1
        /// </summary>
        Effect_288 = 288,
        /// <summary>
        /// Désactive la ligne de vue du sort #1
        /// </summary>
        Effect_DisableLOS = 289,
        /// <summary>
        /// Augmente de #3 le nombre de lancer maximal par tour du sort #1
        /// </summary>
        Effect_IncreaseMaxNumberOfCastPerTurn = 290,
        /// <summary>
        /// Augmente de #3 le nombre de lancer maximal par cible du sort #1
        /// </summary>
        Effect_IncreaseMaxNumberOfCastPerTarget = 291,
        /// <summary>
        /// Fixe à #3 le délai de relance du sort #1
        /// </summary>
        Effect_CooldownSet_292 = 292,
        /// <summary>
        /// Augmente les dégâts de base du sort #1 de #3
        /// </summary>
        Effect_SpellBoostBaseDamage = 293,
        /// <summary>
        /// Diminue la portée maximale du sort #1 de #3
        /// </summary>
        Effect_ReduceSpellRange = 294,
        /// <summary>
        /// Diminue la portée minimale du sort #1 de #3
        /// </summary>
        Effect_ReduceSpellMinimalRange = 295,
        /// <summary>
        /// Augmente de #3 le coût en PA du sort #1
        /// </summary>
        Effect_IncreaseSpellAPCost = 296,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_310 = 310,
        /// <summary>
        /// Vole #1{~1~2 à }#2 PO
        /// </summary>
        Effect_StealRange = 320,
        /// <summary>
        /// Change une couleur
        /// </summary>
        Effect_ChangeColor = 333,
        /// <summary>
        /// Change l'apparence
        /// </summary>
        Effect_ChangeAppearance_335 = 335,
        /// <summary>
        /// 
        /// </summary>
        Effect_350 = 350,
        /// <summary>
        /// 
        /// </summary>
        Effect_PowerSink = 351,
        /// <summary>
        /// Pose un piège de rang #2
        /// </summary>
        Effect_Trap = 400,
        /// <summary>
        /// Pose un glyphe de rang #2
        /// </summary>
        Effect_TurnBeginGlyph = 401,
        /// <summary>
        /// Pose un glyphe de rang #2
        /// </summary>
        Effect_TurnEndGlyph = 402,
        /// <summary>
        /// Tue la cible pour la remplacer par l'invocation : #1
        /// </summary>
        Effect_KillAndSummon = 405,
        /// <summary>
        /// Enlève les effets du sort #2
        /// </summary>
        Effect_RemoveSpellEffects = 406,
        /// <summary>
        /// #1{~1~2 à }#2 (PV rendus)
        /// </summary>
        Effect_HealHP_407 = 407,
        /// <summary>
        /// #1{~1~2 à }#2 Retrait PA
        /// </summary>
        Effect_AddAPAttack = 410,
        /// <summary>
        /// -#1{~1~2 à }#2 Retrait PA
        /// </summary>
        Effect_SubAPAttack = 411,
        /// <summary>
        /// #1{~1~2 à }#2 Retrait PM
        /// </summary>
        Effect_AddMPAttack = 412,
        /// <summary>
        /// -#1{~1~2 à }#2 Retrait PM
        /// </summary>
        Effect_SubMPAttack = 413,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Poussée
        /// </summary>
        Effect_AddPushDamageBonus = 414,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages Poussée
        /// </summary>
        Effect_SubPushDamageBonus = 415,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Poussée
        /// </summary>
        Effect_AddPushDamageReduction = 416,
        /// <summary>
        /// -#1{~1~2 à }#2 Résistance Poussée
        /// </summary>
        Effect_SubPushDamageReduction = 417,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Critiques
        /// </summary>
        Effect_AddCriticalDamageBonus = 418,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages Critiques
        /// </summary>
        Effect_SubCriticalDamageBonus = 419,
        /// <summary>
        /// #1{~1~2 à }#2 Résistance Critiques
        /// </summary>
        Effect_AddCriticalDamageReduction = 420,
        /// <summary>
        /// -#1{~1~2 à }#2 Résistance Critiques
        /// </summary>
        Effect_SubCriticalDamageReduction = 421,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Terre
        /// </summary>
        Effect_AddEarthDamageBonus = 422,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages Terre
        /// </summary>
        Effect_SubEarthDamageBonus = 423,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Feu
        /// </summary>
        Effect_AddFireDamageBonus = 424,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages Feu
        /// </summary>
        Effect_SubFireDamageBonus = 425,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Eau
        /// </summary>
        Effect_AddWaterDamageBonus = 426,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages Eau
        /// </summary>
        Effect_SubWaterDamageBonus = 427,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Air
        /// </summary>
        Effect_AddAirDamageBonus = 428,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages Air
        /// </summary>
        Effect_SubAirDamageBonus = 429,
        /// <summary>
        /// #1{~1~2 à }#2 Dommages Neutre
        /// </summary>
        Effect_AddNeutralDamageBonus = 430,
        /// <summary>
        /// -#1{~1~2 à }#2 Dommages Neutre
        /// </summary>
        Effect_SubNeutralDamageBonus = 431,
        /// <summary>
        /// Vole #1{~1~2 à }#2 PA
        /// </summary>
        Effect_StealAP_440 = 440,
        /// <summary>
        /// Vole #1{~1~2 à }#2 PM
        /// </summary>
        Effect_StealMP_441 = 441,
        /// <summary>
        /// Positionne la boussole
        /// </summary>
        Effect_509 = 509,
        /// <summary>
        /// Pose un prisme
        /// </summary>
        Effect_513 = 513,
        /// <summary>
        /// Afficher les percepteurs les plus riches
        /// </summary>
        Effect_516 = 516,
        /// <summary>
        /// 
        /// </summary>
        Effect_517 = 517,
        /// <summary>
        /// Téléporte au point de sauvegarde
        /// </summary>
        Effect_TeleportToSavePoint = 600,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_601 = 601,
        /// <summary>
        /// Enregistre sa position
        /// </summary>
        Effect_602 = 602,
        /// <summary>
        /// Apprend le métier #3
        /// </summary>
        Effect_603 = 603,
        /// <summary>
        /// Apprend le sort #3
        /// </summary>
        Effect_LearnSpell = 604,
        /// <summary>
        /// +#1{~1~2 à }#2 XP
        /// </summary>
        Effect_AddExp = 605,
        /// <summary>
        /// +#1{~1~2 à }#2 Sagesse
        /// </summary>
        Effect_AddPermanentWisdom = 606,
        /// <summary>
        /// +#1{~1~2 à }#2 Force
        /// </summary>
        Effect_AddPermanentStrength = 607,
        /// <summary>
        /// +#1{~1~2 à }#2 Chance
        /// </summary>
        Effect_AddPermanentChance = 608,
        /// <summary>
        /// +#1{~1~2 à }#2 Agilité
        /// </summary>
        Effect_AddPermanentAgility = 609,
        /// <summary>
        /// +#1{~1~2 à }#2 Vitalité
        /// </summary>
        Effect_AddPermanentVitality = 610,
        /// <summary>
        /// +#1{~1~2 à }#2 Intelligence
        /// </summary>
        Effect_AddPermanentIntelligence = 611,
        /// <summary>
        /// +#1{~1~2 à }#2 points de caractéristique
        /// </summary>
        Effect_612 = 612,
        /// <summary>
        /// +#1{~1~2 à }#2 point(s) de sort
        /// </summary>
        Effect_AddSpellPoints = 613,
        /// <summary>
        /// + #1 XP #2
        /// </summary>
        Effect_614 = 614,
        /// <summary>
        /// Fait oublier le métier de #3
        /// </summary>
        Effect_615 = 615,
        /// <summary>
        /// Fait oublier un niveau du sort #3
        /// </summary>
        Effect_616 = 616,
        /// <summary>
        /// Consulter #3
        /// </summary>
        Effect_ConsultDocument = 620,
        /// <summary>
        /// Invoque : #3 (grade #1)
        /// </summary>
        Effect_621 = 621,
        /// <summary>
        /// Téléporte chez soi
        /// </summary>
        Effect_622 = 622,
        /// <summary>
        /// #3 (#2)
        /// </summary>
        Effect_SoulStoneSummon = 623,
        /// <summary>
        /// Fait oublier un niveau du sort #3
        /// </summary>
        Effect_ForgetSpell = 624,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_625 = 625,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_626 = 626,
        /// <summary>
        /// Reproduit la carte d'origine
        /// </summary>
        Effect_627 = 627,
        /// <summary>
        /// #3 (#2)
        /// </summary>
        Effect_628 = 628,
        /// <summary>
        /// 
        /// </summary>
        Effect_630 = 630,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_631 = 631,
        /// <summary>
        /// 
        /// </summary>
        Effect_632 = 632,
        /// <summary>
        /// Ajoute #3 points d'honneur
        /// </summary>
        Effect_640 = 640,
        /// <summary>
        /// Ajoute #3 points de déshonneur
        /// </summary>
        Effect_641 = 641,
        /// <summary>
        /// Retire #3 points d'honneur
        /// </summary>
        Effect_642 = 642,
        /// <summary>
        /// Retire #3 points de déshonneur
        /// </summary>
        Effect_643 = 643,
        /// <summary>
        /// Ressuscite les alliés présents sur la carte
        /// </summary>
        Effect_645 = 645,
        /// <summary>
        /// #1{~1~2 à }#2 (PV rendus)
        /// </summary>
        Effect_646 = 646,
        /// <summary>
        /// Libère les âmes des ennemis
        /// </summary>
        Effect_647 = 647,
        /// <summary>
        /// Libère une âme ennemie
        /// </summary>
        Effect_648 = 648,
        /// <summary>
        /// Faire semblant d'être #3
        /// </summary>
        Effect_649 = 649,
        /// <summary>
        /// 
        /// </summary>
        Effect_652 = 652,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_654 = 654,
        /// <summary>
        /// Pas d'effet supplémentaire
        /// </summary>
        Effect_NoMoreEffects = 666,
        /// <summary>
        /// Combat annulé
        /// </summary>
        Effects_CancelFight = 667,
        /// <summary>
        /// Incarnation Niveau #5
        /// </summary>
        Effect_669 = 669,
        /// <summary>
        /// #1{~1~2 à }#2% de la vie de l'attaquant (dommages Neutre)
        /// </summary>
        Effect_670 = 670,
        /// <summary>
        /// #1{~1~2 à }#2% de la vie de l'attaquant (dommages Neutre fixes)
        /// </summary>
        Effect_DamagePercentNeutral_671 = 671,
        /// <summary>
        /// #1{~1~2 à }#2% de la vie de l'attaquant (dommages Neutre)
        /// </summary>
        Effect_Punishment_Damage = 672,
        /// <summary>
        /// Lier son métier : #1
        /// </summary>
        Effect_699 = 699,
        /// <summary>
        /// Change l'élément de frappe
        /// </summary>
        Effect_700 = 700,
        /// <summary>
        /// Puissance : #1{~1~2 à }#2
        /// </summary>
        Effect_701 = 701,
        /// <summary>
        /// +#1{~1~2 à }#2 Point(s) de durabilité
        /// </summary>
        Effect_702 = 702,
        /// <summary>
        /// #1% capture d'âme de puissance #3
        /// </summary>
        Effect_SoulStone = 705,
        /// <summary>
        /// #1% de proba de capturer une monture
        /// </summary>
        Effect_706 = 706,
        /// <summary>
        /// Utilise l'équipement rapide n°#3
        /// </summary>
        Effect_707 = 707,
        /// <summary>
        /// Coût supplémentaire
        /// </summary>
        Effect_710 = 710,
        /// <summary>
        /// #1 : #3
        /// </summary>
        Effect_MonsterSuperRaceKilledCount = 715,
        /// <summary>
        /// #1 : #3
        /// </summary>
        Effect_MonsterRaceKilledCount = 716,
        /// <summary>
        /// #1 : #3
        /// </summary>
        Effect_MonsterKilledCount = 717,
        /// <summary>
        /// Nombre de victimes : #2
        /// </summary>
        Effect_720 = 720,
        /// <summary>
        /// Titre : #3
        /// </summary>
        Effect_AddTitle = 724,
        /// <summary>
        /// Renommer la guilde : #4
        /// </summary>
        Effect_725 = 725,
        /// <summary>
        /// Ornement : #3
        /// </summary>
        Effect_AddOrnament = 726,
        /// <summary>
        /// Téléporte au prisme allié le plus proche
        /// </summary>
        Effect_730 = 730,
        /// <summary>
        /// Agresse les personnages d'alliances ennemies automatiquement
        /// </summary>
        Effect_731 = 731,
        /// <summary>
        /// Résistance à l'agression automatique par les joueurs ennemis : #1{~1~2 à }#2
        /// </summary>
        Effect_732 = 732,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_740 = 740,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_741 = 741,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_742 = 742,
        /// <summary>
        /// Bonus aux chances de capture : #1{~1~2 à }#2%
        /// </summary>
        Effect_750 = 750,
        /// <summary>
        /// #1{~1~2 à }#2% Bonus XP monture
        /// </summary>
        Effect_AddMountXpPercent = 751,
        /// <summary>
        /// #1{~1~2 à }#2 Fuite
        /// </summary>
        Effect_AddEvade = 752,
        /// <summary>
        /// #1{~1~2 à }#2 Tacle
        /// </summary>
        Effect_AddLock = 753,
        /// <summary>
        /// -#1{~1~2 à }#2 Fuite
        /// </summary>
        Effect_SubEvade = 754,
        /// <summary>
        /// -#1{~1~2 à }#2 Tacle
        /// </summary>
        Effect_SubLock = 755,
        /// <summary>
        /// Disparaît en se déplaçant
        /// </summary>
        Effect_760 = 760,
        /// <summary>
        /// Interception des dommages
        /// </summary>
        Effect_DamageIntercept = 765,
        /// <summary>
        /// Confusion horaire : #1{~1~2 à }#2 degrés
        /// </summary>
        Effect_TimeConfusionDegrees = 770,
        /// <summary>
        /// Confusion horaire : #1{~1~2 à }#2 Pi/2
        /// </summary>
        Effect_TimeConfusionPI2 = 771,
        /// <summary>
        /// Confusion horaire : #1{~1~2 à }#2 Pi/4
        /// </summary>
        Effect_TimeConfusionPI4 = 772,
        /// <summary>
        /// Confusion contre horaire : #1{~1~2 à }#2 degrés
        /// </summary>
        Effect_AgainstTimeConfusionDegrees = 773,
        /// <summary>
        /// Confusion contre horaire : #1{~1~2 à }#2 Pi/2
        /// </summary>
        Effect_AgainstTimeConfusionDegreesPI2 = 774,
        /// <summary>
        /// Confusion contre horaire : #1{~1~2 à }#2 Pi/4
        /// </summary>
        Effect_AgainstTimeConfusionDegreesPI4 = 775,
        /// <summary>
        /// #1{~1~2 à }#2% Érosion
        /// </summary>
        Effect_AddErosion = 776,
        /// <summary>
        /// Fixe le point de respawn
        /// </summary>
        Effect_778 = 778,
        /// <summary>
        /// Invoque le dernier allié mort avec #1{~1~2 à }#2 % de ses PDV
        /// </summary>
        Effect_ReviveAndGiveHPToLastDiedAlly = 780,
        /// <summary>
        /// Minimise les effets aléatoires
        /// </summary>
        Effect_RandDownModifier = 781,
        /// <summary>
        /// Maximise les effets aléatoires
        /// </summary>
        Effect_RandUpModifier = 782,
        /// <summary>
        /// Pousse jusqu'à la case visée
        /// </summary>
        Effect_RepelsTo = 783,
        /// <summary>
        /// Retour à la position de départ
        /// </summary>
        Effect_ReturnToOriginalPos = 784,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_785 = 785,
        /// <summary>
        /// Soigne l'attaquant de #1{~1~2 à }#2% des dommages subis.
        /// </summary>
        Effect_GiveHpPercentWhenAttack = 786,
        /// <summary>
        /// #1
        /// </summary>
        Effect_787 = 787,
        /// <summary>
        /// Châtiment de #2 sur #3 tour(s)
        /// </summary>
        Effect_Punishment = 788,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_789 = 789,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_790 = 790,
        /// <summary>
        /// Prépare #1{~1~2 à }#2 parchemins pour mercenaire [wait]
        /// </summary>
        Effect_791 = 791,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_792 = 792,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_793 = 793,
        /// <summary>
        /// Arme de chasse
        /// </summary>
        Effect_795 = 795,
        /// <summary>
        /// Restaurer le point de respawn
        /// </summary>
        Effect_796 = 796,
        /// <summary>
        /// Points de vie : #3
        /// </summary>
        Effect_LifePoints = 800,
        /// <summary>
        /// Reçu le : #1
        /// </summary>
        Effect_805 = 805,
        /// <summary>
        /// Corpulence : #1
        /// </summary>
        Effect_Corpulence = 806,
        /// <summary>
        /// Dernier repas : #1
        /// </summary>
        Effect_LastMeal = 807,
        /// <summary>
        /// A mangé le : #1
        /// </summary>
        Effect_LastMealDate = 808,
        /// <summary>
        /// Taille : #3 poces
        /// </summary>
        Effect_810 = 810,
        /// <summary>
        /// Combat(s) restant(s) : #3
        /// </summary>
        Effect_RemainingFights = 811,
        /// <summary>
        /// Utilisations : #2 / #3
        /// </summary>
        Effect_812 = 812,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_MealCount = 813,
        /// <summary>
        /// #1
        /// </summary>
        Effect_814 = 814,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_815 = 815,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_816 = 816,
        /// <summary>
        /// Téléporte
        /// </summary>
        Effect_825 = 825,
        /// <summary>
        /// Téléporte
        /// </summary>
        Effect_826 = 826,
        /// <summary>
        /// Oublier un sort
        /// </summary>
        Effect_831 = 831,
        /// <summary>
        /// Lance un combat contre #2
        /// </summary>
        Effect_905 = 905,
        /// <summary>
        /// 
        /// </summary>
        Effect_911 = 911,
        /// <summary>
        /// 
        /// </summary>
        Effect_916 = 916,
        /// <summary>
        /// 
        /// </summary>
        Effect_917 = 917,
        /// <summary>
        /// Augmente la sérénité, diminue l'agressivité
        /// </summary>
        Effect_930 = 930,
        /// <summary>
        /// Augmente l'agressivité, diminue la sérénité
        /// </summary>
        Effect_931 = 931,
        /// <summary>
        /// Augmente l'endurance
        /// </summary>
        Effect_932 = 932,
        /// <summary>
        /// Diminue l'endurance
        /// </summary>
        Effect_933 = 933,
        /// <summary>
        /// Augmente l'amour
        /// </summary>
        Effect_934 = 934,
        /// <summary>
        /// Diminue l'amour
        /// </summary>
        Effect_935 = 935,
        /// <summary>
        /// Accélère la maturité
        /// </summary>
        Effect_936 = 936,
        /// <summary>
        /// Ralentit la maturité
        /// </summary>
        Effect_937 = 937,
        /// <summary>
        /// Augmente les capacités d'un familier #3
        /// </summary>
        Effect_939 = 939,
        /// <summary>
        /// Capacités accrues
        /// </summary>
        Effect_IncreasePetStats = 940,
        /// <summary>
        /// Retirer temporairement un objet d'élevage
        /// </summary>
        Effect_946 = 946,
        /// <summary>
        /// Récupérer un objet d'enclos
        /// </summary>
        Effect_947 = 947,
        /// <summary>
        /// Objet pour enclos
        /// </summary>
        Effect_948 = 948,
        /// <summary>
        /// Monter/Descendre d'une monture
        /// </summary>
        Effect_949 = 949,
        /// <summary>
        /// État #3
        /// </summary>
        Effect_AddState = 950,
        /// <summary>
        /// Enlève l'état #3
        /// </summary>
        Effect_DispelState = 951,
        /// <summary>
        /// Désactive l'état '#3'
        /// </summary>
        Effect_DisableState = 952,
        /// <summary>
        /// Alignement : #3
        /// </summary>
        Effect_Alignment = 960,
        /// <summary>
        /// Grade : #3
        /// </summary>
        Effect_Grade = 961,
        /// <summary>
        /// Niveau : #3
        /// </summary>
        Effect_Level = 962,
        /// <summary>
        /// Créé depuis : #3 jour(s)
        /// </summary>
        Effect_963 = 963,
        /// <summary>
        /// Nom : #4
        /// </summary>
        Effect_964 = 964,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_LivingObjectId = 970,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_LivingObjectMood = 971,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_LivingObjectSkin = 972,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_LivingObjectCategory = 973,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_LivingObjectLevel = 974,
        /// <summary>
        /// Lié au personnage
        /// </summary>
        Effect_NonExchangeable_981 = 981,
        /// <summary>
        /// Lié au compte
        /// </summary>
        Effect_NonExchangeable_982 = 982,
        /// <summary>
        /// Échangeable : #1
        /// </summary>
        Effect_Exchangeable = 983,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_984 = 984,
        /// <summary>
        /// Modifié par : #4
        /// </summary>
        Effect_985 = 985,
        /// <summary>
        /// Prépare #1{~1~2 à }#2 parchemins
        /// </summary>
        Effect_986 = 986,
        /// <summary>
        /// Appartient à : #4
        /// </summary>
        Effect_BelongsTo = 987,
        /// <summary>
        /// Fabriqué par : #4
        /// </summary>
        Effect_988 = 988,
        /// <summary>
        /// Recherche : #4
        /// </summary>
        Effect_Seek = 989,
        /// <summary>
        /// #4
        /// </summary>
        Effect_990 = 990,
        /// <summary>
        /// !! Certificat invalide !!
        /// </summary>
        Effect_InvalidCertificate = 994,
        /// <summary>
        /// 
        /// </summary>
        Effect_ViewMountCharacteristics = 995,
        /// <summary>
        /// Appartient à : #4
        /// </summary>
        Effect_996 = 996,
        /// <summary>
        /// Nom : #4
        /// </summary>
        Effect_Name = 997,
        /// <summary>
        /// Validité : #1j #2h #3m
        /// </summary>
        Effect_Validity = 998,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_999 = 999,
        /// <summary>
        /// 2
        /// </summary>
        Effect_1002 = 1002,
        /// <summary>
        /// Diminue de #1{~1~2 à }#2 le bonus maximum
        /// </summary>
        Effect_1003 = 1003,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1004 = 1004,
        /// <summary>
        /// Diminue de #1{~1~2 à }#2 le bonus maximum
        /// </summary>
        Effect_1005 = 1005,
        /// <summary>
        /// Diminue de #1{~1~2 à }#2 le bonus minimum
        /// </summary>
        Effect_1006 = 1006,
        /// <summary>
        /// Efficacité : #1{~1~2 à }#2
        /// </summary>
        Effect_1007 = 1007,
        /// <summary>
        /// Invoque : #1
        /// </summary>
        Effect_SummonsBomb = 1008,
        /// <summary>
        /// Active une bombe
        /// </summary>
        Effect_TriggerBomb = 1009,
        /// <summary>
        /// Pose un glyphe de rang #2
        /// </summary>
        Effect_1010 = 1010,
        /// <summary>
        /// Invoque : #1
        /// </summary>
        Effect_SummonSlave = 1011,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Neutre)
        /// </summary>
        Effect_DamageNeutralRemainingMP = 1012,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Air)
        /// </summary>
        Effect_DamageAirRemainingMP = 1013,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Eau)
        /// </summary>
        Effect_DamageWaterRemainingMP = 1014,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Feu)
        /// </summary>
        Effect_DamageFireRemainingMP = 1015,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Terre)
        /// </summary>
        Effect_DamageEarthRemainingMP = 1016,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_1017 = 1017,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_1018 = 1018,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_1019 = 1019,

        Effect_AddShieldPercentLevel = 1020,

        /// <summary>
        /// Repousse de #1 case(s)
        /// </summary>
        Effect_PushBack_1021 = 1021,
        /// <summary>
        /// Attire de #1 case(s)
        /// </summary>
        Effect_PullForward_1022 = 1022,
        /// <summary>
        /// Échange de positions
        /// </summary>
        Effect_SwitchPosition_1023 = 1023,
        /// <summary>
        /// Crée des illusions
        /// </summary>
        Effect_CreateIllusions = 1024,
        /// <summary>
        /// Déclenche les pièges
        /// </summary>
        Effect_TriggerTraps = 1025,
        /// <summary>
        /// Déclenche les glyphes
        /// </summary>
        Effect_TriggerGlyphs = 1026,
        /// <summary>
        /// #1{~1~2 à }#2% Dommages Combo
        /// </summary>
        Effect_AddComboBonus = 1027,
        /// <summary>
        /// Déclenche les poudres
        /// </summary>
        Effect_1028 = 1028,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_Vanish = 1029,
        /// <summary>
        /// Pose de la poudre de rang #2
        /// </summary>
        Effect_1030 = 1030,
        /// <summary>
        /// Termine le tour
        /// </summary>
        Effect_SkipTurn_1031 = 1031,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1032 = 1032,
        /// <summary>
        /// -#1{~1~2 à -}#2% Vitalité
        /// </summary>
        Effect_SubVitalityPercent = 1033,
        /// <summary>
        /// Invoque le dernier allié mort avec #1{~1~2 à }#2 % de ses PDV
        /// </summary>
        Effect_ReviveAlly_1034 = 1034,
        /// <summary>
        /// #1 : +#3 tour(s) de relance
        /// </summary>
        Effect_1035 = 1035,
        /// <summary>
        /// #1 : -#3 tour(s) de relance
        /// </summary>
        Effect_ReduceSpellCooldown = 1036,
        /// <summary>
        /// [TEST] PDV rendus : #1{~1~2 à }#2
        /// </summary>
        Effect_1037 = 1037,
        /// <summary>
        /// Aura : #1
        /// </summary>
        Effect_1038 = 1038,
        /// <summary>
        /// #1{~1~2 à }#2% des PV en bouclier
        /// </summary>
        Effect_AddShieldPercent = 1039,
        /// <summary>
        /// #1{~1~2 à }#2 Bouclier
        /// </summary>
        Effect_AddShield = 1040,
        /// <summary>
        /// Recule de #1 case(s)
        /// </summary>
        Effect_Retreat = 1041,
        /// <summary>
        /// Avance de #1 case(s)
        /// </summary>
        Effect_Advance = 1042,
        /// <summary>
        /// Attire jusqu'à la case visée
        /// </summary>
        Effect_Attract = 1043,
        /// <summary>
        /// Immunité : #1
        /// </summary>
        Effect_SpellImmunity = 1044,
        /// <summary>
        /// #1 : #3 tour(s) de relance
        /// </summary>
        Effect_CooldownSet = 1045,
        /// <summary>
        /// #1 PM utilisés font perdre #2 PV
        /// </summary>
        Effect_1046 = 1046,
        /// <summary>
        /// -#1{~1~2 à }#2 PV
        /// </summary>
        Effect_SubVitality_1047 = 1047,
        /// <summary>
        /// -#1{~1~2 à }#2% PV
        /// </summary>
        Effect_SubVitalityPercent_1048 = 1048,
        /// <summary>
        /// +#1{~1~2 à }#2 niveau
        /// </summary>
        Effect_1049 = 1049,
        /// <summary>
        /// + #1 niveau dans le métier #2
        /// </summary>
        Effect_1050 = 1050,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1051 = 1051,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1052 = 1052,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1053 = 1053,
        /// <summary>
        /// #1{~1~2 à }#2 Puissance (sorts)
        /// </summary>
        Effect_IncreaseDamage_1054 = 1054,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1055 = 1055,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1057 = 1057,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1058 = 1058,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1059 = 1059,
        /// <summary>
        /// Augmente la taille.
        /// </summary>
        Effect_IncreaseSize = 1060,
        /// <summary>
        /// Partage des dommages
        /// </summary>
        Effect_DamageSharing = 1061,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1062 = 1062,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Terre fixes)
        /// </summary>
        Effect_DamageEarthFix = 1063,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Air fixes)
        /// </summary>
        Effect_DamageAirFix = 1064,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Eau fixes)
        /// </summary>
        Effect_DamageWaterFix = 1065,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages Feu fixes)
        /// </summary>
        Effect_DamageFireFix = 1066,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de la cible (dommages Air)
        /// </summary>
        Effect_DamageAirPerHpPercent = 1067,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de la cible (dommages Eau)
        /// </summary>
        Effect_DamageWaterPerHpPercent = 1068,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de la cible (dommages Feu)
        /// </summary>
        Effect_DamageFirePerHpPercent = 1069,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de la cible (dommages Terre)
        /// </summary>
        Effect_DamageEarthPerHpPercent = 1070,
        /// <summary>
        /// #1{~1~2 à }#2% des PV de la cible (dommages Neutre)
        /// </summary>
        Effect_DamageNeutralPerHpPercent = 1071,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1072 = 1072,
        /// <summary>
        /// Change l'élément de frappe
        /// </summary>
        Effect_1073 = 1073,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1074 = 1074,
        /// <summary>
        /// -#1{~1~2 à }#2 Durée des effets
        /// </summary>
        Effect_ReduceEffectsDuration = 1075,
        /// <summary>
        /// #1{~1~2 à }#2% Résistance
        /// </summary>
        Effect_AddResistances = 1076,
        /// <summary>
        /// -#1{~1~2 à -}#2% Résistance
        /// </summary>
        Effect_SubResistances = 1077,
        /// <summary>
        /// #1{~1~2 à }#2% Vitalité
        /// </summary>
        Effect_AddVitalityPercent = 1078,
        /// <summary>
        /// -#1{~1~2 à -}#2 PA
        /// </summary>
        Effect_SubAP_Roll = 1079,
        /// <summary>
        /// -#1{~1~2 à -}#2 PM
        /// </summary>
        Effect_SubMP_Roll = 1080,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1081 = 1081,
        /// <summary>
        /// Emballé par : #4
        /// </summary>
        Effect_1082 = 1082,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1083 = 1083,
        /// <summary>
        /// #1
        /// </summary>
        Effect_1084 = 1084,
        /// <summary>
        /// Quantité : #1
        /// </summary>
        Effect_1085 = 1085,
        /// <summary>
        /// Pour : #4
        /// </summary>
        Effect_1086 = 1086,
        /// <summary>
        /// Écrire un nom
        /// </summary>
        Effect_1087 = 1087,
        /// <summary>
        /// Pose un glyphe-aura de rang #2
        /// </summary>
        Effect_GlyphAura = 1091,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés de la cible (dommages Neutre)
        /// </summary>
        Effect_DamageNeutralPerHPEroded = 1092,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés de la cible (dommages Air)
        /// </summary>
        Effect_DamageAirPerHPEroded = 1093,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés de la cible (dommages Feu)
        /// </summary>
        Effect_DamageFirePerHPEroded = 1094,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés de la cible (dommages Eau)
        /// </summary>
        Effect_DamageWaterPerHPEroded = 1095,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés de la cible (dommages Terre)
        /// </summary>
        Effect_DamageEarthPerHPEroded = 1096,
        /// <summary>
        /// Crée des illusions
        /// </summary>
        Effect_Illusions = 1097,
        /// <summary>
        /// (not found)
        /// </summary>
        Effect_1098 = 1098,
        /// <summary>
        /// Téléporte à la position de début de tour
        /// </summary>
        Effect_Rewind = 1099,
        /// <summary>
        /// Téléporte à la position précédente
        /// </summary>
        Effect_ReturnToLastPos = 1100,
        /// <summary>
        /// 
        /// </summary>
        Effect_TeleportToRune = 1101,
        /// <summary>
        /// 
        /// </summary>
        Effect_1102 = 1102,
        /// <summary>
        /// Pousse de #1 case(s)
        /// </summary>
        Effect_PushBack_1103 = 1103,
        /// <summary>
        /// Téléportation symétrique par rapport à la cible
        /// </summary>
        Effect_SymetricTargetTeleport = 1104,
        /// <summary>
        /// Téléportation symétrique par rapport au lanceur
        /// </summary>
        Effect_SymetricCasterTeleport = 1105,
        /// <summary>
        /// Téléportation symétrique
        /// </summary>
        Effect_SymetricPointTeleport = 1106,
        /// <summary>
        /// Renommer la guilde
        /// </summary>
        Effect_ChangeGuildName = 1107,
        /// <summary>
        /// Changer le blason de la guilde
        /// </summary>
        Effect_ChangeGuildBlazon = 1108,
        /// <summary>
        /// #1{~1~2 à }#2% (PV rendus)
        /// </summary>
        Effect_RestoreHPPercent = 1109,
        /// <summary>
        /// #3 butins
        /// </summary>
        Effect_1111 = 1111,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés du lanceur (dommages Neutre)
        /// </summary>
        Effect_DamageNeutralPerCasterHPEroded = 1118,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés du lanceur (dommages Air)
        /// </summary>
        Effect_DamageAirPerCasterHPEroded = 1119,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés du lanceur (dommages Feu)
        /// </summary>
        Effect_DamageFirePerCasterHPEroded = 1120,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés du lanceur (dommages Eau)
        /// </summary>
        Effect_DamageWaterPerCasterHPEroded = 1121,
        /// <summary>
        /// #1{~1~2 à }#2% des PV érodés du lanceur (dommages Terre)
        /// </summary>
        Effect_DamageEarthPerCasterHPEroded = 1122,
        /// <summary>
        /// Répartit #1{~1~2 à }#2% des dommages subis.
        /// </summary>
        Effect_DispatchDamages = 1123,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_1124 = 1124,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown1125 = 1125,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_1126 = 1126,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_1127 = 1127,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_1128 = 1128,
        /// <summary>
        /// Envoyer vers Krosmaster
        /// </summary>
        Effect_1129 = 1129,
        /// <summary>
        /// #1 PA utilisés font perdre #2 PV (Air)
        /// </summary>
        Effect_DamageAirPerAP = 1131,
        /// <summary>
        /// #1 PA utilisés font perdre #2 PV (Eau)
        /// </summary>
        Effect_DamageWaterPerAP = 1132,
        /// <summary>
        /// #1 PA utilisé fait perdre #2 PV (Feu)
        /// </summary>
        Effect_DamageFirePerAP = 1133,
        /// <summary>
        /// #1 PA utilisés font perdre #2 PV (Neutre)
        /// </summary>
        Effect_DamageNeutralPerAP = 1134,
        /// <summary>
        /// #1 PA utilisés font perdre #2 PV (Terre)
        /// </summary>
        Effect_DamageEarthPerAP = 1135,
        /// <summary>
        /// #1 PM utilisés font perdre #2 PV (Air)
        /// </summary>
        Effect_DamageAirPerMP = 1136,
        /// <summary>
        /// #1 PM utilisés font perdre #2 PV (Eau)
        /// </summary>
        Effect_DamageWaterPerMP = 1137,
        /// <summary>
        /// #1 PM utilisés font perdre #2 PV (Feu)
        /// </summary>
        Effect_DamageFirePerMP = 1138,
        /// <summary>
        /// #1 PM utilisés font perdre #2 PV (Neutre)
        /// </summary>
        Effect_DamageNeutralPerMP = 1139,
        /// <summary>
        /// #1 PM utilisés font perdre #2 PV (Terre)
        /// </summary>
        Effect_DamageEarthPerMP = 1140,
        /// <summary>
        /// 
        /// </summary>
        Effect_1141 = 1141,
        /// <summary>
        /// 
        /// </summary>
        Effect_1142 = 1142,
        /// <summary>
        /// #1{~1~2 à }#2 Puissance (armes)
        /// </summary>
        Effect_AddWeaponDamageBonus = 1144,
        /// <summary>
        /// Changer le blason de l'alliance
        /// </summary>
        Effect_1145 = 1145,
        /// <summary>
        /// Renommer l'alliance
        /// </summary>
        Effect_1146 = 1146,
        /// <summary>
        /// 
        /// </summary>
        Effect_1149 = 1149,
        /// <summary>
        /// 
        /// </summary>
        Effect_1150 = 1150,
        /// <summary>
        /// Apparence : #1
        /// </summary>
        Effect_Appearance = 1151,
        /// <summary>
        /// 
        /// </summary>
        Effect_1152 = 1152,
        /// <summary>
        /// Invoque un percepteur
        /// </summary>
        Effect_SummonTaxcollector = 1153,
        /// <summary>
        /// 
        /// </summary>
        Effect_1154 = 1154,
        /// <summary>
        /// Téléporte
        /// </summary>
        Effect_1155 = 1155,
        /// <summary>
        /// Soins reçus x#1%
        /// </summary>
        Effect_HealBuff = 1159,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_1160 = 1160,
        /// <summary>
        /// #1
        /// </summary>
        Effect_1161 = 1161,
        /// <summary>
        /// Positionne la boussole
        /// </summary>
        Effect_1162 = 1162,
        /// <summary>
        /// Dommages subis x#1%
        /// </summary>
        Effect_DamageMultiplier = 1163,
        /// <summary>
        /// Les dommages reçus soignent.
        /// </summary>
        Effect_HealWhenAttack = 1164,
        /// <summary>
        /// Pose un glyphe de rang #2
        /// </summary>
        Effect_Glyph_1165 = 1165,
        /// <summary>
        /// #1{~1~2 à }#2 Puissance (glyphes)
        /// </summary>
        Effect_IncreaseGlyphDamages = 1166,
        /// <summary>
        /// #1{~1~2 à }#2 Puissance (runes)
        /// </summary>
        Effect_1167 = 1167,
        /// <summary>
        /// 
        /// </summary>
        Effect_1168 = 1168,
        /// <summary>
        /// 
        /// </summary>
        Effect_1169 = 1169,
        /// <summary>
        /// 
        /// </summary>
        Effect_1170 = 1170,
        /// <summary>
        /// Augmente les dommages finaux occasionnés de #1%
        /// </summary>
        Effect_IncreaseFinalDamages = 1171,
        /// <summary>
        /// Réduit les dommages finaux occasionnés de #1%
        /// </summary>
        Effect_ReduceFinalDamages = 1172,
        /// <summary>
        /// 
        /// </summary>
        Effect_1173 = 1173,
        /// <summary>
        /// Supprime les gains d'expérience
        /// </summary>
        Effect_1174 = 1174,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_1175 = 1175,
        /// <summary>
        /// Apparence : #1
        /// </summary>
        Effect_Apparence_Wrapper = 1176,
        /// <summary>
        /// 
        /// </summary>
        Effect_1177 = 1177,
        /// <summary>
        /// 
        /// </summary>
        Effect_1178 = 1178,
        /// <summary>
        /// Compatible avec : #1
        /// </summary>
        Effect_Compatible = 1179,
        /// <summary>
        /// Lire #3
        /// </summary>
        Effect_1180 = 1180,
        /// <summary>
        /// Pose un portail (+#3% dommages)
        /// </summary>
        Effect_SpawnPortal = 1181,
        /// <summary>
        /// Téléportation portail
        /// </summary>
        Effect_TeleportPortal = 1182,
        /// <summary>
        /// Désactiver un portail
        /// </summary>
        Effect_DisablePortal = 1183,
        /// <summary>
        /// Expérience du niveau : #3
        /// </summary>
        Effect_1184 = 1184,
        /// <summary>
        /// Réinitialise les effets d'un objet de niveau inférieur ou égal à #1{~1~2 à }#2
        /// </summary>
        Effect_1185 = 1185,
        /// <summary>
        /// Niveau : #1
        /// </summary>
        Effect_1186 = 1186,
        /// <summary>
        /// #1
        /// </summary>
        Effect_HarnessGID = 1187,
        /// <summary>
        /// Crée un double du lanceur de sort
        /// </summary>
        Effect_Double_1189 = 1189,
        /// <summary>
        /// Répartit #1{~1~2 à }#2% des dommages subis.
        /// </summary>
        Effect_DispatchDamage_1223 = 1223,
        /// <summary>
        /// Répartit #1{~1~2 à }#2% des dommages subis.
        /// </summary>
        Effect_Unknown_1225 = 1225,
        /// <summary>   
        /// #1
        /// </summary>
        /// 
        Effect_CastSpell2017 = 2017,
        /// <summary>
        /// Something related to spells ?
        /// </summary>
        Effect_Unknown_2018 = 2018,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_2019 = 2019,
        /// <summary>
        /// Soigne #1{~1~2 à }#2% des dommages subis.
        /// </summary>
        Effect_HealReceivedDamages = 2020,
        /// <summary>
        /// 
        /// </summary>
        Effect_2021 = 2021,
        /// <summary>
        /// Pose une rune de rang #2
        /// </summary>
        Effect_Rune = 2022,
        /// <summary>
        /// Déclenche les runes
        /// </summary>
        Effect_TriggerRune = 2023,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_2024 = 2024,
        /// <summary>
        /// 
        /// </summary>
        Effect_2025 = 2025,
        /// <summary>
        /// 
        /// </summary>
        Effect_2026 = 2026,
        /// <summary>
        /// Prend le contrôle de l'entité ciblée
        /// </summary>
        Effect_TakeControl = 2027,
        /// <summary>
        /// Transmet une partie de ses caractéristiques
        /// </summary>
        Effect_TransmitCharacteristic = 2028,
        /// <summary>
        /// Lance un coup fatal
        /// </summary>
        Effect_2029 = 2029,
        /// <summary>
        /// 
        /// </summary>
        Effect_LearnFinishMove = 2030,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_2160 = 2160,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_2792 = 2792,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_2793 = 2793,
        /// <summary>
        /// #1
        /// </summary>
        Effect_CastSpell_2794 = 2794,
        /// <summary>
        /// #1 (Couronnes d'épines)
        /// </summary>
        Effect_CastSpell_2795 = 2795,
        /// <summary>
        /// Tue la cible pour la remplacer par l'invocation : #1
        /// </summary>
        Effect_KillAndSummonSlave = 2796,
        /// <summary>
        /// 
        /// </summary>
        Effect_LearnSmileyPack = 2797,
        /// <summary>
        /// 
        /// </summary>
        Effect_2798 = 2798,

        /// <summary>
        /// Aumenta o dano corpo a corpo : #1{~1~2 a }#2% de danos no corpo a corpo 
        /// </summary>
        Effect_MeleeDamageDonePercent = 2800,

        /// <summary>
        /// Diminui o dano corpo a corpo : #1{~1~2 a -}#2% de danos no corpo a corpo
        /// </summary>
        Effect_SubMeleeDamageDonePercent = 2801,

        /// <summary>
        /// Diminui a resistência corpo a corpo : -#1{~1~2 a -}#2% de resistência ao corpo a corpo
        /// </summary>
        Effect_SubMeleeResistance = 2802,

        /// <summary>
        /// Aumenta a resistência corpo a corpo : #1{~1~2 a }#2% de resistência ao corpo a corpo
        /// </summary>
        Effect_AddMeleeResistance = 2803,

        /// <summary>
        /// Aumenta os dano danos à distância : #1{~1~2 a }#2% de danos à distância
        /// </summary>
        Effect_RangedDamageDonePercent = 2804,

        /// <summary>
        /// Dimiui os dano danos à distância : -#1{~1~2 a -}#2% de danos à distância
        /// </summary>
        Effect_SubRangedDamageDonePercent = 2805,

        /// <summary>
        /// Dimiui a resistência à distância : -#1{~1~2 a -}#2% de resistência à distância
        /// </summary>
        Effect_SubRangedResistance = 2806,

        /// <summary>
        /// Aumenta a resistência à distância : #1{~1~2 a }#2% de resistência à distância
        /// </summary>
        Effect_AddRangedResistance = 2807,

        /// <summary>
        /// Aumenta os danos de armas : #1{~1~2 a }#2% de danos de armas
        /// </summary>
        Effect_WeaponDamageDonePercent = 2808,

        /// <summary>
        /// Diminui os danos de armas : -#1{~1~2 a -}#2% de danos de armas
        /// </summary>
        Effect_SubWeaponDamageDonePercent = 2809,

        /// <summary>
        /// Diminui a resistência às armas : -#1{~1~2 a -}#2% de resistência às armas
        /// </summary>
        Effect_SubWeaponResistance = 2810,

        /// <summary>
        /// Aumenta a resistência às armas : #1{~1~2 a }#2% de resistência às armas
        /// </summary>
        Effect_WeaponResistance = 2811,

        /// <summary>
        /// Aumenta o dano de spells : #1{~1~2 a }#2% de danos de feitiços
        /// </summary>
        Effect_SpellDamageDonePercent = 2812,

        /// <summary>
        /// Diminui o dano de spells : -#1{~1~2 a -}#2% de danos de feitiços
        /// </summary>
        Effect_SubSpellDamageDonePercent = 2813,

        /// <summary>
        /// Diminui a resistência aos feitiços : -#1{~1~2 a -}#2% de resistência aos feitiços
        /// </summary>
        Effect_SubSpellResistance = 2814,

        /// <summary>
        /// Aumenta a resistência aos feitiços : #1{~1~2 a }#2% de resistência aos feitiços
        /// </summary>
        Effect_AddSpellResistance = 2815,
        /// <summary>
        /// [UNKNOWN_TEXT_ID_735940]
        /// </summary>
        Effect_Unknown_2816 = 2816,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_2817 = 2817,
        /// <summary>
        /// Craft coopératif impossible.
        /// </summary>
        Effect_CraftError = 2818,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages)
        /// </summary>
        Effect_DamageBestElement = 2822,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages)
        /// </summary>
        Effect_DamageBestElement_2828 = 2828,
        /// <summary>
        /// #1{~1~2 à }#2 (dommages)
        /// </summary>
        Effect_DamageBestElement_2829 = 2829,
        /// <summary>
        /// #1{~1~2 à }#2% Force
        /// </summary>
        Effect_AddStrengthPercent = 2834,
        /// <summary>
        /// -#1{~1~2 à -}#2% Force
        /// </summary>
        Effect_SubStrengthPercent = 2835,
        /// <summary>
        /// #1{~1~2 à }#2% Agilité
        /// </summary>
        Effect_AddAgilityPercent = 2836,
        /// <summary>
        /// -#1{~1~2 à -}#2% Agilité
        /// </summary>
        Effect_SubAgilityPercent = 2837,
        /// <summary>
        /// #1{~1~2 à }#2% Intelligence
        /// </summary>
        Effect_AddIntelligencePercent = 2838,
        /// <summary>
        /// -#1{~1~2 à -}#2% Intelligence
        /// </summary>
        Effect_SubIntelligencePercent = 2839,
        /// <summary>
        /// #1{~1~2 à }#2% Chance
        /// </summary>
        Effect_AddChancePercent = 2840,
        /// <summary>
        /// -#1{~1~2 à -}#2% Chance
        /// </summary>
        Effect_SubChancePercent = 2841,
        /// <summary>
        /// #1{~1~2 à }#2% Sagesse
        /// </summary>
        Effect_AddWisdomPercent = 2842,
        /// <summary>
        /// -#1{~1~2 à -}#2% Sagesse
        /// </summary>
        Effect_SubWisdomPercent = 2843,
        /// <summary>
        /// #1{~1~2 à }#2% Vitalité
        /// </summary>
        Effect_AddVitalityPercent_2844 = 2844,
        /// <summary>
        /// -#1{~1~2 à -}#2% Vitalité
        /// </summary>
        Effect_SubVitalityPercent_2845 = 2845,
        /// <summary>
        /// #1{~1~2 à }#2% PA
        /// </summary>
        Effect_AddAPPercent = 2846,
        /// <summary>
        /// -#1{~1~2 à -}#2% PA
        /// </summary>
        Effect_SubAPPercent = 2847,
        /// <summary>
        /// #1{~1~2 à }#2% PM
        /// </summary>
        Effect_AddMPPercent = 2848,
        /// <summary>
        /// -#1{~1~2 à -}#2% PM
        /// </summary>
        Effect_SubMPPercent = 2849,
        /// <summary>
        /// #1{~1~2 à }#2% Tacle
        /// </summary>
        Effect_AddTacklePercent = 2850,
        /// <summary>
        /// #1{~1~2 à }#2% Fuite
        /// </summary>
        Effect_AddEvadePercent = 2852,
        /// <summary>
        /// #1{~1~2 à }#2% Esquive PA
        /// </summary>
        Effect_AddDodgeAPPercent = 2854,
        /// <summary>
        /// #1{~1~2 à }#2% Esquive PM
        /// </summary>
        Effect_AddDodgeMPPercent = 2856,
        /// <summary>
        /// -#1{~1~2 à -}#2% Esquive PM
        /// </summary>
        Effect_SubDodgeMPPercent = 2857,
        /// <summary>
        /// #1{~1~2 à }#2% Retrait PA
        /// </summary>
        Effect_AddAPAttackPercent = 2858,
        /// <summary>
        /// #1{~1~2 à }#2% Retrait PM
        /// </summary>
        Effect_AddMPAttackPercent = 2860,
        /// <summary>
        /// Augmente la taille de #1{~1~2 à }#2%
        /// </summary>
        Effect_IncreaseSize_2868 = 2868,
        /// <summary>
        /// Diminue la taille de #1{~1~2 à }#2%
        /// </summary>
        Effect_ReduceSize = 2871,
        /// <summary>
        /// Seuil : #1 PV
        /// </summary>
        Effect_HealthThreshold = 2872,
        /// <summary>
        /// Apprend le sort #3
        /// </summary>
        Effect_LearnSpell_2875 = 2875,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_2876 = 2876,
        /// <summary>
        /// 
        /// </summary>
        Effect_Unknown_2877 = 2877,
        
        End = 2878
    }
}