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

        OnCasterRemoveAp, // CAPA

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
        //  OnDamagedByGlyph, // DG ------------> plutot tout les dommages indirects?
        OnDamagedByTrap, //DT
        OnDamagedByEnemyTrap, // DTE
        OnDamagedMelee, //DM
        OnDamagedRange, //DR
        OnDamagedByPush, // PD
        OnDamagedByAllyPush, //PMD
        OnDamagedByEnemyPush, // PPD    

        //OnDamageEnemyByPush, // MMD

        OnSummon, // CI

        OnSwitchPosition, // MS
        OnTurnBegin, // TB
        OnTurnEnd, // TE

        AfterTurnBegin, // ATB

        OnMpRemovalAttempt,
        OnMPLost, //  MPA (effective loss? attempt? )
        OnAPLost, // APA
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

        OnPushed, //MP , P
        OnMoved, //M
        OnTackled, //tF
        OnTackle, //tS

        CasterCriticalHitOnAlly, // DCCBA


        OnTeleportPortal,

        // DTB dommage poisons (? dommages reçus indirectements)


        /*
         * Custom
         */
        Delayed,
        Unknown,



        /*
        *A=lose AP (101)
        *CC=on critical hit
        *d=dispell
        *D=damage
        *DA=damage air
        *DBA=damage on ally
        *DBE=damage on enemy
        *DC=damaged by weapon
        *DE=damage earth
        *DF=damage fire
        *DG=damage from glyph
        *DI=
        *DM=distance between 0 and 1
        *DN=damage neutral
        *DP=damage from trap
        *Dr=
        *DR=distance > 1
        *DS=not weapon
        *DTB=
        *DTE=
        *DW=damage water
        *EO=on add state
        *EO#=on add state #
        *Eo=on state removed
        *Eo#=on state # removed
        *H=on heal
        *I=instant
        *m=lose mp (127)
        *M=OnMoved
        *mA=
        *MD=push damage
        *MDM=receive push damages from enemy push
        *MDP=inflict push damage to enemy
        *ML=
        *MP=Pushed
        *MS=
        *P=
        *R=Lost Range
        *TB=turn begin
        *TE=turn end
        *tF=Tackled
        *tS=Tackle
        *X= Death
        *CT =tackle enemy?
        *CI = Summoned
        */
    }
}
