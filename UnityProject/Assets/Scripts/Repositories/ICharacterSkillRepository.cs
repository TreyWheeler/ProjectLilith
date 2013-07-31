using System;
using System.Collections.Generic;

public interface ICharacterSkillRepository : IRepository<CharacterSkill>
{
    List<CharacterSkill> GetAllFor(int characterID);
}

