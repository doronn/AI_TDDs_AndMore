using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.ElementSettings
{
    public class AttackStats
    {
        public int DefenseResponseRange { get; set; }
        public List<DamageWeight> DamageWeights { get; set; }
        public int? Damage { get; set; }
        public int? AttackRange { get; set; }
        public double? AttackRate { get; set; }
        public int? AttackResponseRange { get; set; }
        public double? MoveRate { get; set; }
        public bool? ShowBullsEye { get; set; }
        public double? Dodge { get; set; }
        public bool? IgnoreArmor { get; set; }
        public int? Armor { get; set; }
        public double? ExtraBuildingDamage { get; set; }
    }
}