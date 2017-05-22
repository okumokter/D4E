using System;
using System.Data.SqlClient;
using D4ECore.Common.Types;
using D4ECore.Monster;
using D4ECore.Monster.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace D4ECoreTest.Monster
{
    [TestClass]
    public class MonsterPatternLoaderTest
    {
        [TestMethod]
        public void LoadTest()
        {
            using (SqlConnection con = new SqlConnection())
            {
                var sb = new SqlConnectionStringBuilder()
                {
                    DataSource = "C64",
                    InitialCatalog = "D4E",
                    IntegratedSecurity = true
                };
                con.ConnectionString = sb.ConnectionString;
                con.Open();

                var obj = new MonsterPatternLoader();
                var monster = obj.Load(con, 1);
                Assert.AreNotEqual(monster, null);
                Assert.AreNotEqual((int)monster.Size, 0, $"{nameof(monster.Size)} must not be zero.");
                Assert.AreEqual(monster.Size, Size.Large, $"{nameof(monster.Size)} does not match.");
                Assert.AreNotEqual((int)monster.Origin, 0, $"{nameof(monster.Origin)} must not be zero.");
                Assert.AreEqual(monster.Origin, MonsterOrigin.Aberrant, $"{nameof(monster.Origin)} does not match.");
                Assert.AreNotEqual((int)monster.Type, 0, $"{nameof(monster.Type)} must not be zero.");
                Assert.AreEqual(monster.Type, MonsterType.MagicalBeast, $"{nameof(monster.Type)} does not match.");
                Assert.AreEqual(monster.Role, MonsterRole.Brute, $"{nameof(monster.Role)} does not match.");
                Assert.AreEqual(monster.Level, 17, $"{nameof(monster.Level)} does not match.");
                Assert.AreEqual(monster.XP, 1600, $"{nameof(monster.XP)} does not match.");

                Assert.AreEqual(monster.Defenses.ArmorClass, 29, $"{nameof(monster.Defenses.ArmorClass)} value does not match ({monster.Defenses.ArmorClass} should be 29).");
                Assert.AreEqual(monster.Defenses.Reflexes, 25, $"{nameof(monster.Defenses.Reflexes)} value does not match ({monster.Defenses.Reflexes} should be 25).");

                Assert.IsTrue((monster.Alignment.Values.Count == 1) &&
                              (monster.Alignment.Values.Contains(Alignment.Evil)), $"{nameof(monster.Alignment)} does not match.");
                Assert.IsTrue((monster.Languages.Values.Count == 1) &&
                              (monster.Languages.Values.Contains(Language.DeepSpeech)), $"{nameof(monster.Languages)} does not match.");
                Assert.AreEqual(monster.AbilityScores.Values[AbilityScore.Strength].Score, 26, $"{nameof(monster.AbilityScores)}({AbilityScore.Strength.ToString()}) value does not match ({monster.AbilityScores.Values[AbilityScore.Strength].Score} should be 26).");
                Assert.AreEqual(monster.AbilityScores.Values[AbilityScore.Wisdom].Score, 22, $"{nameof(monster.AbilityScores)}({AbilityScore.Strength.ToString()}) value does not match ({monster.AbilityScores.Values[AbilityScore.Wisdom].Score} should be 22).");
                Assert.AreEqual(monster.Skills.Values[Skill.Arcana].Value, 19, $"{nameof(monster.Skills)}({Skill.Arcana.ToString()}) value does not match ({monster.Skills.Values[Skill.Arcana].Value} should be 19).");
                Assert.IsTrue((monster.Skills.Values[Skill.Arcana].IsSpecial), $"{nameof(monster.Skills)}({Skill.Arcana.ToString()}) special property does not match.");
                Assert.AreEqual(monster.Skills.Values[Skill.Thievery].Value, 11, $"{nameof(monster.Skills)}({Skill.Thievery.ToString()}) value does not match ({monster.Skills.Values[Skill.Thievery].Value} should be 11).");
                Assert.IsFalse((monster.Skills.Values[Skill.Thievery].IsSpecial), $"{nameof(monster.Skills)}({Skill.Thievery.ToString()}) special property does not match.");
                Assert.IsTrue(monster.Movement.Values.ContainsKey(MonsterMovementType.Normal), $"{nameof(monster.Movement)}({MonsterMovementType.Normal.ToString()}) property is missing.");
                Assert.AreEqual(monster.Movement.Values[MonsterMovementType.Normal].Rate, 5, $"{nameof(monster.Movement)}({MonsterMovementType.Normal.ToString()}) property must be 5.");
                Assert.IsTrue(monster.Movement.Values.ContainsKey(MonsterMovementType.Swim), $"{nameof(monster.Movement)}({MonsterMovementType.Swim.ToString()}) property is missing.");
                Assert.AreEqual(monster.Movement.Values[MonsterMovementType.Swim].Rate, 10, $"{nameof(monster.Movement)}({MonsterMovementType.Swim.ToString()}) property must be 10.");
            }
        }

        [TestMethod]
        public void PingTest30persec()
        {
            using (SqlConnection con = new SqlConnection())
            {
                var sb = new SqlConnectionStringBuilder()
                {
                    DataSource = "mordheim-db.database.windows.net",
                    InitialCatalog = "MORDHEIM",
                    UserID = "sschlewing",
                    Password = "Bumba4@Kopp"
                };
                con.ConnectionString = sb.ConnectionString;
                con.Open();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                int counter = 0;
                do
                {
                    ++counter;
                    using (SqlCommand cmd = new SqlCommand($"SELECT * FROM DATA.V_WARBAND_MEMBERS WHERE ID = {new Random().Next(50)}", con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {

                            }
                        }
                    }
                } while (sw.ElapsedMilliseconds < 1000);
                Assert.IsTrue(counter > 30, $"Only {counter} select operations performed to the database within one second. Minimum is 30.");
            }
        }
    }
}
