using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IAbility
{
    string DisplayName {get;}
    void Do(CombatEntity actor, CombatEntity target);
}
