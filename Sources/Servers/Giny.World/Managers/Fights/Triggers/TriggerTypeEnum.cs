using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Triggers
{
    /// <summary>
    /// CDA -> dernier élement d'attaque dans le jet du sort
    /// </summary>
    public enum TriggerTypeEnum
    {
        Instant, // I

        CasterInflictDamageMelee, // CDM
        CasterInflictDamageRange, // CDR
        CasterInflictDamageEnnemy, // CDBE On inflige des dégats a un enemi
        CasterInflictDamageAlly, // CDBA On inflige des dégats a un allié

        CasterInflictDamageEarth, // CDE
        CasterInflictDamageWater,
        CasterInflictDamageAir,
        CasterInflictDamageFire,
        CasterInflictDamageNeutral,

        OnCasterRemoveMpAttempt,
        OnCasterRemoveApAttempt, // CAPA
        OnCasterUseAp,  // CAP

        LifePointsAffected, // VA
        MaxLifePointsAffected, // VM
        ErodedLifePointsAffected, // VE
        LifeAffected, // V

        CasterHealed, // CH

        OnDamaged, // D
        OnDamagedAir, // DA
        OnDamagedEarth, // DE
        OnDamagedFire, // DF
        OnDamagedWater, // DW
        OnDamagedNeutral, // DN
        OnDamagedByAlly, // DBA   
        OnDamagedByEnemy, // BDE
        OnDamagedBySummon, // DI
        OnDamagedBySpell, // DS
        OnDamagedByWeapon, // DCAC
        OnDamagedByGlyph, // DG 
        OnDamagedByTrap, //DT
        OnDamagedMelee, //DM
        OnDamagedRange, //DR
        OnDamagedByPush, // PD
        OnDamagedByAllyPush, //PMD
        OnDamagedByEnemyPush, // PPD    
        OnDamagedTurnBegin, // DTB
        OnDamagedTurnEnd, // DTE
        OnDamagedOnLife, // DV

        OnSummon, // CI Caster Invoke 

        OnSwitchPosition, // MS
        OnTurnBegin, // TB
        OnTurnEnd, // TE (bad timing when buffs added)

        AfterTurnBegin, // ATB

        OnApRemovalAttempt, // ?????? called but dunno raw trigger
        OnMpRemovalAttempt,
        OnRangeLost, // R

        OnAMpUsed, // CCMPARR (foreach mp used, call it)
        OnMpUsed, // CMPARR (called after all movement)

        OnHealed, //H

        OnPulled, // MA

        OnStateAdded, // EO
        OnStateRemoved, //Eo

        OnDispelled, // DIS
        OnCasterDispelled, // CDIS

        OnCriticalHit, //CC
        OnDeath, //X
        OnKill, // K


        OnPushed, // P
        OnMoved, // M
        OnTackled, // tF
        OnTackle, // tS

        CasterCriticalHitOnAlly, // DCCBA

        CasterAddShield, // CS
        OnShieldApplied, // S

     


        // CMPAS :  si le lanceur réussit sa prochaine tentative de retrait de PM (caster)
        // CMPDEP :  la cible utilise un PM pour se déplacer. (caster)
        // LPU : Quand le lanceur remonte à 100% de sa vie via du soin ou du vol de vie (sort nuée péstidentielle, item : Pestilence de Corruption) 
        // KWW : Lorsque le porteur achève une entité avec une arme

        OnTeleportPortal, // TP

        OnReveals,
        OnInvisible,

        /*
         * Custom
         */
        Delayed,
        Unknown,
    }
}
