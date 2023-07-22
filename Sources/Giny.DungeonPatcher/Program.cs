


using Giny.Core.Misc;
using Giny.ORM;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using System.Diagnostics;
using System.Reflection;
using System.Text;

DatabaseManager.Instance.Initialize(Assembly.GetAssembly(typeof(DungeonRecord)), "127.0.0.1",
  "giny_world", "root", "");


DatabaseManager.Instance.LoadTable<DungeonRecord>();
DatabaseManager.Instance.LoadTable<MonsterRecord>();


var dungeons = DungeonRecord.GetDungeonRecords();


StringBuilder sb = new StringBuilder();


sb.AppendLine("foreach (var value in DungeonRecord.GetDungeonRecords())");
sb.AppendLine("{");
sb.AppendLine("\t value.Rooms.Clear();");
sb.AppendLine("}");

sb.AppendLine();

sb.AppendLine("DungeonRecord dungeon = null;");

foreach (var dungeon in dungeons)
{
    if (dungeon.Rooms.Count == 0 || dungeon.Rooms.All(x => x.Value.MonsterIds.Count == 0))
    {
        continue;
    }

    sb.AppendLine();
    sb.AppendLine("/* " + dungeon.Name + " */");
    sb.AppendLine();

    sb.AppendLine($"dungeon = DungeonRecord.GetDungeon({dungeon.Id});");

    foreach (var room in dungeon.Rooms)
    {
        var monsters = "";

        if (room.Value.MonsterIds.Count > 0)
        {

            monsters = "," + string.Join(',', room.Value.MonsterIds);

            var monsterNames = string.Join(',', room.Value.MonsterIds.Select(x => MonsterRecord.GetMonsterRecord(x).Name));

            var comment = "/* " + monsterNames + "*/";

            sb.AppendLine(comment);
        }




        sb.AppendLine($"dungeon.Rooms.Add({room.Key}, new MonsterRoom({room.Value.RespawnDelay}{monsters}));");
    }
}


sb.AppendLine();

sb.AppendLine("foreach (var value in DungeonRecord.GetDungeonRecords())");
sb.AppendLine("{");
sb.AppendLine("\t value.UpdateInstantElement();");
sb.AppendLine("}");

File.WriteAllText("Dungeon.txt", sb.ToString());



