using Verse;

namespace EthanV.AutoRebuildFence
{
    public static class AutoRebuildSettings
    {
        // ログ出力切替フラグ
        public static bool DebugMode = false;

        // 対象判定のキーワード（defName の部分一致）
        public static readonly string[] TargetDefKeywords = new[]
        {
            "fence",
            "gate",
            "animalflap"
        };

        public static bool IsTargetDef(ThingDef def)
        {
            if (def == null) return false;

            string defName = def.defName.ToLowerInvariant();

            // 建設途中・設計段階のものは除外
            if (defName.StartsWith("frame_") || defName.StartsWith("blueprint_"))
                return false;

            // 対象判定（部分一致）
            foreach (string keyword in TargetDefKeywords)
            {
                if (defName.Contains(keyword))
                    return true;
            }

            return false;
        }
    }
}
