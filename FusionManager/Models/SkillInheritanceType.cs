using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public enum SkillInheritanceType
    {
        [Description("Ph")]
        Physical,
        [Description("F")]
        Fire,
        [Description("I")]
        Ice,
        [Description("E")]
        Electricity,
        [Description("W")]
        Wind,
        [Description("L")]
        Light,
        [Description("D")]
        Dark,
        [Description("A")]
        Almighty,
        [Description("St")]
        Status,
        [Description("R")]
        Recovery,
        [Description("Su")]
        Support,
        [Description("P")]
        Passive,
        [Description("N")]
        Navigator
    }  
}
