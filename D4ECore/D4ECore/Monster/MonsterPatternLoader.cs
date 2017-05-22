using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;
using System.Data.SqlClient;
using System.Data;
using D4ECore.Monster.Types;

namespace D4ECore.Monster
{
    public class MonsterPatternLoader
    {
        private void LoadAbilityScore(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT MAS.ABILITY_SCORE_ID, MAS.VALUE, MASM.VALUE FROM CORE.MONSTER_ABILITY_SCORE MAS 
INNER JOIN CORE.MONSTER_ABILITY_SCORE_MODIFIER MASM 
ON MAS.MONSTER_ID = MASM.MONSTER_ID AND MAS.ABILITY_SCORE_ID = MASM.ABILITY_SCORE_ID
WHERE MAS.MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.AbilityScores.Values.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        monster.AbilityScores.Values.Add((AbilityScore)reader.GetInt32(0), 
                            new MonsterAbilityScoreProperty()
                            {
                                Score = reader.GetInt32(1),
                                Modifier = reader.GetInt32(2)
                            });
                    }
                }
            }
        }

        private void LoadSkills(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT SKILL_ID, VALUE, SPECIAL FROM CORE.MONSTER_SKILL WHERE MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.Skills.Values.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        monster.Skills.Values.Add((Skill)reader.GetInt32(0), new MonsterSkillProperty()
                        {
                            Value = reader.GetInt32(1),
                            IsSpecial = reader.GetBoolean(2)
                        });
                    }
                }
            }
        }

        private void LoadLanguages(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT LANGUAGE_ID FROM CORE.MONSTER_LANGUAGE_RELATION WHERE MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.Languages.Values.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        monster.Languages.Values.Add((Language)reader.GetInt32(0));
                    }
                }
            }
        }

        private void LoadAlignment(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT ALIGNMENT_ID FROM CORE.MONSTER_ALIGNMENT WHERE MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.Alignment.Values.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        monster.Alignment.Values.Add((Alignment)reader.GetInt32(0));
                    }
                }
            }
        }

        private void LoadMovement(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT MONSTER_MOVEMENT_TYPE_ID, VALUE, ISNULL(REMARK, '') FROM CORE.MONSTER_MOVEMENT WHERE MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.Movement.Values.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        monster.Movement.Values.Add((MonsterMovementType)reader.GetInt32(0),
                            new MonsterMovementProperty()
                            {
                                Rate = reader.GetInt32(1),
                                Remark = (string.IsNullOrEmpty(reader.GetString(2)) ? null : reader.GetString(2))
                            });
                    }
                }
            }
        }

        private void LoadSavingThrowsAgainstEffects(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT EFFECT_TYPE_ID, VALUE FROM CORE.MONSTER_EFFECT_SAVING_THROW WHERE MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.SavingThrows.AgainstEffects.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        monster.SavingThrows.AgainstEffects.Add((EffectType)reader.GetInt32(0), reader.GetInt32(1));
                    }
                }
            }
        }

        private void LoadPowers(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT MP.*, MPA.* FROM CORE.MONSTER_POWER MP LEFT JOIN CORE.MONSTER_POWER_ATTACK MPA ON MP.ID = MPA.MONSTER_POWER_ID WHERE MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.Powers.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        PowerCategory category = (PowerCategory)reader.GetInt32(6);
                        MonsterPower power;
                        switch (category)
                        {
                            case PowerCategory.Attack:
                                if (reader.IsDBNull(9))
                                    throw new Exception($"Incomplete database entries for attack power with id {reader.GetInt32(0)}.");
                                MonsterAttackPower attackPower = new MonsterAttackPower();
                                attackPower.AttackType = (PowerAttackType)reader.GetInt32(9);
                                if (!reader.IsDBNull(10))
                                    attackPower.AttackBonus = reader.GetInt32(10);
                                else
                                    attackPower.AttackBonus = null;
                                if (!reader.IsDBNull(11))
                                    attackPower.TargetDefense = (Defense)reader.GetInt32(11);
                                else
                                    attackPower.TargetDefense = null;
                                attackPower.Basic = reader.GetBoolean(12);
                                power = attackPower;
                                break;
                            case PowerCategory.Utility:
                                power = new MonsterUtilityPower();
                                break;
                            default:
                                throw new Exception($"Power category {category.ToString()} is not supported.");
                        }
                        power.Name = reader.GetString(2);
                        power.Detail = reader.GetString(3);
                        power.Action = (PowerAction)reader.GetInt32(4);
                        power.Recharge = (PowerRecharge)reader.GetInt32(5);
                        power.Requirement = (!reader.IsDBNull(7) ? reader.GetString(7) : null);
                        monster.Powers.Add(power);
                    }
                }
            }
        }

        private void LoadFeats(SqlConnection con, MonsterPattern monster)
        {
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT * FROM CORE.MONSTER_FEAT WHERE MONSTER_ID = @id", con))
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = monster.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    monster.Feats.Clear();
                    while (reader.HasRows && reader.Read())
                    {
                        MonsterFeat feat = new MonsterFeat();
                        feat.Name = reader.GetString(2);
                        feat.Detail = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            feat.Requirement = reader.GetString(4);
                        else
                            feat.Requirement = null;
                        monster.Feats.Add(feat);
                    }
                }
            }
        }

        public MonsterPattern Load(SqlConnection con, int id)
        {
            var monster = new MonsterPattern();
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT * FROM CORE.MONSTER WHERE ID = @id", con))                       
            {
                cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        monster.Id = reader.GetInt32(0);
                        monster.Name = reader.GetString(2);
                        monster.Size = (Size)reader.GetInt32(3);
                        monster.Origin = (MonsterOrigin)reader.GetInt32(4);
                        monster.Type = (MonsterType)reader.GetInt32(5);
                        monster.Role = (MonsterRole)reader.GetInt32(6);
                        monster.Level = reader.GetInt32(7);
                        monster.XP = reader.GetInt32(8);
                        monster.Initiative = reader.GetInt32(9);
                        monster.HealthPoints = reader.GetInt32(10);
                        monster.Defenses.ArmorClass = reader.GetInt32(11);
                        monster.Defenses.Fortitude = reader.GetInt32(12);
                        monster.Defenses.Reflexes = reader.GetInt32(13);
                        monster.Defenses.Willpower = reader.GetInt32(14);
                        // AbilityScore / + Modifier
                        monster.Senses.Perception = reader.GetInt32(27);
                        monster.ActionPoints = (!reader.IsDBNull(28) ? reader.GetInt32(28) : 0);
                        monster.SavingThrows.General = (!reader.IsDBNull(29) ? reader.GetInt32(29) : 0);
                        if (!reader.IsDBNull(30) && (reader.GetInt32(30) > 0))
                            monster.Senses.Telepathy = new MonsterSenseTelepathy() { Range = reader.GetInt32(30) };
                    }
                    else
                    {
                        throw new DataException($"Unable to load monster pattern with id {id}.");
                    }
                }
            }
            LoadAbilityScore(con, monster);
            LoadSkills(con, monster);
            LoadLanguages(con, monster);
            LoadAlignment(con, monster);
            LoadMovement(con, monster);
            LoadSavingThrowsAgainstEffects(con, monster);
            LoadPowers(con, monster);
            LoadFeats(con, monster);
            return monster;
        }

        public MonsterPattern Load(SqlConnection con, string keyword)
        {
            int id = 0;
            using (SqlCommand cmd = new SqlCommand($@"
                SELECT ID FROM CORE.MONSTER WHERE NAME LIKE @keyword", con))
            {
                cmd.Parameters.Add("keyword", SqlDbType.VarChar).Value = keyword;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }
            if (id > 0)
                return Load(con, id);
            else
                return null;
        }
    }
}
