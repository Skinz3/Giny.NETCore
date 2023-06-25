using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Item", "")]
    public class Item : IDataObject , IIndexedData
    {        public const string MODULE = "Items";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint typeId;
        public uint descriptionId;
        public uint iconId;
        public uint level;
        public uint realWeight;
        public bool cursed;
        public int useAnimationId;
        public bool usable;
        public bool targetable;
        public bool exchangeable;
        public double price;
        public bool twoHanded;
        public bool etheral;
        public int itemSetId;
        public string criteria;
        public string criteriaTarget;
        public bool hideEffects;
        public bool enhanceable;
        public bool nonUsableOnAnother;
        public uint appearanceId;
        public bool secretRecipe;
        public List<uint> dropMonsterIds;
        public List<uint> dropTemporisMonsterIds;
        public uint recipeSlots;
        public List<uint> recipeIds;
        public bool objectIsDisplayOnWeb;
        public bool bonusIsSecret;
        public List<EffectInstance> possibleEffects;
        public List<uint> evolutiveEffectIds;
        public List<uint> favoriteSubAreas;
        public uint favoriteSubAreasBonus;
        public int craftXpRatio;
        public string craftVisible;
        public string craftConditional;
        public string craftFeasible;
        public bool needUseConfirm;
        public bool isDestructible;
        public bool isLegendary;
        public bool isSaleable;
        public List<List<double>> nuggetsBySubarea;
        public List<uint> containerIds;
        public List<List<int>> resourcesBySubarea;
        public string visibility;
        public uint importantNoticeId;
        public string changeVersion;
        public double tooltipExpirationDate;

        [D2OIgnore]
        public int Id_
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        [D2OIgnore]
        public uint NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        [D2OIgnore]
        public uint TypeId
        {
            get
            {
                return typeId;
            }
            set
            {
                typeId = value;
            }
        }
        [D2OIgnore]
        public uint DescriptionId
        {
            get
            {
                return descriptionId;
            }
            set
            {
                descriptionId = value;
            }
        }
        [D2OIgnore]
        public uint IconId
        {
            get
            {
                return iconId;
            }
            set
            {
                iconId = value;
            }
        }
        [D2OIgnore]
        public uint Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        [D2OIgnore]
        public uint RealWeight
        {
            get
            {
                return realWeight;
            }
            set
            {
                realWeight = value;
            }
        }
        [D2OIgnore]
        public bool Cursed
        {
            get
            {
                return cursed;
            }
            set
            {
                cursed = value;
            }
        }
        [D2OIgnore]
        public int UseAnimationId
        {
            get
            {
                return useAnimationId;
            }
            set
            {
                useAnimationId = value;
            }
        }
        [D2OIgnore]
        public bool Usable
        {
            get
            {
                return usable;
            }
            set
            {
                usable = value;
            }
        }
        [D2OIgnore]
        public bool Targetable
        {
            get
            {
                return targetable;
            }
            set
            {
                targetable = value;
            }
        }
        [D2OIgnore]
        public bool Exchangeable
        {
            get
            {
                return exchangeable;
            }
            set
            {
                exchangeable = value;
            }
        }
        [D2OIgnore]
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        [D2OIgnore]
        public bool TwoHanded
        {
            get
            {
                return twoHanded;
            }
            set
            {
                twoHanded = value;
            }
        }
        [D2OIgnore]
        public bool Etheral
        {
            get
            {
                return etheral;
            }
            set
            {
                etheral = value;
            }
        }
        [D2OIgnore]
        public int ItemSetId
        {
            get
            {
                return itemSetId;
            }
            set
            {
                itemSetId = value;
            }
        }
        [D2OIgnore]
        public string Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                criteria = value;
            }
        }
        [D2OIgnore]
        public string CriteriaTarget
        {
            get
            {
                return criteriaTarget;
            }
            set
            {
                criteriaTarget = value;
            }
        }
        [D2OIgnore]
        public bool HideEffects
        {
            get
            {
                return hideEffects;
            }
            set
            {
                hideEffects = value;
            }
        }
        [D2OIgnore]
        public bool Enhanceable
        {
            get
            {
                return enhanceable;
            }
            set
            {
                enhanceable = value;
            }
        }
        [D2OIgnore]
        public bool NonUsableOnAnother
        {
            get
            {
                return nonUsableOnAnother;
            }
            set
            {
                nonUsableOnAnother = value;
            }
        }
        [D2OIgnore]
        public uint AppearanceId
        {
            get
            {
                return appearanceId;
            }
            set
            {
                appearanceId = value;
            }
        }
        [D2OIgnore]
        public bool SecretRecipe
        {
            get
            {
                return secretRecipe;
            }
            set
            {
                secretRecipe = value;
            }
        }
        [D2OIgnore]
        public List<uint> DropMonsterIds
        {
            get
            {
                return dropMonsterIds;
            }
            set
            {
                dropMonsterIds = value;
            }
        }
        [D2OIgnore]
        public List<uint> DropTemporisMonsterIds
        {
            get
            {
                return dropTemporisMonsterIds;
            }
            set
            {
                dropTemporisMonsterIds = value;
            }
        }
        [D2OIgnore]
        public uint RecipeSlots
        {
            get
            {
                return recipeSlots;
            }
            set
            {
                recipeSlots = value;
            }
        }
        [D2OIgnore]
        public List<uint> RecipeIds
        {
            get
            {
                return recipeIds;
            }
            set
            {
                recipeIds = value;
            }
        }
        [D2OIgnore]
        public bool ObjectIsDisplayOnWeb
        {
            get
            {
                return objectIsDisplayOnWeb;
            }
            set
            {
                objectIsDisplayOnWeb = value;
            }
        }
        [D2OIgnore]
        public bool BonusIsSecret
        {
            get
            {
                return bonusIsSecret;
            }
            set
            {
                bonusIsSecret = value;
            }
        }
        [D2OIgnore]
        public List<EffectInstance> PossibleEffects
        {
            get
            {
                return possibleEffects;
            }
            set
            {
                possibleEffects = value;
            }
        }
        [D2OIgnore]
        public List<uint> EvolutiveEffectIds
        {
            get
            {
                return evolutiveEffectIds;
            }
            set
            {
                evolutiveEffectIds = value;
            }
        }
        [D2OIgnore]
        public List<uint> FavoriteSubAreas
        {
            get
            {
                return favoriteSubAreas;
            }
            set
            {
                favoriteSubAreas = value;
            }
        }
        [D2OIgnore]
        public uint FavoriteSubAreasBonus
        {
            get
            {
                return favoriteSubAreasBonus;
            }
            set
            {
                favoriteSubAreasBonus = value;
            }
        }
        [D2OIgnore]
        public int CraftXpRatio
        {
            get
            {
                return craftXpRatio;
            }
            set
            {
                craftXpRatio = value;
            }
        }
        [D2OIgnore]
        public string CraftVisible
        {
            get
            {
                return craftVisible;
            }
            set
            {
                craftVisible = value;
            }
        }
        [D2OIgnore]
        public string CraftConditional
        {
            get
            {
                return craftConditional;
            }
            set
            {
                craftConditional = value;
            }
        }
        [D2OIgnore]
        public string CraftFeasible
        {
            get
            {
                return craftFeasible;
            }
            set
            {
                craftFeasible = value;
            }
        }
        [D2OIgnore]
        public bool NeedUseConfirm
        {
            get
            {
                return needUseConfirm;
            }
            set
            {
                needUseConfirm = value;
            }
        }
        [D2OIgnore]
        public bool IsDestructible
        {
            get
            {
                return isDestructible;
            }
            set
            {
                isDestructible = value;
            }
        }
        [D2OIgnore]
        public bool IsLegendary
        {
            get
            {
                return isLegendary;
            }
            set
            {
                isLegendary = value;
            }
        }
        [D2OIgnore]
        public bool IsSaleable
        {
            get
            {
                return isSaleable;
            }
            set
            {
                isSaleable = value;
            }
        }
        [D2OIgnore]
        public List<List<double>> NuggetsBySubarea
        {
            get
            {
                return nuggetsBySubarea;
            }
            set
            {
                nuggetsBySubarea = value;
            }
        }
        [D2OIgnore]
        public List<uint> ContainerIds
        {
            get
            {
                return containerIds;
            }
            set
            {
                containerIds = value;
            }
        }
        [D2OIgnore]
        public List<List<int>> ResourcesBySubarea
        {
            get
            {
                return resourcesBySubarea;
            }
            set
            {
                resourcesBySubarea = value;
            }
        }
        [D2OIgnore]
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
            }
        }
        [D2OIgnore]
        public uint ImportantNoticeId
        {
            get
            {
                return importantNoticeId;
            }
            set
            {
                importantNoticeId = value;
            }
        }
        [D2OIgnore]
        public string ChangeVersion
        {
            get
            {
                return changeVersion;
            }
            set
            {
                changeVersion = value;
            }
        }
        [D2OIgnore]
        public double TooltipExpirationDate
        {
            get
            {
                return tooltipExpirationDate;
            }
            set
            {
                tooltipExpirationDate = value;
            }
        }

    }}
